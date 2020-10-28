using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    public class HomeController : Controller
    {
        UsuarioService<Usuario> usuarioService = new UsuarioService<Usuario>();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(FormCollection formCollection)
        {

            Usuario usuario = new Usuario();
            var userEmail = formCollection["Email"];

            if (usuarioService.GetByEmail(userEmail))
            {
                ModelState.AddModelError("Email", "El mail ya se encuentra uso, pruebe utilizando otro");
                return View("Register");
            }
            else
            {
                usuario.Password = formCollection["Password"];
                usuario.FechaRegistracion = DateTime.Now;

                usuarioService.Insert(usuario);

                return View("Index");
            }

        }

        [HttpPost]
        public ActionResult Login(FormCollection formCollection)
        {
            string userEmail = formCollection["Email"];
            string userPassword = formCollection["Password"];

            bool isUserValid = usuarioService.IsUserValid(userEmail, userPassword);

            if (!isUserValid)
            {
                ModelState.AddModelError("Email", "El usuario no es valido, intente de nuevo");
                return View("Index");
            }
            else
            {
                return Redirect("/Bienvenido");
            }

        }

    }
}