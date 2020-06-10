using Boozawang.Web.Cache;
using Boozawang.Web.Interface;
using Boozawang.Web.Models.Fx.Consts;
using Boozawang.Web.Models.Fx.Entities;
using Boozawang.Web.Models.Fx.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Boozawang.Web.Service
{
    public class FxERApiService : IFxService
	{
		/// <summary>
		/// 외환 캐시 - memory cache
		/// </summary>
		private static FxCache cache = new FxCache();

		/// <summary>
		/// 전체 조회
		/// </summary>
		/// <param name="targetType"></param>
		/// <returns></returns>
		public List<FxItem> GetAll(CurrencyTypes targetType)
		{
			var result = GetAllExByCache(targetType);

			if (!HasAllPrice(result))
            {
				//result = GetAllExByAPI(targetType);
				SetAllExCache(result);
			}

			return result;
		}

		/// <summary>
		/// 타입으로 조회
		/// </summary>
		/// <param name="targetType"></param>
		/// <param name="baseType"></param>
		/// <returns></returns>
		public FxItem GetByType(CurrencyTypes targetType, CurrencyTypes baseType)
		{
			decimal price = GetExByCache(targetType, baseType);

			if (price == 0)
			{
				price = GetExByAPI(targetType, baseType);
				SetExCache(targetType, baseType, price);
			}

			return new FxItem()
			{
				TargetCurrency = targetType.ToString(),
				BaseCurrency = baseType.ToString(),
				Price = price
			};
		}

		


		/// <summary>
		/// 캐시로 환율조회
		/// </summary>
		/// <param name="baseType"></param>
		/// <param name="targetType"></param>
		/// <returns></returns>
		private decimal GetExByCache(CurrencyTypes targetType, CurrencyTypes baseType)
		{
			string key = targetType.ToString() + "/" + baseType.ToString();
			return cache.GetCache(key);
		}

		/// <summary>
		/// 캐시에 저장
		/// </summary>
		/// <param name="baseType"></param>
		/// <param name="targetType"></param>
		/// <param name="price"></param>
		private void SetExCache(CurrencyTypes targetType, CurrencyTypes baseType, decimal price)
		{
			string key = targetType.ToString() + "/" + baseType.ToString();
			cache.Set(key, price);
		}

		/// <summary>
		/// 모든 캐시 가져오기
		/// </summary>
		/// <param name="targetType"></param>
		/// <returns></returns>
		private List<FxItem> GetAllExByCache(CurrencyTypes targetType)
		{
			List<FxItem> result = new List<FxItem>();

			foreach (CurrencyTypes type in Enum.GetValues(typeof(CurrencyTypes)))
            {
				string key = targetType.ToString() + "/" + type.ToString();
				result.Add(
					new FxItem()
					{
						TargetCurrency = targetType.ToString(),
						BaseCurrency = type.ToString(),
						Price = cache.GetCache(key)
					}
				);
			}

			return result;
		}

		/// <summary>
		/// 모든 캐시 저장
		/// </summary>
		/// <param name="data"></param>
		private void SetAllExCache(List<FxItem> data)
		{
			foreach(var entity in data)
            {
				string key = entity.TargetCurrency.ToString() + "/" + entity.BaseCurrency.ToString();
				cache.Set(key, entity.Price);
			}
		}


		/// <summary>
		/// api로 환율조회
		/// </summary>
		/// <param name="targetType"></param>
		/// <param name="baseType"></param>
		/// <returns></returns>
		private decimal GetExByAPI(CurrencyTypes targetType, CurrencyTypes baseType)
		{
			decimal result = 0;
			string reqUrl = MakeGetCurrencyURL(baseType, targetType);
			string responseStr = GetRequestAPI(reqUrl);
			LatestT responseT = JsonConvert.DeserializeObject<LatestT>(responseStr);
			result = GetTargetCurrency(targetType, responseT);
			return result;
		}

		/// <summary>
		/// api로 환율조회
		/// </summary>
		/// <param name="targetType"></param>
		/// <param name="baseType"></param>
		/// <returns></returns>
		private decimal GetAllExByAPI(CurrencyTypes targetType, CurrencyTypes baseType)
		{
			decimal result = 0;
			string reqUrl = MakeGetCurrencyURL(baseType, targetType);
			string responseStr = GetRequestAPI(reqUrl);
			LatestT responseT = JsonConvert.DeserializeObject<LatestT>(responseStr);
			result = GetTargetCurrency(targetType, responseT);
			return result;
		}

		/// <summary>
		/// api에서 해당 통화정보 가져옴
		/// </summary>
		/// <param name="targetType"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		private static decimal GetTargetCurrency(CurrencyTypes targetType, LatestT data)
		{
			decimal result = 0;

			if (data == null || data.rates == null)
				return result;

			switch (targetType)
			{
				case CurrencyTypes.AUD:
					result = data.rates.AUD;
					break;
				case CurrencyTypes.CAD:
					result = data.rates.CAD;
					break;
				case CurrencyTypes.CNY:
					result = data.rates.CNY;
					break;
				case CurrencyTypes.EUR:
					result = data.rates.EUR;
					break;
				case CurrencyTypes.GBP:
					result = data.rates.GBP;
					break;
				case CurrencyTypes.JPY:
					result = data.rates.JPY;
					break;
				case CurrencyTypes.KRW:
					result = data.rates.KRW;
					break;
				case CurrencyTypes.USD:
					result = data.rates.USD;
					break;
			}

			return result;
		}


		/// <summary>
		/// api url생성
		/// </summary>
		/// <param name="targetType"></param>
		/// <param name="baseType"></param>
		/// <returns></returns>
		private static string MakeGetCurrencyURL(CurrencyTypes targetType, CurrencyTypes baseType)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(ExchangeRateApiConsts.CURRENCY_API_URL);
			sb.Append("symbols=");
			sb.Append(targetType.ToString());
			sb.Append("&base=");
			sb.Append(baseType.ToString());
			return sb.ToString();
		}

		/// <summary>
		/// HTTP GET 요청
		/// </summary>
		/// <param name="reqUrl"></param>
		/// <returns></returns>
		private string GetRequestAPI(string reqUrl)
		{
			HttpWebRequest httpWebRequest = null;

			string result = string.Empty;

			try
			{
				httpWebRequest = (HttpWebRequest)WebRequest.Create(reqUrl);
				httpWebRequest.Method = "GET";
				httpWebRequest.ContentType = "application/json; charset=utf-8";
				httpWebRequest.Accept = "application/json";
				httpWebRequest.Timeout = 2000;

				using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			catch
			{
				result = string.Empty;
			}
			finally
			{
				if (null != httpWebRequest)
				{
					try { httpWebRequest.Abort(); httpWebRequest = null; } catch { }
				}
			}

			return result;

		}

		/// <summary>
		/// 모두 값이 있나 확인
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private bool HasAllPrice(List<FxItem> data)
		{
			if (data?.Any() ?? false)
			{
				if (!data.Any(x => x.Price == 0))
					return true;
			}
			return false;
		}
	}
}

