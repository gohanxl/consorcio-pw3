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
        public ActionResult Index()
        {
            IEnumerable<Gasto> gastos = gastoService.GetAll();
            CargarListasEnViewBag();
            return View(gastos);
        }

        [HttpPost]
        public ActionResult Add(Gasto gasto, HttpPostedFileBase file)
        {
            string email = this.User.Identity.Name;
            Usuario usuario = usuarioService.GetByEmail(email);
            gasto.FechaCreacion = DateTime.Now;
            gasto.IdUsuarioCreador = usuario.IdUsuario;
            if (file == null && file.ContentLength == 0)
            {
                //No hay archivo error
                return Redirect("");
            }
            string path = GuardarArchivo(file);
            gasto.ArchivoComprobante = path;
            gastoService.Insert(gasto);

            this.AddNotification($"Gasto {gasto.Nombre} creado con exito!", NotificationType.SUCCESS);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            gastoService.Delete(id);
            return RedirectToAction("Index");
        }

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
            return RedirectToAction("Index");
        }

        private void CargarListasEnViewBag()
        {
            CargarConsorciosEnViewBag();
            CargarTipoGastoEnViewBag();
        }

        private void CargarTipoGastoEnViewBag()
        {
            IEnumerable<TipoGasto> tipoGastos = tipoGastoService.GetAll();
            ViewBag.TipoGastos = tipoGastos;
        }

        public void CargarConsorciosEnViewBag()
        {
            IEnumerable<Consorcio> consorcios = consorcioService.GetAll();
            ViewBag.Consorcios = consorcios;
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