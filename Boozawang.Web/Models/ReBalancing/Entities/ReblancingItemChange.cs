using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.ReBalancing
{
	/// <summary>
	/// 변경 요약
	/// </summary>
    public class ReblancingItemChange
    {
		/// <summary>
		/// 이름
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 기준가격
		/// </summary>
		public decimal BasePrice { get; set; }

		/// <summary>
		/// 수량
		/// </summary>
		public int ChangeQty { get; set; }
	}
}
