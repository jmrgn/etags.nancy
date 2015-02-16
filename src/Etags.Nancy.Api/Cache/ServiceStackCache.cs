using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace Etags.Nancy.Api.Cache
{
    public class ServiceStackCache : ICache
    {
        private IRedisClientsManager manager;
        private ICacheClient client;

        public ServiceStackCache(IRedisClientsManager manager)
        {

            this.manager = manager;
            client = manager.GetCacheClient();
           
        }

        public bool Contains(string key)
        {
            bool result = false;
            var obj = client.Get<string>(key);
            if (!string.IsNullOrEmpty(obj))
            {
                result = true;
            }
            return result;
        }

        public object Get(string key)
        {
            return client.Get<string>(key).ToJson();
        }

        public void Add(string key, object value)
        {
            client.Add(key, value);
        }

        public void Add(string key, object value, int expiration)
        {
            var expires = new TimeSpan(0, 0, expiration);
            client.Add(key, value.ToJson(), expires);
        }

        public void AddOrUpdate(string key, object value, int expiration)
        {
            var expires = new TimeSpan(0, 0, expiration);
            client.Add(key, value.ToJson(), expires);
        }

        public void Delete(string key)
        {
            var result = client.Remove(key);
        }
    }
}
