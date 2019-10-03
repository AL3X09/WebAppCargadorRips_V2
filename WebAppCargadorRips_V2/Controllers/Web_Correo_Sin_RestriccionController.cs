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
    public class Web_Correo_Sin_RestriccionController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_Correo_Sin_Restriccion
        public async Task<ActionResult> Index()
        {
            var web_Correo_Sin_Restriccion = db.Web_Correo_Sin_Restriccion.Include(w => w.Estado_RIPS);
            return View(await web_Correo_Sin_Restriccion.ToListAsync());
        }

        // GET: Web_Correo_Sin_Restriccion/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Correo_Sin_Restriccion web_Correo_Sin_Restriccion = await db.Web_Correo_Sin_Restriccion.FindAsync(id);
            if (web_Correo_Sin_Restriccion == null)
            {
                return HttpNotFound();
            }
            return View(web_Correo_Sin_Restriccion);
        }

        // GET: Web_Correo_Sin_Restriccion/Create
        public ActionResult Create()
        {
            ViewBag.FK_correo_sin_restriccion_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Web_Correo_Sin_Restriccion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "correo_sin_restriccion_id,correo,FK_correo_sin_restriccion_estado_rips,fecha_modificacion")] Web_Correo_Sin_Restriccion web_Correo_Sin_Restriccion)
        {
            if (ModelState.IsValid)
            {
                db.Web_Correo_Sin_Restriccion.Add(web_Correo_Sin_Restriccion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_correo_sin_restriccion_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Correo_Sin_Restriccion.FK_correo_sin_restriccion_estado_rips);
            return View(web_Correo_Sin_Restriccion);
        }

        // GET: Web_Correo_Sin_Restriccion/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Correo_Sin_Restriccion web_Correo_Sin_Restriccion = await db.Web_Correo_Sin_Restriccion.FindAsync(id);
            if (web_Correo_Sin_Restriccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_correo_sin_restriccion_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Correo_Sin_Restriccion.FK_correo_sin_restriccion_estado_rips);
            return View(web_Correo_Sin_Restriccion);
        }

        // POST: Web_Correo_Sin_Restriccion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "correo_sin_restriccion_id,correo,FK_correo_sin_restriccion_estado_rips,fecha_modificacion")] Web_Correo_Sin_Restriccion web_Correo_Sin_Restriccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_Correo_Sin_Restriccion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_correo_sin_restriccion_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Correo_Sin_Restriccion.FK_correo_sin_restriccion_estado_rips);
            return View(web_Correo_Sin_Restriccion);
        }

        // GET: Web_Correo_Sin_Restriccion/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Correo_Sin_Restriccion web_Correo_Sin_Restriccion = await db.Web_Correo_Sin_Restriccion.FindAsync(id);
            if (web_Correo_Sin_Restriccion == null)
            {
                return HttpNotFound();
            }
            return View(web_Correo_Sin_Restriccion);
        }

        // POST: Web_Correo_Sin_Restriccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_Correo_Sin_Restriccion web_Correo_Sin_Restriccion = await db.Web_Correo_Sin_Restriccion.FindAsync(id);
            db.Web_Correo_Sin_Restriccion.Remove(web_Correo_Sin_Restriccion);
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
