using EconomyProject.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EconomyProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public byte[] Img { get; set; }
        public byte[] File { get; set; }
        public int Group1Id { get; set; }
        [ForeignKey("Group1Id")]
        public Group1 group1 { get; set; }
        public ICollection<CartProduct> CartProduct { get; set; }
        public int Count { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string FileName { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<ProductLike> ProductLikes { get; set; }
        public string Abstract { get; set; }

    }
}
