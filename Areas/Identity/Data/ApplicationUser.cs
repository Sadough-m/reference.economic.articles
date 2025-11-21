using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EconomyProject.Models;

namespace EconomyProject.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistryDate { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsAuthenticated { get; set; }

        public ICollection<Cart> carts { get; set; }
        public ICollection<Product> products { get; set; }
        public ICollection<ProductLike> ProductLikes { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }

    }
}
