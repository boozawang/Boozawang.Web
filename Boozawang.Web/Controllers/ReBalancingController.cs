﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Boozawang.Web.Models.ReBalancing;
using Boozawang.Web.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Boozawang.Web.Controllers
{
    [Route("api/ReBalancing")]
    [ApiController]
    public class ReBalancingController : ControllerBase
    {
        /// <summary>
        /// 리밸런싱
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public RebalancingRes Post(RebalancingReq req)
        {
            return ReblancingService.Rebalancing(req.StockItem, req.AdditionalMoney, req.RestOption);
        }

    }
}
