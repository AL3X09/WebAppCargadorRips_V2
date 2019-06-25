using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppCargadorRips_V2.Controllers
{
    public class AdministracionController : Controller
    {
        // GET: Administracion
        public ActionResult Index()
        {
            return View();
        }

        // GET: Rol_Permiso_Modulo
        public ActionResult Rol_Permiso_Modulo()
        {
            return View();
        }
        

        // GET: Administracion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Administracion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administracion/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Administracion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Administracion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Administracion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Administracion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
