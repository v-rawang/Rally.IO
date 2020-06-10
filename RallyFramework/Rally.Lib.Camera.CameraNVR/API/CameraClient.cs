using CameraNVR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CameraNVR.CHCNetSDK;

namespace Rally.Lib.Camera.CameraNVR.API
{
    public class CameraClient
    {
        private int m_lLogin = -1;
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
                m_lPlayHandel[i] = -1;
            }
        }

        /// <summary>
        /// 初始化数字录像机
        /// </summary>
        public static void Init()
        {
            if (s_initialized == false && userNum <= 0)
            {
                try
                {
                    s_initialized = CHCNetSDK.NET_DVR_Init();
                }
                catch (Exception ex)
                {
                    s_initialized = false;
                    return;
                }
            }
            userNum++;
        }

        public static void Cleanup()
        {
            if (s_initialized && userNum <= 0)
            {
                CHCNetSDK.NET_DVR_Cleanup();
                s_initialized = false;
            }
            userNum--;
        }

        public uint GetLastError()
        {
            uint resulte = 0;
            if (s_initialized)
            {
                resulte = CHCNetSDK.NET_DVR_GetLastError();
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
            CHCNetSDK.NET_DVR_DEVICEINFO_V30 deviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();
            //NET_DVR_DEVICEINFO nET = new NET_DVR_DEVICEINFO() { };
            m_lLogin = CHCNetSDK.NET_DVR_Login_V30(pchDVRIP, wDVRPort, pchUserName, pchPassword, ref deviceInfo);
            //m_lLogin = SDK_Login(pchDVRIP, wDVRPort, pchUserName, pchPassword, out m_lpDeviceInfo, out error, nSpecCap, pCapParam);
            return m_lLogin;
        }

        /// <summary>
        /// 注销用户
        /// </summary>        
        public void Logout()
        {
            if (m_lLogin >= 0)//己经登录过的设备用户
            {
                CHCNetSDK.NET_DVR_Logout(m_lLogin);
                m_lLogin = -1;
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
            if (nChannelID < 0 || nChannelID > 31 || 0 > m_lLogin)
            {
                return 0;
            }
            NET_DVR_CLIENTINFO lpClientInfo = new NET_DVR_CLIENTINFO();
            lpClientInfo.hPlayWnd = hWnd;
            lpClientInfo.lChannel = nChannelID;
            lpClientInfo.lLinkMode = 0;
            lpClientInfo.sMultiCastIP = "";

            CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
            lpPreviewInfo.hPlayWnd = hWnd;//预览窗口
            lpPreviewInfo.lChannel = nChannelID;//预te览的设备通道
            lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
            lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
            lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
            lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
            lpPreviewInfo.byProtoType = 0;
            lpPreviewInfo.byPreviewMode = 0;


            IntPtr pUser = new IntPtr();//用户数据
            //CHCNetSDK.NET_DVR_RealPlay
            m_lPlayHandel[nChannelID] = CHCNetSDK.NET_DVR_RealPlay_V40(m_lLogin, ref lpPreviewInfo, null/*RealData*/, pUser);

            uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();

            return m_lPlayHandel[nChannelID];//!= 0
        }

        public bool StopRealPlay(int nChannelID)
        {
            if (nChannelID < 0 || nChannelID > 31 || 0 > m_lLogin)
            {
                return false;
            }
            if (0 > m_lPlayHandel[nChannelID])
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lPlayHandel[nChannelID]);
                m_lPlayHandel[nChannelID] = -1;
            }
            return true;
        }

        /// <summary>
        /// 保存图片，对显示图像进行瞬间抓图，只有打开图像的函数参数hWnd有效时该函数获取的参数才有效，否则无意义
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="pchPicFileName"></param>
        /// <returns></returns>
        public bool CapturePicture(int lRealHandle, string pchPicFileName)
        {
            return CHCNetSDK.NET_DVR_CapturePicture(lRealHandle, pchPicFileName);
        }

        public bool StartRecord(int lRealHandle, string pchFileName)
        {
            CHCNetSDK.NET_DVR_MakeKeyFrame(m_lLogin, 0);
            return CHCNetSDK.NET_DVR_SaveRealData(lRealHandle, pchFileName);
        }

        public bool StopRecord(int lRealHandle)
        {
            return CHCNetSDK.NET_DVR_StopSaveRealData(lRealHandle);
        }

        /// <summary>
        /// 实时数据回调
        /// </summary>
        /// <param name="lRealHandle">SDK_RealPlay 的返回值</param>
        /// <param name="cbRealData">回调函数</param>
        /// <param name="dwUser">用户数据</param>
        //public void RealDataCallBack(int lRealHandle, FRealDataCallBack cbRealData)
        //{
        //    SDK_SetRealDataCallBack(lRealHandle, cbRealData, 0);
        //}
    }
}
