using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boozawang.Web.Interface;
using Boozawang.Web.Models.Email;
using Boozawang.Web.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Boozawang.Web.Controllers
{
    /// <summary>
    /// 이메일 관련 api
    /// </summary>
    [Route("api/mail")]
    [ApiController]
    public class MailController : ControllerBase
    {
        /// <summary>
        /// 이메일 서비스
        /// </summary>
        IEmailService emailService = new MailKitService();

        /// <summary>
        /// 메일 보내기
        /// </summary>
        /// <param name="req"></param>
        [HttpPost]
        public void Post([FromBody] SendEmailReq req)
        {
            emailService.Send(req.SmtpType, req.UserName, req.UserPass, req.FromEmail, req.ToEmail, req.SubjectEmail, req.BodyEmail);
        }
    }
}
