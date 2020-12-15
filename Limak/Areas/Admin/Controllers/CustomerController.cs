using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Limak.Abstractions;
using Limak.Areas.Admin.ViewModels;
using Limak.Models;
using Limak.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Limak.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IDeclarationRepository declarationRepository;
        private readonly IBalanceRepository balanceRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;
        private readonly IOperationRepository operationRepository;
        private readonly IOrderStatusRepository orderStatusRepository;
        private readonly IDeclarationStatusRepository declarationStatusRepository;
       

        public CustomerController(UserManager<ApplicationUser> userManager, IMapper mapper,
            RoleManager<IdentityRole> roleManager,IDeclarationRepository declarationRepository,
            IBalanceRepository balanceRepository,IOrderRepository orderRepository,
            IUserRepository userRepository,IOperationRepository operationRepository,IOrderStatusRepository orderStatusRepository,
            IDeclarationStatusRepository declarationStatusRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.declarationRepository = declarationRepository;
            this.balanceRepository = balanceRepository;
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
            this.operationRepository = operationRepository;
            this.orderStatusRepository = orderStatusRepository;
            this.declarationStatusRepository = declarationStatusRepository;
         
        }
        public async Task<IActionResult> Index()
        {
            var users = userManager.Users.ToList();
            var isRoleExists = await roleManager.RoleExistsAsync("Customer");
            //var roles = roleManager.Roles.ToList();
            List<ApplicationUser> customers = new List<ApplicationUser>();
            if (isRoleExists)
            {
                foreach (var user in users)
                {
                    if (await userManager.IsInRoleAsync(user, "Customer"))
                    {
                        customers.Add(user);
                    }
                }

            }

            CustomerIndexViewModel model = new CustomerIndexViewModel();
            model.Customers = customers;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            CustomerDetailsViewModel model = new CustomerDetailsViewModel();
            //Find user
            var user = await userManager.FindByIdAsync(id);
            mapper.Map(user, model);

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            CustomerEditViewModel model = new CustomerEditViewModel();
            //Find user
            var user = await userManager.FindByIdAsync(id);
            mapper.Map(user, model);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                mapper.Map(model, user);
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Customer");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddDeclare(string id)
        {
            
            CustomerAddDeclarationViewModel model = new CustomerAddDeclarationViewModel();
            model.UserId = id;
            

            return View(model);
        }
        public static string GenerateOrderNo(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        [HttpPost]
        public async Task<IActionResult> AddDeclare(CustomerAddDeclarationViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            Declaration declaration = new Declaration();
            model.OrderNo = GenerateOrderNo(9);
            
            declaration.Status = "Sifariş verildi";
            declaration.CargoPrice = CalculateCargoPrice(model.Weight);
            mapper.Map(model, declaration);
          
            var result=await declarationRepository.Create(declaration);
            if (result != null)
            {
                await CreateDeclarationStatus(result);
                return RedirectToAction("Index", "Customer");
            }


            return View(model);
        }
        private decimal CalculateCargoPrice(decimal weight)
        {
            decimal result = 0;
            if (weight == 0)
            {
                result = 0;
            }
            else if (weight > 0 && weight <= 0.25m)
            {
                result = 2m;
            }
            else if (weight > 0.25m && weight <= 0.5m)
            {
                result = 3m;
            }
            else if (weight > 0.5m && weight <= 0.7m)
            {
                result = 4m ;
            }
            else if(weight>0.7m &&weight<=1)
            {
                result = 4.5m;
            }
            else
            {
                result = 10m;
            }
            return result;
        }
        public async Task CreateDeclarationStatus(Declaration declaration)
        {
            DeclarationStatus declarationStatus = new DeclarationStatus();
            declarationStatus.DeclarationId = declaration.Id;
            declarationStatus.Ordered = true;
            
            var result = await declarationStatusRepository.Create(declarationStatus);

        }
        [HttpGet]
        public async Task<IActionResult> Declarations(string id)
        {
            var customer= await userManager.FindByIdAsync(id);
            var declarations = await declarationRepository.GetAll();
            var customerDeclares = declarations.Where(d => d.UserId ==id).ToList();
            CustomerDeclarationsViewModel model = new CustomerDeclarationsViewModel();
            model.Declarations = customerDeclares;

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Orders(string id)
        {
            var customer = await userManager.FindByIdAsync(id);
            var orders = await orderRepository.GetAll();
            var customerOrders = orders.Where(o => o.UserId == id).ToList();
            CustomerOrdersViewModel model = new CustomerOrdersViewModel();
            model.Orders = customerOrders;

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditDeclaration(int id)
        {
            CustomerEditDeclarationViewModel model = new CustomerEditDeclarationViewModel();
            //Find user
            var declaration = await declarationRepository.GetById(id);
           
            mapper.Map(declaration, model);

            return View(model);
        }
       
        [HttpPost]
        public async Task<IActionResult> EditDeclaration(CustomerEditDeclarationViewModel model)
        {

            if (ModelState.IsValid)
            {

                Declaration declaration = new Declaration();
            
                mapper.Map(model,declaration);
                declaration.CargoPrice = CalculateCargoPrice(model.Weight);
                var result = await declarationRepository.Edit(declaration);
                DeclarationStatus declarationStatus = await declarationStatusRepository.GetByDeclarationId(model.Id);
                if (result)
                {
                    await UpdateStatus(declarationStatus, model.Status);
                    return RedirectToAction("Declarations", "Customer",new { id=model.UserId});
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditOrder(int id)
        {
            CustomerEditOrderViewModel model = new CustomerEditOrderViewModel();
            //Find user
            var order = await orderRepository.GetById(id);
            mapper.Map(order, model);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditOrder(CustomerEditOrderViewModel model)
        {
            

            if (ModelState.IsValid)
            {
                
                Order order = new Order();

                mapper.Map(model, order);
                order.OrderCargoPrice = CalculateCargoPrice(model.OrderWeight);
                var result = await orderRepository.Edit(order);
                OrderStatus orderStatus = await orderStatusRepository.GetByOrderId(model.Id);

                if (result && orderStatus != null)
                {
                    await UpdateStatus(orderStatus, model.Status);
                    return RedirectToAction("Orders", "Customer", new { id = model.UserId });
                }
            }
            return View(model);
        }
        public async Task UpdateStatus(Status status,string statusName)
        {
            OrderStatus orderStatus = status as OrderStatus;
            DeclarationStatus declarationStatus = status as DeclarationStatus;
            switch (statusName)
            {
                case "Sifariş verildi":
                    if (orderStatus != null)
                    {
                        orderStatus.Ordered = true;
                        orderStatus.OrderedDate = DateTime.Now;
                    }
                    else if (declarationStatus != null)
                    {
                        declarationStatus.Ordered = true;
                        declarationStatus.OrderedDate = DateTime.Now;
                    }
                    break;
                case "Xaricdəki anbar":
                    if (orderStatus != null)
                    {
                        orderStatus.AbroadWarehouse = true;
                        orderStatus.AbroadWarehouseDate = DateTime.Now;
                    }
                    else if (declarationStatus != null)
                    {
                        declarationStatus.AbroadWarehouse = true;
                        declarationStatus.AbroadWarehouseDate= DateTime.Now;
                    }
                  
                    break;
                case "Yoldadır":
                    if (orderStatus != null)
                    {
                        orderStatus.OnWay = true;
                        orderStatus.OnWayDate = DateTime.Now;
                    }
                    else if (declarationStatus != null)
                    {
                        declarationStatus.OnWay = true;
                        declarationStatus.OnWayDate = DateTime.Now;
                    }
                 
                    break;
                case "Gömrük yoxlanışı":
                    if (orderStatus != null)
                    {
                        orderStatus.CustomsControl = true;
                        orderStatus.CustomsControlDate = DateTime.Now;
                    }
                    else if (declarationStatus != null)
                    {
                        declarationStatus.CustomsControl = true;
                        declarationStatus.CustomsControlDate = DateTime.Now;
                    }
                   
                    break;
                case "Bakı anbarı":
                    if (orderStatus != null)
                    {
                        orderStatus.BakuWarehouse = true;
                        orderStatus.BakuWarehouseDate = DateTime.Now;
                    }
                    else if (declarationStatus != null)
                    {
                        declarationStatus.BakuWarehouse = true;
                        declarationStatus.BakuWarehouseDate = DateTime.Now;
                    }
                  
                    break;
                case "Kuryer çatdırma":
                    if (orderStatus != null)
                    {
                        orderStatus.Courier = true;
                        orderStatus.CourierDate = DateTime.Now;
                    }
                    else if (declarationStatus != null)
                    {
                        declarationStatus.Courier = true;
                        declarationStatus.CourierDate = DateTime.Now;
                    }

                    break;
                case "İade":
                    if (orderStatus != null)
                    {
                        orderStatus.Return = true;
                        orderStatus.ReturnDate = DateTime.Now;
                    }
                    else if (declarationStatus != null)
                    {
                        declarationStatus.Return = true;
                        declarationStatus.ReturnDate = DateTime.Now;
                    }
                   
                    break;
                case "Tamamlanmış":
                    if (orderStatus != null)
                    {
                        orderStatus.Completed = true;
                        orderStatus.CompletedDate = DateTime.Now;
                    }
                    else if (declarationStatus != null)
                    {
                        declarationStatus.Completed = true;
                        declarationStatus.CompletedDate = DateTime.Now;
                    }
                   
                    break;
                default:
                    break;
            }
            if (declarationStatus != null)
            {
                 await declarationStatusRepository.Edit(declarationStatus);

            }
            else if(orderStatus!=null)
            {
                await orderStatusRepository.Edit(orderStatus);
            }
        }
        [HttpGet]
        public async Task<IActionResult> IncreaseBalanceTRY(string id)
        {
            CustomerIncBalanceTRYViewModel model = new CustomerIncBalanceTRYViewModel();
            var customer  = await userManager.FindByIdAsync(id);
            model.Id = id;
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> IncreaseBalanceTRY(CustomerIncBalanceTRYViewModel model)
        {
            var customer = await userManager.FindByIdAsync(model.Id);
            var customerWithBalance = await userRepository.GetUserWithBalance(customer.UserName);
            customerWithBalance.Balance.TL += model.TRYAmount;
            //Create operation
            Operation operation = new Operation()
            {
                Type = "Mədaxil",
                Amount = model.TRYAmount,
                Date = DateTime.Now,
                User = customerWithBalance,
                UserId = customerWithBalance.Id,
                CurrencyType = "TRY"
            };
            var createResult = await operationRepository.Create(operation);
            //Update user balance
            var result =  await userManager.UpdateAsync(customerWithBalance);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Customer");
            }

            return View(model);
        }

        private string GetMonthName(int monthNumber)
        {
            var monthName = "";
            switch (monthNumber)
            {
                case 1:
                    monthName = "Yanvar";
                    break;
                case 2:
                    monthName = "Fevral";
                    break;
                case 3:
                    monthName = "Mart";
                    break;
                case 4:
                    monthName = "Aprel";
                    break;
                case 5:
                    monthName = "May";
                    break;
                case 6:
                    monthName = "İyun";
                    break;
                case 7:
                    monthName = "İyul";
                    break;
                case 8:
                    monthName = "Avqust";
                    break;
                case 9:
                    monthName = "Sentyabr";
                    break;
                case 10:
                    monthName = "Oktyabr";
                    break;
                case 11:
                    monthName = "Noyabr";
                    break;
                case 12:
                    monthName = "Dekabr";
                    break;
                default:
                    break;
            }
            return monthName;
        }
    }
}