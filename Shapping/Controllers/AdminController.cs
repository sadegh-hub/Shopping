using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Shapping.Models;

namespace Shapping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int Groupkalas, int? Subgroupkalas, int? page, string sortoption, int? minprice, int? maxprice)
        {
            var pageNamber = page ?? 1;
            var productkala = db.Productkala.Include(p => p.Subgroupkala);
            if (Subgroupkalas == null)
            {
                productkala = productkala.Where(x => x.Subgroupkala.IDGroup == Groupkalas);

            }
            else
            {
                productkala = productkala.Where(x => x.IDSubGroup == Subgroupkalas);
            }
            if (!string.IsNullOrEmpty(sortoption))
            {
                if (sortoption == "1")
                {
                    productkala = productkala.OrderBy(x => x.Price);
                }
                if (sortoption == "2")
                {
                    productkala = productkala.OrderByDescending(x => x.Price);

                }



            }
            if (minprice != null)
            {
                productkala = productkala.Where(x => x.Price >= minprice).OrderBy(x => x.ID);
            }
            if (maxprice != null)
            {
                productkala = productkala.Where(x => x.Price <= maxprice).OrderBy(x => x.ID);
            }
            else
            {
                productkala = productkala.OrderBy(x => x.ID);

            }
            return View(productkala.ToPagedList(pageNamber, 4));
        }

    }
}