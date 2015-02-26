using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Etags.Nancy.Api.Models;
using Nancy.Conventions;

namespace Etags.Nancy.Api.Cache
{
    public class CachingInterceptor : IInterceptor
    {
        private ICache cache;

        public CachingInterceptor(ICache cache)
        {
            this.cache = cache;
        }

        public void Intercept(IInvocation invocation)
        {
            //Writes cache-bust - make cache ID a Sha1 hash of ClassName + UUID?
            // Write this: key: UUID, value { last modified }
            if (ShouldInvalidateCache(invocation))
            {
                var arg = (Person)invocation.Arguments[0];
                var toRemove = string.Format("{0}-{1}", arg.Id, arg.LastModifiedDate); 
                cache.Delete(toRemove);
            }

            invocation.Proceed();
            if (ShouldUpdateCache(invocation))
            {
                int id = 0;
                Person person;
                if (invocation.Arguments[0].GetType() == typeof (Person))
                {
                    var arg = (Person) invocation.Arguments[0];
                    id = arg.Id;
                    person = arg;
                }
                else
                {
                    id = (int) invocation.Arguments[0];
                    person = (Person) invocation.ReturnValue;
                }

                var tag = string.Format("{0}-{1}", id, person.LastModifiedDate.Ticks);
                var toCache = new { etag = tag, lastModifiedDate = person.LastModifiedDate};
                cache.AddOrUpdate(id.ToString(), toCache, 20000);
                var res = cache.Get(id.ToString());
            }
            
        }

        public bool ShouldUpdateCache(IInvocation invocation)
        {
            return invocation.Method.Name.Contains("Add") || invocation.Method.Name.Contains("Edit") || invocation.Method.Name.Contains("Get")
;
        }

        public bool ShouldInvalidateCache(IInvocation invocation)
        {
            return invocation.Method.Name.Contains("Edit");
;
        }

    }
}
