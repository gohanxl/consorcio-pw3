using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ConsorcioPW3.Controllers
{
    public class ExpensasApiController : ApiController
    {
        ConsortiumContext entities;
        GastoService gastoService;
        UnidadService unidadService;


        public ExpensasApiController()
        {
            entities = new ConsortiumContext();
            gastoService = new GastoService(entities);
            unidadService = new UnidadService(entities);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            List<ExpensaDTO> expensas = gastoService.GetExpensasById(id);
            return Json(expensas);
        }
    }
}
