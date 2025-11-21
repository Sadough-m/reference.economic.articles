using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EconomyProject.Areas.Identity.Data;


namespace EconomyProject.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public ICollection<CartProduct> CartProduct { get; set; }

        public DateTime CreateDate { get; set; }
        public Nullable<DateTime> PayDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ShamsiPayDate
        {
            get
            {
                if (PayDate == null) return "";
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                return $"{p.GetYear(PayDate.Value)}/{p.GetMonth(PayDate.Value)}/{p.GetDayOfMonth(PayDate.Value)}";
            }
            set { }
        }


    }
}
