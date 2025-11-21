using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EconomyProject.Models
{
    public class CartProduct
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product product { get; set; }
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public  Cart Cart { get; set; }

    }
}
