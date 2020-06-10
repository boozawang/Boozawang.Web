using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.Fx.Consts
{
    /// <summary>
    /// api.exchangeratesapi.io 관련 consts
    /// </summary>
    public class ExchangeRateApiConsts
    {
        /// <summary>
        /// url
        /// </summary>
        //https://api.exchangeratesapi.io/latest?symbols=KRW&base=CNY
        public const string CURRENCY_API_URL = "https://api.exchangeratesapi.io/latest?";  
    }
}
