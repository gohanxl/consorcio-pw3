using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpPost]
        public ActionResult CrearUsuario(FormCollection formCollection)
        {
            Usuario usuario = new Usuario();
            usuario.Email = formCollection["Email"];
            usuario.Password = formCollection["Password"];
            usuario.FechaRegistracion = DateTime.Now;

            usuarioService.Insert(usuario);

            return View("Index");
        }
    }
}