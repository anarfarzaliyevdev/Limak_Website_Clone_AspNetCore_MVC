using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Limak.Abstractions;
using Limak.Models;
using Limak.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;

namespace Limak.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IOrderRepository orderRepository;
        private readonly IDeclarationRepository declarationRepository;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly IOperationRepository operationRepository;
        private readonly IBalanceRepository balanceRepository;
        private readonly IOrderStatusRepository orderStatusRepository;
        private readonly IDeclarationStatusRepository declarationStatusRepository;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IOrderRepository orderRepository
            , IDeclarationRepository declarationRepository, IMapper mapper,
            IUserRepository userRepository, IOperationRepository operationRepository,
            IBalanceRepository balanceRepository, IOrderStatusRepository orderStatusRepository
            , IDeclarationStatusRepository declarationStatusRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.orderRepository = orderRepository;
            this.declarationRepository = declarationRepository;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.operationRepository = operationRepository;
            this.balanceRepository = balanceRepository;
            this.orderStatusRepository = orderStatusRepository;
            this.declarationStatusRepository = declarationStatusRepository;
        }
        [HttpGet]
        public async Task<IActionResult> PanelPage()
        {

            var userWithBalance = await userRepository.GetUserWithBalance(User.Identity.Name);
            //Create view model
            PanelPageViewModel model = new PanelPageViewModel();
            model.CustomerId = userWithBalance.CustomerId;
            model.Balance = userWithBalance.Balance;
            //Get operations
            var operations = await operationRepository.GetAll();
            model.Operations = operations.Where(o => o.UserId == userWithBalance.Id && o.CurrencyType == "AZN").ToList();
            return View(model);
        }
        public IActionResult ForeignAddresses()
        {
            ViewBag.ActivePage = "ForeignAddresses";
            return View();
        }


        public async Task<IActionResult> Orders()
        {
            ViewBag.ActivePage = "Orders";
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var userOrders = await userRepository.GetUserOrders(user.UserName);
            OrdersViewModel model = new OrdersViewModel();
            model.Orders = userOrders.Orders;
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> GetOrderStatuses(int orderId)
        {
            OrderStatus orderStatus = await orderStatusRepository.GetByOrderId(orderId);


            if (orderStatus != null)
            {

                return Json(new
                {
                    success = true,
                    ordered = orderStatus.Ordered,
                    orderedDate = orderStatus.OrderedDate.ToString("MM/dd/yyyy"),
                    abroadWarehouse = orderStatus.AbroadWarehouse,
                    abroadWarehouseDate = orderStatus.AbroadWarehouseDate.ToString("MM/dd/yyyy"),
                    onWay = orderStatus.OnWay,
                    onWayDate = orderStatus.OnWayDate.ToString("MM/dd/yyyy"),
                    customsControl = orderStatus.CustomsControl,
                    customsControlDate = orderStatus.CustomsControlDate.ToString("MM/dd/yyyy"),
                    bakuWarehouse = orderStatus.BakuWarehouse,
                    bakuWarehouseDate = orderStatus.BakuWarehouseDate.ToString("MM/dd/yyyy"),
                    courier = orderStatus.Courier,
                    courierDate = orderStatus.CourierDate.ToString("MM/dd/yyyy"),
                    Return = orderStatus.Return,
                    returnDate = orderStatus.ReturnDate.ToString("MM/dd/yyyy"),
                    completed = orderStatus.Completed,
                    completedDate = orderStatus.CompletedDate.ToString("MM/dd/yyyy")
                });
            }
            return Json(new { success = false });
        }
        [HttpGet]
        public async Task<IActionResult> GetDeclarationStatuses(int declarationId)
        {
            DeclarationStatus declarationStatus = await declarationStatusRepository.GetByDeclarationId(declarationId);


            if (declarationStatus != null)
            {

                return Json(new
                {
                    success = true,
                    ordered = declarationStatus.Ordered,
                    orderedDate = declarationStatus.OrderedDate.ToString("MM/dd/yyyy"),
                    abroadWarehouse = declarationStatus.AbroadWarehouse,
                    abroadWarehouseDate = declarationStatus.AbroadWarehouseDate.ToString("MM/dd/yyyy"),
                    onWay = declarationStatus.OnWay,
                    onWayDate = declarationStatus.OnWayDate.ToString("MM/dd/yyyy"),
                    customsControl = declarationStatus.CustomsControl,
                    customsControlDate = declarationStatus.CustomsControlDate.ToString("MM/dd/yyyy"),
                    bakuWarehouse = declarationStatus.BakuWarehouse,
                    bakuWarehouseDate = declarationStatus.BakuWarehouseDate.ToString("MM/dd/yyyy"),
                    courier = declarationStatus.Courier,
                    courierDate = declarationStatus.CourierDate.ToString("MM/dd/yyyy"),
                    Return = declarationStatus.Return,
                    returnDate = declarationStatus.ReturnDate.ToString("MM/dd/yyyy"),
                    completed = declarationStatus.Completed,
                    completedDate = declarationStatus.CompletedDate.ToString("MM/dd/yyyy")
                });
            }
            return Json(new { success = false });
        }
        public async Task<IActionResult> Packages()
        {
            ViewBag.ActivePage = "Packages";
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var userPackages = await userRepository.GetUserpackages(user.UserName);
            PackagesViewModel model = new PackagesViewModel();
            model.Declarations = userPackages.Declarations;
            return View(model);
        }

        public async Task<IActionResult> GetPackage(int id)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var userPackages = await userRepository.GetUserpackages(user.UserName);
            var package = userPackages.Declarations.Find(x => x.Id == id);
            if (package != null)
            {
                return Json(new
                {
                    success = true,
                    storename = package.StoreName,
                    price = package.Price,
                    productType = package.PackageProductType,
                    trackid = package.TrackId,
                    productCount = package.PackageProductNumber,
                    comment = package.PackageNote,
                    deliveryOffice = package.DeliveryOffice,
                    packageid = package.Id
                });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public async Task<ActionResult> UpdatePackage(PackageUpdateViewModel model)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var userPackages = await userRepository.GetUserpackages(user.UserName);
            var package = userPackages.Declarations.Find(x => x.Id == model.Id);
            mapper.Map(model, package);

            var result = await declarationRepository.Edit(package);
            if (result)
            {
                return Json(new { success = true });

            }
            return Json(new { success = false });

        }

        public async Task<ActionResult> DeletePackage(int id)
        {
            var result = await declarationRepository.Delete(id);
            if (result)
            {
                return Json(new { success = true });

            }
            return Json(new { success = false });

        }

        [HttpPost]
        public async Task<ActionResult> PayPackageCargo(List<int> packageIds, decimal totalCargoAmount)
        {

            //Get user with balance
            var userWithBalance = await userRepository.GetUserWithBalance(User.Identity.Name);
            //Check user balance
            if (userWithBalance.Balance.AZN >= totalCargoAmount)
            {
                userWithBalance.Balance.AZN -= totalCargoAmount;

                foreach (var id in packageIds)
                {
                    var declaration = await declarationRepository.GetById(id);
                    if (declaration != null)
                    {
                        declaration.IsCargoPaid = true;
                        await declarationRepository.Edit(declaration);
                    }
                }
                //Create operation
                Operation operation = new Operation()
                {
                    Type = "Məxaric",
                    Amount = totalCargoAmount,
                    Date = DateTime.Now,
                    UserId = userWithBalance.Id,
                    CurrencyType = "AZN"
                };
                var createResult = await operationRepository.Create(operation);
            }
            else
            {
                return Json(new { success = false, message = "Zehmet olmasa balansinizi artirin" });
            }
            //Update user balance
            var result = await balanceRepository.Edit(userWithBalance.Balance);
            if (result)
            {
                return Json(new { success = true });

            }
            return Json(new { success = false, message = "Odenisde xeta" });
        }
        public async Task<IActionResult> AznBalance()
        {
            ViewBag.ActivePage = "AznBalance";
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var userWithBalance = await userRepository.GetUserWithBalance(user.UserName);
            //Get user operations
            var operations = await operationRepository.GetAll();
            //Create view model
            AznBalanceViewModel model = new AznBalanceViewModel();
            model.Balance = userWithBalance.Balance;
            model.Operations = operations.Where(o => o.UserId == user.Id && o.CurrencyType == "AZN").ToList();
            return View(model);
        }

        public async Task<ActionResult> IncreaseAznBalance(decimal amount)
        {

            //Get user with balance
            var userWithBalance = await userRepository.GetUserWithBalance(User.Identity.Name);
            //Increase balance
            userWithBalance.Balance.AZN += amount;
            //Create operation
            Operation operation = new Operation()
            {
                Type = "Mədaxil",
                Amount = amount,
                Date = DateTime.Now,
                User = userWithBalance,
                UserId = userWithBalance.Id,
                CurrencyType = "AZN"
            };
            var createResult = await operationRepository.Create(operation);

            //Update user balance
            var result = await userManager.UpdateAsync(userWithBalance);
            if (result.Succeeded)
            {
                return Json(new { success = true, azn_balance = $"{userWithBalance.Balance.AZN}", date = $"{operation.Date}", amount = $"{amount}" });

            }
            return Json(new { success = false });

        }
        public IActionResult Courier()
        {
            ViewBag.ActivePage = "Courier";
            return View();
        }
        public IActionResult Question()
        {
            ViewBag.ActivePage = "Question";
            return View();
        }
        public async Task<IActionResult> TlBalance()
        {
            //Get user with balance
            var userWithBalance = await userRepository.GetUserWithBalance(User.Identity.Name);
            //Get user operations
            var operations = await operationRepository.GetAll();
            //Create model
            TRYBalanceViewModel model = new TRYBalanceViewModel();
            model.TRYBalance = userWithBalance.Balance.TL;
            model.Operations = operations.Where(o => o.UserId == userWithBalance.Id && o.CurrencyType == "TRY").ToList();
            ViewBag.ActivePage = "TlBalance";
            return View(model);
        }

        public async Task<IActionResult> Settings()
        {
            ViewBag.ActivePage = "Settings";
            //Find current user
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            //Map user to view model
            SettingsViewModel model = new SettingsViewModel();

            mapper.Map(user, model);

            return View(model);
        }
        public async Task<ActionResult> UpdateProfileInformation(UpdateProfileViewModel model)
        {
            //Find user
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            //Map user
            mapper.Map(model, user);
            //Update user
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Json(new { success = true });

            }
            return Json(new { success = false });

        }
        public async Task<ActionResult> UpdateIdInformation(UpdateIdInformationViewModel model)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            mapper.Map(model, user);

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Json(new { success = true });

            }
            return Json(new { success = false });

        }
        [HttpPost]

        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Json(new { success = false, message = "Istifadeci tapilmadi" });
                }

                var result = await userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }

                //await signInManager.RefreshSignInAsync(user);

            }

            return Json(new { success = false, message = "Sifre yenilemede xeta" });
        }
        public IActionResult Declare()
        {
            ViewBag.ActivePage = "Declare";
            return View();
        }
        public async Task<ActionResult> AddDeclares(Declaration model)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            model.UserId = user.Id;
            model.User = user;
            model.Status = "Sifariş verildi";
            model.OrderNo = GenerateOrderNo(9);
         
            var result = await declarationRepository.Create(model);
            if (result != null)
            {
                await CreateDeclarationStatus(result);
                return Json(new { success = true });

            }
            return Json(new { success = false });
        }
        public async Task<IActionResult> Order()
        {
            //Get user with balance
            var userWithBalance = await userRepository.GetUserWithBalance(User.Identity.Name);
            ViewBag.ActivePage = "Order";
            OrderGetViewModel model = new OrderGetViewModel();
            model.TRYBalance = userWithBalance.Balance.TL;

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddOrders(OrderViewModel[] orders, decimal TotalOrderPrice)
        {

            //Get user with balance
            var userWithBalance = await userRepository.GetUserWithBalance(User.Identity.Name);

            if (userWithBalance.Balance.TL >= TotalOrderPrice)
            {
                userWithBalance.Balance.TL -= TotalOrderPrice;
            }
            else
            {
                return Json(new { success = false, message = "Zehmet olmasa balansinizi artirin" });
            }
            //Update user balance

            var updateUserResult = await userManager.UpdateAsync(userWithBalance);
            int successCount = 0;
            foreach (var data in orders)
            {
                Order order = new Order();
                mapper.Map(data, order);
                order.UserId = userWithBalance.Id;
                order.User = userWithBalance;
                order.Status = "Sifariş verildi";

                var result = await orderRepository.Create(order);

                if (result != null)
                {
                    await CreateOrderStatus(result);
                    successCount++;
                }
            }

            if (successCount == orders.Length)
            {
                //Create operation
                Operation operation = new Operation()
                {
                    Type = "Məxaric",
                    Amount = TotalOrderPrice,
                    Date = DateTime.Now,
                    User = userWithBalance,
                    UserId = userWithBalance.Id,
                    CurrencyType = "TRY"

                };
                var createResult = await operationRepository.Create(operation);
                //Get user with balance
                var updatedUser = await userRepository.GetUserWithBalance(User.Identity.Name);
                return Json(new { success = true, updatedUserBalance = updatedUser.Balance.TL });

            }
            return Json(new { success = false });

        }
        public async Task CreateOrderStatus(Order order)
        {
            OrderStatus orderStatus = new OrderStatus();
            orderStatus.OrderId = order.Id;
            orderStatus.Ordered = true;
            orderStatus.OrderedDate = DateTime.Now;
            var result = await orderStatusRepository.Create(orderStatus);

        }
        public async Task CreateDeclarationStatus(Declaration declaration)
        {
            DeclarationStatus declarationStatus = new DeclarationStatus();
            declarationStatus.DeclarationId = declaration.Id;
            declarationStatus.Ordered = true;
            declarationStatus.OrderedDate = DateTime.Now;
            var result = await declarationStatusRepository.Create(declarationStatus);

        }

        [HttpGet]
        public ActionResult GetCurrencies()
        {
            CalculatorViewModel model = new CalculatorViewModel();
            model = GetCurrency();
            var AZN_TRY = model.AZN_TRY;
            if (model != null)
            {
                return Json(new
                {
                    success = true,
                    AZN_TRY = AZN_TRY.ToString(),
                    TRY_AZN = model.TRY_AZN,
                    AZN_USD = model.AZN_USD,
                    USD_AZN = model.USD_AZN,
                    TRY_USD = model.TRY_USD,
                    USD_TRY = model.USD_TRY
                });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public static string GenerateOrderNo(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
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


    }
}
