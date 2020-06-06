using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.ReBalancing
{
	/// <summary>
	/// 주식 아이템
	/// </summary>
    public class StockItem
    {
		/// <summary>
		/// 이름
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 가격
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// 수량
		/// </summary>
		public int Qty { get; set; }

		/// <summary>
		/// 비중
		/// </summary>
		public decimal Weight { get; set; }
	}
}
