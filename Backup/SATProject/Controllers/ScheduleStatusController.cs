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
    public class ScheduleStatusController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /ScheduleStatus/

        public ViewResult Index()
        {
            return View(db.ScheduledClassStatus.ToList());
        }

        //
        // GET: /ScheduleStatus/Details/5

        public ViewResult Details(int id)
        {
            ScheduledClassStatu scheduledclassstatu = db.ScheduledClassStatus.Single(s => s.SCSID == id);
            return View(scheduledclassstatu);
        }

        //
        // GET: /ScheduleStatus/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ScheduleStatus/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(ScheduledClassStatu scheduledclassstatu)
        {
            if (ModelState.IsValid)
            {
                db.ScheduledClassStatus.AddObject(scheduledclassstatu);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(scheduledclassstatu);
        }
        
        //
        // GET: /ScheduleStatus/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ScheduledClassStatu scheduledclassstatu = db.ScheduledClassStatus.Single(s => s.SCSID == id);
            return View(scheduledclassstatu);
        }

        //
        // POST: /ScheduleStatus/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(ScheduledClassStatu scheduledclassstatu)
        {
            if (ModelState.IsValid)
            {
                db.ScheduledClassStatus.Attach(scheduledclassstatu);
                db.ObjectStateManager.ChangeObjectState(scheduledclassstatu, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scheduledclassstatu);
        }

        //
        // GET: /ScheduleStatus/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            ScheduledClassStatu scheduledclassstatu = db.ScheduledClassStatus.Single(s => s.SCSID == id);
            return View(scheduledclassstatu);
        }

        //
        // POST: /ScheduleStatus/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ScheduledClassStatu scheduledclassstatu = db.ScheduledClassStatus.Single(s => s.SCSID == id);
            db.ScheduledClassStatus.DeleteObject(scheduledclassstatu);
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