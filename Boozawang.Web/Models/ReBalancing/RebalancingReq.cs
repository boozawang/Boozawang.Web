using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.ReBalancing
{
    public class RebalancingReq
    {
        public List<StockItem> StockItem { get; set; }

        public decimal AdditionalMoney { get; set; }
    }
}
