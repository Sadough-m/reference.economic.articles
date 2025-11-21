using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EconomyProject.Areas.Identity.Data;
using EconomyProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EconomyProject.Controllers
{
    public class CartController : Controller
    {
        public SignInManager<ApplicationUser> SignINmanager { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        public DBEconomyProject.Models.DBEconomyProjectContext db { get; set; }

        public CartController(SignInManager<ApplicationUser> _SignInManager, UserManager<ApplicationUser> _UserManager, DBEconomyProject.Models.DBEconomyProjectContext _db)
        {
            SignINmanager = _SignInManager;
            UserManager = _UserManager;
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> InsertCart(int ProductId)
        {

            string result ;
            if (User.Identity.IsAuthenticated==true)
            {
                var userId = (await UserManager.FindByNameAsync(User.Identity.Name)).Id;
                var Cart = await db.Carts.SingleOrDefaultAsync(x => x.UserId == userId && x.IsPaid == false);

                if (Cart != null)
                {
                    var CartProducts = db.CartProducts.Include(x => x.Cart).FirstOrDefault(x => x.Cart.UserId == userId && x.ProductId == ProductId);
                    if (CartProducts!=null)
                    {
                        result = "b";

                    }
                    else
                    {

                    CartProduct cp = new CartProduct() {
                        CartId = Cart.Id,
                        ProductId= ProductId,
                        Count=1,

                    };
                    db.Add(cp);
                    db.SaveChanges();
                        result = "t";

                    }
                }
                else
                {
                    Cart cart = new Cart() {
                        UserId = userId,
                        IsPaid = false,
                        CreateDate = Convert.ToDateTime(DateTime.Now.ToShortDateString())
                    };
                    db.Add(cart);
                    db.SaveChanges();
                    var cartId = db.Carts.Max(x => x.Id);
                    var CartProducts = db.CartProducts.Include(x => x.Cart).FirstOrDefault(x => x.Cart.UserId == userId && x.ProductId == ProductId);
                    if (CartProducts != null)
                    {
                        result = "b";
                    }
                    else
                    {

                        CartProduct cp = new CartProduct()
                        {
                            CartId = cartId,
                            ProductId = ProductId,
                            Count = 1,

                        };
                        db.Add(cp);
                        db.SaveChanges();
                        result = "t";

                    }


                }
            }
            else
            {
                result = "f";

            }
            return Json(new {res= result });
        }
        public async Task< IActionResult> CartProduct()
        {
            if (User.Identity.IsAuthenticated==true)
            {
                var userId = (await UserManager.FindByNameAsync(User.Identity.Name)).Id;
                var Cart = db.Carts.SingleOrDefault(x => x.UserId == userId && x.IsPaid == false);
                if (Cart!=null)
                {
                    //var CartProducts = db.CartProducts.Include(x=>x.product).Where(x => x.CartId == Cart.Id);
                    ViewData["TotalFa"] =$"{(await TotalFactor()):0,0} تومان";
                    return View(db.CartProducts.Include(x => x.product.group1).Where(x => x.CartId == Cart.Id).ToList()) ;
                }
                else
                {
                    return RedirectToAction("index", "home");
                }
            }
            else
            {
                return RedirectToAction("index", "home");

            }
        }
        public async Task<IActionResult> RemoveProductFromCart(int Id)
        {
            

                var CartProdeuct = await db.CartProducts.FindAsync(Id);
                db.CartProducts.Remove(CartProdeuct);
                await db.SaveChangesAsync();
            var totalprice = await TotalFactor();
                return Json(new {status=true, totalprice = $"{totalprice:0,0}" });

            
        }
        public async Task<int> TotalFactor()
        {
            
                var userId = (await UserManager.FindByNameAsync(User.Identity.Name)).Id;
                var CartId =(await db.Carts.SingleOrDefaultAsync(x => x.UserId == userId && x.IsPaid == false)).Id;
                var CartProducts = db.CartProducts.Include(x => x.product).Where(x => x.CartId == CartId).ToList();
                return  CartProducts.Sum(x => x.Count * x.product.Price);
            
            
        }
        public async Task< IActionResult> ChangeCountOfProduct(int Id,int Count)
        {
            var CartProdeuct = await db.CartProducts.FindAsync(Id);
            CartProdeuct.Count = Count;
            await db.SaveChangesAsync();
            var TotalRo = (await db.Products.FirstOrDefaultAsync(x=>x.Id==CartProdeuct.ProductId)).Price* CartProdeuct.Count;
            var totalprice = await TotalFactor();
            return Json(new { status = true, tr = $"{TotalRo:0,0}", totalprice = $"{totalprice:0,0}" });

        }
        public async Task<IActionResult> PaymentLevelOne()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                string userId = (await UserManager.FindByNameAsync(User.Identity.Name)).Id;
                var purchaseCart =
                    db.Carts.FirstOrDefault(x => x.IsPaid == false && x.UserId == userId);
                if (purchaseCart != null)
                {
                    var VerifyService = new Verify.VerifyClient();
                    var res = await VerifyService.getTransactionAsync("A51C", "10078218", "82047463");

                    Token.TokensClient client = new Token.TokensClient();
                    Token.tokenResponse tokenResp = await client.MakeTokenAsync(
                         ( await TotalFactor() * 10).ToString()
                        , "AA8B", "54545545", /*"662584852"*/ purchaseCart.Id.ToString(), "",
                         "https://localhost:44332/Cart/Verify", "Test Sample");

                    ViewBag.clientToken = tokenResp.token;
                    ViewBag.merchantId = "AA8B";
                    ViewBag.PaymentId = /*"662584852"*/purchaseCart.Id.ToString();
                    ViewData["price"] = await TotalFactor() *10;
                }
            }
            return View();
        }

        public async Task<ActionResult> Verify(string token, string merchantId, string resultCode, string paymentId, string referenceId)
        {

            // You should send this parameter to verifier service 
            var sha1Key = "22338240992352910814917221751200141041845518824222260";
            var VerifyService = new Verify.VerifyClient();
            {
                //resultCode != null && resultCode != ""
                if (!string.IsNullOrEmpty(resultCode) && resultCode == "110")
                {
                    var res = await VerifyService.KicccPaymentsVerificationAsync(token, merchantId, referenceId, sha1Key);
                    if (res < 0)
                    {
                        int purchasecartId = Convert.ToInt32(paymentId);
                        var p =
                            db.Carts.Include(x => x.CartProduct)
                            .ThenInclude(x => x.product).FirstOrDefault(x => x.Id == purchasecartId);

                        p.IsPaid = true;
                        p.PayDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        p.CartProduct.ToList().ForEach(x =>
                        {
                            x.product.Count -= x.Count;
                        });
                        db.SaveChanges();
                    }
                    else
                    {
                        //  Verification Failed , your statements Goes here
                    }
                }
            }

            return RedirectToAction("ProductList", "Product");
        }
        public async Task< IActionResult> CartHistory()
        {
            if (User.Identity.IsAuthenticated==true)
            {
                var userId = (await UserManager.FindByNameAsync(User.Identity.Name)).Id;
                return View(db.Carts.Where(x => x.UserId == userId && x.IsPaid==true).ToList());
            }
            else
            {
                TempData["msg"] = ". شما از طریق حساب کاربریتان وارد نشده اید";
                return RedirectToAction("UserLoginRigester", "Account");
            }
        }
        public   IActionResult CartProductContent(int CartId)
        {
            var i = db.CartProducts.Where(x => x.CartId == CartId).ToList();
            return View(db.CartProducts.Include(x=>x.product).ThenInclude(x=>x.group1).Where(x => x.CartId == CartId).ToList());
        }
        public IActionResult DownloadFile(int ProductId)
        {
            var Product = db.Find<Product>(ProductId);
            return File(Product.File, "multipart/form-data", $"{Product.FileName}");
        }
    }
}