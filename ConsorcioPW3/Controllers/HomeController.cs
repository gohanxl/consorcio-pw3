using Repositories;
using Services;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;

namespace ConsorcioPW3.Controllers
{
    public class HomeController : Controller
    {
        ConsortiumContext context;
        UsuarioService usuarioService;

        public HomeController()
        {
            context = new ConsortiumContext();
            usuarioService = new UsuarioService(context);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuario user)
        {

            if (usuarioService.EmailExist(user.Email))
            {
                ModelState.AddModelError("Email", "El email ya se encuentra uso, pruebe utilizando otro");
                return View();
            }

            user.FechaRegistracion = DateTime.Now;

            usuarioService.Insert(user);

            return RedirectToAction("Login");

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario user, string ReturnUrl)
        {
            
            Usuario userFound = usuarioService.IsUserValid(user.Email, user.Password);

            if (userFound == null)
            {
                ModelState.AddModelError("Error", "Usuario o contraseña invalidos");
                return View();
            }

            UpdateLastLogin(userFound);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userFound.Email, DateTime.Now, DateTime.Now.AddMinutes(10), true, userFound.IdUsuario.ToString());
            String encrypt = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie("SESSION", encrypt);

            Response.Cookies.Add(cookie);

            if(!ReturnUrl.IsEmpty())
            {
                return Redirect(ReturnUrl);
            }
            return Redirect("/Bienvenido");

        }

        public ActionResult Logout()
        {
            HttpContext.User = new GenericPrincipal(new GenericIdentity(""), null);
            FormsAuthentication.SignOut();
            HttpCookie cookie = Request.Cookies["SESSION"];
            cookie.Expires = DateTime.Now.AddDays(-1D);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }
        
        private void UpdateLastLogin(Usuario user)
        {
            user.FechaUltLogin = DateTime.Now;
            usuarioService.Update(user);
        }
    }
}