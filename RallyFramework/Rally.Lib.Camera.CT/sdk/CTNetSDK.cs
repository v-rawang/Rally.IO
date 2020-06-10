/*
 * ************************************************************************
 *                            SDK
 *                      网络SDK(C#版)
 * 
 * (c) Copyright 2012
 *                      All Rights Reserved
 * 版 本 号:1.0.0.0
 * 文件名称:NetSDK.cs
 * 功能说明:在现有的SDK(C++版)上再一次封装，针对C#应用开发
 * 作    者:
 * 作成日期:2012/02/08
 * 修改日志:    日期        版本号      作者        变更事由
 *              2012/02/08  1.0                   新建作成
 *              2015/04/16  1.0.001    Alfan      适应内容需求修改
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
    /// 本类是在网络SDK(C++版)的基础上针对C#应用程序开发的开发包，使用此开
    /// 发包时需要将原有的SDK的动态链接库文件和本SDK文件放置在同一个目录下
    /// </summary>
    public class CameraClient
    {
        private RVNetDeviceInfo m_lpDeviceInfo = new RVNetDeviceInfo();
        private int m_lLogin = 0;
        private int[] m_lPlayHandel = new int[32];
        private static int userNum = 0;
        private static bool s_initialized = false;

        /// <summary>
        /// 登录ID
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
        /// 初始化数字录像机
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
        /// 注册用户到设备的扩展接口，支持一个用户指定设备支持的能力
        /// </summary>
        /// <param name="pchDVRIP">设备IP</param>
        /// <param name="wDVRPort">设备端口</param>
        /// <param name="pchUserName">用户名</param>
        /// <param name="pchPassword">用户密码</param>
        /// <param name="nSpecCap">设备支持的能力，值为2表示主动侦听模式下的用户登录。[车载dvr登录]</param>
        /// <param name="pCapParam">对nSpecCap 的补充参数, nSpecCap = 2时，pCapParam填充设备序列号字串。[车载dvr登录]</param>
        /// <param name="lpDeviceInfo">设备信息,属于输出参数</param>
        /// <param name="error">返回登录错误码</param>
        /// <returns>返回0，成功返回,</returns>
        public int Login(string pchDVRIP, ushort wDVRPort, string pchUserName, string pchPassword, int nSpecCap, string pCapParam)
        {
            int error;
            m_lLogin = SDK_Login(pchDVRIP, wDVRPort, pchUserName, pchPassword, out m_lpDeviceInfo, out error, nSpecCap, pCapParam);
            return m_lLogin != 0 ? 0 : error;
        }

        /// <summary>
        /// 注销用户
        /// </summary>        
        public void Logout()
        {
            if (m_lLogin != 0)//己经登录过的设备用户
            {
                SDK_Logout(m_lLogin);
                m_lLogin = 0;
            }
        }

        /// <summary>
        /// 启动实时监视或多画面预览
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
        /// 保存图片，对显示图像进行瞬间抓图，只有打开图像的函数参数hWnd有效时该函数获取的参数才有效，否则无意义
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
        /// 实时数据回调
        /// </summary>
        /// <param name="lRealHandle">SDK_RealPlay 的返回值</param>
        /// <param name="cbRealData">回调函数</param>
        /// <param name="dwUser">用户数据</param>
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

        #region << 原SDK调用 >>

        /// <summary>
        /// 返回函数执行失败代码
        /// </summary>
        /// <returns>执行失败代码</returns>
        [DllImport("libmodel.dll")]
        private static extern Int32 SDK_GetLastError();

        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <param name="cbDisConnect">
        /// 断线回调函数,参见委托<seealso cref="fDisConnect"/>
        /// </param>
        /// <param name="dwUser">用户信息</param>
        /// <returns>true:成功;false:失败</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_Init(PFDisConnect cbDisConnect, IntPtr dwUser);

        /// <summary>
        /// 清空SDK, 释放占用的资源，在所有的SDK函数之后调用
        /// </summary>
        [DllImport("libmodel.dll")]
        private static extern void SDK_Cleanup();

        /// <summary>
        /// 设置断线重连的回调函数，不调用sdk内部就不进行断线重连
        /// <param name="cbAutoConnect">断线重连成功的回调函数</param>
        /// <param name="dwUser">用户信息</param>
        /// </summary>
        [DllImport("libmodel.dll")]
        private static extern void SDK_SetAutoReconnect(PFHaveReConnect cbAutoConnect, IntPtr dwUser);

        /// <summary>
        /// 客户端与设备的连接等待时间
        /// <param name="nWaitTime ">客户端与设备的连接等待时间，毫秒级</param>
        /// <param name="nTryTimes ">连接次数，暂时为无效值，填NULL</param>
        /// </summary>
        [DllImport("libmodel.dll")]
        private static extern void SDK_SetConnectTime(int nWaitTime, int nTryTimes);


        /// <summary>
        /// 注册用户到设备的扩展接口，支持一个用户指定设备支持的能力
        /// </summary>
        /// <param name="pchDVRIP">设备IP</param>
        /// <param name="wDVRPort">设备端口</param>
        /// <param name="pchUserName">用户名</param>
        /// <param name="pchPassword">用户密码</param>
        /// <param name="nSpecCap">设备支持的能力，值为2表示主动侦听模式下的用户登录。[车载dvr登录]</param>
        /// <param name="pCapParam">对nSpecCap 的补充参数, nSpecCap = 2时，pCapParam填充设备序列号字串。[车载dvr登录]</param>
        /// <param name="lpDeviceInfo">设备信息,属于输出参数</param>
        /// <param name="error">返回登录错误码</param>
        /// <returns>失败返回0，成功返回设备ID</returns>
        [DllImport("libmodel.dll")]
        private static extern int SDK_Login(string pchDVRIP, ushort wDVRPort, string pchUserName, string pchPassword, out RVNetDeviceInfo lpDeviceInfo, out int error, int nSpecCap, string pCapParam);

        /// <summary>
        /// 注销设备用户
        /// </summary>
        /// <param name="lLoginID">设备用户登录ID:<seealso cref="SDK_Login"/>的返回值</param>
        /// <returns>true:成功;false:失败</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_Logout(int lLoginID);

        /// <summary>
        /// 启动实时监视
        /// </summary>
        /// <param name="lLoginID">设备用户登录ID:<seealso cref="SDK_Login"/>的返回值</param>
        /// <param name="nChannelID">通道ID</param>
        /// <param name="hWnd">显示容器窗口句柄</param>
        /// <returns>失败返回0，成功返回实时监视ID(实时监视句柄)</returns>
        [DllImport("libmodel.dll")]
        private static extern int SDK_RealPlay(int lLoginID, int nChannelID, IntPtr hWnd, int nStreamType, int nNetType);

        /// <summary>
        /// 停止实时监视
        /// </summary>
        /// <param name="lRealHandle">实时监视句柄:<seealso cref="SDK_RealPlay"/>的返回值</param>
        /// <returns>true:成功;false:失败</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_StopRealPlay(int lRealHandle);

        /// <summary>
        /// 保存图片，对显示图像进行瞬间抓图，只有打开图像的函数参数hWnd有效时该函数获取的参数才有效，否则无意义
        /// </summary>
        /// <param name="lRealHandle">实时监视句柄:<seealso cref="SDK_RealPlay"/>的返回值</param>
        /// <param name="pchPicFileName">位图文件名，当前只支持BMP位图</param>
        /// <returns>true:成功;false:失败</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_CapturePicture(int lRealHandle, string pchPicFileName);

        /// <summary>
        /// 开始保存实时监视数据,对前端设备监视的图像进行数据保存,形成录像文件,此数据是设备端传送过来的原始视频数据
        /// </summary>
        /// <param name="lRealHandle">实时监视句柄:<seealso cref="SDK_RealPlay"/>的返回值</param>
        /// <param name="pchFileName ">实时监视保存文件名</param>
        /// <returns>true:成功;false:失败</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_SaveRealData(int lRealHandle, string pchFileName);

        /// <summary>
        /// 停止保存实时监视数据，关闭保存的文件
        /// </summary>
        /// <param name="lRealHandle">实时监视句柄:<seealso cref="SDK_RealPlay"/>的返回值</param>
        /// <returns>true:成功;false:失败</returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_StopSaveRealData(int lRealHandle);


        [DllImport("libmodel.dll")]
        private static extern bool SDK_QueryLog(int lLoginID, RV_LOG_QUERY_TYPE logType, IntPtr pLogBuffer, int maxlen, ref int nLogBufferlen, IntPtr reserved, int waittime);

        /// <summary>
        /// 设置实时监视数据回调，给用户提供设备流出的数据，当cbRealData为NULL时结束回调数据
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="cbRealData"></param>
        /// <param name="dwUser"></param>
        /// <returns></returns>
        [DllImport("libmodel.dll")]
        private static extern bool SDK_SetRealDataCallBack(int lRealHandle,FRealDataCallBack cbRealData, int dwUser);


        #endregion

        #region << 结构定义 >>

        public delegate void PFDisConnect(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser);

        public delegate void PFHaveReConnect(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser);

        public delegate void FRealDataCallBack(int lRealHandle,int dwDataType,byte[] pBuffer,int dwBufsize,int dwUser);

        /// <summary>
        /// 网络设备信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RVNetDeviceInfo
        {
            /// <summary>
            /// 序列号[长度48]
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
            public byte[] sSerialNumber;

            /// <summary>
            /// DVR报警输入个数
            /// </summary>
            public byte byAlarmInPortNum;

            /// <summary>
            /// DVR报警输出个数
            /// </summary>
            public byte byAlarmOutPortNum;

            /// <summary>
            /// DVR硬盘个数
            /// </summary>
            public byte byDiskNum;

            /// <summary>
            /// DVR类型
            /// </summary>
            public byte byDVRType;

            /// <summary>
            /// DVR通道个数
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
            RVLOG_ALL = 0,		//all log [所有日志]
            RVLOG_SYSTEM,		//system log [系统日志]
            RVLOG_CONFIG,		//configure log [配置日志]
            RVLOG_STORAGE,		//store log [存储相关]
            RVLOG_ALARM,		//alarm log [报警日志]
            RVLOG_RECORD,		//record log [录象相关]
            RVLOG_ACCOUNT,		//account log [帐号相关]
            RVLOG_CLEAR,		//clear log [清除日志]
            RVLOG_PLAYBACK		//replay log [回放相关]
        }
        /// <summary>
        /// 流码类型
        /// </summary>
        public enum RV_RealPlayType
        {
            /// <summary>
            /// 实时预览
            /// </summary>
            RV_RType_RealPlay,
            /// <summary>
            /// 多画面预览
            /// </summary>
            RV_RType_Multiplay,
            /// <summary>
            /// 实时监视-主码流，等同于RV_RType_Realplay
            /// </summary>
            RV_RType_RealPlay_0,
            /// <summary>
            /// 实时监视-从码流1
            /// </summary>
            RV_RType_RealPlay_1,
            /// <summary>
            /// 实时监视-从码流2
            /// </summary>
            RV_RType_RealPlay_2,
            /// <summary>
            /// 实时监视-从码流3
            /// </summary>
            RV_RType_RealPlay_3,
            /// <summary>
            /// 多画面预览－1画面
            /// </summary>
            RV_RType_Multiplay_1,
            /// <summary>
            /// 多画面预览－4画面
            /// </summary>
            RV_RType_Multiplay_4,
            /// <summary>
            /// 多画面预览－8画面
            /// </summary>
            RV_RType_Multiplay_8,
            /// <summary>
            /// 多画面预览－9画面
            /// </summary>
            RV_RType_Multiplay_9,
            /// <summary>
            /// 多画面预览－16画面 
            /// </summary>
            RV_RType_Multiplay_16,
            /// <summary>
            ///  多画面预览－6画面
            /// </summary>
            RV_RType_Multiplay_6,
            /// <summary>
            /// 多画面预览－12画面
            /// </summary>
            RV_RType_Multiplay_12
        }

        /// <summary>
        /// 日志结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RV_LOG_ITEM
        {
            /// <summary>
            /// 日期
            /// </summary>
            public int time;
            /// <summary>
            /// 类型
            /// </summary>
            public UInt16 type;
            /// <summary>
            /// 保留
            /// </summary>
            public byte reserved;
            /// <summary>
            /// 数据
            /// </summary>
            public byte data;
            /// <summary>
            /// 内容
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

