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
    public class StudentController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /Student/

        public ViewResult Index(string search = "", string major = "", bool showInactive = false,
            int page = 1)
        {
            var students = db.Students.Include("StudentStatu").ToList();

            //if the search string has a value apply the search
            if (search != "")
            {
                search = search.ToLower();

                students = (from s in students
                           where s.firstName.ToLower().Contains(search) ||
                           s.lastName.ToLower().Contains(search) ||
                           s.major.ToLower().Contains(search) ||
                           s.studentId.ToString().Contains(search)
                           select s).ToList();
            }//end if
            if (major != "")
            {
                students = (from s in students
                           where s.major == major
                           select s).ToList();
            }//end if
            if (showInactive)
            {
                students = (from si in students
                            where si.statusId == 2 ||
                            si.statusId == 3 ||
                            si.statusId == 4 ||
                            si.statusId == 5
                            select si).ToList();
            }
            else
            {
                students = (from si in students
                            where si.statusId == 1
                            select si).ToList();
            }
            //non admins should only see active students
            if (!User.IsInRole("Admin"))
            {
                students = (from s in students
                           where s.statusId == 1
                           select s).ToList();
            }//end if

            //dropdownlist for majors

            //distinct list of major names
            var majors = (from s in db.Students
                         select s.major).Distinct().ToList();
            //add A SELECT LIST to the ViewBag for the DropDown
            ViewBag.Major = new SelectList(majors);

            //search params must be added to the viewbag in order to
            //keep them as we page
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentDept = major;

            int pageSize = 5;
            //make sure page doesn't drop below 1
            page = page <= 0 ? 1 : page;


            return View(students.ToPagedList(page, pageSize));
        }

        //
        // GET: /Student/Details/5

        public ViewResult Details(int id)
        {
            Student student = db.Students.Single(s => s.studentId == id);
            return View(student);
        }

        //
        // GET: /Student/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.statusId = new SelectList(db.StudentStatus, "SSID", "SSName");
            return View();
        } 

        //
        // POST: /Student/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Student student,
            HttpPostedFileBase studentImage)//added for the file upload)
        {
            if (ModelState.IsValid)
            {
                //if the file upload has file
                if (studentImage != null)
                {
                    //get the imagename from the control
                    string imgName = studentImage.FileName;
                    //get the extension using a substring
                    string ext = imgName.Substring(imgName.LastIndexOf("."));
                    //create the new fileName
                    string renameImage = Guid.NewGuid().ToString();
                    //add the extension to the new fileName
                    renameImage += ext;
                    //save the file to the webserver
                    studentImage.SaveAs(Server.MapPath("~/images/students/" +
                        renameImage));
                    //add the property to the Student object
                    student.pictureUrl = renameImage;
                }//end if
                //no file exists
                else
                {
                    //default to nophoto.jpg
                    student.pictureUrl = "nophoto.jpg";
                }//end else
                db.Students.AddObject(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }//end if

            ViewBag.statusId = new SelectList(db.StudentStatus, "SSID", "SSName", student.statusId);
            return View(student);
        }
        
        //
        // GET: /Student/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Student student = db.Students.Single(s => s.studentId == id);
            ViewBag.statusId = new SelectList(db.StudentStatus, "SSID", "SSName", student.statusId);
            return View(student);
        }

        //
        // POST: /Student/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Student student, 
            HttpPostedFileBase studentImage)
        {
            if (ModelState.IsValid)
            {
                //if the file upload has file
                if (studentImage != null)
                {
                    //get the imagename from the control
                    string imgName = studentImage.FileName;
                    //get the extension using a substring
                    string ext = imgName.Substring(imgName.LastIndexOf("."));
                    //create the new fileName
                    string renameImage = Guid.NewGuid().ToString();
                    //add the extension to the new fileName
                    renameImage += ext;
                    //save the file to the webserver
                    studentImage.SaveAs(Server.MapPath("~/images/students/" +
                        renameImage));
                    //add the property to the Student object
                    student.pictureUrl = renameImage;
                }//end if
                //no file exists
                else
                {
                    //retrieve the original image and add it to the product object
                    student.pictureUrl = (from p in db.Students
                                          where p.studentId == student.studentId
                                          select p.pictureUrl).Single();
                }//end else
                db.Students.Attach(student);
                db.ObjectStateManager.ChangeObjectState(student, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }//end if
            ViewBag.statusId = new SelectList(db.StudentStatus, "SSID", "SSName", student.statusId);
            return View(student);
        }

        //
        // GET: /Student/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Student student = db.Students.Single(s => s.studentId == id);
            return View(student);
        }

        //
        // POST: /Student/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Student student = db.Students.Single(s => s.studentId == id);
            db.Students.DeleteObject(student);
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