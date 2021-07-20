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
            var userID = User.Identity.GetUserId();
            foreach (Course i in upcomingCourse)

            {
                //tìm Name của user từ lectureid
                ApplicationUser user =

                System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(
                ).FindById(i.LecturerId);
                i.Name = user.Name;
                //lấy ds tham gia khóa học
                if (userID != null)

                {
                    i.isLogin = true;
                    //ktra user đó chưa tham gia khóa học

                    Attendance find = db.Attendance.FirstOrDefault(p =>

                    p.CourseId == i.Id && p.Attendee == userID);
                    if (find == null)
                        i.isShowGoing = true;
                    //ktra user đã theo dõi giảng viên của khóa học ?

                    Following findFollow = db.Following.FirstOrDefault(p =>

                    p.FollowerId == userID && p.ForlloweeId == i.LecturerId);

                    if (findFollow == null)
                        i.isShowFollow = true;
                }
            }
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