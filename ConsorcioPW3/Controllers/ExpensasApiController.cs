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
        [HttpGet]
        public string Get()
        {
            return "test value";
        }
    }
}
