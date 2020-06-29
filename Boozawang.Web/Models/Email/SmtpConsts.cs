using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.Email
{
    /// <summary>
    /// smtp 관련 const
    /// </summary>
    public static class SmtpConsts
    {
        /// <summary>
        /// SMTP 주소
        /// </summary>
        public const string SMTP_GMAIL_ADDR = "smtp.gmail.com";

        /// <summary>
        /// SMTP 포트
        /// </summary>
        public const int SMTP_GMAIL_PORT = 465;
    }
}
