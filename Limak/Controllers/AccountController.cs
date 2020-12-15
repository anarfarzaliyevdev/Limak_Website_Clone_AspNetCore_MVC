using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Limak.Models;
using Limak.ViewModels;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Limak.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace Limak.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        private readonly IBalanceRepository balanceRepository;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,IBalanceRepository balanceRepository,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.balanceRepository = balanceRepository;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
               
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
     

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                

                if (user != null && !user.EmailConfirmed &&
                                    (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError("Email", "Email aktiv olunmayib");
                    return View(model);
                }
                if (user != null && !user.IsActive && user.EmailConfirmed &&
                                    (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError("Email", "Hesabiniz blokdadir");
                    return View(model);
                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                                        model.RememberMe,false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }



                    return RedirectToAction("PanelPage", "User");
                }

                

                ModelState.AddModelError("Email", "Sifre ve ya Email duzgun deyil");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        private async Task CreateBalance(ApplicationUser user)
        {
            Balance balance = new Balance()
            {
                User = user
            };
            await balanceRepository.Create(balance);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.isAgree)
                {
                    //Passport validation 
                    if (model.IdType == "AZE")
                    {
                        if (model.SeriaNumber.Length == 8)
                        {
                            model.SeriaNumber = model.SeriaNumber.Insert(0, model.IdType);
                        }
                        else
                        {
                            ModelState.AddModelError("SeriaNumber", "AZE:Uzunluq 8 olmalidir");
                            return View(model);
                        }
                    }
                    else if (model.IdType == "AA")
                    {
                        if (model.SeriaNumber.Length == 7)
                        {
                            model.SeriaNumber = model.SeriaNumber.Insert(0, model.IdType);
                        }
                        else
                        {
                            ModelState.AddModelError("SeriaNumber", "AA:Uzunluq 7 olmalidir");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("SeriaNumber", "Yanlis passport formati");
                        return View(model);
                    }
                    //Create user 
                    var user = new ApplicationUser();
                    mapper.Map(model, user);
                    //Define customerId for new user
                    user.CustomerId = GenerateCustomerId(7);
                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        //Created user
                        var user_ = await userManager.FindByEmailAsync(user.Email);
                        //Create balance for user
                        await CreateBalance(user_);
                        //Confirmation link
                        var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                                new { userId = user.Id, token = token }, Request.Scheme);
                        try
                        {
                            SendMail(user.Email, confirmationLink, "click to activate your account");
                        }
                        catch (Exception e)
                        {
                            ViewBag.Message = e.Message;
                            return RedirectToAction("Error");
                        }
                        
                        //Give role to user
                        var role = await CreateRole("Customer");
                        await AddUserToRole(user_, "Customer");

                        return View("PleaseConfirmEmail");
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
                else
                {
                ModelState.AddModelError("isAgree", "Zehmet olmasa qebul edin");

                }
            }

            return View(model);
        }
        
        public async Task<IdentityRole> CreateRole(string roleName)
        {
            IdentityRole role = new IdentityRole();
            role.Name = roleName;
           var result= await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return role;
            }
            return null;
        }
        public async Task AddUserToRole(ApplicationUser user,string roleName)
        {
            var isUserInRole= await userManager.IsInRoleAsync(user, roleName);
          
            if (!isUserInRole)
            {
                var result= await userManager.AddToRoleAsync(user, roleName);

            }
            
        }
        public void SendMail(string email,string link,string message)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("limakmmc@gmail.com");
                mail.To.Add($"{email}");
                mail.Subject = "Activation link";
                mail.IsBodyHtml = true;

                mail.Body = $"Please <a href=\"{link}\">{message}</a>"; 
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("limakmmc@gmail.com", "limak2020");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult PleaseConfirmEmail()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await userManager.FindByIdAsync(userId);

           
            var result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                user.IsActive = true;
                await userManager.UpdateAsync(user);
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }
        [HttpGet]
 
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);


                    SendMail(user.Email, passwordResetLink,"click and change your password");
                    return View("PasswordSent");
                }

                return View("PasswordSent");
            }

            return View(model);
        }
        public IActionResult PasswordSent()
        {
            return View();
        }
        [HttpGet]
       
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                       
                        return View("ResetPasswordConfirmation");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                return View("ResetPasswordConfirmation");
            }

            return View(model);
        }
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public static string GenerateCustomerId(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

    }
}