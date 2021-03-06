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
            Usuario creatorUser = usuarioService.GetById(consorcio.IdUsuarioCreador);
            bool isCurrentUserCreator = consorcioService.ValidateCreatorWithCurrentUser(HttpContext.User.Identity.Name, creatorUser.Email);
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            if (isCurrentUserCreator)
            {

                ViewBag.ConsorcioId = id;
                return View(unidades);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { error = "403" });
            }
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
            if (ModelState.IsValid)
            {
                InsertUnidad(unidad);
                return RedirectToAction("Index", new { id = unidad.IdConsorcio });
            }
            else
            {
                return View();
            }
        }

        [HttpPost]

        [MultipleButton(Name = "action", Argument = "unidad")]
        public ActionResult AddAndCreate(Unidad unidad)
        {
            if (ModelState.IsValid)
            {
                InsertUnidad(unidad);
                return RedirectToAction("Add", new { id = unidad.IdConsorcio });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Unidad unidad = unidadService.GetById(id);
            Usuario creatorUser = usuarioService.GetById(unidad.IdUsuarioCreador);
            bool isCurrentUserCreator = consorcioService.ValidateCreatorWithCurrentUser(HttpContext.User.Identity.Name, creatorUser.Email);
            if (isCurrentUserCreator)
            {
                return View(unidad);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { error = "403" });
            }
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
            Usuario creatorUser = usuarioService.GetById(unidad.IdUsuarioCreador);
            bool isCurrentUserCreator = consorcioService.ValidateCreatorWithCurrentUser(HttpContext.User.Identity.Name, creatorUser.Email);
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            if (isCurrentUserCreator)
            {
                ViewBag.Consorcio = consorcio;
                return View(unidad);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { error = "403" });
            }
        }

        [HttpPost]
        public ActionResult Update(Unidad unidad)
        {
            if (ModelState.IsValid)
            {
                unidadService.Update(unidad);
                return RedirectToAction("Index", new { id = unidad.IdConsorcio });
            }
            else
            {
                return View();
            }
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