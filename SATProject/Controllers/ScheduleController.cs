using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SATProject;
using PagedList;

namespace SATProject.Controllers
{ 
    public class ScheduleController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /Schedule/

        public ViewResult Index(string search = "", string instructorName = "", bool showInactive = false,
            int page = 1)
        {
            var scheduledclasses = db.ScheduledClasses.Include("Cours").Include("ScheduledClassStatu").ToList();

            if (search != "")
            {
                search = search.ToLower();

                scheduledclasses = (from c in scheduledclasses
                           where c.Cours.courseName.ToLower().Contains(search) ||
                           c.instructorName.ToLower().Contains(search)
                           select c).ToList();
            }//end if
            if (instructorName != "")
            {
                scheduledclasses = (from c in scheduledclasses
                                    where c.instructorName == instructorName
                                    select c).ToList();
            }//end if
            if (showInactive)
            {
                scheduledclasses = (from c in scheduledclasses
                           where c.statusId == 3 ||
                           c.statusId == 2
                           select c).ToList();
            }
            else
            {
                scheduledclasses = (from c in scheduledclasses
                           where c.statusId == 1
                           select c).ToList();
            }
            //non admins should only see active courses
            if (!User.IsInRole("Admin"))
            {
                scheduledclasses = (from c in scheduledclasses
                           where c.statusId == 1
                           select c).ToList();
            }//end if

            //dropdownlist for department

            //distinct list of department names
            var instructors = (from c in db.ScheduledClasses
                                  select c.instructorName).Distinct().ToList();
            //add A SELECT LIST to the ViewBag for the DropDown
            ViewBag.instructorName = new SelectList(instructors);

            //search params must be added to the viewbag in order to
            //keep them as we page
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentDept = instructorName;

            int pageSize = 5;
            //make sure page doesn't drop below 1
            page = page <= 0 ? 1 : page;

            return View(scheduledclasses.ToPagedList(page, pageSize));
        }

        //
        // GET: /Schedule/Details/5

        public ViewResult Details(int id)
        {
            ScheduledClass scheduledclass = db.ScheduledClasses.Single(s => s.scheduledClassId == id);
            return View(scheduledclass);
        }

        //
        // GET: /Schedule/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName");
            ViewBag.statusId = new SelectList(db.ScheduledClassStatus, "SCSID", "SCSName");
            return View();
        } 

        //
        // POST: /Schedule/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(ScheduledClass scheduledclass)
        {
            if (ModelState.IsValid)
            {
                db.ScheduledClasses.AddObject(scheduledclass);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", scheduledclass.courseId);
            ViewBag.statusId = new SelectList(db.ScheduledClassStatus, "SCSID", "SCSName", scheduledclass.statusId);
            return View(scheduledclass);
        }
        
        //
        // GET: /Schedule/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ScheduledClass scheduledclass = db.ScheduledClasses.Single(s => s.scheduledClassId == id);
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", scheduledclass.courseId);
            ViewBag.statusId = new SelectList(db.ScheduledClassStatus, "SCSID", "SCSName", scheduledclass.statusId);
            return View(scheduledclass);
        }

        //
        // POST: /Schedule/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(ScheduledClass scheduledclass)
        {
            if (ModelState.IsValid)
            {
                db.ScheduledClasses.Attach(scheduledclass);
                db.ObjectStateManager.ChangeObjectState(scheduledclass, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", scheduledclass.courseId);
            ViewBag.statusId = new SelectList(db.ScheduledClassStatus, "SCSID", "SCSName", scheduledclass.statusId);
            return View(scheduledclass);
        }

        //
        // GET: /Schedule/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            ScheduledClass scheduledclass = db.ScheduledClasses.Single(s => s.scheduledClassId == id);
            return View(scheduledclass);
        }

        //
        // POST: /Schedule/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ScheduledClass scheduledclass = db.ScheduledClasses.Single(s => s.scheduledClassId == id);
            db.ScheduledClasses.DeleteObject(scheduledclass);
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