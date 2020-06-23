using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.Fx.Entities
{
	public class LatestT
	{
		//{"date":"2018-11-19","rates":{"KRW":162.2742567644},"base":"CNY"}
		public DateTime date { get; set; }
		public RateDataT rates { get; set; }
		public string @base { get; set; }
	}
}
