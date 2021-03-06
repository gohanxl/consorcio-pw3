﻿using ConsorcioPW3.Helpers;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsorcioPW3.Controllers
{
    [Authorize]
    public class GastosController : Controller
    {
        ConsortiumContext context;
        GastoService gastoService;
        TipoGastoService tipoGastoService;
        ConsorcioService consorcioService;
        UsuarioService usuarioService;

        public GastosController()
        {
            context = new ConsortiumContext();
            gastoService = new GastoService(context);
            tipoGastoService = new TipoGastoService(context);
            consorcioService = new ConsorcioService(context);
            usuarioService = new UsuarioService(context);
        }

        [AllowAnonymous]
        public ActionResult Index(int id)
        {
            IEnumerable<Gasto> gastos = gastoService.GetAllByConsorcioId(id);
            CargarListasEnViewBag();
            Consorcio consorcio = consorcioService.GetById(id);
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            ViewBag.Consorcio = consorcio;
            ViewBag.ConsorcioId = id;
            return View(gastos);
        }

        public ActionResult Add(int id)
        {
            Consorcio consorcio = consorcioService.GetById(id);
            Usuario creatorUser = usuarioService.GetById(consorcio.IdUsuarioCreador);
            bool isCurrentUserCreator = consorcioService.ValidateCreatorWithCurrentUser(HttpContext.User.Identity.Name, creatorUser.Email);
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            if (isCurrentUserCreator)
            {
                CargarListasEnViewBag();
                ViewBag.Consorcio = consorcio;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Error", new { error = "403" });
            }

        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "save")]
        public ActionResult Add(Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                string path = GetAndSaveFile();
                InsertGasto(gasto, path);
                this.AddNotification($"Gasto {gasto.Nombre} creado con exito!", NotificationType.SUCCESS);
                return RedirectToAction("Index", new { id = gasto.IdConsorcio });
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "gasto")]
        public ActionResult AddAndCreateGasto(Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                string path = GetAndSaveFile();
                InsertGasto(gasto, path);
                this.AddNotification($"Gasto {gasto.Nombre} creado con exito!", NotificationType.SUCCESS);
                return RedirectToAction("Add", new { id = gasto.IdConsorcio });
            }
            else
            {
                return View();
            }
        }

        public ActionResult Update(int id)
        {
            Gasto gasto = gastoService.GetById(id);
            Consorcio consorcio = gasto.Consorcio;
            Usuario creatorUser = usuarioService.GetById(consorcio.IdUsuarioCreador);
            bool isCurrentUserCreator = consorcioService.ValidateCreatorWithCurrentUser(HttpContext.User.Identity.Name, creatorUser.Email);
            SitemapHelper.SetConsorcioBreadcrumbTitle(consorcio.Nombre);
            if (isCurrentUserCreator)
            {
                CargarListasEnViewBag();
                ViewBag.Consorcio = consorcio;
                return View(gasto);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { error = "403" });
            }

        }

        [HttpPost]
        public ActionResult Update(Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                string path = GetAndSaveFile();
                if (path != "" && gasto.ArchivoComprobante != path)
                {
                    gasto.ArchivoComprobante = path;
                }
                gastoService.Update(gasto);
                return RedirectToAction("Index", new { id = gasto.IdConsorcio });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Gasto gasto = gastoService.GetById(id);
            Consorcio consorcio = gasto.Consorcio;
            Usuario creatorUser = usuarioService.GetById(consorcio.IdUsuarioCreador);
            bool isCurrentUserCreator = consorcioService.ValidateCreatorWithCurrentUser(HttpContext.User.Identity.Name, creatorUser.Email);

            if (isCurrentUserCreator)
            {
                return View(gasto);
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
            if (ModelState.IsValid)
            {
                gastoService.Delete(int.Parse(form["IdGasto"]));
                return RedirectToAction("Index", new { id = int.Parse(form["IdConsorcio"]) });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Download(int id)
        {
            Gasto gasto = gastoService.GetById(id);

            Consorcio consorcio = gasto.Consorcio;
            Usuario creatorUser = usuarioService.GetById(consorcio.IdUsuarioCreador);
            bool isCurrentUserCreator = consorcioService.ValidateCreatorWithCurrentUser(HttpContext.User.Identity.Name, creatorUser.Email);

            if (isCurrentUserCreator)
            {

                string absolutePath = gastoService.GetComprobanteAbsolutePath(gasto.ArchivoComprobante);
                string fileName = gastoService.GetComprobanteFileName(gasto.ArchivoComprobante, $"Gasto-{gasto.Nombre}");
                byte[] fileBytes;

                try
                {
                    fileBytes = System.IO.File.ReadAllBytes(absolutePath);
                }
                catch (Exception e)
                {
                    this.AddNotification($"Comprobante no encontrado", NotificationType.ERROR);
                    return RedirectToAction("Index", new { id = gasto.IdConsorcio });
                }
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { error = "403" });

            }
        }

        private void InsertGasto(Gasto gasto, string pathArchivo)
        {
            string email = this.User.Identity.Name;
            Usuario usuario = usuarioService.GetByEmail(email);

            gasto.FechaCreacion = DateTime.Now;
            gasto.ArchivoComprobante = pathArchivo;
            gasto.IdUsuarioCreador = usuario.IdUsuario;

            gastoService.Insert(gasto);
        }

        private void CargarListasEnViewBag()
        {
            CargarTipoGastoEnViewBag();
        }

        private void CargarTipoGastoEnViewBag()
        {
            IEnumerable<TipoGasto> tipoGastos = tipoGastoService.GetAll();
            ViewBag.TipoGastos = tipoGastos;
        }

        private string GetAndSaveFile()
        {
            string path = "";
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                path = GuardarArchivo(Request.Files[0]);
            }

            return path;
        }

        private string GuardarArchivo(HttpPostedFileBase file)
        {

            string directoryPath = Server.MapPath("~/Assets/Gastos/");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = DateTime.Now.ToString("yyyyMMddTHHmmssfffffff") + Path.GetFileName(file.FileName);

            string absolutePath = Path.Combine(directoryPath, fileName);
            string relativePath = "/Gastos/" + fileName;

            file.SaveAs(absolutePath);

            return relativePath;
        }
    }
}