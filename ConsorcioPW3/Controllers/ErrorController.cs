using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Por favor, intente de nuevo en otro momento.";
                    break;
                case 404:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La pagina no existe";
                    break;
                case 403:
                    ViewBag.Title = "Acceso denegado";
                    ViewBag.Description = "Pagina no disponible";
                    break;
                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "No Messirve";
                    break;
            }

            return View("~/Views/Error/Error.cshtml");
        }
    }
}