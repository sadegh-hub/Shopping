using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Shapping.Models;

namespace Shapping.Controllers
{
    public class ProfileViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
      

        // GET: ProfileViewModels/Details/5
        public ActionResult Details()
        {
            string CurentuserID = User.Identity.GetUserId();
            ApplicationUser curentuser = db.Users.FirstOrDefault(x => x.Id == CurentuserID);
            var viewmodel = new ProfileViewModels();
            viewmodel.ID = curentuser.Id;
            viewmodel.Address = curentuser.Address;
            viewmodel.Phonenumber = curentuser.PhoneNumber;
            viewmodel.Email = curentuser.Email;
           
            return View(viewmodel);
        }

        // GET: ProfileViewModels/Edit/5
        public ActionResult Edit()
        {

            string CurentuserID = User.Identity.GetUserId();
            ApplicationUser curentuser = db.Users.FirstOrDefault(x => x.Id == CurentuserID);
            var viewmodel = new ProfileViewModels();
            viewmodel.ID = curentuser.Id;
            viewmodel.Address = curentuser.Address;
            viewmodel.Phonenumber = curentuser.PhoneNumber;
            viewmodel.Email = curentuser.Email;

            return View(viewmodel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Address,Phonenumber")] ProfileViewModels profileViewModels)
        {
            string CurentuserID = User.Identity.GetUserId();
            ApplicationUser curentuser = db.Users.FirstOrDefault(x => x.Id == CurentuserID);
            curentuser.PhoneNumber = profileViewModels.Phonenumber;
            curentuser.Address = profileViewModels.Address;
            db.Entry(curentuser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details");
        }

        // GET: ProfileViewModels/Delete/5
      
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
