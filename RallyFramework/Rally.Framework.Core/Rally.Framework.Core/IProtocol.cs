using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Core
{
    public interface IProtocol
    {
        bool Initialize(dynamic Argument);

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="Response"></param>
        /// <returns></returns>
        bool Validate(dynamic Request, out dynamic Response, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction);

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="Response"></param>
        /// <returns></returns>
        bool Validate<TRequest, TResponse>(TRequest Request, out TResponse Response, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class;

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        dynamic Register(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        TResponse Register<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class;


        /// <summary>
        /// 读取测量数据
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        dynamic GetMeasurementData(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction);

        /// <summary>
        /// 读取测量数据
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        TResponse GetMeasurementData<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class;

        /// <summary>
        /// 读取测量配置参数
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        dynamic GetMeasurementConfig(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction);

        /// <summary>
        /// 读取测量配置参数
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        TResponse GetMeasurementConfig<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class;

        /// <summary>
        /// 设置测量配置参数
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        dynamic SetMeasurementConfig(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction);

        /// <summary>
        /// 设置测量配置参数
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        TResponse SetMeasurementConfig<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class;

        /// <summary>
        /// 操控下位机
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        dynamic ManipulateSCM(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction);

        /// <summary>
        /// 操控下位机
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        TResponse ManipulateSCM<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class;

        /// <summary>
        /// 设置下位机系统参数
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        dynamic ConfigureSCM(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction);

        /// <summary>
        /// 设置下位机系统参数
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        TResponse ConfigureSCM<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class;

        /// <summary>
        /// 故障自检
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        dynamic Diagnose(dynamic Request, IDictionary<string, int[]> RequestByteMappings, IDictionary<string, int[]> ResponseByteMappings, Func<object, object> ExtensionFunction);

        /// <summary>
        /// 故障自检
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        TResponse Diagnose<TRequest, TResponse>(TRequest Request, Func<object, object> ExtensionFunction) where TRequest : class where TResponse : class;

        /// <summary>
        /// 获取协议信息
        /// </summary>
        /// <returns></returns>
        IDictionary<string, object> Info();
    }
}
