using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EconomyProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EconomyProject.Controllers
{[Authorize(Policy ="adminpolicy")]
    public class ManagerController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public DBEconomyProject.Models.DBEconomyProjectContext db { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public ManagerController(SignInManager<ApplicationUser> _SignInManager,
            UserManager<ApplicationUser> _UserManager,
            DBEconomyProject.Models.DBEconomyProjectContext _db,
            RoleManager<IdentityRole> _roleManager)
        {
            UserManager = _UserManager;
            db = _db;
            RoleManager = _roleManager;
        }
        [ViewData]
        public List<ApplicationUser> UserList { get; set; }
        [ViewData]
        public List<IdentityRole> Rolelist { get; set; }
        public IActionResult ManageuserRole()
        {
            UserList = db.Users.ToList();
            Rolelist = db.Roles.ToList();
            return View();
        }
        public async Task UsersRole(string UserId,string RoleId,bool Status)
        { var user = await UserManager.FindByIdAsync(UserId);
            if (user != null)
            {
               var rolName= (await RoleManager.FindByIdAsync(RoleId)).Name;
                if (Status)
                {
                    UserManager.AddToRoleAsync(user, rolName).Wait();
                }
                else
                {
                    UserManager.RemoveFromRoleAsync(user, rolName).Wait();
                }
            }  
        }
        public async Task< IActionResult> DleteAccount(string UserId)
        {
            string result="f";
           var user= await UserManager.FindByIdAsync(UserId);
            if (user !=null)
            {
                UserManager.DeleteAsync(user).Wait();
                result = "t";
            }
            return Json(new {res= result });
        }
    }
}