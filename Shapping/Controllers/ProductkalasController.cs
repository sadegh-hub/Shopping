using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using Shapping.Models;

namespace Shapping.Controllers
{
    public class ProductkalasController : Controller
    {
        private ApplicationUserManager _userManager;
        public ProductkalasController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
          
        }
        public ProductkalasController()
        {
           

        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Productkalas
        public ActionResult Index(int Groupkalas ,int? Subgroupkalas,int? page,string sortoption,int? minprice, int? maxprice)
        {
            var pageNamber = page ?? 1;
            var productkala = db.Productkala.Include(p => p.Subgroupkala);
            if (Subgroupkalas==null)
            {
                productkala = productkala.Where(x => x.Subgroupkala.IDGroup == Groupkalas);

            }
            else
            {
                productkala = productkala.Where(x => x.IDSubGroup == Subgroupkalas);
            }
            if (!string.IsNullOrEmpty(sortoption))
            {
                if (sortoption=="1")
                {
                    productkala = productkala.OrderBy(x => x.Price);
                }
                if(sortoption=="2"){
                    productkala = productkala.OrderByDescending(x => x.Price);

                }
                


            }
            if (minprice !=null)
            {
                productkala = productkala.Where(x => x.Price >= minprice).OrderBy(x => x.ID);
            }
            if (maxprice!=null)
            {
                productkala = productkala.Where(x => x.Price <= maxprice).OrderBy(x => x.ID);
            }
            else
            {
                productkala = productkala.OrderBy(x => x.ID);

            }
            return View(productkala.ToPagedList(pageNamber,4));
        }

        // GET: Productkalas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productkala productkala = db.Productkala.Find(id);
            if (productkala == null)
            {
                return HttpNotFound();
            }
            return View(productkala);
        }

        // GET: Productkalas/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IDSubGroup = new SelectList(db.Subgroupkala, "ID", "Name");
            return View();
        }

        // POST: Productkalas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Name,Price,Image,Discription,IDSubGroup")] Productkala productkala,HttpPostedFileBase productimage)
        {
            if (productimage!=null)
            {
                var allowedextensions = new[]
                {
                    ".jpg",".png"
                };
                var filename = Path.GetFileName(productimage.FileName);
                var ext = Path.GetExtension(productimage.FileName);
                if (allowedextensions.Contains(ext))
                {
                    var path = Path.Combine(Server.MapPath("~/image/"), filename);
                    productimage.SaveAs(path);
                    productkala.Image = "../../../../../image/"+filename;

                }
            }
            if (ModelState.IsValid)
            {
                db.Productkala.Add(productkala);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDSubGroup = new SelectList(db.Subgroupkala, "ID", "Name", productkala.IDSubGroup);
            return View(productkala);
        }

        // GET: Productkalas/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productkala productkala = db.Productkala.Find(id);
            if (productkala == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDSubGroup = new SelectList(db.Subgroupkala, "ID", "Name", productkala.IDSubGroup);
            return View(productkala);
        }

        // POST: Productkalas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Image,Discription,IDSubGroup")] Productkala productkala, HttpPostedFileBase productimage,string imagebeforedit)
        {


            if (ModelState.IsValid)
            {
                if (productimage != null)
                {
                    var allowedextensions = new[]
                    {
                    ".jpg",".png"
                };
                    var filename = Path.GetFileName(productimage.FileName);
                    var ext = Path.GetExtension(productimage.FileName);
                    if (allowedextensions.Contains(ext))
                    {
                        var path = Path.Combine(Server.MapPath("~/image"), filename);
                        productimage.SaveAs(path);
                        productkala.Image = "../../../../../image/" + filename;

                    }
                }
                else
                {
                    productkala.Image = imagebeforedit;
                }

                db.Entry(productkala).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDSubGroup = new SelectList(db.Subgroupkala, "ID", "Name", productkala.IDSubGroup);
            return View(productkala);
        }

        // GET: Productkalas/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productkala productkala = db.Productkala.Find(id);
            if (productkala == null)
            {
                return HttpNotFound();
            }
            return View(productkala);
        }

        // POST: Productkalas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Productkala productkala = db.Productkala.Find(id);
            db.Productkala.Remove(productkala);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddToCart(int pid)
        {
            
            
                var product = db.Productkala.Find(pid);

            
            
            
            if (Session["ShappingCart"]==null)
            {
                List<ShappingcartViewModels> product_list = new List<ShappingcartViewModels>();
                product_list.Add(new ShappingcartViewModels {product=product,Quntity=1 });
                Session["ShappingCart"] = product_list;

            }
            else
            {
                bool exist = false;
                var product_list = Session["ShappingCart"] as List<ShappingcartViewModels>;
                foreach (var item in product_list)
                {
                    if (item.product.ID==product.ID)
                    {
                        item.Quntity = item.Quntity + 1;
                        exist = true;

                    }

                }
                if (!exist)
                {
                    product_list.Add(new ShappingcartViewModels { product = product, Quntity = 1 });
                }
                
                Session["ShappingCart"] = product_list;
            }

            return RedirectToAction("shappingcart");
        }
        public ActionResult shappingcart( string status)
        {
            int totalprice = 0;
            List<ShappingcartViewModels> product_list = new List<ShappingcartViewModels>();
            if (Session["ShappingCart"] !=null)
            {
                product_list = Session["ShappingCart"] as List<ShappingcartViewModels>;
                foreach (var item in product_list)
                {
                    totalprice = totalprice + (item.product.Price * item.Quntity);

                }

            }
            ViewBag.status = status;
            ViewBag.totalprice = totalprice;
           
            return View(product_list);
        }
        public ActionResult DeleteFormCart(int pid)
        {

            var product_list = Session["ShappingCart"] as List<ShappingcartViewModels>;
            var product = product_list.Where(x => x.product.ID == pid).FirstOrDefault();
            product_list.Remove(product);
            Session["ShappingCart"] = product_list;
            return RedirectToAction("shappingcart");
        }
        public ActionResult increasequntity(int pid)
        {
            var product_list = Session["ShappingCart"] as List<ShappingcartViewModels>;
            foreach (var item in product_list)
            {
                if (item.product.ID==pid)
                {
                    item.Quntity=item.Quntity + 1;

                }

            }
            Session["ShappingCart"] = product_list;
            return RedirectToAction("shappingcart");
        }
        public ActionResult decreasequntity(int pid)
        {
            var product_list = Session["ShappingCart"] as List<ShappingcartViewModels>;
            foreach (var item in product_list)
            {
                if (item.product.ID == pid)
                {
                    if (item.Quntity>1)
                    {
                        item.Quntity = item.Quntity - 1;
                    }
                    else
                    {
                        var shappingCartitem = product_list.Where(x => x.product.ID== pid).FirstOrDefault();
                        product_list.Remove(shappingCartitem);
                        break;

                    }
                  

                }

            }
            Session["ShappingCart"] = product_list;

            return RedirectToAction("shappingcart");
        }
        [ChildActionOnly]
        public ActionResult itemCount()
        {
            int Cartitemcount = 0;
            if (Session["ShappingCart"] !=null)
            {
                var product_list = Session["ShappingCart"] as List<ShappingcartViewModels>;
                Cartitemcount = product_list.Count();


            }
            ViewBag.count = Cartitemcount;
            return PartialView();
        }
        public ActionResult submit_order(string payment_method)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("shappingcart",new {status="برای نهای کردن خرید لطفا در سایت عضو شوید یا وارد حساب کاربری خود شوید" });
            }
            var user = UserManager.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var product_list=Session["ShappingCart"] as List<ShappingcartViewModels>;
            Order order = new Order();
            order.UserName = User.Identity.Name;
            order.PhoneNumber = user.PhoneNumber;
            order.address = user.Address;
            order.Isdelivered = false;
            int totalprice = 0;
            foreach (var item in product_list)
            {
                totalprice = totalprice + (item.product.Price * item.Quntity);

            }
            order.TotalPrice = totalprice;
            if (payment_method== "Online")
            {
                ViewBag.pay = "پرداخت انلاین";
            }
            if (payment_method=="Cash")
            {
                order.Ispayed = false;
                ViewBag.pay = "پرداخت درب منزل";

            }
            db.Order.Add(order);
            db.SaveChanges();
            List<OrdereDetail> orderDetails_list = new List<OrdereDetail>();
            foreach (var item in product_list)
            {
                orderDetails_list.Add(new OrdereDetail { OrderId = order.ID, ProductID = item.product.ID, Quntity = item.Quntity });
               
            }
            db.OrdereDetails.AddRange(orderDetails_list);
            db.SaveChanges();
            ViewBag.orderid = order.ID;
            return View();


        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
