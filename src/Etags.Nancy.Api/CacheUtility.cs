using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etags.Nancy.Api.Cache;
using Nancy;
using Newtonsoft.Json.Linq;
using ServiceStack;
using StackExchange.Redis;

namespace Etags.Nancy.Api
{
    public class CacheUtility
    {
        private ICache cache;
        private const char Delim = '-';

        public CacheUtility(ICache cache)
        {
            this.cache = cache;
        }

        public void CheckForIfNonMatch(NancyContext context)
        {
            var request = context.Request;
            if (request.Method.Equals("get", StringComparison.CurrentCultureIgnoreCase))
            {

                var token = GetIfNoneMatch(request);
                var split = ParseToken(token);
                if (split.Length != 2)
                {   
                    return;
                }

                var cachedVal = SearchCache(split[0]);
                if (cachedVal != null && cachedVal["etag"].Value == token)
                {
                        context.Response = HttpStatusCode.NotModified;
                }
            }
        }

        private string GetIfNoneMatch(Request request)
        {
            var token = String.Empty;
            var responseETag = request.Headers.IfNoneMatch;
            if (responseETag != null && responseETag.Count() > 0)
            {
                var tagList = responseETag.ToList();
                token = tagList[0];
            }
            return token;
        }

        private string[] ParseToken(string token)
        {
            return token.Split(Delim);
        }

        private dynamic SearchCache(string key)
        {
            var val = (RedisValue) cache.Get(key);
            if (!val.IsNullOrEmpty)
            {
                var cachedVal = JObject.Parse(val);
                return cachedVal;

            }
            return null;
        }
    }
}
