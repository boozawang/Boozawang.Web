using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.Material
{

    /// <summary>
    /// 메터리얼 정보
    /// </summary>
    public class MaterialRes
    {
        /// <summary>
        /// 기준 시각
        /// </summary>
        public DateTime BaseTime { get; set; }

        /// <summary>
        /// 기준 통화
        /// </summary>
        public decimal BaseCurrency { get; set; }

        /// <summary>
        /// 가격 정보
        /// </summary>
        public List<MaterialItem> MaterialItems { get; set; }
    }
}
