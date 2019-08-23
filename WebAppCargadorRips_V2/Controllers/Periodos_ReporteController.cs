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
    public class Periodos_ReporteController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Periodos_Reporte
        public async Task<ActionResult> Index()
        {
            var periodos_Reporte = db.Periodos_Reporte.Include(p => p.Estado_RIPS).Include(p => p.Web_Rol);
            return View(await periodos_Reporte.ToListAsync());
        }

        // GET: Periodos_Reporte/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodos_Reporte periodos_Reporte = await db.Periodos_Reporte.FindAsync(id);
            if (periodos_Reporte == null)
            {
                return HttpNotFound();
            }
            return View(periodos_Reporte);
        }

        // GET: Periodos_Reporte/Create
        public ActionResult Create()
        {
            ViewBag.FK_periodos_estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            ViewBag.FK_periodos_rol_id = new SelectList(db.Web_Rol, "rol_id", "nombre");
            return View();
        }

        // POST: Periodos_Reporte/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "periodos_reporte_id,fecha_minima,fecha_maxima,fecha_reporte,FK_periodos_rol_id,FK_periodos_estado_rips_id,fecha_modificacion")] Periodos_Reporte periodos_Reporte)
        {
            if (ModelState.IsValid)
            {
                db.Periodos_Reporte.Add(periodos_Reporte);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_periodos_estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", periodos_Reporte.FK_periodos_estado_rips_id);
            ViewBag.FK_periodos_rol_id = new SelectList(db.Web_Rol, "rol_id", "nombre", periodos_Reporte.FK_periodos_rol_id);
            return View(periodos_Reporte);
        }

        // GET: Periodos_Reporte/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodos_Reporte periodos_Reporte = await db.Periodos_Reporte.FindAsync(id);
            if (periodos_Reporte == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_periodos_estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", periodos_Reporte.FK_periodos_estado_rips_id);
            ViewBag.FK_periodos_rol_id = new SelectList(db.Web_Rol, "rol_id", "nombre", periodos_Reporte.FK_periodos_rol_id);
            return View(periodos_Reporte);
        }

        // POST: Periodos_Reporte/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "periodos_reporte_id,fecha_minima,fecha_maxima,fecha_reporte,FK_periodos_rol_id,FK_periodos_estado_rips_id,fecha_modificacion")] Periodos_Reporte periodos_Reporte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(periodos_Reporte).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_periodos_estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", periodos_Reporte.FK_periodos_estado_rips_id);
            ViewBag.FK_periodos_rol_id = new SelectList(db.Web_Rol, "rol_id", "nombre", periodos_Reporte.FK_periodos_rol_id);
            return View(periodos_Reporte);
        }

        // GET: Periodos_Reporte/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodos_Reporte periodos_Reporte = await db.Periodos_Reporte.FindAsync(id);
            if (periodos_Reporte == null)
            {
                return HttpNotFound();
            }
            return View(periodos_Reporte);
        }

        // POST: Periodos_Reporte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Periodos_Reporte periodos_Reporte = await db.Periodos_Reporte.FindAsync(id);
            db.Periodos_Reporte.Remove(periodos_Reporte);
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
