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
    [Authorize(Roles ="Admin")]
    public class GroupkalasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groupkalas
        public ActionResult Index()
        {
            return View(db.Groupkala.ToList());
        }

        // GET: Groupkalas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupkala groupkala = db.Groupkala.Find(id);
            if (groupkala == null)
            {
                return HttpNotFound();
            }
            return View(groupkala);
        }

        // GET: Groupkalas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groupkalas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Groupkala groupkala)
        {
            if (ModelState.IsValid)
            {
                db.Groupkala.Add(groupkala);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupkala);
        }

        // GET: Groupkalas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupkala groupkala = db.Groupkala.Find(id);
            if (groupkala == null)
            {
                return HttpNotFound();
            }
            return View(groupkala);
        }

        // POST: Groupkalas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Groupkala groupkala)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupkala).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupkala);
        }

        // GET: Groupkalas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupkala groupkala = db.Groupkala.Find(id);
            if (groupkala == null)
            {
                return HttpNotFound();
            }
            return View(groupkala);
        }

        // POST: Groupkalas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Groupkala groupkala = db.Groupkala.Find(id);
            db.Groupkala.Remove(groupkala);
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
