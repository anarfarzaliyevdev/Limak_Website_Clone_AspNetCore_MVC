using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Limak.Models;
using Microsoft.AspNetCore.Identity;
using Limak.ViewModels;
using Limak.Abstractions;
using System.Net.Http;
using Newtonsoft.Json;

namespace Limak.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IBalanceRepository balanceRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IDeclarationRepository declarationRepository;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,IBalanceRepository balanceRepository
            ,IOrderRepository orderRepository,IDeclarationRepository declarationRepository)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.balanceRepository = balanceRepository;
            this.orderRepository = orderRepository;
            this.declarationRepository = declarationRepository;
        }

        public IActionResult Index()
        {
          
            return View();
        }
        public IActionResult Brands()
        {
            return View();
        }
        public IActionResult Calculator()
        {
            CalculatorViewModel model = new CalculatorViewModel();
            model = GetCurrency();
            return View(model);
        }
        private CalculatorViewModel GetCurrency()
        {
            CurrencyApiUSD_TRY currencyApiUSD_TRY = new CurrencyApiUSD_TRY();
            CurrencyApiAZN_TRY currencyApiAZN_TRY = new CurrencyApiAZN_TRY();
            CalculatorViewModel calculatorViewModel = new CalculatorViewModel();
            using (HttpClient client = new HttpClient())
            {
                //Currency Api Request
                var result = client.GetAsync("https://fcsapi.com/api-v2/forex/latest?symbol=TRY/USD,USD/TRY,USD/AZN,AZN/USD&access_key=WuYRtOVAGwtltbhuWtRpn469AzLpAEM6hNF713vq1URRbrsg8r").Result;


                var jsonString = result.Content.ReadAsStringAsync().Result;


                currencyApiUSD_TRY = JsonConvert.DeserializeObject<CurrencyApiUSD_TRY>(jsonString);
                foreach (var item in currencyApiUSD_TRY.response)
                {
                    if (item.symbol == @"USD/TRY")
                    {
                        calculatorViewModel.USD_TRY = Convert.ToDecimal(item.price);

                    }
                    else if (item.symbol == @"TRY/USD")
                    {
                        calculatorViewModel.TRY_USD = Convert.ToDecimal(item.price);
                    }
                    else if (item.symbol == @"USD/AZN")
                    {
                        calculatorViewModel.USD_AZN = Convert.ToDecimal(item.price);

                    }
                    else if (item.symbol == @"AZN/USD")
                    {
                        calculatorViewModel.AZN_USD = Convert.ToDecimal(item.price);
                    }
                }
                var result2 = client.GetAsync("https://fcsapi.com/api-v2/forex/converter?symbol=AZN/TRY&access_key=WuYRtOVAGwtltbhuWtRpn469AzLpAEM6hNF713vq1URRbrsg8r").Result;


                var jsonString2 = result2.Content.ReadAsStringAsync().Result;
                currencyApiAZN_TRY = JsonConvert.DeserializeObject<CurrencyApiAZN_TRY>(jsonString2);
                calculatorViewModel.AZN_TRY = decimal.Round(1 / (Convert.ToDecimal(currencyApiAZN_TRY.response.price_1x_AZN)), 4);
                calculatorViewModel.TRY_AZN = Convert.ToDecimal(currencyApiAZN_TRY.response.price_1x_AZN);

            }

            return calculatorViewModel;
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Countries()
        {
            return View();
        }
        public IActionResult Rules()
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
