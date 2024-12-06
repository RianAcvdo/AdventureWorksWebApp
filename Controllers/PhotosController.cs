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
    public class PhotosController : Controller
    {
        private AdventureWorks_DBEntities db = new AdventureWorks_DBEntities();

        // GET: Photos
        public ActionResult Index()
        {
            var photo = db.Photo.Include(p => p.User);
            return View(photo.ToList());
        }


        // GET: Photos
        public ActionResult LatestPhotos()
        {
            var photo = db.Photo.Include(p => p.User);
            photo = photo.OrderByDescending(r => r.CreatedDate).Take(4);
            return View(photo.ToList());
        }
        public ActionResult Carousel(int? id)
        {
            var photo = db.Photo.Include(p => p.User);
            return View(photo.ToList());
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
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
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            ViewBag.Owner = new SelectList(db.User, "User1", "Name");
            return View();
        }

        // POST: Photos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "PhotoID,Title,PhotoFile,Description,CreatedDate,Owner")] Photo photo, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid && imgFile != null)
            {
                var imgByte = new byte[imgFile.ContentLength];
                imgFile.InputStream.Read(imgByte, 0, imgFile.ContentLength);
                photo.PhotoFile = imgByte;
                db.Photo.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Owner = new SelectList(db.User, "User1", "Name", photo.Owner);
            return View(photo);
        }

        [Authorize]
        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Owner = new SelectList(db.User, "User1", "Name", photo.Owner);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoID,Title,PhotoFile,Description,CreatedDate,Owner")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Owner = new SelectList(db.User, "User1", "Name", photo.Owner);
            return View(photo);
        }



        // GET: Photos/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
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
            return View(photo);
        }

        [Authorize]
        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photo.Find(id);
            db.Photo.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveImagen(int? id, HttpPostedFileBase imgFile)
        {
            Photo photo = db.Photo.Find(id);

            if (imgFile != null)
            {
                var imgByte = new byte[imgFile.ContentLength];
                imgFile.InputStream.Read(imgByte, 0, imgFile.ContentLength);
                photo.PhotoFile = imgByte;
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photo);
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
