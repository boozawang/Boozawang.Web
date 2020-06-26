using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Boozawang.Web.Cache
{
	/// <summary>
	/// 미국 주식 캐시
	/// </summary>
    public class UsaStockCache
    {
		private ObjectCache cache = MemoryCache.Default;
		private const int EXP_MIN = 10;

		/// <summary>
		/// 캐시 가져오기
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetCache(string key)
		{
			var getResult = cache[key];
			if (getResult == null)
				return string.Empty;
			else
				return getResult.ToString();
		}


		/// <summary>
		/// 캐시 저장하기
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void Set(string key, string value)
		{
			CacheItemPolicy policy = new CacheItemPolicy();
			policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(EXP_MIN);
			cache.Set(key, value, policy);
		}
	}
}
