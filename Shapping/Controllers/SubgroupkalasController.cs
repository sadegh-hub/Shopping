using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shapping.Models;

namespace Shapping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubgroupkalasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subgroupkalas
        public ActionResult Index()
        {
            var subgroupkala = db.Subgroupkala.Include(s => s.Groupkala);
            return View(subgroupkala.ToList());
        }

        // GET: Subgroupkalas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subgroupkala subgroupkala = db.Subgroupkala.Find(id);
            if (subgroupkala == null)
            {
                return HttpNotFound();
            }
            return View(subgroupkala);
        }

        // GET: Subgroupkalas/Create
        public ActionResult Create()
        {
            ViewBag.IDGroup = new SelectList(db.Groupkala, "ID", "Name");
            return View();
        }

        // POST: Subgroupkalas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,IDGroup")] Subgroupkala subgroupkala)
        {
            if (ModelState.IsValid)
            {
                db.Subgroupkala.Add(subgroupkala);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDGroup = new SelectList(db.Groupkala, "ID", "Name", subgroupkala.IDGroup);
            return View(subgroupkala);
        }

        // GET: Subgroupkalas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subgroupkala subgroupkala = db.Subgroupkala.Find(id);
            if (subgroupkala == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDGroup = new SelectList(db.Groupkala, "ID", "Name", subgroupkala.IDGroup);
            return View(subgroupkala);
        }

        // POST: Subgroupkalas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,IDGroup")] Subgroupkala subgroupkala)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subgroupkala).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDGroup = new SelectList(db.Groupkala, "ID", "Name", subgroupkala.IDGroup);
            return View(subgroupkala);
        }

        // GET: Subgroupkalas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subgroupkala subgroupkala = db.Subgroupkala.Find(id);
            if (subgroupkala == null)
            {
                return HttpNotFound();
            }
            return View(subgroupkala);
        }

        // POST: Subgroupkalas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subgroupkala subgroupkala = db.Subgroupkala.Find(id);
            db.Subgroupkala.Remove(subgroupkala);
            db.SaveChanges();
            return RedirectToAction("Index");
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
