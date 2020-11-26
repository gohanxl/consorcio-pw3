﻿using ConsorcioPW3.Helpers;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;
using Repositories;
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
            Usuario user = GetUser();
            List<Consorcio> consorcios = consorcioService.GetAllByUser(user.IdUsuario);
            return View(consorcios);
        }

        public ActionResult Add()
        {
            CargarListasEnViewBag();
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "save")]
        public ActionResult Add(Consorcio consorcio)
        {
            InsertConsorcio(consorcio);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "consorcio")]
        public ActionResult AddAndCreateConsorcio(Consorcio consorcio)
        {
            InsertConsorcio(consorcio);
            return RedirectToAction("Add");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "unidades")]
        public ActionResult AddAndCreateUnidades(Consorcio consorcio)
        {
            InsertConsorcio(consorcio);
            return RedirectToAction("Add", "Unidades", new { id = consorcio.IdConsorcio });
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

        [SiteMapTitle("title")]
        public ActionResult Update(int id)
        {
            CargarListasEnViewBag();
            Consorcio consorcio = consorcioService.GetById(id);
            int unidadesCount = unidadService.CountUnidadesByConsorcioId(id);
            ViewData["Title"] = consorcio.Nombre;
            ViewBag.UnidadesCount = unidadesCount;
            return View(consorcio);
        }

        [HttpPost]
        public ActionResult Update(Consorcio consorcio)
        {
            consorcioService.Update(consorcio);
            return RedirectToAction("Index");
        }

        private void InsertConsorcio(Consorcio consorcio)
        {
            Usuario user = GetUser();
            consorcio.FechaCreacion = DateTime.Now;
            consorcio.IdUsuarioCreador = user.IdUsuario;
            consorcioService.Insert(consorcio);
        }

        private void CargarListasEnViewBag()
        {
            CargarProvinciasEnViewBag();
        }

        private Usuario GetUser()
        {
            string email = this.User.Identity.Name;
            Usuario user = usuarioService.GetByEmail(email);
            return user;
        }

        private void CargarProvinciasEnViewBag()
        {
            List<Provincia> provincias = provinciaService.GetAll();
            ViewBag.Provincias = provincias;
        }
    }
}