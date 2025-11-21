using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EconomyProject.ViewModels
{
    public class UserRegistryViewModel
    {
        [Required(ErrorMessage ="*")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده صحیح نمی باشد")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*")]
        [MinLength(6,ErrorMessage ="پسورد باید حداقل 6 کارکتر باشد .")]
        public string Pass { get; set; }
        [Compare("Pass",ErrorMessage ="پسوردها یکسان نیست")]
        public string RePass { get; set; }
        public string Tel { get; set; }
        [Required(ErrorMessage = "*")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
