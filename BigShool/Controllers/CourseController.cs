using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigShool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BigShool.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Create()
        {
            MyDBContext db = new MyDBContext();

            Course course = new Course();
            course.ListCategory = db.Category.ToList();
            return View(course);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            MyDBContext db = new MyDBContext();

            ModelState.Remove("LecturerId");
            if (!ModelState.IsValid)
            {
                course.ListCategory = db.Category.ToList();
                return View("Create", course);
            }

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            course.LecturerId = user.Id;

            db.Course.Add(course);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Attending()
        {
            MyDBContext db = new MyDBContext();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var listAtten = db.Attendance.Where(m => m.Attendee == user.Id).ToList();
            var courses = new List<Course>();
            foreach(Attendance tmp in listAtten)
            {
                Course obj = tmp.Course;
                courses.Add(obj);
            }
            return View(courses);
        }

        public ActionResult Mine()
        {
            MyDBContext db = new MyDBContext();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var courses = db.Course.Where(m => m.DateTime > DateTime.Now && m.LecturerId == user.Id).ToList();
 
            return View(courses);
        }
    }
}