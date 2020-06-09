using Boozawang.Web.Cache;
using Boozawang.Web.Interface;
using Boozawang.Web.Models.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace Boozawang.Web.Service
{
    public class MaterialApmexService : IMaterialService
    {
		/// <summary>
		/// 금속 캐시 - memory cache
		/// </summary>
		private static MaterialCache cache = new MaterialCache();

		public List<MaterialItem> GetAll(bool useCache, bool updateCache)
        {
            return new List<MaterialItem>();
        }

		/// <summary>
		/// 가격 조회(달러/트로이온스)
		/// </summary>
		/// <param name="materialType"></param>
		/// <param name="useCache"></param>
		/// <param name="updateCache"></param>
		/// <returns></returns>
		public MaterialItem GetByType(MaterialTypes materialType, bool useCache, bool updateCache)
		{
			MaterialItem result = new MaterialItem();
			decimal price = 0;

			if (useCache == true)
			{
				price = GetMaterailByCache(materialType);
			}

			if (updateCache == true || price == 0)
			{
				price = GetMaterailByHTML(materialType);
				SetMaterailCache(materialType, price);
			}

			result.Name = materialType.ToString();
			result.Price = price;

			return result;
		}

		/// <summary>
		/// 캐시에서 가격 가져오기
		/// </summary>
		/// <param name="materialType"></param>
		/// <returns></returns>
		private decimal GetMaterailByCache(MaterialTypes materialType)
		{
			string key = "MaterialTypes/" + materialType.ToString();
			return cache.GetCache(key);
		}

		/// <summary>
		/// 캐시에 가격 기록
		/// </summary>
		/// <param name="materialType"></param>
		/// <param name="val"></param>
		private void SetMaterailCache(MaterialTypes materialType, decimal val)
		{
			string key = "MaterialTypes/" + materialType.ToString();
			cache.Set(key, val);
		}

		/// <summary>
		/// html로 가격 가져오기
		/// </summary>
		/// <param name="materialType"></param>
		/// <returns></returns>
		private decimal GetMaterailByHTML(MaterialTypes materialType)
		{
			decimal result = 0;

			// 사이트 html 받아오기
			CsQuery.CQ siteInfo = GetSiteInfoHtml();

			// 정보 거르기
			result = GetPriceFromInfo(materialType, siteInfo);

			return result;
		}

		/// <summary>
		/// 사이트에서 중요 정보 가져오기 - Apmex
		/// </summary>
		/// <returns></returns>
		private CsQuery.CQ GetSiteInfoHtml()
		{
			CsQuery.CQ infoDom = new CsQuery.CQ();

			CsQuery.CQ siteDom = GetHTML(ApmexConst.GOLD_SILVER_URL);
			infoDom = GetExtractTitleFromApmex(siteDom);

			return infoDom;
		}

		/// <summary>
		/// 원하는 정보 추출
		/// </summary>
		/// <param name="type"></param>
		/// <param name="siteInfo"></param>
		/// <returns></returns>
		private decimal GetPriceFromInfo(MaterialTypes type, CsQuery.CQ siteInfo)
		{
			decimal result = 0;
			string dollarStr = string.Empty;
			var data = siteInfo[ApmexConst.APMEX_LiST_QUERY].ToList();

			switch (type)
			{
				case MaterialTypes.Gold:
					dollarStr = data[0].FirstChild.ToString();
					break;
				case MaterialTypes.Silver:
					dollarStr = data[1].FirstChild.ToString();
					break;
				case MaterialTypes.Platinum:
					dollarStr = data[2].FirstChild.ToString();
					break;
				case MaterialTypes.Palladium:
					dollarStr = data[3].FirstChild.ToString();
					break;
			}

			// decimal 변환
			Decimal.TryParse(dollarStr.Replace("$", ""), out result);

			return result;
		}

		/// <summary>
		/// 페이지 dom 가져오기
		/// </summary>
		/// <param name="urlAddress"></param>
		/// <returns></returns>
		private string GetHTML(string urlAddress)
		{
			#region 변수 선언

			string result = string.Empty;
			Uri uriResult;
			bool testResult = false;
			#endregion

			#region 파라메터 유효성 검사

			// 정상 url 인지 확인

			testResult = Uri.TryCreate(urlAddress, UriKind.Absolute, out uriResult)
				&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

			if (testResult == false)
				return result;

			#endregion

			#region 행동 정의

			// HTML 불러오기
			using (WebClient client = new WebClient())
			{
				client.Encoding = Encoding.UTF8;
				if (urlAddress.IndexOf("https") >= 0)
					System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
				result = client.DownloadString(urlAddress);
			}

			#endregion

			#region 결과값 생성

			return result;

			#endregion
		}

		/// <summary>
		/// apmex 사이트중 위 헤더 받아오기
		/// </summary>
		/// <param name="siteDom"></param>
		/// <returns></returns>
		private CsQuery.CQ GetExtractTitleFromApmex(CsQuery.CQ siteDom)
		{
			return siteDom[ApmexConst.APMEX_TITLE_QUERY];
		}

	}
}
