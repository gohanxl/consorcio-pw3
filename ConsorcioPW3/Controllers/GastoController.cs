using Repositories;
using Services.Gasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    public class GastoController : Controller
    {
        GastoService<Gasto> gastoService = new GastoService<Gasto>();
        // GET: Gastos
        public ActionResult Index()
        {
            IEnumerable<Gasto> gastos = gastoService.GetAll();
            return View(gastos);
        }
    }
}