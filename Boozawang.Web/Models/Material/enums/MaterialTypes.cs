using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.Material
{
	/// <summary>
	/// 금속 종류
	/// 0 : All 
	/// 1 : Gold
	/// 2 : Silver
	/// 3 : Platinum
	/// 4 : Palladium
	/// </summary>
	public enum MaterialTypes
    {
		/// <summary>
		/// 알수 없음
		/// </summary>
		All = 0,
		/// <summary>
		/// 금
		/// </summary>
		Gold = 1,
		/// <summary>
		/// 은
		/// </summary>
		Silver = 2,
		/// <summary>
		/// 백금
		/// </summary>
		Platinum = 3,
		/// <summary>
		/// 팔라듐
		/// </summary>
		Palladium = 4
	}
}
