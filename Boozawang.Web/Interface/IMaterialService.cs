using Boozawang.Web.Models.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Interface
{
    public interface IMaterialService
    {
        /// <summary>
        /// 모든 금속 정보를 가져옴
        /// </summary>
        /// <param name="useCache"></param>
        /// <param name="updateCache"></param>
        /// <returns></returns>
        List<MaterialItem> GetAll(bool useCache, bool updateCache);

        /// <summary>
        /// 해당 금속 정보를 가져옴
        /// </summary>
        /// <param name="materailType"></param>
        /// <param name="useCache"></param>
        /// <param name="updateCache"></param>
        /// <returns></returns>
        MaterialItem GetByType(MaterialTypes materailType, bool useCache, bool updateCache);
    }
}
