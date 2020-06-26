using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boozawang.Web.Interface;
using Boozawang.Web.Models.UsaStock;
using Boozawang.Web.Service;
using Microsoft.AspNetCore.Mvc;


namespace Boozawang.Web.Controllers
{
    /// <summary>
    /// 미국 주식 정보 조회
    /// </summary>
    [Route("api/USA")]
    [ApiController]
    public class UsaStockController : ControllerBase
    {
        IUsaStockService stockService = new YahooFinanceService();

        /// <summary>
        /// 티커로 정보 조회
        /// </summary>
        /// <param name="ticker">개별주식 티커. ETF 안된다</param>
        /// <returns></returns>
        [HttpGet("{ticker}")]
        public UsaStockKeyStats Get(string ticker)
        {
            return stockService.GetKeyStats(ticker);
        }

        /// <summary>
        /// post for slack
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        [HttpPost]
        public UsaStockKeyStats Post(string ticker)
        {
            return stockService.GetKeyStats(ticker);
        }
    }
}
