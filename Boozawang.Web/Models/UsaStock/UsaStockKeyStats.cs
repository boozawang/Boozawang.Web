using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.UsaStock
{
    /// <summary>
    /// 개별 주식 정보
    /// </summary>
    public class UsaStockKeyStats
    {
        /// <summary>
        /// 티커
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// 회사명
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 마지막 가격
        /// </summary>
        public string LastPrice { get; set; }

        /// <summary>
        /// 예상 배당(율) (TTM)
        /// </summary>
        public string DivTTM { get; set; }

        /// <summary>
        /// Payout Ratio
        /// </summary>
        public string PayOutRate { get; set; }

        /// <summary>
        /// 시가 총액
        /// </summary>
        public string MarketCap { get; set; }

        /// <summary>
        /// PE Ratio (TTM)
        /// </summary>
        public string PerTTM { get; set; }

        /// <summary>
        /// ROE (TTM)
        /// </summary>
        public string RoeTTM { get; set; }

        /// <summary>
        /// 영업이익률 (TTM)
        /// </summary>
        public string OperatingMargin { get; set; }
    }
}
