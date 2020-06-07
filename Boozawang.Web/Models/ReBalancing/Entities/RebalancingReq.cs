using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.ReBalancing
{
    public class RebalancingReq
    {
        /// <summary>
        /// 리밸런싱 목표 데이터
        /// </summary>
        public List<StockItemReq> StockItem { get; set; }

        /// <summary>
        /// 추가금액
        /// </summary>
        public decimal AdditionalMoney { get; set; }

        /// <summary>
        /// 나머지 금액 옵션
        /// </summary>
        public RestOptionTypes RestOption { get; set; }
    }
}
