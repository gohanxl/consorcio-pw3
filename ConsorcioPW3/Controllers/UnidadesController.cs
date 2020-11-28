﻿using Repositories;
using Services;
using ConsorcioPW3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    public class UnidadesController : Controller
    {
        ConsortiumContext context;
        ConsorcioService consorcioService;
        UnidadService unidadService;
        UsuarioService usuarioService;

        public UnidadesController()
        {
            context = new ConsortiumContext();
            consorcioService = new ConsorcioService(context);
            unidadService = new UnidadService(context);
            usuarioService = new UsuarioService(context);
        }

        public ActionResult Index(int id)
        {
            Consorcio consorcio = consorcioService.GetById(id);
            List<Unidad> unidades = unidadService.GetAllByConsorcioId(id);
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            ViewBag.ConsorcioId = id;
            return View(unidades);
        }

        public ActionResult Add(int id)
        {
            Consorcio consorcio = consorcioService.GetById(id);
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            ViewBag.Consorcio = consorcio;
            return View();
        }


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "save")]
        public ActionResult Add(Unidad unidad)
        {
            InsertUnidad(unidad);
            return RedirectToAction("Index", new { id = unidad.IdConsorcio });
        }

        [HttpPost]

        [MultipleButton(Name = "action", Argument = "unidad")]
        public ActionResult AddAndCreate(Unidad unidad)
        {
            InsertUnidad(unidad);
            return RedirectToAction("Add", new { id = unidad.IdConsorcio });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Unidad unidad = unidadService.GetById(id);
            return View(unidad);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(FormCollection form)
        {
            unidadService.Delete(int.Parse(form["IdUnidad"]));
            return RedirectToAction("Index", new { id = int.Parse(form["IdConsorcio"]) });
        }

        public ActionResult Update(int id)
        {
            Unidad unidad = unidadService.GetById(id);
            Consorcio consorcio = consorcioService.GetById(unidad.IdConsorcio);
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            ViewBag.Consorcio = consorcio;
            return View(unidad);
        }

        [HttpPost]
        public ActionResult Update(Unidad unidad)
        {
            unidadService.Update(unidad);
            return RedirectToAction("Index", new { id = unidad.IdConsorcio });
        }

        private void InsertUnidad(Unidad unidad)
        {
            try
            {
                string email = this.User.Identity.Name;
                Usuario usuario = usuarioService.GetByEmail(email);
                unidad.FechaCreacion = DateTime.Now;
                unidad.IdUsuarioCreador = usuario.IdUsuario;
                unidadService.Insert(unidad);
                this.AddNotification($"Unidad {unidad.Nombre} creada con exito!", NotificationType.SUCCESS);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error while trying to InsertUnidad", exception);
            }
        }
    }
}