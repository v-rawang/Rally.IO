using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace Rally.Lib.Caching
{
    public class CacheManager 
    {
        private RedisClient redisClient;

        public CacheManager(string ServerAddress, int Port, string Password, long DBIndex)
        {
            this.redisClient = new RedisClient(ServerAddress, Port);
            this.redisClient.Password = Password;
            this.redisClient.ChangeDb(DBIndex);
        }

        public object ClearCache(string ID)
        {
            return this.redisClient.Del(ID);
        }

        public bool AddCache<T>(string ID, T ObjectToCache)
        {
            return this.redisClient.Add<T>(ID, ObjectToCache);
        }

        public bool AddCache<T>(string ID, T ObjectToCache, TimeSpan ExpiresIn)
        {
            return this.redisClient.Add<T>(ID, ObjectToCache, ExpiresIn);
        }

        public object GetCache(string ID)
        {
            return this.redisClient.GetValue(ID);
        }

        public T GetCache<T>(string ID)
        {
            return this.redisClient.GetById<T>(ID);
        }

        public void SetCache(string ID, object ObjectToCache)
        {
            this.redisClient.SetValue(ID, (string)(ObjectToCache));
            //this.redisClient.Save();
        }

        public bool SetCache<T>(string ID, T ObjectToCache)
        {
           return this.redisClient.Set<T>(ID, ObjectToCache);
            //this.redisClient.Save();
        }

        public void SetCache(string ID, object ObjectToCache, TimeSpan ExpiresIn)
        {
            this.redisClient.SetValue(ID, (string)(ObjectToCache), ExpiresIn);
        }

        public bool SetCache<T>(string ID, T ObjectToCache, TimeSpan ExpiresIn)
        {
            return this.redisClient.Set<T>(ID, ObjectToCache, ExpiresIn);
            //this.redisClient.Save();
        }

        public void Subscribe(string[] Channels, Func<object, object> ExtensionFunction)
        {
            using (var subscription = this.redisClient.CreateSubscription())
            {
                subscription.OnMessage = (chn, msg) => {
                    if (ExtensionFunction != null)
                    {
                        ExtensionFunction(new { Channel = chn, Message = msg});
                    }
                };

                subscription.SubscribeToChannels(Channels);
            }
        }
    }
}
