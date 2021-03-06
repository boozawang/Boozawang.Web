﻿using Boozawang.Web.Models.ReBalancing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;

namespace Boozawang.Web.Service
{
    public class ReblancingService
    {
		public static RebalancingRes Rebalancing(List<ReblancingItemReq> before, decimal additionalMoney, RestOptionTypes restOptionType)
		{
			#region 변수선언

			RebalancingRes result = new RebalancingRes();
			List<ReblancingItemReq> after = new List<ReblancingItemReq>();
			#endregion

			#region 파라메터 유효성 검사
			if (!(before?.Any() ?? false))
				return result;

			#endregion

			#region 행동 정의

			// 리밸런싱			
			after = RebalancingStockItem(before, additionalMoney, restOptionType);

			// 잔액 계산
			result.RestAmount = SumPriceQty(before) + additionalMoney - SumPriceQty(after);

			// 변경점 계산
			result.ChangeSummary = MakeChangeSummary(before, after);

			#endregion

			#region 결과값 생성

			return result;

			#endregion

		}




		/// <summary>
		/// 리밸런싱
		/// </summary>
		/// <param name="before"></param>
		/// <param name="additionalMoney"></param>
		/// <returns></returns>
		private static List<ReblancingItemReq> RebalancingStockItem(List<ReblancingItemReq> before, decimal additionalMoney, RestOptionTypes restOptionType)
		{
			#region 변수 선언

			List<ReblancingItemReq> result = new List<ReblancingItemReq>();
			decimal rest = 0;
			#endregion

			#region 파라메터 유효성 검사

			if (!(before?.Any() ?? false))
				return result;

			#endregion

			#region 행동 정의

			// 비중 분배
			result = SetByWeight(before, additionalMoney);

			// 나머지 계산
			rest = SumPriceQty(before) + additionalMoney - SumPriceQty(result);

			// 나머지 분배
			result = SetByRest(result, rest, restOptionType);

			#endregion

			#region 결과 값 생성

			return result;

			#endregion

		}

		/// <summary>
		/// 수량 가격 곱해줌
		/// </summary>
		/// <param name="before"></param>
		/// <returns></returns>
		private static decimal SumPriceQty(List<ReblancingItemReq> before)
		{
			decimal result = 0;

			if (before?.Any() ?? false)
			{
				foreach (ReblancingItemReq entity in before)
				{
					if (entity != null)
					{
						if (entity.CurrentPrice < Decimal.MaxValue && entity.CurrentQty < int.MaxValue)
							result += entity.CurrentPrice * entity.CurrentQty;
					}
				}
			}

			return result;
		}

		private static decimal SumWeight(List<ReblancingItemReq> before)
		{
			decimal result = 0;


			if (before?.Any() ?? false)
			{
				decimal overflowAverage = Decimal.MaxValue / before.Count;
				foreach (ReblancingItemReq entity in before)
				{
					if (entity != null)
					{
						if (entity.TargetWeight < overflowAverage)
							result += entity.TargetWeight;
					}
				}
			}

			return result;
		}

		/// <summary>
		/// 나머지 금액을 내림차순으로 최대한 분배
		/// </summary>
		/// <param name="before"></param>
		/// <param name="rest"></param>
		/// <returns></returns>
		private static List<ReblancingItemReq> SetByRest(List<ReblancingItemReq> before, decimal rest, RestOptionTypes restOptionType)
		{
			List<ReblancingItemReq> result = new List<ReblancingItemReq>();

			switch(restOptionType)
            {
				case RestOptionTypes.Unknown:
				case RestOptionTypes.MinRest:
                {
					// 큰 가격순 정렬
					if (before?.Any() ?? false)
					{
						before = before.OrderByDescending(x => x.CurrentPrice).ToList();

						foreach (ReblancingItemReq entity in before)
						{
							ReblancingItemReq one = new ReblancingItemReq();
							one.Name = entity.Name;
							one.CurrentPrice = entity.CurrentPrice;
							one.TargetWeight = entity.TargetWeight;

							if (entity.CurrentPrice < rest)
							{
								int plusQty = (int)(rest / entity.CurrentPrice);
								one.CurrentQty = entity.CurrentQty + plusQty;
								rest = rest - (entity.CurrentPrice * plusQty);
							}
							else
								one.CurrentQty = entity.CurrentQty;

							result.Add(one);
						}
					}
				}
					break;
            }			

			return result;
		}

		/// <summary>
		/// 추가금을 합한 총액을 원하는 비중에 따른 분배
		/// </summary>
		/// <param name="before"></param>
		/// <param name="additional"></param>
		/// <returns></returns>
		private static List<ReblancingItemReq> SetByWeight(List<ReblancingItemReq> before, decimal additional)
		{
			List<ReblancingItemReq> result = new List<ReblancingItemReq>();
			decimal originalSum = 0;
			decimal weightSum = 0;

			// 총 합 구함 + additionalMoney
			if (before?.Any() ?? false)
			{
				originalSum = SumPriceQty(before) + additional;
				weightSum = SumWeight(before);

				// 나머지 내림 분배
				foreach (ReblancingItemReq entity in before)
				{
					if (entity != null)
					{
						ReblancingItemReq one = new ReblancingItemReq();
						one.Name = entity.Name;
						one.CurrentPrice = entity.CurrentPrice;
						one.TargetWeight = entity.TargetWeight;
						if (entity.TargetWeight != 0 && entity.CurrentPrice != 0)
						{
							one.CurrentQty = (int)Math.Floor((originalSum * entity.TargetWeight / weightSum) / one.CurrentPrice);
							result.Add(one);
						}
					}
				}
			}

			return result;
		}

		/// <summary>
		/// 리밸런싱 전후 변경 요약
		/// </summary>
		/// <param name="before"></param>
		/// <param name="after"></param>
		/// <returns></returns>
		private static List<ReblancingItemChange> MakeChangeSummary(List<ReblancingItemReq> before, List<ReblancingItemReq> after)
        {
			List<ReblancingItemChange> result = new List<ReblancingItemChange>();

			foreach (ReblancingItemReq entity in after)
            {
				var selectedBefore = before.Find(x => x.Name == entity.Name);

				result.Add(
					new ReblancingItemChange
					{
						Name = entity.Name,
						BasePrice = entity.CurrentPrice,
						ChangeQty = entity.CurrentQty - selectedBefore.CurrentQty
					}
				);
			}

			return result;
        }
	}
}
