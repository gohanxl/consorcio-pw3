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
        GastoService<Gasto> gastoService = new GastoService<Gasto>();
        TipoGastoService<TipoGasto> tipoGastoService = new TipoGastoService<TipoGasto>();
        ConsorcioService<Consorcio> consorcioService = new ConsorcioService<Consorcio>();
        UsuarioService<Usuario> usuarioService = new UsuarioService<Usuario>();
        
        // GET: Gastos
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
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Assets/Gastos/"), fileName);
                file.SaveAs(path);
                gasto.ArchivoComprobante = path;
            }
            gastoService.Insert(gasto);
            return Redirect("/Gastos/Index");
        }

        public ActionResult Delete(int id)
        {
            gastoService.Delete(id);
            return Redirect("/Gastos/Index");
        }

        public ActionResult Update(FormCollection formCollection)
        {

            Gasto gasto = new Gasto();

            gasto.AnioExpensa =  Int32.Parse(formCollection["AnioExpensa"]);

            gastoService.Update(gasto);

            return View("/Gastos/Index");
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
    }
}