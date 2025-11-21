using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EconomyProject.ViewModels;
using EconomyProject.Models;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using Image = System.Drawing.Image;
using Microsoft.AspNetCore.Identity;
using EconomyProject.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;


namespace EconomyProject.Controllers
{
    public class ProductController : Controller
    {
        public DBEconomyProject.Models.DBEconomyProjectContext db { get; set; }
        public UserManager<ApplicationUser> UserManager { get; }

        public ProductController(DBEconomyProject.Models.DBEconomyProjectContext _db, UserManager<ApplicationUser> _UserManager)
        {

            db = _db;
            UserManager = _UserManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductInsert()
        {
            ViewData["Group1"] = db.Group1S.ToList();
            return View();
        }
        public async Task<string> InsertProductConfirm(ProductViewModel model)
        {
            if (User.Identity.IsAuthenticated == true)
            {


                if (model.Img != null)
                {
                    if (model.Img.Length <= Math.Pow(1024, 2))
                    {
                        if (Path.GetExtension(model.Img.FileName).ToLower() == ".jpg"
                            ||
                            Path.GetExtension(model.Img.FileName).ToLower() == ".png")
                        {

                            Product p = new Product();
                            p.Name = model.Name;
                            p.Price = model.Price;
                            p.Count = model.Count;
                            byte[] b = new byte[model.Img.Length];
                            model.Img.OpenReadStream().Read(b, 0, b.Length);

                            var mem = new MemoryStream(b);
                            Image image = Image.FromStream(mem);
                            Bitmap bitmap = new Bitmap(image, 200, 200/*image.Height * 200 / image.Width*/);
                            var mem2 = new MemoryStream();
                            bitmap.Save(mem2, System.Drawing.Imaging.ImageFormat.Jpeg);
                            p.Img = mem2.ToArray();
                            if (model.File != null)
                            {
                                if (model.File.Length <= 10 * Math.Pow(1024, 2))
                                {
                                    if (Path.GetExtension(model.File.FileName).ToLower() == ".pdf"
                                        ||
                                        Path.GetExtension(model.File.FileName).ToLower() == ".zip"
                                        ||
                                        Path.GetExtension(model.File.FileName).ToLower() == ".rar")
                                    {
                                        byte[] d = new byte[model.File.Length];
                                        model.File.OpenReadStream().Read(d, 0, d.Length);
                                        p.File = d;
                                        p.FileName = model.File.FileName;
                                        p.Group1Id = model.Group1Id;
                                        p.UserId = (await UserManager.FindByNameAsync(User.Identity.Name)).Id;
                                        db.Add(p);
                                        var i = db.SaveChanges();
                                        if (i != 0)
                                        {
                                            return "t";
                                        }

                                    }


                                }
                            }
                        }
                    }
                }
            }
            return "f";

        }
        public async Task< IActionResult> ProductList()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = (await UserManager.FindByNameAsync(User.Identity.Name));
                ViewData["CClike"] = db.Products.Include(x=>x.ProductLikes)
                    .Select(x => new AnonymosType1
                    {
                        PCId = x.Id
                    ,
                        UserId = x.ProductLikes.FirstOrDefault(y => y.UserId == user.Id).UserId
                    ,
                        Num = db.ProductLikes.Where(y => y.ProductId == x.Id).Count()
                    }).ToList();

            }
            else
            {
                ViewData["CClike"] = db.Products.Include(x => x.ProductLikes)
                    .Select(x => new AnonymosType1
                    {
                        PCId = x.Id
                    ,

                        UserId = null
                    ,
                        Num = db.ProductLikes.Where(y => y.ProductId == x.Id).Count()
                    }).ToList();
            }
            return View(db.Products.Include(x=>x.User).ToList());
        }
        public async Task<IActionResult> ProductDetail(int ProductId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = (await UserManager.FindByNameAsync(User.Identity.Name));
                ViewData["ProductLike"] = db.ProductLikes.SingleOrDefault(x => x.ProductId == ProductId && x.UserId == user.Id);
            }
            else
            {
                ViewData["ProductLike"] = null;
            }

            ViewData["product"] = db.Products.Include(x => x.group1).SingleOrDefault(x => x.Id == ProductId);
            ViewData["Plike"] = $"{db.ProductLikes.Where(x => x.ProductId == ProductId).Count():0,0}";
            return View();
        }
        public async Task<IActionResult> ProductCom(int ProductId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = (await UserManager.FindByNameAsync(User.Identity.Name));
                ViewData["CClike"] = db.ProductComments.Include(x => x.User).Include(x => x.CommentLikes).Where(x => x.ProductId == ProductId)
                    .Select(x => new AnonymosType1
                    {
                        PCId = x.Id
                    ,
                        UserId = x.CommentLikes.FirstOrDefault(y => y.UserId == user.Id).UserId
                    ,
                        Num = db.CommentLikes.Where(y => y.ProductCommentId == x.Id).Count()
                    }).ToList();

            }
            else
            {
                ViewData["CClike"] = db.ProductComments.Include(x => x.User).Where(x => x.ProductId == ProductId)
                    .Select(x => new AnonymosType1
                    {
                        PCId = x.Id
                    ,

                        UserId = null
                    ,
                        Num = db.CommentLikes.Where(y => y.ProductCommentId == x.Id).Count()
                    }).ToList();
            }
            return View(db.ProductComments.Where(x => x.ProductId == ProductId).Include(x => x.User).OrderByDescending(x => x.Date).ToList());
        }
        public async Task<string> InsertComment(ProductCommentViewModel ProductComment, int ProductId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = (await UserManager.FindByNameAsync(User.Identity.Name)).Id;

                ProductComment PC = new ProductComment()
                {
                    UserId = userId,
                    ProductId = ProductId,
                    Comment = ProductComment.Comment,

                    DisAgree = ProductComment.DisAgree,
                    Date = DateTime.Now,
                };
                db.Add(PC);
                var i = db.SaveChanges();
                if (i != 0)
                {
                    return "t";
                }
                else
                {
                    return "f";

                }
            }
            return "l";

        }
        public string DeletComment(int CommentId)
        {
            //int PId = db.ProductComments.FirstOrDefault(x => x.Id == CommentId).ProductId;
            var s = db.CommentLikes.Where(x => x.ProductCommentId == CommentId).ToList();
            db.CommentLikes.RemoveRange(s);
            db.ProductComments.Remove(db.ProductComments.SingleOrDefault(x => x.Id == CommentId));
            var i = db.SaveChanges();
            if (i != 0)
            {
                return "t";
            }
            return "f";

        }
        public async Task<IActionResult> ProductLike(int Productid, bool Status)
        {
            if (User.Identity.IsAuthenticated)
            {


                var UserId = (await UserManager.FindByNameAsync(User.Identity.Name)).Id;
                int Tl;
                if (Status)
                {
                    ProductLike pl = new ProductLike()
                    {
                        UserId = UserId,
                        ProductId = Productid
                    };
                    db.Add(pl);
                }
                else
                {
                    db.ProductLikes.Remove(db.ProductLikes.SingleOrDefault(x => x.ProductId == Productid && x.UserId == UserId));
                }
                var i = db.SaveChanges();
                if (i != 0)
                {
                    Tl = db.ProductLikes.Where(x => x.ProductId == Productid).Count();
                    return Json(new { totallike = $"{Tl:0,0}" });
                }

            }
            return null;
        }

        public async Task<IActionResult> CommentLike(int ProductCommentid, bool Status)
        {
            if (User.Identity.IsAuthenticated)
            {
                var UserId = (await UserManager.FindByNameAsync(User.Identity.Name)).Id;
                if (Status)
                {
                    CommentLike cl = new CommentLike()
                    {
                        UserId = UserId,
                        ProductCommentId = ProductCommentid
                    };
                    db.Add(cl);
                    db.SaveChanges();


                }
                else
                {
                    db.CommentLikes.Remove(db.CommentLikes.SingleOrDefault(x => x.ProductCommentId == ProductCommentid && x.UserId == UserId));
                    db.SaveChanges();
                }

                var Tl = db.CommentLikes.Where(x => x.ProductCommentId == ProductCommentid).Count();
                return Json(new { totallike = $"{Tl:0,0}" });
            }
            return null;
            
        }

    }
}