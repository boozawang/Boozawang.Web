using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.ReBalancing
{
    public enum RestOptionTypes
    {
        /// <summary>
        /// 알수없음
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 나머지가 가장 적게 리밸런싱
        /// </summary>
        MinRest = 1,

        /// <summary>
        /// 나머지 그냥 냅둠
        /// </summary>
        MaxRest = 2
    }
}
