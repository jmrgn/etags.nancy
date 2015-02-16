using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using StackExchange.Redis;

namespace Etags.Nancy.Api.Cache
{
    public class RedisCache : ICache
    {
        private ConnectionMultiplexer connectionMultiplexer;
        private IDatabase db;

        public RedisCache(ConnectionMultiplexer multiplexer)
        {
            this.connectionMultiplexer = multiplexer;
            this.db = multiplexer.GetDatabase();
        }

        public bool Contains(string key)
        {
            return db.KeyExists(key);
        }

        public object Get(string key)
        {
            return db.StringGet(key);
        }

        public void Add(string key, object value)
        {
            if (Contains(key))
            {
                throw new ArgumentException(string.Format("The key {0} already exists.", key));
            }
            db.StringSet(key, value.ToJson());
        }

        public void Add(string key, object value, int expiration)
        {
            if (Contains(key))
            {
                throw new ArgumentException(string.Format("The key {0} already exists.", key));
            }
            AddOrUpdate(key, value, expiration); 
        }

        public void AddOrUpdate(string key, object value, int expiration)
        {
            var span = new TimeSpan(0, 0, 0, 0, expiration);
            db.StringSet(key, value.ToJson(), span);
        }

        public void Delete(string key)
        {
            db.KeyDelete(key, CommandFlags.FireAndForget);
        }
    }
}
