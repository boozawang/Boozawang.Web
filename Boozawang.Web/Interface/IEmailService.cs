using Boozawang.Web.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Interface
{
    /// <summary>
    /// 이메일 관련 서비스
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// 이메일 보내기
        /// </summary>
        /// <param name="smtpType"></param>
        /// <param name="userName"></param>
        /// <param name="userPass"></param>
        /// <param name="fromEmail"></param>
        /// <param name="toEmail"></param>
        /// <param name="subjectEmail"></param>
        /// <param name="bodyEmail"></param>
        void Send(SmtpEnumTypes smtpType, string userName, string userPass, string fromEmail, string toEmail, string subjectEmail, string bodyEmail);
    }
}
