/*
 * ************************************************************************
 *                            SDK
 *                      ����SDK(C#��)
 * 
 * (c) Copyright 2012
 *                      All Rights Reserved
 * �� �� ��:1.0.0.0
 * �ļ�����:NetSDK.cs
 * ����˵��:�����е�SDK(C++��)����һ�η�װ�����C#Ӧ�ÿ���
 * ��    ��:
 * ��������:2012/02/08
 * �޸���־:    ����        �汾��      ����        �������
 *              2012/02/08  1.0                   �½�����
 *              2015/04/16  1.0.001    Alfan      ��Ӧ���������޸�
 * 
 * ************************************************************************
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Net;

namespace CameraNet
{
    public class StructBit
    {
        byte[] _pBytes;
        public StructBit(byte[] pBytes)
        {
            _pBytes = pBytes;
        }

        int GetAt(int nIndex)
        {
            int nByte = nIndex / 8;
            int nBit = nIndex % 8;
            return (_pBytes[nByte] & (0x1 << nBit)) != 0 ? 1 : 0;
        }

        public int GetInt(int nIndex, int nLength)
        {
            int nRet = 0;
            if (_pBytes == null || nIndex < 0 || nIndex + nLength > _pBytes.Length * 8)
            {
                return 0;
            }

            int nBitMove = nLength;
            for (int i = nIndex + nLength - 1; i >= nLength; --i)
            {
                nRet |= (GetAt(i) << --nBitMove);
            }
            return nRet;
        }
    }
    /// <summary>
    /// ������������SDK(C++��)�Ļ��������C#Ӧ�ó��򿪷��Ŀ�������ʹ�ô˿�
    /// ����ʱ��Ҫ��ԭ�е�SDK�Ķ�̬���ӿ��ļ��ͱ�SDK�ļ�������ͬһ��Ŀ¼��
    /// </summary>
    public class CameraClient
    {
        private RVNetDeviceInfo m_lpDeviceInfo = new RVNetDeviceInfo();
        private int m_lLogin = 0;
        private int[] m_lPlayHandel = new int[32];
        private static int userNum = 0;
        private static bool s_initialized = false;

        /// <summary>
        /// ��¼ID
        /// </summary>
        public int lLogin { get { return m_lLogin; } }
        public CameraClient()
        {
            for (int i = 0; i < 32; i++)
            {
                m_lPlayHandel[i] = 0;
            }
        }

        /// <summary>
        /// ��ʼ������¼���
        /// </summary>
        public static void Init()
        {
            if (s_initialized == false&& userNum<=0)
            {
                SDK_Init(null, IntPtr.Zero);
                s_initialized = true;
            }
            userNum++;
        }

        public static void Cleanup()
        {
            if (s_initialized&& userNum<=0)
            {
                SDK_Cleanup();
                s_initialized = false;
            }
            userNum--;
        }

        public int GetLastError()
        {
            int resulte = -1;
            if (s_initialized)
            {
                resulte=SDK_GetLastError();                
            }
            return resulte;
        }


        /// <summary>
        /// ע���û����豸����չ�ӿڣ�֧��һ���û�ָ���豸֧�ֵ�����
        /// </summary>
        /// <param name="pchDVRIP">�豸IP</param>
        /// <param name="wDVRPort">�豸�˿�</param>
        /// <param name="pchUserName">�û���</param>
        /// <param name="pchPassword">�û�����</param>
        /// <param name="nSpecCap">�豸֧�ֵ�������ֵΪ2��ʾ��������ģʽ�µ��û���¼��[����dvr��¼]</param>
        /// <param name="pCapParam">��nSpecCap �Ĳ������, nSpecCap = 2ʱ��pCapParam����豸���к��ִ���[����dvr��¼]</param>
        /// <param name="lpDeviceInfo">�豸��Ϣ,�����������</param>
        /// <param name="error">���ص�¼������</param>
        /// <returns>����0���ɹ�����,</returns>
        public int Login(string pchDVRIP, ushort wDVRPort, string pchUserName, string pchPassword, int nSpecCap, string pCapParam)
        {
            int error;
            m_lLogin = SDK_Login(pchDVRIP, wDVRPort, pchUserName, pchPassword, out m_lpDeviceInfo, out error, nSpecCap, pCapParam);
            return m_lLogin != 0 ? 0 : error;
        }

        /// <summary>
        /// ע���û�
        /// </summary>        
        public void Logout()
        {
            if (m_lLogin != 0)//������¼�����豸�û�
            {
                SDK_Logout(m_lLogin);
                m_lLogin = 0;
            }
        }

        /// <summary>
        /// ����ʵʱ���ӻ�໭��Ԥ��
        /// </summary>
        /// <param name="nChannelID"></param>
        /// <param name="hWnd"></param>
        /// <param name="rType"></param>
        /// <returns></returns>
        public int RealPlay(int nChannelID, IntPtr hWnd, int nStreamType, int nNetType)
        {
            if (nChannelID < 0 || nChannelID > 31 || 0 == m_lLogin)
            {
                return 0;
            }
            m_lPlayHandel[nChannelID] = SDK_RealPlay(m_lLogin, nChannelID, hWnd, nStreamType, nNetType);
            return m_lPlayHandel[nChannelID] ;//!= 0
        }

        public bool StopRealPlay(int nChannelID)
        {
            if (nChannelID < 0 || nChannelID > 31 || 0 == m_lLogin)
            {
                return false;
            }
            if (0 != m_lPlayHandel[nChannelID])
            {
                SDK_StopRealPlay(m_lPlayHandel[nChannelID]);
                m_lPlayHandel[nChannelID] = 0;
            }
            return true;
        }

        /// <summary>
        /// ����ͼƬ������ʾͼ�����˲��ץͼ��ֻ�д�ͼ��ĺ�������hWnd��Чʱ�ú�����ȡ�Ĳ�������Ч������������
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="pchPicFileName"></param>
        /// <returns></returns>
        public  bool CapturePicture(int lRealHandle, string pchPicFileName)
        {
            return SDK_CapturePicture(lRealHandle, pchPicFileName);
        }

        public bool StartRecord(int lRealHandle, string pchFileName)
        {
           return SDK_SaveRealData(lRealHandle, pchFileName);
        }

        public bool StopRecord(int lRealHandle)
        {
            return SDK_StopSaveRealData(lRealHandle);
        }

        /// <summary>
        /// ʵʱ���ݻص�
        /// </summary>
        /// <param name="lRealHandle">SDK_RealPlay �ķ���ֵ</param>
        /// <param name="cbRealData">�ص�����</param>
        /// <param name="dwUser">�û�����</param>
        public void RealDataCallBack(int lRealHandle, FRealDataCallBack cbRealData)
        {
            SDK_SetRealDataCallBack(lRealHandle, cbRealData, 0);
        }

        public int QueryLog(RV_LOG_QUERY_TYPE logType, SLogItem[] logItems, IntPtr reserved, int waittime)
        {
            int nStructSize = Marshal.SizeOf(typeof(RV_LOG_ITEM));
            int maxlen = nStructSize * logItems.Length;
            if (maxlen < 0)
            {
                return 0;
            }
            int nLogBufferlen = 0;
            IntPtr pLogBuffer = Marshal.AllocHGlobal(maxlen);
            SDK_QueryLog(m_lLogin, logType, pLogBuffer, maxlen, ref nLogBufferlen, reserved, waittime);
            int nCount = nLogBufferlen / Marshal.SizeOf(typeof(RV_LOG_ITEM));
            RV_LOG_ITEM rvItem;
            for (int i = 0; i < logItems.Length; ++i)
            {
                rvItem = (RV_LOG_ITEM)Marshal.PtrToStructure((IntPtr)((UInt32)pLogBuffer + i * nStructSize), typeof(RV_LOG_ITEM));
                IntToTime(rvItem, logItems[i]);
            }
            Marshal.FreeHGlobal(pLogBuffer);
            return nCount;
        }

        public bool IntToTime(RV_LOG_ITEM rvLogItem, SLogItem logItem)
        {
            int nSeconde = rvLogItem.time & 0x3f;
            int nMinite = (rvLogItem.time >> 6) & 0x3f;
            int nHour = (rvLogItem.time >> 12) & 0x1f;
            int nDay = (rvLogItem.time >> 17) & 0x1f;
            int nMoth = (rvLogItem.time >> 22) & 0xf;
            int nYear = (rvLogItem.time >> 26) + 2000;
            return true;
        }

        #region << ԭSDK���� >>

        /// <summary>
        /// ���غ���ִ��ʧ�ܴ���
        /// </summary>
        /// <returns>ִ��ʧ�ܴ���</returns>
        [DllImport("libmodel.dll")]
        private static extern Int32 SDK_GetLastError();

        /// <summary>
        /// ��ʼ��SDK
        /// </summary>
        /// <param name="cbDisConnect">
        /// ���߻ص�����,�μ�ί��<seealso cref="fDisConnect"/>
        /// </param>
        /// <param name="dwUser">�û���Ϣ</param>
        /// <returns>true:�ɹ�;false:ʧ��</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_Init(PFDisConnect cbDisConnect, IntPtr dwUser);

        /// <summary>
        /// ���SDK, �ͷ�ռ�õ���Դ�������е�SDK����֮�����
        /// </summary>
        [DllImport("libmodel.dll")]
        private static extern void SDK_Cleanup();

        /// <summary>
        /// ���ö��������Ļص�������������sdk�ڲ��Ͳ����ж�������
        /// <param name="cbAutoConnect">���������ɹ��Ļص�����</param>
        /// <param name="dwUser">�û���Ϣ</param>
        /// </summary>
        [DllImport("libmodel.dll")]
        private static extern void SDK_SetAutoReconnect(PFHaveReConnect cbAutoConnect, IntPtr dwUser);

        /// <summary>
        /// �ͻ������豸�����ӵȴ�ʱ��
        /// <param name="nWaitTime ">�ͻ������豸�����ӵȴ�ʱ�䣬���뼶</param>
        /// <param name="nTryTimes ">���Ӵ�������ʱΪ��Чֵ����NULL</param>
        /// </summary>
        [DllImport("libmodel.dll")]
        private static extern void SDK_SetConnectTime(int nWaitTime, int nTryTimes);


        /// <summary>
        /// ע���û����豸����չ�ӿڣ�֧��һ���û�ָ���豸֧�ֵ�����
        /// </summary>
        /// <param name="pchDVRIP">�豸IP</param>
        /// <param name="wDVRPort">�豸�˿�</param>
        /// <param name="pchUserName">�û���</param>
        /// <param name="pchPassword">�û�����</param>
        /// <param name="nSpecCap">�豸֧�ֵ�������ֵΪ2��ʾ��������ģʽ�µ��û���¼��[����dvr��¼]</param>
        /// <param name="pCapParam">��nSpecCap �Ĳ������, nSpecCap = 2ʱ��pCapParam����豸���к��ִ���[����dvr��¼]</param>
        /// <param name="lpDeviceInfo">�豸��Ϣ,�����������</param>
        /// <param name="error">���ص�¼������</param>
        /// <returns>ʧ�ܷ���0���ɹ������豸ID</returns>
        [DllImport("libmodel.dll")]
        private static extern int SDK_Login(string pchDVRIP, ushort wDVRPort, string pchUserName, string pchPassword, out RVNetDeviceInfo lpDeviceInfo, out int error, int nSpecCap, string pCapParam);

        /// <summary>
        /// ע���豸�û�
        /// </summary>
        /// <param name="lLoginID">�豸�û���¼ID:<seealso cref="SDK_Login"/>�ķ���ֵ</param>
        /// <returns>true:�ɹ�;false:ʧ��</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_Logout(int lLoginID);

        /// <summary>
        /// ����ʵʱ����
        /// </summary>
        /// <param name="lLoginID">�豸�û���¼ID:<seealso cref="SDK_Login"/>�ķ���ֵ</param>
        /// <param name="nChannelID">ͨ��ID</param>
        /// <param name="hWnd">��ʾ�������ھ��</param>
        /// <returns>ʧ�ܷ���0���ɹ�����ʵʱ����ID(ʵʱ���Ӿ��)</returns>
        [DllImport("libmodel.dll")]
        private static extern int SDK_RealPlay(int lLoginID, int nChannelID, IntPtr hWnd, int nStreamType, int nNetType);

        /// <summary>
        /// ֹͣʵʱ����
        /// </summary>
        /// <param name="lRealHandle">ʵʱ���Ӿ��:<seealso cref="SDK_RealPlay"/>�ķ���ֵ</param>
        /// <returns>true:�ɹ�;false:ʧ��</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_StopRealPlay(int lRealHandle);

        /// <summary>
        /// ����ͼƬ������ʾͼ�����˲��ץͼ��ֻ�д�ͼ��ĺ�������hWnd��Чʱ�ú�����ȡ�Ĳ�������Ч������������
        /// </summary>
        /// <param name="lRealHandle">ʵʱ���Ӿ��:<seealso cref="SDK_RealPlay"/>�ķ���ֵ</param>
        /// <param name="pchPicFileName">λͼ�ļ�������ǰֻ֧��BMPλͼ</param>
        /// <returns>true:�ɹ�;false:ʧ��</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_CapturePicture(int lRealHandle, string pchPicFileName);

        /// <summary>
        /// ��ʼ����ʵʱ��������,��ǰ���豸���ӵ�ͼ��������ݱ���,�γ�¼���ļ�,���������豸�˴��͹�����ԭʼ��Ƶ����
        /// </summary>
        /// <param name="lRealHandle">ʵʱ���Ӿ��:<seealso cref="SDK_RealPlay"/>�ķ���ֵ</param>
        /// <param name="pchFileName ">ʵʱ���ӱ����ļ���</param>
        /// <returns>true:�ɹ�;false:ʧ��</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_SaveRealData(int lRealHandle, string pchFileName);

        /// <summary>
        /// ֹͣ����ʵʱ�������ݣ��رձ�����ļ�
        /// </summary>
        /// <param name="lRealHandle">ʵʱ���Ӿ��:<seealso cref="SDK_RealPlay"/>�ķ���ֵ</param>
        /// <returns>true:�ɹ�;false:ʧ��</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_StopSaveRealData(int lRealHandle);


        [DllImport("libmodel.dll")]
        private static extern bool SDK_QueryLog(int lLoginID, RV_LOG_QUERY_TYPE logType, IntPtr pLogBuffer, int maxlen, ref int nLogBufferlen, IntPtr reserved, int waittime);

        /// <summary>
        /// ����ʵʱ�������ݻص������û��ṩ�豸���������ݣ���cbRealDataΪNULLʱ�����ص�����
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="cbRealData"></param>
        /// <param name="dwUser"></param>
        /// <returns></returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_SetRealDataCallBack(int lRealHandle,FRealDataCallBack cbRealData, int dwUser);


        #endregion

        #region << �ṹ���� >>

        public delegate void PFDisConnect(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser);

        public delegate void PFHaveReConnect(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser);

        public delegate void FRealDataCallBack(int lRealHandle,int dwDataType,byte[] pBuffer,int dwBufsize,int dwUser);

        /// <summary>
        /// �����豸��Ϣ
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RVNetDeviceInfo
        {
            /// <summary>
            /// ���к�[����48]
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
            public byte[] sSerialNumber;

            /// <summary>
            /// DVR�����������
            /// </summary>
            public byte byAlarmInPortNum;

            /// <summary>
            /// DVR�����������
            /// </summary>
            public byte byAlarmOutPortNum;

            /// <summary>
            /// DVRӲ�̸���
            /// </summary>
            public byte byDiskNum;

            /// <summary>
            /// DVR����
            /// </summary>
            public byte byDVRType;

            /// <summary>
            /// DVRͨ������
            /// </summary>
            public byte byChanNum;

        }

        public struct RVLoginInfo
        {
            public string pDVRCTlane;
            public string pchDVRIP;
            public ushort wDVRPort;
            public string pchUserName;
            public string pchPassword;
            public int nSpecCap;
            public string pCapParam;
            //public Panel pDVpanel;
            public int pDVPlayHandel;
            public int pDVRealHandle;
           // public Client pDVClient;
            public bool Enable;
        }


        public enum RV_LOG_QUERY_TYPE
        {
            RVLOG_ALL = 0,		//all log [������־]
            RVLOG_SYSTEM,		//system log [ϵͳ��־]
            RVLOG_CONFIG,		//configure log [������־]
            RVLOG_STORAGE,		//store log [�洢���]
            RVLOG_ALARM,		//alarm log [������־]
            RVLOG_RECORD,		//record log [¼�����]
            RVLOG_ACCOUNT,		//account log [�ʺ����]
            RVLOG_CLEAR,		//clear log [�����־]
            RVLOG_PLAYBACK		//replay log [�ط����]
        }
        /// <summary>
        /// ��������
        /// </summary>
        public enum RV_RealPlayType
        {
            /// <summary>
            /// ʵʱԤ��
            /// </summary>
            RV_RType_RealPlay,
            /// <summary>
            /// �໭��Ԥ��
            /// </summary>
            RV_RType_Multiplay,
            /// <summary>
            /// ʵʱ����-����������ͬ��RV_RType_Realplay
            /// </summary>
            RV_RType_RealPlay_0,
            /// <summary>
            /// ʵʱ����-������1
            /// </summary>
            RV_RType_RealPlay_1,
            /// <summary>
            /// ʵʱ����-������2
            /// </summary>
            RV_RType_RealPlay_2,
            /// <summary>
            /// ʵʱ����-������3
            /// </summary>
            RV_RType_RealPlay_3,
            /// <summary>
            /// �໭��Ԥ����1����
            /// </summary>
            RV_RType_Multiplay_1,
            /// <summary>
            /// �໭��Ԥ����4����
            /// </summary>
            RV_RType_Multiplay_4,
            /// <summary>
            /// �໭��Ԥ����8����
            /// </summary>
            RV_RType_Multiplay_8,
            /// <summary>
            /// �໭��Ԥ����9����
            /// </summary>
            RV_RType_Multiplay_9,
            /// <summary>
            /// �໭��Ԥ����16���� 
            /// </summary>
            RV_RType_Multiplay_16,
            /// <summary>
            ///  �໭��Ԥ����6����
            /// </summary>
            RV_RType_Multiplay_6,
            /// <summary>
            /// �໭��Ԥ����12����
            /// </summary>
            RV_RType_Multiplay_12
        }

        /// <summary>
        /// ��־�ṹ
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RV_LOG_ITEM
        {
            /// <summary>
            /// ����
            /// </summary>
            public int time;
            /// <summary>
            /// ����
            /// </summary>
            public UInt16 type;
            /// <summary>
            /// ����
            /// </summary>
            public byte reserved;
            /// <summary>
            /// ����
            /// </summary>
            public byte data;
            /// <summary>
            /// ����
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] context;
        }

        public struct SLogItem
        {
            public UInt16 type;
            public DateTime time;
            public String strText;
        }

        #endregion
    }
}

