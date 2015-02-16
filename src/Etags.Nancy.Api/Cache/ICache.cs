using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etags.Nancy.Api.Cache
{
    public interface ICache
    {
        bool Contains(string key);
        object Get(string key);
        void Add(string key, object value);
        void Add(string key, object value, int expiration);
        void AddOrUpdate(string key, object value, int expiration);
        void Delete(string key);
    }
}
