using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    [Authorize]
    public class BienvenidoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bienvenido()
        {
            return View();
        }
    }
}