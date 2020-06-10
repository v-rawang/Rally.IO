using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Camera.Core;

namespace UnitTestCamera
{
    public class CameraUnitTest
    {
        private IVideoCamera videoCamera;

        public CameraUnitTest(IVideoCamera Camera){
            this.videoCamera = Camera;
        }

        public void InitializeTest(){
            this.videoCamera.Initialize(new Rally.Lib.Camera.Core.Parameter.IPCameraParameter() {
                 Address = "",
                 Port = 0,
                 UserName = "",
                 Password = "" });
        }

        public void PreviewTest(){
            bool result = false;
            FormTest formTest = new FormTest();

            this.videoCamera.Preview(formTest.PlayerHandle, null);

            if (formTest.ShowDialog() == System.Windows.Forms.DialogResult.Abort){
                result = this.videoCamera.StopPreview();
            }

            Console.WriteLine(result);
            IDictionary<string, object> info = this.videoCamera.Get();

            foreach (string key in info.Keys){
                Console.WriteLine($"{key}:{info[key]}");
            }
        }

        public void RecordTest(string filePath){
            bool result = false;
            FormTest formTest = new FormTest();

            result = this.videoCamera.Record(formTest.PlayerHandle, filePath,  (o)=>{
                foreach (var item in (o as object[])){
                    Console.WriteLine(item);
                }
                return o; });

            Console.WriteLine(result);
        }

        public void RealPlayTest(){
            this.videoCamera.RealPlay((o)=> {
                foreach (var item in (o as object[])){
                    Console.WriteLine(item);
                }
                return o;
            });
        }
    }
}
