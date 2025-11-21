using EconomyProject.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EconomyProject.Models
{
    public class ProductComment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product product { get; set; }
        public string Comment { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }
        public Nullable<DateTime> Date { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ShamsiDate
        {
            get
            {
                if (Date == null) return "";
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                return $"{p.GetYear(Date.Value)}/{p.GetMonth(Date.Value)}/{p.GetDayOfMonth(Date.Value)}";
            }
            set { }
        }
        public bool DisAgree { get; set; }
    }
}
