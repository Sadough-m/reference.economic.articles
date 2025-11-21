using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EconomyProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EconomyProject.Models;

namespace DBEconomyProject.Models
{
    public class DBEconomyProjectContext : IdentityDbContext<ApplicationUser>
    {
        public DBEconomyProjectContext(DbContextOptions<DBEconomyProjectContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Group1> Group1S { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductLike> ProductLikes { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }




    }
}
