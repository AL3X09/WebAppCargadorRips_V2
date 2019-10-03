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
    [Authorize]
    public class Plantilla_CorreoController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Plantilla_Correo
        public async Task<ActionResult> Index()
        {
            var plantilla_Correo = db.Plantilla_Correo.Include(p => p.Estado_RIPS);
            return View(await plantilla_Correo.ToListAsync());
        }

        // GET: Plantilla_Correo/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plantilla_Correo plantilla_Correo = await db.Plantilla_Correo.FindAsync(id);
            if (plantilla_Correo == null)
            {
                return HttpNotFound();
            }
            return View(plantilla_Correo);
        }

        // GET: Plantilla_Correo/Create
        public ActionResult Create()
        {
            ViewBag.FK_plantillas_correo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Plantilla_Correo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "plantilla_correo_id,nombre,descripcion,asunto,cuerpo,FK_plantillas_correo_estado_rips,fecha_modificacion")] Plantilla_Correo plantilla_Correo)
        {
            if (ModelState.IsValid)
            {
                db.Plantilla_Correo.Add(plantilla_Correo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_plantillas_correo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", plantilla_Correo.FK_plantillas_correo_estado_rips);
            return View(plantilla_Correo);
        }

        // GET: Plantilla_Correo/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plantilla_Correo plantilla_Correo = await db.Plantilla_Correo.FindAsync(id);
            if (plantilla_Correo == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_plantillas_correo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", plantilla_Correo.FK_plantillas_correo_estado_rips);
            return View(plantilla_Correo);
        }

        // POST: Plantilla_Correo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "plantilla_correo_id,nombre,descripcion,asunto,cuerpo,FK_plantillas_correo_estado_rips,fecha_modificacion")] Plantilla_Correo plantilla_Correo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plantilla_Correo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_plantillas_correo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", plantilla_Correo.FK_plantillas_correo_estado_rips);
            return View(plantilla_Correo);
        }

        // GET: Plantilla_Correo/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plantilla_Correo plantilla_Correo = await db.Plantilla_Correo.FindAsync(id);
            if (plantilla_Correo == null)
            {
                return HttpNotFound();
            }
            return View(plantilla_Correo);
        }

        // POST: Plantilla_Correo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Plantilla_Correo plantilla_Correo = await db.Plantilla_Correo.FindAsync(id);
            db.Plantilla_Correo.Remove(plantilla_Correo);
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
