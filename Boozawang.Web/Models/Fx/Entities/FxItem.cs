using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.Fx.Entities
{
    /// <summary>
    /// 환율 정보
    /// </summary>
    public class FxItem
    {
        /// <summary>
        /// 대상 통화 (분자)
        /// </summary>
        public string TargetCurrency { get; set; }

        /// <summary>
        /// 기준 통화 (분모)
        /// </summary>
        public string BaseCurrency { get; set; }

        /// <summary>
        /// 가격 
        /// </summary>
        public decimal Price { get; set; }
    }
}
