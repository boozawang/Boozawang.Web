using Boozawang.Web.Models.ReBalancing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Service.ReBalancing
{
    public class ReblancingService
    {
		public static RebalancingRes Rebalancing(List<StockItemReq> before, decimal additionalMoney)
		{
			#region 변수선언

			RebalancingRes result = new RebalancingRes();
			List<StockItemReq> after = new List<StockItemReq>();
			#endregion

			#region 파라메터 유효성 검사
			if (!(before?.Any() ?? false))
				return result;

			#endregion

			#region 행동 정의

			// 리밸런싱			
			after = RebalancingStockItem(before, additionalMoney);

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
		private static List<StockItemReq> RebalancingStockItem(List<StockItemReq> before, decimal additionalMoney)
		{
			#region 변수 선언

			List<StockItemReq> result = new List<StockItemReq>();
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
			result = SetByRest(result, rest);

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
		private static decimal SumPriceQty(List<StockItemReq> before)
		{
			decimal result = 0;

			if (before?.Any() ?? false)
			{
				foreach (StockItemReq entity in before)
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

		private static decimal SumWeight(List<StockItemReq> before)
		{
			decimal result = 0;


			if (before?.Any() ?? false)
			{
				decimal overflowAverage = Decimal.MaxValue / before.Count;
				foreach (StockItemReq entity in before)
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
		private static List<StockItemReq> SetByRest(List<StockItemReq> before, decimal rest)
		{
			List<StockItemReq> result = new List<StockItemReq>();

			// 큰 가격순 정렬
			if (before?.Any() ?? false)
			{
				before = before.OrderByDescending(x => x.CurrentPrice).ToList();

				foreach (StockItemReq entity in before)
				{
					StockItemReq one = new StockItemReq();
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

			return result;
		}

		/// <summary>
		/// 추가금을 합한 총액을 원하는 비중에 따른 분배
		/// </summary>
		/// <param name="before"></param>
		/// <param name="additional"></param>
		/// <returns></returns>
		private static List<StockItemReq> SetByWeight(List<StockItemReq> before, decimal additional)
		{
			List<StockItemReq> result = new List<StockItemReq>();
			decimal originalSum = 0;
			decimal weightSum = 0;

			// 총 합 구함 + additionalMoney
			if (before?.Any() ?? false)
			{
				originalSum = SumPriceQty(before) + additional;
				weightSum = SumWeight(before);

				// 나머지 내림 분배
				foreach (StockItemReq entity in before)
				{
					if (entity != null)
					{
						StockItemReq one = new StockItemReq();
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
		private static List<StockItemChange> MakeChangeSummary(List<StockItemReq> before, List<StockItemReq> after)
        {
			List<StockItemChange> result = new List<StockItemChange>();

			foreach (StockItemReq entity in after)
            {
				var selectedBefore = before.Find(x => x.Name == entity.Name);

				result.Add(
					new StockItemChange
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
