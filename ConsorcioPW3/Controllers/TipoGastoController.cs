﻿using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    public class TipoGastoController : Controller
    {
        ConsortiumContext context;

        public ActionResult Index()
        {
            return View();
        }
    }
}