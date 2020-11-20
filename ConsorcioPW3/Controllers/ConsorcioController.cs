using Repositories;
using Services.Consorcio;
using Services.Provincia;
using Services.Unidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    public class ConsorcioController : Controller
    {
        // GET: Consorcio
        public class consorciosController : Controller
        {
            ConsortiumContext context = new ConsortiumContext();
            ConsorcioService<Consorcio> consorcioService;
            ProvinciaService<Provincia> provinciaService;
            UnidadService<Unidad> unidadService;


            public consorciosController()
            {
                consorcioService = new ConsorcioService<Consorcio>(context);
                provinciaService = new ProvinciaService<Provincia>(context);
                unidadService = new UnidadService<Unidad>(context);
            }

            // GET: consorcios
            [AllowAnonymous]
            public ActionResult Index()
            {
                IEnumerable<Consorcio> consorcios = consorcioService.GetAll();
                CargarListasEnViewBag();
                return View(consorcios);
            }

            [HttpPost]
            public ActionResult Add(Consorcio consorcio)
            {
                consorcioService.Insert(consorcio);
                return Redirect("/Consorcio/Index");
            }

            public ActionResult Delete(int id)
            {
                consorcioService.Delete(id);
                return Redirect("/Consorcio/Index");
            }

            public ActionResult Update(int id)
            {
                CargarListasEnViewBag();
                Consorcio consorcio = consorcioService.GetById(id);
                return View(consorcio);
            }

            [HttpPost]
            public ActionResult Update(Consorcio consorcio)
            {
                consorcioService.Update(consorcio);
                return Redirect("/Consorcio/Index");
            }

            private void CargarListasEnViewBag()
            {
                CargarConsorciosEnViewBag();
                CargarTipoconsorcioEnViewBag();
            }

            private void CargarTipoconsorcioEnViewBag()
            {
                IEnumerable<Provincia> provincias = provinciaService.GetAll();
                ViewBag.Provincias = provincias;
            }

            public void CargarConsorciosEnViewBag()
            {
                IEnumerable<Consorcio> consorcios = consorcioService.GetAll();
                ViewBag.Consorcios = consorcios;
            }
        }
    }
}