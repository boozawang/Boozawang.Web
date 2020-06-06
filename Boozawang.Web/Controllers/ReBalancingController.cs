using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Boozawang.Web.Models.ReBalancing;
using Boozawang.Web.Service.ReBalancing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Boozawang.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReBalancingController : ControllerBase
    {
        [HttpPost]
        public RebalancingResponse Post(List<StockItem> stockItem, decimal additionalMoney)
        {
            return ReblancingService.Rebalancing(stockItem, additionalMoney);
        }

    }
}
