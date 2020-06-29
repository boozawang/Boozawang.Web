using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Models.Email
{
    /// <summary>
    /// 이메일 보내기 요청
    /// </summary>
    public class SendEmailReq
    {
        /// <summary>
        /// 이용할 SMTP 서버 타입
        /// </summary>
        public SmtpEnumTypes SmtpType { get; set; }

        /// <summary>
        /// 사용자 명 - 로그인
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 사용자 비번 - 로그인
        /// </summary>
        public string UserPass { get; set; }

        /// <summary>
        /// 보내는이 이메일 주소
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// 받는이 이메일 주소
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// 이메일 제목
        /// </summary>
        public string SubjectEmail { get; set; }

        /// <summary>
        /// 이메일 내용
        /// </summary>
        public string BodyEmail { get; set; }
    }
}
