using Boozawang.Web.Models.UsaStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Interface
{
    /// <summary>
    /// 미국 주식 조회 서비스
    /// </summary>
    public interface IUsaStockService
    {
        /// <summary>
        /// 미국 주식 주요정보 조회
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        UsaStockKeyStats GetKeyStats(string ticker);
    }
}
