using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.Email
{
    /// <summary>
    /// smtp 서버 종류
    /// </summary>
    public enum SmtpEnumTypes
    {
        /// <summary>
        /// 알수 없음
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// googld gmail
        /// </summary>
        GMail = 1
    }
}
