using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Camera.Core.Parameter;

namespace Rally.Lib.Camera.Core
{
   public interface IVideoCamera
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Parameters"></param>
        bool Initialize(ParameterBase Parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool Close();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HWnd"></param>
        /// <param name="FilePath"></param>
        /// <param name="Callback"></param>
        /// <returns></returns>
        bool Record(IntPtr HWnd, string FilePath, Func<object, object> Callback);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool StopRecord();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HWnd"></param>
        /// <param name="FilePath"></param>
        /// <param name="Callback"></param>
        /// <returns></returns>
        bool Capture(IntPtr HWnd, string FilePath, Func<object, object> Callback);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HWnd"></param>
        /// <param name="Callback"></param>
        /// <returns></returns>
        bool Preview(IntPtr HWnd, Func<object, object> Callback); 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool StopPreview();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Callback"></param>
        /// <returns></returns>
        bool RealPlay(Func<object, object> Callback);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDictionary<string, object> Get();
    }
}
