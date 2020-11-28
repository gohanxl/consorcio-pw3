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
        public ActionResult Index(int id)
        {
            IEnumerable<Gasto> gastos = gastoService.GetAll();
            CargarListasEnViewBag();
            Consorcio consorcio = consorcioService.GetById(id);
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            ViewBag.Consorcio = consorcio;
            ViewBag.ConsorcioId = id;
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
			this.AddNotification($"Gasto {gasto.Nombre} creado con exito!", NotificationType.SUCCESS);
            return RedirectToAction("Index", new { id = gasto.IdConsorcio });
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "gasto")]
        public ActionResult AddAndCreateGasto(Gasto gasto)
        {
            string path = GetAndSaveFile();

            InsertGasto(gasto, path);
            return RedirectToAction("Add", new { id = gasto.IdConsorcio });
        }

        public ActionResult Update(int id)
        {
            Gasto gasto = gastoService.GetById(id);
            Consorcio consorcio = gasto.Consorcio;
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            CargarListasEnViewBag();
            ViewBag.Consorcio = consorcio;

            return View(gasto);
        }

        [HttpPost]
        public ActionResult Update(Gasto gasto)
        {
            string path = GetAndSaveFile();
            gasto.ArchivoComprobante = path;
            gastoService.Update(gasto);
            return RedirectToAction("Index", new { id = gasto.IdConsorcio });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Gasto gasto = gastoService.GetById(id);
            return View(gasto);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(FormCollection form)
        {
            gastoService.Delete(int.Parse(form["IdGasto"]));
            return RedirectToAction("Index", new { id = int.Parse(form["IdConsorcio"]) });
        }

        [HttpGet]
        public ActionResult Download(int id)
        {
            Gasto gasto = gastoService.GetById(id);

            string absolutePath = gastoService.GetComprobanteAbsolutePath(gasto.ArchivoComprobante);
            string fileName = gastoService.GetComprobanteFileName(gasto.ArchivoComprobante);
            byte[] fileBytes = null;

            try {
                fileBytes = System.IO.File.ReadAllBytes(absolutePath);
            } 
            catch (Exception e) {
                this.AddNotification($"Comprobante no encontrado", NotificationType.ERROR);
                return RedirectToAction("Index", new { id = gasto.IdConsorcio });
            }
            
            
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
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

        private string GetAndSaveFile()
        {
            string path = "";
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                path = GuardarArchivo(Request.Files[0]);
            }

            return path;
        }

        private string GuardarArchivo(HttpPostedFileBase file)
        {
            string fileName = Path.GetFileName(file.FileName);

            string absolutePath = Path.Combine(Server.MapPath("~/Assets/Gastos/"), fileName);
            string relativePath = "/Gastos/" + fileName;

            file.SaveAs(absolutePath);

            return relativePath;
        }
    }
}