using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SATProject;

namespace SATProject.Controllers
{ 
    public class StudentStatusController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /StudentStatus/

        public ViewResult Index()
        {
            return View(db.StudentStatus.ToList());
        }

        //
        // GET: /StudentStatus/Details/5

        public ViewResult Details(int id)
        {
            StudentStatu studentstatu = db.StudentStatus.Single(s => s.SSID == id);
            return View(studentstatu);
        }

        //
        // GET: /StudentStatus/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /StudentStatus/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(StudentStatu studentstatu)
        {
            if (ModelState.IsValid)
            {
                db.StudentStatus.AddObject(studentstatu);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(studentstatu);
        }
        
        //
        // GET: /StudentStatus/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            StudentStatu studentstatu = db.StudentStatus.Single(s => s.SSID == id);
            return View(studentstatu);
        }

        //
        // POST: /StudentStatus/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(StudentStatu studentstatu)
        {
            if (ModelState.IsValid)
            {
                db.StudentStatus.Attach(studentstatu);
                db.ObjectStateManager.ChangeObjectState(studentstatu, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentstatu);
        }

        //
        // GET: /StudentStatus/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            StudentStatu studentstatu = db.StudentStatus.Single(s => s.SSID == id);
            return View(studentstatu);
        }

        //
        // POST: /StudentStatus/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            StudentStatu studentstatu = db.StudentStatus.Single(s => s.SSID == id);
            db.StudentStatus.DeleteObject(studentstatu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}