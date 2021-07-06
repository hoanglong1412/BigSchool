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
   
    public class HomeController : Controller
    {
        MyDBContext db = new MyDBContext();
        public ActionResult Index()
        {
            IEnumerable<Course> upcomingCourse = db.Course.Where(m => m.DateTime > DateTime.Now).OrderBy(m => m.DateTime);
            return View(upcomingCourse);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}