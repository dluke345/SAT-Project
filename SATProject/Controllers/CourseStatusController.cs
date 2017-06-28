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
    public class CourseStatusController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /CourseStatus/

        public ViewResult Index()
        {
            return View(db.CourseStatus.ToList());
        }

        //
        // GET: /CourseStatus/Details/5

        public ViewResult Details(int id)
        {
            CourseStatu coursestatu = db.CourseStatus.Single(c => c.CSID == id);
            return View(coursestatu);
        }

        //
        // GET: /CourseStatus/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /CourseStatus/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(CourseStatu coursestatu)
        {
            if (ModelState.IsValid)
            {
                db.CourseStatus.AddObject(coursestatu);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(coursestatu);
        }
        
        //
        // GET: /CourseStatus/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            CourseStatu coursestatu = db.CourseStatus.Single(c => c.CSID == id);
            return View(coursestatu);
        }

        //
        // POST: /CourseStatus/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(CourseStatu coursestatu)
        {
            if (ModelState.IsValid)
            {
                db.CourseStatus.Attach(coursestatu);
                db.ObjectStateManager.ChangeObjectState(coursestatu, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coursestatu);
        }

        //
        // GET: /CourseStatus/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            CourseStatu coursestatu = db.CourseStatus.Single(c => c.CSID == id);
            return View(coursestatu);
        }

        //
        // POST: /CourseStatus/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CourseStatu coursestatu = db.CourseStatus.Single(c => c.CSID == id);
            db.CourseStatus.DeleteObject(coursestatu);
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