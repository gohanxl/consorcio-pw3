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
        ExpensaService expensasService;

        public ExpensasController()
        {
            expensasService = new ExpensaService();
        }

        public async Task<ActionResult> Index(int id)
        {
            List<ExpensaDTO> expensas = await expensasService.GetExpensasByConsorioIdAsync(id);
            return View(expensas);
        }
    }
}