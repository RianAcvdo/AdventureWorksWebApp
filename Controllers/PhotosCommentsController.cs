using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdventureWorksWebApp.Models;

namespace AdventureWorksWebApp.Controllers
{
    [Authorize]
    public class PhotosCommentsController : Controller
    {
        private AdventureWorks_DBEntities db = new AdventureWorks_DBEntities();

        // GET: PhotosComments/New/5
        public ActionResult New(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            Comment comment = new Comment();
            comment.PhotoID = photo.PhotoID;
            return View(comment);
        }

        // POST: PhotosComments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentID,User,Subject,Body,PhotoID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.User = User.Identity.Name;
                Photo photo = db.Photo.Find(comment.PhotoID);
                if(photo == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                photo.Comment.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index", "Photos");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: PhotosComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            if(comment.User != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Comment.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index", "Photos");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
