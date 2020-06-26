using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.ReBalancing
{
    public class RebalancingRes
    {
		/// <summary>
		/// 변경 요약
		/// </summary>
		public List<ReblancingItemChange> ChangeSummary { get; set; }

		/// <summary>
		/// 리밸런싱 결과
		/// </summary>
		//public List<StockItem> StockData { get; set; }

		/// <summary>
		/// 리밸런싱 후 잔액
		/// </summary>
		public decimal RestAmount { get; set; }

		
	}
}
