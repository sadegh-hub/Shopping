using Shapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shapping.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.product = db.Productkala.OrderByDescending(x => x.ID).Take(6).ToList();
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [ChildActionOnly]

        public ActionResult _navpartial()
        {
            ViewBag.Groupkalas = db.Groupkala.ToList();
            var model = db.Subgroupkala.ToList();
            return PartialView(model);

        }
    }
}