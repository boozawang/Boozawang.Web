using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.ReBalancing
{
	/// <summary>
	/// 주식 아이템
	/// </summary>
    public class ReblancingItemReq
    {
		/// <summary>
		/// 이름
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 가격
		/// </summary>
		public decimal CurrentPrice { get; set; }

		/// <summary>
		/// 수량
		/// </summary>
		public int CurrentQty { get; set; }

		/// <summary>
		/// 목표 비중
		/// </summary>
		public decimal TargetWeight { get; set; }
	}
}
