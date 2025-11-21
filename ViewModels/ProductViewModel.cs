using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EconomyProject.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public IFormFile Img { get; set; }
        public IFormFile File { get; set; }
        public int Group1Id { get; set; }
        public int Count { get; set; }
        public string FileName { get; set; }
        public string Abstract { get; set; }



    }
}
