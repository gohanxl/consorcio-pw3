using ConsorcioPW3.Helpers;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    [Authorize]
    public class ConsorciosController : Controller
    {
        ConsortiumContext context;
        ConsorcioService consorcioService;
        ProvinciaService provinciaService;
        UnidadService unidadService;
        UsuarioService usuarioService;

        public ConsorciosController()
        {
            context = new ConsortiumContext();
            consorcioService = new ConsorcioService(context);
            provinciaService = new ProvinciaService(context);
            unidadService = new UnidadService(context);
            usuarioService = new UsuarioService(context);
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            Usuario user = GetUser();
            List<Consorcio> consorcios = consorcioService.GetAllByUser(user.IdUsuario);
            return View(consorcios);
        }

        public ActionResult Add()
        {
            CargarListasEnViewBag();
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "save")]
        public ActionResult Add(Consorcio consorcio)
        {
            if (ModelState.IsValid)
            {
                InsertConsorcio(consorcio);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "consorcio")]
        public ActionResult AddAndCreateConsorcio(Consorcio consorcio)
        {
            if (ModelState.IsValid)
            {
                InsertConsorcio(consorcio);
                return RedirectToAction("Add");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "unidades")]
        public ActionResult AddAndCreateUnidades(Consorcio consorcio)
        {
            if (ModelState.IsValid)
            {
                InsertConsorcio(consorcio);
                return RedirectToAction("Add", "Unidades", new { id = consorcio.IdConsorcio });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Consorcio consorcio = consorcioService.GetById(id);
            Usuario creatorUser = usuarioService.GetById(consorcio.IdUsuarioCreador);
            bool isCurrentUserCreator = consorcioService.ValidateCreatorWithCurrentUser(HttpContext.User.Identity.Name, creatorUser.Email);
            if (isCurrentUserCreator)
            {
                return View(consorcio);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { error = "403" });
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            if (ModelState.IsValid)
            {
                consorcioService.Delete(id);
                return RedirectToAction("Index");
            }
            {
                return View();
            }
        }

        [SiteMapTitle("title")]
        public ActionResult Update(int id)
        {
            CargarListasEnViewBag();
            Consorcio consorcio = consorcioService.GetById(id);
            Usuario creatorUser = usuarioService.GetById(consorcio.IdUsuarioCreador);
            bool isCurrentUserCreator = consorcioService.ValidateCreatorWithCurrentUser(HttpContext.User.Identity.Name, creatorUser.Email);
            if (isCurrentUserCreator)
            {
                int unidadesCount = unidadService.CountUnidadesByConsorcioId(id);
                ViewData["Title"] = consorcio.Nombre;
                ViewBag.UnidadesCount = unidadesCount;
                return View(consorcio);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { error = "403" });
            }
        }

        [HttpPost]
        public ActionResult Update(Consorcio consorcio)
        {
            if (ModelState.IsValid)
            {
                consorcioService.Update(consorcio);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void InsertConsorcio(Consorcio consorcio)
        {
            try
            {
                Usuario user = GetUser();
                consorcio.FechaCreacion = DateTime.Now;
                consorcio.IdUsuarioCreador = user.IdUsuario;
                consorcioService.Insert(consorcio);
                this.AddNotification($"Consorcio {consorcio.Nombre} creado con exito!", NotificationType.SUCCESS);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error while trying to InsertConsorcio", exception);
            }
        }

        private void CargarListasEnViewBag()
        {
            CargarProvinciasEnViewBag();
        }

        private Usuario GetUser()
        {
            string email = this.User.Identity.Name;
            Usuario user = usuarioService.GetByEmail(email);
            return user;
        }

        private void CargarProvinciasEnViewBag()
        {
            List<Provincia> provincias = provinciaService.GetAll();
            ViewBag.Provincias = provincias;
        }
    }
}