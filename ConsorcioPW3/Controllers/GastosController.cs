using ConsorcioPW3.Helpers;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    [Authorize]
    public class GastosController : Controller
    {
        ConsortiumContext context;
        GastoService gastoService;
        TipoGastoService tipoGastoService;
        ConsorcioService consorcioService;
        UsuarioService usuarioService;

        public GastosController()
        {
            context = new ConsortiumContext();
            gastoService = new GastoService(context);
            tipoGastoService = new TipoGastoService(context);
            consorcioService = new ConsorcioService(context);
            usuarioService = new UsuarioService(context);
        }

        [AllowAnonymous]
        public ActionResult Index(int consorcioId)
        {
            IEnumerable<Gasto> gastos = gastoService.GetAll();
            CargarListasEnViewBag();
            Consorcio consorcio = consorcioService.GetById(consorcioId);
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            ViewBag.ConsorcioId = consorcioId;
            return View(gastos);
        }

        public ActionResult Add(int id)
        {            
            Consorcio consorcio = consorcioService.GetById(id);
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            CargarListasEnViewBag();
            ViewBag.Consorcio = consorcio;
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "save")]
        public ActionResult Add(Gasto gasto)
        {
            string path = GetAndSaveFile();
            
            InsertGasto(gasto, path);
            return RedirectToAction("Index", new { consorcioId = gasto.IdConsorcio });
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "gasto")]
        public ActionResult AddAndCreateGasto(Gasto gasto)
        {
            string path = GetAndSaveFile();

            InsertGasto(gasto, path);
            return RedirectToAction("Add", new { id = gasto.IdConsorcio });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            gastoService.Delete(id);
            return Redirect("/Gastos/Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            CargarListasEnViewBag();
            Gasto gasto = gastoService.GetById(id);
            return View(gasto);
        }

        [HttpPost]
        public ActionResult Update(Gasto gasto, HttpPostedFileBase file)
        {
            string path = GuardarArchivo(file);
            gasto.ArchivoComprobante = path;
            gastoService.Update(gasto);
            return RedirectToAction("Gastos/Index");
        }

        private string GetAndSaveFile() {
            string path = "";
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                path = GuardarArchivo(Request.Files[0]);
            }

            return path;
        }

        private void InsertGasto(Gasto gasto, string pathArchivo)
        {
            string email = this.User.Identity.Name;
            Usuario usuario = usuarioService.GetByEmail(email);
            gasto.FechaCreacion = DateTime.Now;
            gasto.ArchivoComprobante = pathArchivo;
            gasto.IdUsuarioCreador = usuario.IdUsuario;
            gastoService.Insert(gasto);
        }

        private void CargarListasEnViewBag()
        {
            CargarTipoGastoEnViewBag();
        }

        private void CargarTipoGastoEnViewBag()
        {
            IEnumerable<TipoGasto> tipoGastos = tipoGastoService.GetAll();
            ViewBag.TipoGastos = tipoGastos;
        }

        private string GuardarArchivo(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Assets/Gastos/"), fileName);
            file.SaveAs(path);
            return path;
        }
    }
}