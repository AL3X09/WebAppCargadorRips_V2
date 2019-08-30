using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppCargadorRips_V2.EF_Models;

namespace WebAppCargadorRips_V2.Controllers
{
    public class FechasController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Fechas
        public async Task<ActionResult> Index()
        {
            var fecha = db.Fecha.Include(f => f.Estado_RIPS).Include(f => f.Web_Rol);
            return View(await fecha.ToListAsync());
        }

        // GET: Fechas/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fecha fecha = await db.Fecha.FindAsync(id);
            if (fecha == null)
            {
                return HttpNotFound();
            }
            return View(fecha);
        }

        // GET: Fechas/Create
        public ActionResult Create()
        {
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre");
            ViewBag.rol_ide = new SelectList(db.Web_Rol, "rol_id", "nombre");
            return View();
        }

        // POST: Fechas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "fecha_id,nombre_fecha,rol_ide,valor_fecha,fecha_modificacion,estado_rips_id")] Fecha fecha)
        {
            if (ModelState.IsValid)
            {
                db.Fecha.Add(fecha);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", fecha.estado_rips_id);
            ViewBag.rol_ide = new SelectList(db.Web_Rol, "rol_id", "nombre", fecha.rol_ide);
            return View(fecha);
        }

        // GET: Fechas/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fecha fecha = await db.Fecha.FindAsync(id);
            if (fecha == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", fecha.estado_rips_id);
            ViewBag.rol_ide = new SelectList(db.Web_Rol, "rol_id", "nombre", fecha.rol_ide);
            return View(fecha);
        }

        // POST: Fechas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "fecha_id,nombre_fecha,rol_ide,valor_fecha,fecha_modificacion,estado_rips_id")] Fecha fecha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fecha).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", fecha.estado_rips_id);
            ViewBag.rol_ide = new SelectList(db.Web_Rol, "rol_id", "nombre", fecha.rol_ide);
            return View(fecha);
        }

        // GET: Fechas/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fecha fecha = await db.Fecha.FindAsync(id);
            if (fecha == null)
            {
                return HttpNotFound();
            }
            return View(fecha);
        }

        // POST: Fechas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Fecha fecha = await db.Fecha.FindAsync(id);
            db.Fecha.Remove(fecha);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
