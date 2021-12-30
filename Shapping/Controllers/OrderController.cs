using Shapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shapping.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Order]\
        [Authorize]
        public ActionResult Index()
        {
           
            var userorder = db.Order.Where(x => x.UserName == User.Identity.Name).ToList();
            return View(userorder);
        }
        public ActionResult Details(int orderid)
        {

            var userorder = db.OrdereDetails.Where(x => x.OrderId== orderid).ToList();
            return View(userorder);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Admin_Index()
        {
            var userorder = db.Order.ToList();
            return View(userorder);
        }
        public ActionResult Admin_Details(int orderid)
        {
            var userorder = db.OrdereDetails.Where(x => x.OrderId == orderid).ToList();
            return View(userorder);
        }
    }
}