using Boozawang.Web.Models.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Interface
{
    /// <summary>
    /// 금속 시세 조회 인터페이스
    /// </summary>
    public interface IMaterialService
    {
        /// <summary>
        /// 모든 금속 정보를 가져옴
        /// </summary>
        /// <returns></returns>
        List<MaterialItem> GetAll();

        /// <summary>
        /// 해당 금속 정보를 가져옴
        /// </summary>
        /// <param name="materailType"></param>
        /// <returns></returns>
        MaterialItem GetByType(MaterialTypes materailType);
    }
}
