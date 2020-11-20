using Repositories;
using Services;
using Services.Consorcio;
using Services.Gasto;
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
        ConsortiumContext context = new ConsortiumContext();
        GastoService<Gasto> gastoService;
        TipoGastoService<TipoGasto> tipoGastoService;
        ConsorcioService<Consorcio> consorcioService;
        UsuarioService<Usuario> usuarioService;

        public GastosController()
        {
            gastoService = new GastoService<Gasto>(context);
            tipoGastoService = new TipoGastoService<TipoGasto>(context);
            consorcioService = new ConsorcioService<Consorcio>(context);
            usuarioService = new UsuarioService<Usuario>(context);
        }

        // GET: Gastos
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
                //No hay archivo
                return Redirect("");
            }
            string path = GuardarArchivo(file);
            gasto.ArchivoComprobante = path;
            gastoService.Insert(gasto);
            return Redirect("/Gastos/Index");
        }

        public ActionResult Delete(int id)
        {
            gastoService.Delete(id);
            return Redirect("/Gastos/Index");
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
            return Redirect("/Gastos/Index");
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