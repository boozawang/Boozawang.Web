using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boozawang.Web.Interface;
using Boozawang.Web.Models.Fx.Entities;
using Boozawang.Web.Models.Fx.Enums;
using Boozawang.Web.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Boozawang.Web.Controllers
{
    /// <summary>
    /// 외환 정보 
    /// </summary>
    [Route("api/Fx")]
    [ApiController]
    public class FxController : ControllerBase
    {
        IFxService service = new FxERApiService();

        /// <summary>
        /// 전체 외환 조회 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<FxItem> Get()
        {
            return service.GetAll(CurrencyTypes.KRW);
        }

        /// <summary>
        /// 통화별 가격 조회
        /// </summary>
        /// <param name="currencyType">1:KRW, 2:USD,  3:EUR, 4:CNY, 5:JPY, 6:GBP, 7:CAD, 8:AUD</param>
        /// <returns></returns>
        [HttpGet("{currencyType}")]
        public FxItem Get(CurrencyTypes currencyType)
        {
            return service.GetByType(CurrencyTypes.KRW, currencyType);
        }
    }
}
