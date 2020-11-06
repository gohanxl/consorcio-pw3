using Repositories;
using Services.Gasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    public class GastosController : Controller
    {
        GastoService<Gasto> gastoService = new GastoService<Gasto>();
        // GET: Gastos
        public ActionResult Index()
        {
            IEnumerable<Gasto> gastos = gastoService.GetAll();
            return View(gastos);
        }

        public ActionResult Delete(int id)
        {
            gastoService.Delete(id);
            return Redirect("/Gastos/Index");
        }

        public ActionResult Modify(FormCollection formCollection)
        {

            Gasto gasto = new Gasto();

            gasto.AnioExpensa =  Int32.Parse(formCollection["AnioExpensa"]);
            //Resto de data desde el form collection

            gastoService.Update(gasto);

            return View("/Gastos/Index");
        }
    }
}