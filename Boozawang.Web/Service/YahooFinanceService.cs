using Boozawang.Web.Cache;
using Boozawang.Web.Interface;
using Boozawang.Web.Models.UsaStock;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boozawang.Web.Service
{
    /// <summary>
    /// 주식정보 야후 파이낸스에서 조회
    /// </summary>
    public class YahooFinanceService : IUsaStockService
    {
        /// <summary>
        /// 미국 주식 캐시 - memory cache
        /// </summary>
        private static UsaStockCache cache = new UsaStockCache();

        /// <summary>
        /// 주요 정보 조회
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        public UsaStockKeyStats GetKeyStats(string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
                return null;

            // 캐시에서 가져오기
            var result = GetUsaStockCache(ticker);

            if(!HasData(result))
            {
                var htmlDoc = GetHtml(GetUrl(ticker));
                result = SetResultByDoc(ticker, htmlDoc);
                SetUsaStockCache(ticker, result);
            }

            return result;
        }

        /// <summary>
        /// doc로 결과 만들기
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="htmlDoc"></param>
        /// <returns></returns>
        private static UsaStockKeyStats SetResultByDoc(string ticker, HtmlDocument htmlDoc)
        {
            return new UsaStockKeyStats
            {
                Ticker = ticker,
                CompanyName = GetCompNameByDoc(htmlDoc),
                MarketCap = GetMarketCapByDoc(htmlDoc),
                LastPrice = GetPriceByDoc(htmlDoc),
                PerTTM = GetPerByDoc(htmlDoc),
                DivTTM = GetDivByDoc(htmlDoc)
            };
        }

        /// <summary>
        /// 캐시에서 가져오기
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        private UsaStockKeyStats GetUsaStockCache(string ticker)
        {
            string cacheResult = cache.GetCache(ticker);
            if (!string.IsNullOrEmpty(cacheResult))
                return Newtonsoft.Json.JsonConvert.DeserializeObject<UsaStockKeyStats>(cacheResult);
            else
                return null;
        }

        /// <summary>
        /// 캐시에 저장하기
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="val"></param>
        private void SetUsaStockCache(string ticker, UsaStockKeyStats val)
        {   
            cache.Set(ticker, Newtonsoft.Json.JsonConvert.SerializeObject(val));
        }

        /// <summary>
        /// 데이터 있나 검사
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool HasData(UsaStockKeyStats data)
        {
            if (!string.IsNullOrEmpty(data?.Ticker))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 티커로 url가져오기
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        private static string GetUrl(string ticker)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(YahooFinanceConst.PAGE);
            sb.Append(ticker.ToUpper());
            sb.Append("?p=");
            sb.Append(ticker.ToUpper());
            return sb.ToString();
        }

        /// <summary>
        /// htmlDoc에서 가격정보 get
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <returns></returns>
        private static string GetPriceByDoc(HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.SelectSingleNode(YahooFinanceConst.XPATH_PRICE).InnerText;
        }

        /// <summary>
        /// htmlDoc에서 회사명 get
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <returns></returns>
        private static string GetCompNameByDoc(HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.SelectSingleNode(YahooFinanceConst.XPATH_COMP_NAME).InnerText;
        }


        /// <summary>
        /// htmlDoc에서 PER 정보 get
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <returns></returns>
        private static string GetPerByDoc(HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.SelectSingleNode(YahooFinanceConst.XPATH_PER).InnerText;
        }

        /// <summary>
        /// htmlDoc에서 배당정보 get
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <returns></returns>
        private static string GetDivByDoc(HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.SelectSingleNode(YahooFinanceConst.XPATH_DIV).InnerText;
        }

        /// <summary>
        /// html 정보에서 시총정보 get
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <returns></returns>
        private static string GetMarketCapByDoc(HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.SelectSingleNode(YahooFinanceConst.XPATH_MARKET_CAP).InnerText;
        }

        /// <summary>
        /// html 가져오기
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private HtmlDocument GetHtml(string url)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(url);
        }

        /// <summary>
        /// html 가져오기 (비동기)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<HtmlDocument> GetHtmlAsync(string url)
        {
            HtmlWeb web = new HtmlWeb();
            return await web.LoadFromWebAsync(url);
        }
    }
}
