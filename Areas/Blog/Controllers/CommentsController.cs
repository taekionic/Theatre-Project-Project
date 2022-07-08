using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using TheatreCMS3.Areas.Blog.Models;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Blog
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blog/Comments
        public ActionResult Index(Comments comments)
        {
            return View(db.Comments.OrderByDescending(c => c.CommentDate).ToList());
        }

        // GET: Blog/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // GET: Blog/Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Author,Message,CommentDate,Likes,Dislikes")] Comments comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
     
            return View();
        }

        // GET: Blog/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(db.Comments.ToList().OrderByDescending(c => c.CommentDate));
        }

        // POST: Blog/Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Author,Message,CommentDate,Likes,Dislikes")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comments);
        }

        // GET: Blog/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // POST: Blog/Comments/Delete/5
        [HttpPost]
        public JsonResult DeleteConfirmed(int? id)
        {
            Comments comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]

        public JsonResult ldratiobar(int? id)
        {
            Comments comment = db.Comments.Find(id);
            var ratio = comment.likesRatio();
            var result = new JsonResult();
            result.Data = ratio;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLike(int? id)
        {
            Comments comment = db.Comments.Find(id);
            var result = new JsonResult();
            comment.Likes++;
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();
            result.Data = comment;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult GetDislike(int? id)
        {
            Comments comment = db.Comments.Find(id);
            var result = new JsonResult();
            comment.Dislikes++;
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();
            result.Data = comment;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //public async Task<List<Comments>> Edit(int id, bool like)
        //{
        //    var comments = await db.Comments.FindAsync(id);
        //    if (like)
        //    {
        //        comments.Likes++;
        //    }
        //    else
        //    {
        //        comments.Dislikes++;
        //    }
        //    await db.SaveChangesAsync();
        //    return db.Comments.ToList();

        //}
    }
}
