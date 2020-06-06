using Boozawang.Web.Models.ReBalancing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boozawang.Web.Service.ReBalancing
{
    public class ReblancingService
    {
		public static RebalancingResponse Rebalancing(List<StockItem> before, decimal additionalMoney)
		{
			#region 변수선언

			RebalancingResponse result = new RebalancingResponse();
			List<StockItem> after = new List<StockItem>();

			#endregion

			#region 파라메터 유효성 검사
			if (!(before?.Any() ?? false))
				return result;

			#endregion

			#region 행동 정의

			// 리밸런싱			
			result.StockData = RebalancingStockItem(before, additionalMoney);

			// 잔액 계산
			result.ResultAmount = SumPriceQty(before) + additionalMoney - SumPriceQty(after);

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
		private static List<StockItem> RebalancingStockItem(List<StockItem> before, decimal additionalMoney)
		{
			#region 변수 선언

			List<StockItem> result = new List<StockItem>();
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
		private static decimal SumPriceQty(List<StockItem> before)
		{
			decimal result = 0;

			if (before?.Any() ?? false)
			{
				foreach (StockItem entity in before)
				{
					if (entity != null)
					{
						if (entity.Price < Decimal.MaxValue && entity.Qty < int.MaxValue)
							result += entity.Price * entity.Qty;
					}
				}
			}

			return result;
		}

		private static decimal SumWeight(List<StockItem> before)
		{
			decimal result = 0;


			if (before?.Any() ?? false)
			{
				decimal overflowAverage = Decimal.MaxValue / before.Count;
				foreach (StockItem entity in before)
				{
					if (entity != null)
					{
						if (entity.Weight < overflowAverage)
							result += entity.Weight;
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
		private static List<StockItem> SetByRest(List<StockItem> before, decimal rest)
		{
			List<StockItem> result = new List<StockItem>();

			// 큰 가격순 정렬
			if (before?.Any() ?? false)
			{
				before = before.OrderByDescending(x => x.Price).ToList();

				foreach (StockItem entity in before)
				{
					StockItem one = new StockItem();
					one.Name = entity.Name;
					one.Price = entity.Price;
					one.Weight = entity.Weight;

					if (entity.Price < rest)
					{
						int plusQty = (int)(rest / entity.Price);
						one.Qty = entity.Qty + plusQty;
						rest = rest - (entity.Price * plusQty);
					}
					else
						one.Qty = entity.Qty;

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
		private static List<StockItem> SetByWeight(List<StockItem> before, decimal additional)
		{
			List<StockItem> result = new List<StockItem>();
			decimal originalSum = 0;
			decimal weightSum = 0;

			// 총 합 구함 + additionalMoney
			if (before?.Any() ?? false)
			{
				originalSum = SumPriceQty(before) + additional;
				weightSum = SumWeight(before);

				// 나머지 내림 분배
				foreach (StockItem entity in before)
				{
					if (entity != null)
					{
						StockItem one = new StockItem();
						one.Name = entity.Name;
						one.Price = entity.Price;
						one.Weight = entity.Weight;
						if (entity.Weight != 0 && entity.Price != 0)
						{
							one.Qty = (int)Math.Floor((originalSum * entity.Weight / weightSum) / one.Price);
							result.Add(one);
						}
					}
				}
			}

			return result;
		}
	}
}
