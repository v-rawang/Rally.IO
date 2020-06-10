using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Reflection;


namespace Rally.Lib.Protocol.Message
{
  
    public static class Mapper<T> where T : class //必须为引用类型
    {
        private static readonly Dictionary<string, PropertyInfo> propertyMap;

        static Mapper()
        { 
            // 创建属性名称-属性信息字典.
            propertyMap = typeof(T).GetProperties().ToDictionary
                (//p => p.Name.ToLower(),
                    p=>p.Name,
                    p => p);
        }

        public static void Map(ExpandoObject Source, T Destination)
        {
            //非空判断
            if (Source == null)
                throw new ArgumentNullException("source");
            if (Destination == null)
                throw new ArgumentNullException("destination");
      
            foreach (var kv in Source)
            {
                PropertyInfo p;
                if (propertyMap.TryGetValue(kv.Key, out p))//if (propertyMap.TryGetValue(kv.Key.ToLower(), out p))
                {
                    var propType = p.PropertyType;
                    if (kv.Value == null)
                    {
                        if (!propType.IsByRef && propType.Name != "Nullable`1")
                        {
                            // 如果不是 Nullable<>则抛异常
                            throw new ArgumentException("not nullable");
                        }
                    }
                    else if (kv.Value.GetType() != propType)
                    {
                        throw new ArgumentException("type mismatch");
                    }
                    p.SetValue(Destination, kv.Value, null);
                }
            }
        }

        public static void Map(T Source, ExpandoObject Destination)
        {
            //非空判断
            if (Source == null)
                throw new ArgumentNullException("source");
            if (Destination == null)
                throw new ArgumentNullException("destination");

            foreach (var item in propertyMap)
            {
                if (!(Destination as IDictionary<string, object>).ContainsKey(item.Key))
                {
                    (Destination as IDictionary<string, object>).Add(item.Value.Name, item.Value.GetValue(Source));
                }               
            }
        }
    }
}
