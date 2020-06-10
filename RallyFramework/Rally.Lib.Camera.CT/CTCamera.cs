using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraNet;
using Rally.Lib.Camera.Core;
using Rally.Lib.Camera.Core.Parameter;

namespace Rally.Lib.Camera.CT
{
    public class CTCamera : IVideoCamera
    {
        private CameraClient cameraClient;
        private IPCameraParameter cameraParameter;
        private int currentHandle;
        private bool isPlaying;
        private bool isRecording;
        private Func<object, object> realPlayCallback;

        public static IVideoCamera NewInstance()
        {
            return new CTCamera();
        }

        public bool Initialize(ParameterBase Parameters)
        {
            CameraClient.Init();
            this.cameraParameter = Parameters as IPCameraParameter;
            this.cameraClient = new CameraClient();
            int error = this.cameraClient.Login(this.cameraParameter.Address, this.cameraParameter.Port, this.cameraParameter.UserName, this.cameraParameter.Password, this.cameraParameter.DeviceSpecification, this.cameraParameter.DeviceSN);

            return error <= 0;
        }

        public bool Preview(IntPtr HWnd, Func<object, object> Callback)
        {
            if (this.cameraClient.lLogin > 0)//登录成功
            {
                //3、启动实时监视或多画面预览(参数如何填写)
                //this.currentHandle = cameraClient.RealPlay(0, HWnd, 0, 0);
                this.currentHandle = cameraClient.RealPlay(this.cameraParameter.PreviewChannelID, HWnd, this.cameraParameter.StreamType, this.cameraParameter.NetType);

                this.isPlaying = this.currentHandle > 0;    
            }

            if (Callback != null)
            {
                Callback(new object[] { this.currentHandle });
            }

            return this.isPlaying;
        }

        public bool StopPreview()
        {
            this.isPlaying = false;
            return this.cameraClient.StopRealPlay(this.cameraParameter.PreviewChannelID);
        }

        public bool RealPlay(Func<object, object> Callback)
        {
            this.realPlayCallback = Callback;

            //CameraClient.Init();

            if (cameraClient.lLogin > 0)//登录成功
            {
                //3、启动实时监视或多画面预览
                //int hPlayHandle = cameraClient.RealPlay(1, IntPtr.Zero, 0, 0);
                int hPlayHandle = cameraClient.RealPlay(this.cameraParameter.RealPlayChannelID, IntPtr.Zero, this.cameraParameter.StreamType, this.cameraParameter.NetType);
                if (hPlayHandle > 0)
                {
                    cameraClient.RealDataCallBack(hPlayHandle, this.ReadDataCallBack);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
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

            if (this.realPlayCallback != null)
            {
                this.realPlayCallback(new object[] { lRealHandle, dwDataType, pBuffer, dwBufsize, dwUser });
            }
        }

        public bool Record(IntPtr HWnd, string FilePath, Func<object, object> Callback)
        {
            if (!this.isRecording)
            {
               this.isRecording = this.cameraClient.StartRecord(this.currentHandle, FilePath);
            }

            if (Callback != null)
            {
                Callback(new object[] { this.currentHandle, this.isRecording, FilePath });
            }

            return this.isRecording;
        }

        public bool StopRecord()
        {
            bool result = cameraClient.StopRecord(this.currentHandle);
            if (result)
            {
                this.isRecording = false;
            }
            return result;
        }

        public bool Capture(IntPtr HWnd, string FilePath, Func<object, object> Callback)
        {
            if (this.currentHandle > 0)
            {
                bool result = this.cameraClient.CapturePicture(this.currentHandle, FilePath);
                return result;
            }

            if (Callback != null)
            {
                Callback(new object[] { this.currentHandle, FilePath });
            }

            return false;
        }

        public bool Close()
        {
            if (!this.isRecording && !this.isPlaying && this.cameraClient != null)
            {
                //1、停止实时监控
                cameraClient.StopRealPlay(this.cameraParameter.RealPlayChannelID);
                //2、退出登录
                cameraClient.Logout();
                //3、清空SDK
                CameraClient.Cleanup();
                this.currentHandle = -1;
            }

            return true;
        }

        public IDictionary<string, object> Get()
        {
            return new Dictionary<string, object>() {
                {"IsPlaying", this.isPlaying },
                {"IsRecording", this.isRecording},
                {"CurrentHandle", this.currentHandle},
                {"Parameter", this.cameraParameter },
                {"Model", "ZXAD-2CC865MF"},
                {"Specification", "ZXAD-2CC865MF"},
                {"Manufacturer", "智鑫安盾"},
                {"Brand", "智鑫安盾"},
                {"Version", "1.0.0.0"}
            };
        }
    }
}
