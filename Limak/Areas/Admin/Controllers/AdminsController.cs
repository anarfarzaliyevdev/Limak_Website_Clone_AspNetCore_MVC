using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Limak.Abstractions;
using Limak.Areas.Admin.ViewModels;
using Limak.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly IBalanceRepository balanceRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IDeclarationRepository declarationRepository;

        public AdminsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IMapper mapper, IBalanceRepository balanceRepository, IOrderRepository orderRepository,
            IDeclarationRepository declarationRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.balanceRepository = balanceRepository;
            this.orderRepository = orderRepository;
            this.declarationRepository = declarationRepository;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            
            var users = userManager.Users.ToList();
            var isRoleExists = await roleManager.RoleExistsAsync("Admin");
            //var roles = roleManager.Roles.ToList();
            List<ApplicationUser> admins = new List<ApplicationUser>();
            if (isRoleExists)
            {
                foreach (var user in users)
                {
                    if (await userManager.IsInRoleAsync(user, "Admin"))
                    {
                        admins.Add(user);
                    }
                }

            }

            AdminIndexViewModel model = new AdminIndexViewModel();
            model.Admins = admins;
            model.CurrentUserId = currentUser.Id;
            return View(model);

        }
        [HttpGet]
        public IActionResult AddAdmin()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminsAddAdminViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser();
                mapper.Map(model, user);
                //Define customerId for new user
                user.CustomerId = GenerateCustomerId(7);
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    //Created user
                    var user_ = await userManager.FindByEmailAsync(user.Email);
                    //Create balance for user
                    await CreateBalance(user_);
                    //Confirm Admin email
                    user_.EmailConfirmed = true;
                    user_.IsActive = true;
                    var resultUpdateUser = await userManager.UpdateAsync(user_);
                    if (resultUpdateUser.Succeeded)
                    {
                        //Give role to user
                        var role = await CreateRole("Admin");
                        await AddUserToRole(user_, "Admin");

                        return RedirectToAction("Index", "Admins");
                    }

                }
                var isUserExists = await userManager.FindByEmailAsync(user.Email);
                if (isUserExists != null)
                {
                    ModelState.AddModelError("Email", "Bu email istifade olunub");
                    return View(model);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditAdmin(string id)
        {
            AdminsEditAdminViewModel model = new AdminsEditAdminViewModel();
            //Find user
            var user = await userManager.FindByIdAsync(id);
            mapper.Map(user, model);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditAdmin(AdminsEditAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                mapper.Map(model, user);
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admins");
                }
            }
            return View(model);

        }
        public static string GenerateCustomerId(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        private async Task CreateBalance(ApplicationUser user)
        {
            Balance balance = new Balance()
            {
                User = user
            };
            await balanceRepository.Create(balance);
        }
        public async Task<IdentityRole> CreateRole(string roleName)
        {
            IdentityRole role = new IdentityRole();
            role.Name = roleName;
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return role;
            }
            return null;
        }
        public async Task AddUserToRole(ApplicationUser user, string roleName)
        {
            var isUserInRole = await userManager.IsInRoleAsync(user, roleName);

            if (!isUserInRole)
            {
                var result = await userManager.AddToRoleAsync(user, roleName);

            }

        }
        [HttpPost]
        public async Task<ActionResult> DeleteAdmin(string id)
        {
            
            var user = await userManager.FindByIdAsync(id);
            var balances = await balanceRepository.GetAll();
            //Delete user balance
            var userBalance = balances.FirstOrDefault(b => b.UserId == user.Id);
            if (userBalance != null)
            {
                var deleteBalanceResult =await balanceRepository.Delete(userBalance.Id);
            }
           
            //Delete user orders
            var orders = await orderRepository.GetAll();
            var userOrders = orders.Where(o => o.UserId == user.Id).ToList();
            if (userOrders.Count > 0)
            {
                foreach (var order in userOrders)
                {
                   await orderRepository.Delete(order.Id);
                }

            }
            //Delete user declares
            var declares = await declarationRepository.GetAll();
            var userDeclares = declares.Where(o => o.UserId == user.Id).ToList();
            if (userDeclares.Count > 0)
            {
                foreach (var declaration in userDeclares)
                {
                   await declarationRepository.Delete(declaration.Id);
                }
            }

            //Delete user
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Deleted !" });
            }
            return Json(new { success = false, message = "Error appeared !" });
        }
    }
}