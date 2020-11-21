﻿using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    [Authorize]
    public class ConsorciosController : Controller
    {
        ConsortiumContext context;
        ConsorcioService consorcioService;
        ProvinciaService provinciaService;
        UnidadService unidadService;
        UsuarioService usuarioService;

        public ConsorciosController()
        {
            context = new ConsortiumContext();
            consorcioService = new ConsorcioService(context);
            provinciaService = new ProvinciaService(context);
            unidadService = new UnidadService(context);
            usuarioService = new UsuarioService(context);
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Consorcio> consorcios = consorcioService.GetAll();
            return View(consorcios);
        }

        public ActionResult Add()
        {
            CargarListasEnViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Add(Consorcio consorcio)
        {
            string email = this.User.Identity.Name;
            Usuario usuario = usuarioService.GetByEmail(email);
            consorcio.FechaCreacion = DateTime.Now;
            consorcio.IdUsuarioCreador = usuario.IdUsuario;
            consorcioService.Insert(consorcio);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Consorcio consorcio = consorcioService.GetById(id);
            return View(consorcio);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            consorcioService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            CargarListasEnViewBag();
            Consorcio consorcio = consorcioService.GetById(id);
            return View(consorcio);
        }

        [HttpPost]
        public ActionResult Update(Consorcio consorcio)
        {
            consorcioService.Update(consorcio);
            return RedirectToAction("Index");
        }

        private void CargarListasEnViewBag()
        {
            CargarProvinciasEnViewBag();
        }

        private void CargarProvinciasEnViewBag()
        {
            List<Provincia> provincias = provinciaService.GetAll();
            ViewBag.Provincias = provincias;
        }
    }
}