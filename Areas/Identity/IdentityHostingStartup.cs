using System;
using System.Threading.Tasks;
using DBEconomyProject.Models;
using EconomyProject.Areas.Identity.Data;
using EconomyProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EconomyProject.Areas.Identity.IdentityHostingStartup))]
namespace EconomyProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DBEconomyProjectContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("EconomyProjectContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<DBEconomyProjectContext>();
                services.ConfigureApplicationCookie(x =>
                {
                    x.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = y =>
                        {
                            y.Response.Redirect("/Account/UserLoginRigester");
                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = y =>
                        {
                            y.Response.Redirect("/Account/UserLoginRigester");
                            return Task.CompletedTask;
                        }
                    };
                });
                services.AddAuthentication();
                services.AddAuthorization(x =>
                {
                    x.AddPolicy("adminpolicy", p => { p.RequireRole("ادمین"); });
                    x.AddPolicy("propolicy", p => { p.RequireRole("حرفه ای"); });
                });
            });
        }
    }
}