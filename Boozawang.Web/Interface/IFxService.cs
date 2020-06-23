using Boozawang.Web.Models.Fx.Entities;
using Boozawang.Web.Models.Fx.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Interface
{
    /// <summary>
    /// 외환 관련 서비스 인터페이스
    /// </summary>
    public interface IFxService
    {
        /// <summary>
        /// 기준 통화에 대한 모든 환율 정보 가져오기(ex : KRW)
        /// </summary>
        /// <param name="targetType">기준 통화(분자)</param>
        /// <returns></returns>
        List<FxItem> GetAll(CurrencyTypes targetType);

        /// <summary>
        /// 기준 통화와 대상 통화의 환율 정보 가져오기
        /// </summary>
        /// <param name="targetType">기준통화(분자)</param>
        /// <param name="baseType">대상통화(분모)</param>
        /// <returns></returns>
        FxItem GetByType(CurrencyTypes targetType, CurrencyTypes baseType);
    }
}
