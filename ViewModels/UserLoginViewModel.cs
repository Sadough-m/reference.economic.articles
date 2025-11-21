using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EconomyProject.ViewModels
{
    public class UserLoginViewModel
    {
        public string UserName { get; set; }
        public string Pass { get; set; }
        public bool RememberMe { get; set; }
    }
}
