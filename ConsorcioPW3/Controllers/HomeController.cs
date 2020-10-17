using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CrearUsuario(FormCollection formCollection)
        {
            Usuario usuario = new Usuario();
            usuario.Email = formCollection["Email"];
            usuario.Password = formCollection["Password"];

            return View("Index");
        }
    }
}