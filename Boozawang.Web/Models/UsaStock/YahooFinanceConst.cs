using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.UsaStock
{
    /// <summary>
    /// 야후 파이낸스에서 쓰는 상수
    /// </summary>
    public static class YahooFinanceConst
    {
        /// <summary>
        /// 정보 가져오는 메인 페이지
        /// </summary>
        public const string PAGE = "https://finance.yahoo.com/quote/";

        /// <summary>
        /// 회사명 XPATH
        /// </summary>
        public const string XPATH_COMP_NAME = "/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[4]/div/div/div/div[2]/div[1]/div[1]/h1";

        /// <summary>
        /// 가격 정보 XPATH
        /// </summary>
        public const string XPATH_PRICE = "/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[4]/div/div/div/div[3]/div/div/span[1]";

        /// <summary>
        /// PE Ratio XPATH
        /// </summary>
        public const string XPATH_PER = "/html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/div/div[2]/div[2]/table/tbody/tr[3]/td[2]/span";

        /// <summary>
        /// 배당 정보 XPATH
        /// </summary>
        public const string XPATH_DIV = "//html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/div/div[2]/div[2]/table/tbody/tr[6]/td[2]";

        /// <summary>
        /// 시총 정보 XPATH
        /// </summary>
        public const string XPATH_MARKET_CAP = "//html/body/div[1]/div/div/div[1]/div/div[3]/div[1]/div/div[1]/div/div/div/div[2]/div[2]/table/tbody/tr[1]/td[2]/span";

    }
}
