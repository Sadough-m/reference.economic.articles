using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EconomyProject.ViewModels
{
    public class ProductCommentViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
        public bool DisAgree { get; set; }

    }
}
