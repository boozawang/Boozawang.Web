using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boozawang.Web.Interface;
using Boozawang.Web.Models.Email;
using MailKit.Net.Smtp;
using MimeKit;

namespace Boozawang.Web.Service
{
    /// <summary>
    /// mail kit 를 이용한 email 서비스
    /// </summary>
    public class MailKitService : IEmailService
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
        public void Send(SmtpEnumTypes smtpType, string userName, string userPass, string fromEmail, string toEmail, string subjectEmail, string bodyEmail)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress fromAddress = new MailboxAddress("Seungbin`s Home", fromEmail);
            message.From.Add(fromAddress);

            MailboxAddress toAddress = new MailboxAddress("결과보고", toEmail);
            message.To.Add(toAddress);

            message.Subject = subjectEmail;

            BodyBuilder bodyBuilder = new BodyBuilder();            
            bodyBuilder.TextBody = bodyEmail;
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect(GetSmtpAddressByEnumType(smtpType), GetSmtpPorttByEnumType(smtpType), true);
            client.Authenticate(userName, userPass);

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }

        /// <summary>
        /// smtp enum Type으로 smtp addr 가져오기
        /// </summary>
        /// <param name="smtpType"></param>
        /// <returns></returns>
        private string GetSmtpAddressByEnumType(SmtpEnumTypes smtpType)
        {
            switch(smtpType)
            {
                case SmtpEnumTypes.Unknown:
                case SmtpEnumTypes.GMail:
                    return SmtpConsts.SMTP_GMAIL_ADDR;
            }

            return string.Empty;
        }

        /// <summary>
        /// smtp enum Type으로 smtp port 가져오기
        /// </summary>
        /// <param name="smtpType"></param>
        /// <returns></returns>
        private int GetSmtpPorttByEnumType(SmtpEnumTypes smtpType)
        {
            switch (smtpType)
            {
                case SmtpEnumTypes.Unknown:
                case SmtpEnumTypes.GMail:
                    return SmtpConsts.SMTP_GMAIL_PORT;
            }

            return 0;
        }
    }
}
