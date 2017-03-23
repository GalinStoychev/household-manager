using Ninject.Extensions.Interception;
using System;
using System.Web;
using System.Web.Caching;

namespace HouseholdManager.Web.App_Start.Interceptors
{
    public class CachedInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var cache = HttpContext.Current.Cache;
            var callingMethodName = invocation.Request.Method.Name;
            var cachedValue = cache[callingMethodName];
            if (cachedValue == null)
            {
                invocation.Proceed();
                cache.Insert(invocation.Request.Method.Name, invocation.ReturnValue, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
            }
            else
            {
                invocation.ReturnValue = cachedValue;
            }
        }
    }
}