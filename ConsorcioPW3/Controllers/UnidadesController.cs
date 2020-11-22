using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    public class UnidadesController : Controller
    {
        ConsortiumContext context;
        ConsorcioService consorcioService;
        UnidadService unidadService;
        UsuarioService usuarioService;

        public UnidadesController()
        {
            context = new ConsortiumContext();
            consorcioService = new ConsorcioService(context);
            unidadService = new UnidadService(context);
            usuarioService = new UsuarioService(context);
        }

        public ActionResult Index(int consorcioId)
        {
            List<Unidad> unidades = unidadService.GetAllByConsorcioId(consorcioId);
            return View(unidades);
        }

        public ActionResult Add(int id)
        {
            Consorcio consorcio = consorcioService.GetById(id);
            ViewBag.Consorcio = consorcio;
            return View();
        }


        [HttpPost]
        public ActionResult Add(Unidad unidad)
        {
            string email = this.User.Identity.Name;
            Usuario usuario = usuarioService.GetByEmail(email);
            unidad.FechaCreacion = DateTime.Now;
            unidad.IdUsuarioCreador = usuario.IdUsuario;
            unidadService.Insert(unidad);
            return RedirectToAction("Index", new { consorcioId = unidad.IdConsorcio });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Unidad unidad = unidadService.GetById(id);
            return View(unidad);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            unidadService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}