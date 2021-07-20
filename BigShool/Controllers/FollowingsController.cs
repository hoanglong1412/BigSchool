using BigShool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigShool.Controllers
{
    public class FollowingsController : ApiController
    {
        MyDBContext db = new MyDBContext();
        [HttpPost]
        public IHttpActionResult Follow(Following follow)
        {
            var userID = User.Identity.GetUserId();
            if (userID == null)
                return BadRequest("Please login first");
            if(userID== follow.ForlloweeId)
                return BadRequest("Can not follow myself!!!");
            Following find = db.Following.FirstOrDefault(m => m.FollowerId == userID && m.ForlloweeId == follow.ForlloweeId);
            if (find != null)
            {
                //return BadRequest("The already following exist");

                db.Following.Remove(db.Following.SingleOrDefault(p =>
                p.FollowerId == userID && p.ForlloweeId == follow.ForlloweeId));
                db.SaveChanges();
                return Ok("cancel");
            }    

            follow.FollowerId = userID;
            db.Following.Add(follow);
            db.SaveChanges();

            return Ok();
        }
    }
}
