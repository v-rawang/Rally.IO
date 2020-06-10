using Rally.Lib.Camera.CameraNVR.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Rally.Lib.Camera.CameraNVR
{
    /// <summary>
    /// 海康威视摄像头
    /// </summary>
    public class CameraServer
    {
        private CameraClient cameraClient;
        /// <summary>
        /// 视频开启句柄，当>=0时，说明已经开启
        /// </summary>
        private int myPlayHandle = -1;
        /// <summary>
        /// 标示是否正在实时查看，true：是
        /// false：不是
        /// </summary>
        private bool videoShow = false;
        /// <summary>
        /// 是否正在保存视频
        /// </summary>
        private bool videoRecord = false;

        /// <summary>
        /// 返回实时流数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="plateforms"></param>
        public bool SendByteRealData(CameraEquipment camera)
        {
            //CameraClient.Init();
            //CameraEquipment equipment = camera as CameraEquipment;
            /////用于摄像头回调
            //FRealDataCallBack fRealDataCallBack = new FRealDataCallBack(ReadDataCallBack);
            //if (cameraClient == null || cameraClient.lLogin < 0)
            //{
            //    //2、登录网络设备
            //    cameraClient = new CameraClient();
            //    int error = cameraClient.Login(equipment.IP, ushort.Parse(equipment.Port), equipment.UserName, equipment.PassWord, 0, "");
            //    if (error > 0)
            //    {
            //        return false;
            //    }
            //}
            //if (cameraClient.lLogin >= 0)//登录成功
            //{
            //    //3、启动实时监视或多画面预览
            //    int hPlayHandle = cameraClient.RealPlay(1, IntPtr.Zero, 0, 0);
            //    if (hPlayHandle >= 0)
            //    {
            //        cameraClient.RealDataCallBack(hPlayHandle, fRealDataCallBack);
            //        return true;
            //    }
            //}
            return false;
        }

        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwDataType"></param>
        /// <param name="pBuffer"></param>
        /// <param name="dwBufsize"></param>
        /// <param name="dwUser"></param>
        private void ReadDataCallBack(int lRealHandle, int dwDataType, byte[] pBuffer, int dwBufsize, int dwUser)
        {
            if (dwDataType == 0)
            {
                //3、上传（UDP）
            }
        }
        public bool SavePic(CameraEquipment camera, IntPtr hWnd, string fileName, out string errMsg)
        {
            openVideo(camera, hWnd, out errMsg);
            if (myPlayHandle >= 0)
            {
                bool result = cameraClient.CapturePicture(myPlayHandle, fileName);
                closeVideo();
                return result;
            }
            closeVideo();
            return false;
        }


        /// <summary>
        /// 启动实时监控
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="plateform"></param>
        /// <param name="hWnd"></param>
        public bool StartVideoPreview(CameraEquipment camera, IntPtr hWnd, out string errMsg)
        {
            errMsg = "";
            if (!videoShow)
            {
                videoShow = openVideo(camera, hWnd, out errMsg);
            }
            return videoShow;
        }
        /// <summary>
        /// 停止实时监控
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="plateform"></param>
        public bool StopVideoPreview()
        {
            videoShow = false;
            closeVideo();
            return !videoShow;
        }

        /// <summary>
        /// 保存实时数据，包括（图片，视频）
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="plateform"></param>
        /// <param name="filePath"></param>
        public bool StartRecord(CameraEquipment camera, IntPtr hWnd, String filePath, out string errMsg)
        {
            errMsg = "";
            if (!videoRecord)
            {
                //FileHelper.CreateDirByFile(filePath);
                bool result = openVideo(camera, hWnd, out errMsg);
                if (result)
                {
                    videoRecord = cameraClient.StartRecord(myPlayHandle, filePath);
                }
            }
            return videoRecord;
        }
        public bool StopRecord()
        {
            //停止保存实时监视数据，关闭保存的文件
            bool result = cameraClient.StopRecord(myPlayHandle);
            if (result)
            {
                videoRecord = false;
                closeVideo();
                return !videoRecord;
            }
            return result;
        }

        /// <summary>
        /// 开启视频
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="hWnd"></param>
        /// <param name="errMsg"></param>
        private bool openVideo(CameraEquipment camera, IntPtr hWnd, out string errMsg)
        {
            errMsg = "";
            if (myPlayHandle >= 0)
            {
                return true;
            }
            //1、SDK初始化
            CameraClient.Init();
            if (cameraClient == null || cameraClient.lLogin < 0)
            {
                try
                {
                    //2、登录网络设备
                    cameraClient = new CameraClient();
                    int error = cameraClient.Login(camera.IP, ushort.Parse(camera.Port.ToString()), camera.UserName, camera.PassWord, 0, "");
                    if (error > 0)
                    {
                        errMsg = "登录失败";
                    }
                }
                catch (Exception ex)
                {

                    errMsg = "登录失败";
                }
            }
            if (cameraClient.lLogin >= 0)//登录成功
            {
                //3、启动实时监视或多画面预览(参数如何填写)
                myPlayHandle = cameraClient.RealPlay(1, hWnd, 0, 0);
                if (myPlayHandle >= 0)
                {
                    videoShow = true;
                }
                else
                {
                    videoShow = false;
                }
            }
            return videoShow;
        }
        /// <summary>
        /// 关闭视频
        /// </summary>
        private void closeVideo()
        {
            if (!videoRecord && !videoShow && cameraClient != null)
            {
                //1、停止实时监控
                cameraClient.StopRealPlay(1);
                //2、退出登录
                cameraClient.Logout();
                //3、清空SDK
                CameraClient.Cleanup();
                myPlayHandle = -1;
            }
        }

        public void playVideo(IntPtr hWnd, String filePath)
        {
            try
            {
                PlayCtrlSDK.PlayM4_OpenFile(0, filePath);
                PlayCtrlSDK.PlayM4_Play(0, hWnd);
            }
            catch { }
        }

        public bool StartRecord1(CameraEquipment camera, IntPtr hWnd, String filePath, out string errMsg)
        {
            errMsg = "";
            //1、SDK初始化
            CameraClient.Init();
            if (cameraClient == null || cameraClient.lLogin < 0)
            {
                //2、登录网络设备
                cameraClient = new CameraClient();
                int error = cameraClient.Login(camera.IP, ushort.Parse(camera.Port.ToString()), camera.UserName, camera.PassWord, 0, "");
                if (error > 0)
                {
                    errMsg = "登录失败";
                }
            }
            if (cameraClient.lLogin > 0)//登录成功
            {
                //FileHelper.CreateDirByFile(filePath);
                //3、启动实时监视或多画面预览(参数如何填写)
                myPlayHandle = cameraClient.RealPlay(1, hWnd, 0, 0);
                if (myPlayHandle >= 0)
                {
                    videoRecord = cameraClient.StartRecord(myPlayHandle, filePath);
                }
            }
            return videoRecord;
        }
    }
}
