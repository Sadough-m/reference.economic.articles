using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EconomyProject.Models
{
    public class Group1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> product { get; set; }
    }
}
