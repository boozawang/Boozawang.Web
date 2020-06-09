using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.Material
{
    public static class ApmexConst
    {
		// MATERIAL 
		public const string GOLD_SILVER_URL = "https://www.apmex.com/spotprices/gold-prices";
		public const string APMEX_TITLE_QUERY = ".spotprice-embed";
		public const string APMEX_LiST_QUERY = "li .price span.current";

		//public const string CURRENCY_API_URL = "https://api.exchangeratesapi.io/latest?";  //https://api.exchangeratesapi.io/latest?symbols=KRW&base=CNY
	}
}
