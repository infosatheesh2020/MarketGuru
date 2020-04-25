using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MarketGuru.Models;
using MarketGuru.BusinessLogic;
using Microsoft.Azure.Cosmos;

namespace MarketGuru.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICosmosDbService _cosmosDbService;

        public HomeController(ILogger<HomeController> logger, ICosmosDbService cosmosDbService)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService;
        }


        [HttpPost]
        public async Task<IActionResult> IndexAsync(string scrip)
        {
            if (scrip != null)
            {
                Quote result = new Quote();
                try
                {
                    StockModel stockModel = new StockModel();
                    Task<Quote> myQuote = stockModel.GetQuote(scrip);
                    result = myQuote.Result;

                    result.Color = Utilities.CalculateColor(result.Currentprice, result.PreviousClose);
                    result.Recommendation = Utilities.BuyOrSell(result.Volume, result.Avg10DVolume, result.Currentprice, result.FiftyDayAverage);
                    result.ReadableMktTime = Utilities.HumanReadableTime(result.MktTime);

                    result.Id = Guid.NewGuid().ToString();
                    await _cosmosDbService.AddItemAsync(result);
                    _logger.LogInformation($"Successfully processed data : {result} at {DateTime.Now.ToString()}");
                }
                catch(CosmosException ex)
                {
                    //Log the error to log analytics
                    result.ErrorMessage = "Not able to access Cosmos DB";
                    _logger.LogError($"CosmosException at at {DateTime.Now.ToString()} : {result.ErrorMessage} : {ex.StackTrace}");
                }
                catch (Exception ex)
                {
                    //Log the error to log analytics
                    result.ErrorMessage = "Not able to access Quote backend API";
                    _logger.LogError($"Exception at at {DateTime.Now.ToString()} : {result.ErrorMessage} : {ex.StackTrace}");
                }
                return View(result);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
