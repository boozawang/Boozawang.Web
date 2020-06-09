using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Boozawang.Web.Cache
{
    public class MaterialCache
    {
		private ObjectCache cache = MemoryCache.Default;
		private const int EXP_MIN = 10;

		public decimal GetCache(string key)
		{
			var getResult = cache[key];
			if (getResult == null)
				return 0;
			else
				return (decimal)getResult;
		}

		public void Set(string key, decimal value)
		{
			CacheItemPolicy policy = new CacheItemPolicy();
			policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(EXP_MIN);
			cache.Set(key, value, policy);
		}
	}
}
