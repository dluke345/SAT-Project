using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SATProject;
using PagedList;//for the pagedlist

namespace SATProject.Controllers
{ 
    public class CourseController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /Course/

        public ViewResult Index(string search = "", string department = "", bool showInactive = false,
            int page = 1)
        {
            var courses = db.Courses.Include("CourseStatu").ToList();

            //if the search string has a value apply the search
            if (search != "")
            {
                search = search.ToLower();

                courses = (from c in courses
                          where c.courseName.ToLower().Contains(search) ||
                          c.courseDescription.ToLower().Contains(search) ||
                          c.department.ToLower().Contains(search)
                          select c).ToList();
            }//end if
            if (department != "")
            {
                courses = (from c in courses
                          where c.department == department
                          select c).ToList();
            }//end if
            if (showInactive)
            {
                courses = (from c in courses
                           where c.statusId == 2
                           select c).ToList();
            }
            else
            {
                courses = (from c in courses
                           where c.statusId == 1
                           select c).ToList();
            }
            //non admins should only see active courses
            if (!User.IsInRole("Admin"))
            {
                courses = (from c in courses
                          where c.statusId == 1
                          select c).ToList();
            }//end if

            //dropdownlist for department

            //distinct list of department names
            var depts = (from c in db.Courses
                        select c.department).Distinct().ToList();
            //add A SELECT LIST to the ViewBag for the DropDown
            ViewBag.Department = new SelectList(depts);

            //search params must be added to the viewbag in order to
            //keep them as we page
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentDept = department;

            int pageSize = 5;
            //make sure page doesn't drop below 1
            page = page <= 0 ? 1 : page;

            //return a topagedlist instead of a tolist
            return View(courses.ToPagedList(page, pageSize));
        }

        //
        // GET: /Course/Details/5

        public ViewResult Details(int id)
        {
            Cours cours = db.Courses.Single(c => c.courseId == id);
            return View(cours);
        }

        //
        // GET: /Course/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.statusId = new SelectList(db.CourseStatus, "CSID", "CSName");
            return View();
        } 

        //
        // POST: /Course/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.AddObject(cours);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.statusId = new SelectList(db.CourseStatus, "CSID", "CSName", cours.statusId);
            return View(cours);
        }
        
        //
        // GET: /Course/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Cours cours = db.Courses.Single(c => c.courseId == id);
            ViewBag.statusId = new SelectList(db.CourseStatus, "CSID", "CSName", cours.statusId);
            return View(cours);
        }

        //
        // POST: /Course/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Attach(cours);
                db.ObjectStateManager.ChangeObjectState(cours, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.statusId = new SelectList(db.CourseStatus, "CSID", "CSName", cours.statusId);
            return View(cours);
        }

        //
        // GET: /Course/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Cours cours = db.Courses.Single(c => c.courseId == id);
            return View(cours);
        }

        //
        // POST: /Course/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Cours cours = db.Courses.Single(c => c.courseId == id);
            cours.statusId = 2;
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