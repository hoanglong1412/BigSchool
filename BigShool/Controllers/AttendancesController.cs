using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BigShool.Models;
using Microsoft.AspNet.Identity;

namespace BigShool.Controllers
{
    public class AttendancesController : ApiController
    {
        MyDBContext db = new MyDBContext();
        [HttpPost]
        public IHttpActionResult Attend(Course attenC)
        {
            var userID = User.Identity.GetUserId();
            if(db.Attendance.Any(m=>m.Attendee == userID && m.CourseId == attenC.Id))
            {
                //return BadRequest("The attendaces already exist");  
                db.Attendance.Remove(db.Attendance.SingleOrDefault(p =>
                p.Attendee == userID && p.CourseId == attenC.Id));
                db.SaveChanges();
                return Ok("cancel");
            }
            var attendance = new Attendance()
            {
                CourseId = attenC.Id,
                Attendee = User.Identity.GetUserId()
            };
            db.Attendance.Add(attendance);
            db.SaveChanges();
            return Ok();
        }
    }
}
