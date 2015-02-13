using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Etags.Nancy.Api.Cache
{
    public class CachingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //Writes cache-bust - make cache ID a Sha1 hash of UUID
            invocation.Proceed();
        }
    }
}
