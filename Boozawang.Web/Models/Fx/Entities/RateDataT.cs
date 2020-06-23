using Boozawang.Web.Models.Fx.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.Fx.Entities
{
    public class RateDataT
    {
		public decimal KRW { get; set; }
		public decimal USD { get; set; }
		public decimal EUR { get; set; }
		public decimal CNY { get; set; }
		public decimal JPY { get; set; }
		public decimal GBP { get; set; }
		public decimal CAD { get; set; }
		public decimal AUD { get; set; }

		public decimal GetByType(CurrencyTypes type)
        {
			switch(type)
            {
				case CurrencyTypes.AUD:
					return AUD;
				case CurrencyTypes.CAD:
					return CAD;
				case CurrencyTypes.CNY:
					return CNY;
				case CurrencyTypes.EUR:
					return EUR;
				case CurrencyTypes.GBP:
					return GBP;
				case CurrencyTypes.JPY:
					return JPY;
				case CurrencyTypes.KRW:
					return KRW;
				case CurrencyTypes.USD:
					return USD;
			}

			return 0;
        }
	}
}
