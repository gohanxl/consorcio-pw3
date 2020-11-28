using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    public class ExpensasController : Controller
    {
        ConsortiumContext context = new ConsortiumContext();
        ExpensaService expensasService;
        GastoService gastoService;
        UnidadService unidadService;
        ConsorcioService consorcioService;


        public ExpensasController()
        {
            expensasService = new ExpensaService();
            gastoService = new GastoService(context);
            unidadService = new UnidadService(context);
            consorcioService = new ConsorcioService(context);

        }

        public async Task<ActionResult> Index(int consorcioId)
        {
            Consorcio consorcio = consorcioService.GetById(consorcioId);
            List<ExpensaDTO> expensas = await expensasService.GetExpensasByConsorcioIdAsync(consorcioId);
            ViewBag.GastosTotal = gastoService.GetGastosTotalCurrentMonthByConsorcio(consorcioId);
            ViewBag.CantidadUnidades = unidadService.CountUnidadesByConsorcioId(consorcioId);
            ViewBag.Consorcio = consorcio;
            return View(expensas);
        }
    }
}