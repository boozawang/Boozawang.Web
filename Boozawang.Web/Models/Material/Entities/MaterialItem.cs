using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.Material
{
    /// <summary>
    /// 금속 가격
    /// </summary>
    public class MaterialItem
    {
        /// <summary>
        /// 종류
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 가격
        /// </summary>
        public decimal Price { get; set; }

    }
}
