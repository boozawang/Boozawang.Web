using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.ReBalancing
{
    public class RebalancingResponse
    {
		/// <summary>
		/// 리밸런싱 결과
		/// </summary>
		public List<StockItem> StockData { get; set; }

		/// <summary>
		/// 리밸런싱 후 잔액
		/// </summary>
		public decimal ResultAmount { get; set; }
	}
}
