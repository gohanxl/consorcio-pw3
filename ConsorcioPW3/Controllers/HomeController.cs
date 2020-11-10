using Repositories;
using Services;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

            if (usuarioService.EmailExist(userEmail))
            {
                ModelState.AddModelError("Email", "El email ya se encuentra uso, pruebe utilizando otro");
                return View("Register");
            }

            usuario.Email = userEmail;

            usuario.Password = formCollection["Password"];

            usuario.FechaRegistracion = DateTime.Now;

            usuarioService.Insert(usuario);

            return View("Index");

        }

        [HttpPost]
        public ActionResult Login(FormCollection formCollection)
        {
            string userEmail = formCollection["Email"];
            string userPassword = formCollection["Password"];
            
            Usuario userFound = usuarioService.IsUserValid(userEmail, userPassword);

            if (userFound == null)
            {
                ModelState.AddModelError("Error", "Usuario o contraseña invalidos");
                return View("Index");
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userFound.Email, DateTime.Now, DateTime.Now.AddMinutes(10), true, userFound.IdUsuario.ToString());
            String encrypt = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie("SESSION", encrypt);

            Response.Cookies.Add(cookie);

            return Redirect("/Bienvenido");

        }

        public ActionResult Logout()
        {
            HttpContext.User = new GenericPrincipal(new GenericIdentity(""), null);
            FormsAuthentication.SignOut();
            HttpCookie cookie = Request.Cookies["SESSION"];
            cookie.Expires = DateTime.Now.AddDays(-1D);
            Response.Cookies.Add(cookie);

            return View("Index");
        }

    }
}