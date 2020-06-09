using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Boozawang.Web.Service;
using Boozawang.Web.Models.Material;
using Boozawang.Web.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Boozawang.Web.Controllers
{
    [Route("api/Material")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        IMaterialService service = new MaterialApmexService();

        /// <summary>
        /// 전체 조회(달러/트로이온스)
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public List<MaterialItem> Get()
        {
            return service.GetAll(true, true);
        }

        /// <summary>
        /// 각각 조회(달러/트로이온스)
        /// </summary>
        /// <param name="materialType">	0 : All, 1 : Gold, 2 : Silver, 3 : Platinum, 4 : Palladium</param>
        /// <returns></returns>
        [HttpGet("{materialType}")]
        public MaterialItem Get(MaterialTypes materialType)
        {
            return service.GetByType(materialType, true, true);
        }
    }
}
