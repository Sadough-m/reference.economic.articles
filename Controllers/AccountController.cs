using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EconomyProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using EconomyProject.Areas.Identity.Data;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;

namespace EconomyProject.Controllers
{
    public class AccountController : Controller
    {
        public SignInManager<ApplicationUser> SignINmanager { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        public DBEconomyProject.Models.DBEconomyProjectContext db { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public AccountController(SignInManager<ApplicationUser> _SignInManager,
            UserManager<ApplicationUser> _UserManager,
            DBEconomyProject.Models.DBEconomyProjectContext _db,
            RoleManager<IdentityRole> _roleManager)
        {
            SignINmanager = _SignInManager;
            UserManager = _UserManager;
            db = _db;
            RoleManager = _roleManager;
            InitializRoll().Wait();
        }
        public async Task InitializRoll()
        {
            if ((await RoleManager.RoleExistsAsync("ادمین"))== false)
            {
                var rol = new IdentityRole("ادمین");
                await RoleManager.CreateAsync(rol);
            }
            if ((await RoleManager.RoleExistsAsync("حرفه ای")) == false)
            {
                var rol = new IdentityRole("حرفه ای");
                await RoleManager.CreateAsync(rol);
            }
            var Uadmin = await UserManager.FindByEmailAsync("admin@gmail.com");
            if (Uadmin == null)
            {


                Uadmin = new ApplicationUser
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                FirstName = "ادمین",
                EmailConfirmed = true,

            };
                await UserManager.CreateAsync(Uadmin, "admin159753");
            }
            if (!(await UserManager.IsInRoleAsync(Uadmin,"ادمین")))
            {
                await UserManager.AddToRoleAsync(Uadmin, "ادمین");
            }
        } 
        public IActionResult UserLoginRigester()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmLogIn(UserLoginViewModel model)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                if (user.EmailConfirmed == true)
                {

                    var result = await SignINmanager.PasswordSignInAsync(user, model.Pass, model.RememberMe, false);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    return RedirectToAction("UserLoginRigester", "Account");

                }
            }
            else
            {
                return RedirectToAction("UserLoginRigester", "Account");

            }

        }
        public async Task<IActionResult> LogOut()
        {
            await SignINmanager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        
        public async Task<IActionResult> RegistryConfirm(UserRegistryViewModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.Tel,
                BirthDate = model.BirthDate,
                RegistryDate = DateTime.Now,
                IsAuthenticated = false
            };

            var result = await UserManager.CreateAsync(user, model.Pass);
            if (result.Succeeded == true)
            {
                ApplicationUser RigesteredUser = await UserManager.FindByNameAsync(model.UserName);
                string token = await UserManager.GenerateEmailConfirmationTokenAsync(RigesteredUser);
                string href = Url.Action("RegistryConfirm2", "Account", new { Id = RigesteredUser.Id, Token = token }, "https");
                MailMessage mail = new MailMessage("mohammadpourmvc@gmail.com", RigesteredUser.UserName);
                mail.Subject = "احراز ایمیل آدرس";
                mail.Body = $"<div style='text - align:right'><label style='text - align:right'>سلام , {RigesteredUser.FirstName} {RigesteredUser.LastName} عزیز</label><br/>" +
                    $"<label style='text - align:right'>لطفا روی لینک زیر کلیک کنید تا ثبت نام شما تکمیل شود</label><br/>" + $": <a style='text - align:right' href='{href}'>لطفا اینجا را کلیک کنید</a></div>";
                mail.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;

                smtpClient.Credentials = new System.Net
                    .NetworkCredential("mohammadpourmvc@gmail.com", "reza_1234567890");
                try
                {

                smtpClient.Send(mail);
                return View();
                }
                catch (Exception)
                {
                    
                    return RedirectToAction("UserLoginRigester", "Account");

                }
            }
            else
            {
                return RedirectToAction("UserLoginRigester", "Account");

            }
        }
        public async Task<IActionResult> RegistryConfirm2(string Id, string Token)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(Id);
            var result = await UserManager.ConfirmEmailAsync(user, Token);
            if (result.Succeeded == true)
            {
                user.IsAuthenticated = true;
                await db.SaveChangesAsync();
                return RedirectToAction("UserLoginRigester", "Account");

            }
            else
            {
                return RedirectToAction("index", "home");

            }
        }
        public IActionResult Security1() => View();

        public async Task<IActionResult> Security2(string UserName)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(UserName);
            string token = await UserManager.GeneratePasswordResetTokenAsync(user);
            string href = Url.Action("Security3", "Account", new { Id = user.Id, Token = token }, "https");
            MailMessage mail = new MailMessage("mohammadpourmvc@gmail.com", user.UserName);
            mail.Subject = "تغییر رمز";
            mail.Body = $"<div style='text - align:right'><label style='text - align:right'>سلام , {user.FirstName} {user.LastName} عزیز</label><br/>" +
                $"<label style='text - align:right'>لطفا روی لینک زیر کلیک کنید تا تغییر رمز ادامه یابد</label><br/>" + $": <a style='text - align:right' href='{href}'>لطفا اینجا را کلیک کنید</a></div>";
            mail.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new System.Net
                .NetworkCredential("mohammadpourmvc@gmail.com", "reza_1234567890");

            smtpClient.Send(mail);
            return View();
        }
        public  IActionResult Security3(string Id, string Token)
        {
            
                HttpContext.Session.SetString("Id", Id);
                HttpContext.Session.SetString("Token", Token);
                return View();

            
            
        }
        public async Task< IActionResult> Security4(UserRegistryViewModel model) 
        {
           string id= HttpContext.Session.GetString("Id");
               string token = HttpContext.Session.GetString("Token");
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            
              var result= await UserManager.ResetPasswordAsync(user, token, model.Pass);
            if (true)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("UserLoginRigester", "Account");
            }
            




        }

    }
}
        
        


   