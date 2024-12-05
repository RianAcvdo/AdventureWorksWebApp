using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdventureWorksWebApp.Models;
using AdventureWorksWebApp.Models.ViewModels;
using AdventureWorksWebApp.Utils;

namespace AdventureWorksWebApp.Views
{
    [Authorize]
    public class AccountController : Controller
    {
        private AdventureWorks_DBEntities db = new AdventureWorks_DBEntities();

        // GET: Account
        public Task<ActionResult> Index()
        {
            return Task.FromResult<ActionResult>(View());
        }

        // GET: Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var foundUser = db.User.Where(u => u.User1.ToLower() == model.Username.ToLower()).FirstOrDefault();

                if (foundUser == null)
                {
                    ViewBag.ErrorMessage = "El usuario o contraseña no son validos.";
                }
                else
                {
                    bool passwordMatches = Utils.Utils.CheckIfPasswordMatches(model.Password, foundUser.Password);

                    if (!passwordMatches)
                    {
                        ViewBag.ErrorMessage = "El usuario o contraseña no son validos.";
                    }

                    return RedirectToLocal(returnUrl);
                }
            }

            return View(model);
        }

        // [GET]: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.User.FirstOrDefault(u => u.User1 == model.Username);

                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "El nombre de usuario ingresado no se encuentra disponible.";
                    return View(model);
                }

                User user = new User
                {
                    User1 = model.Username,
                    Password = Utils.Utils.GetHashedPassword(model.Password),
                    Name = model.Username
                };

                db.User.Add(user);
                db.SaveChanges();

                ViewBag.SuccessMessage = "Registro exitoso. Ya puedes iniciar sesión!";
                
            }

            return View();
        }


        public ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
