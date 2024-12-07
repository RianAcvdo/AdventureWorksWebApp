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
    public class CommentsController : Controller
    {
        private AdventureWorks_DBEntities db = new AdventureWorks_DBEntities();

        // GET: Comments
        public ActionResult Index()
        {
            var comment = db.Comment.Include(c => c.User1);
            return View(comment.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
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
            return View(comment);
        }

        // GET: PhotosComments/New/5
        public ActionResult NewComment(int? id)
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

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.PhotoID = new SelectList(db.Photo, "PhotoID", "Title");
            ViewBag.User = new SelectList(db.User, "User1", "Name");
            return View();
        }

        // POST: Comments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentID,User,Subject,Body,PhotoID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comment.Add(comment);
                db.SaveChanges();
                /*return RedirectToAction("Index");*/
                string mensaje = String.Format("../Photos/Details/{0}", comment.PhotoID);
                return RedirectToAction(mensaje);
            }

            ViewBag.PhotoID = new SelectList(db.Photo, "PhotoID", "Title", comment.PhotoID);
            ViewBag.User = new SelectList(db.User, "User1", "Name", comment.User);
            return View(comment);
            
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.PhotoID = new SelectList(db.Photo, "PhotoID", "Title", comment.PhotoID);
            ViewBag.User = new SelectList(db.User, "User1", "Name", comment.User);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,User,Subject,Body,PhotoID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            ViewBag.PhotoID = new SelectList(db.Photo, "PhotoID", "Title", comment.PhotoID);
            ViewBag.User = new SelectList(db.User, "User1", "Name", comment.User);
            return View(comment);
        }

        // GET: Comments/Delete/5
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
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comment.Find(id);
            db.Comment.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult GetImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var imgByte = db.Photo.Find(id).PhotoFile;

            if (imgByte == null)
            {
                return HttpNotFound();
            }
            return new FileContentResult(imgByte, "image/png");
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
