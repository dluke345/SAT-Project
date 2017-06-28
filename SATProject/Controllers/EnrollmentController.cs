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
    public class EnrollmentController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /Enrollment/

        public ViewResult Index(string search = "", string courseName = "", int page = 1)
        {
            var enrollments = db.Enrollments.Include("ScheduledClass").Include("Student").ToList();
            //if the search string has a value apply the search
            if (search != "")
            {
                search = search.ToLower();

                enrollments = (from c in enrollments
                           where c.ScheduledClass.Cours.courseName.ToLower().Contains(search) ||
                           c.Student.firstName.ToLower().Contains(search) ||
                           c.Student.lastName.ToLower().Contains(search)
                           select c).ToList();
            }//end if
            if (courseName != "")
            {
                enrollments = (from c in enrollments
                           where c.ScheduledClass.Cours.courseName == courseName
                           select c).ToList();
            }//end if
            //dropdownlist for CourseName

            //distinct list of Course names
            var courses = (from c in db.Enrollments
                         select c.ScheduledClass.Cours.courseName).Distinct().ToList();
            //add A SELECT LIST to the ViewBag for the DropDown
            ViewBag.CourseName = new SelectList(courses);

            //search params must be added to the viewbag in order to
            //keep them as we page
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentCourse = courseName;

            int pageSize = 5;
            //make sure page doesn't drop below 1
            page = page <= 0 ? 1 : page;

            return View(enrollments.ToPagedList(page, pageSize));
        }

        //
        // GET: /Enrollment/Details/5

        public ViewResult Details(int id)
        {
            Enrollment enrollment = db.Enrollments.Single(e => e.enrollmentId == id);
            return View(enrollment);
        }

        //
        // GET: /Enrollment/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.scheduledClassId = new SelectList(GetClassDdlItems(), "ID", "Name");

            //student ddl

            ViewBag.studentId = new SelectList(GetStudentDdlItems(), "ID", "Name");
            return View();
        }

        private List<DropDownItem> GetStudentDdlItems()
        {
            var students = (from s in db.Students.ToList()
                            orderby s.lastName
                            where s.statusId == 1
                            select new DropDownItem()
                            {
                                ID = s.studentId,
                                Name = string.Format(
                                "{0}, {1} - ID:{2}",
                                s.lastName, s.firstName, s.studentId)
                            }).ToList();
            return students;
        }

        private List<DropDownItem> GetClassDdlItems()
        {
            var classes = (from c in db.ScheduledClasses.ToList()
                           orderby c.startDate descending
                           where c.statusId != 3
                           select new DropDownItem()
                           {
                               ID = c.scheduledClassId,
                               //Name is what will show in the DDL
                               Name = string.Format(
                               "{0} - {1:d}", c.Cours.courseName, c.startDate)
                           }).ToList();
            return classes;
        } 

        //
        // POST: /Enrollment/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.AddObject(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.scheduledClassId = new SelectList(GetClassDdlItems(), "ID", "Name", enrollment.scheduledClassId);
            ViewBag.studentId = new SelectList(GetStudentDdlItems(), "ID", "Name", enrollment.studentId);
            return View(enrollment);
        }
        
        //
        // GET: /Enrollment/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Enrollment enrollment = db.Enrollments.Single(e => e.enrollmentId == id);
            ViewBag.scheduledClassId = new SelectList(GetClassDdlItems(), "ID", "Name", enrollment.scheduledClassId);
            ViewBag.studentId = new SelectList(GetStudentDdlItems(), "ID", "Name", enrollment.studentId);
            return View(enrollment);
        }

        //
        // POST: /Enrollment/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Attach(enrollment);
                db.ObjectStateManager.ChangeObjectState(enrollment, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.scheduledClassId = new SelectList(GetClassDdlItems(), "ID", "Name", enrollment.scheduledClassId);
            ViewBag.studentId = new SelectList(GetStudentDdlItems(), "ID", "Name", enrollment.studentId);
            return View(enrollment);
        }

        //
        // GET: /Enrollment/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Enrollment enrollment = db.Enrollments.Single(e => e.enrollmentId == id);
            return View(enrollment);
        }

        //
        // POST: /Enrollment/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Enrollment enrollment = db.Enrollments.Single(e => e.enrollmentId == id);
            db.Enrollments.DeleteObject(enrollment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }//end controller
    public class DropDownItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}//end namespace