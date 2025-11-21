using EconomyProject.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EconomyProject.Models
{
    public class CommentLike
    {
        public int ProductCommentId { get; set; }
        [ForeignKey("ProductCommentId")]
        public ProductComment ProductComment { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int Id { get; set; }
    }
}
