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
    public class Web_Pregunta_FrecuenteController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_Pregunta_Frecuente
        public async Task<ActionResult> Index()
        {
            var web_Pregunta_Frecuente = db.Web_Pregunta_Frecuente.Include(w => w.Estado_RIPS);
            return View(await web_Pregunta_Frecuente.ToListAsync());
        }

        // GET: Web_Pregunta_Frecuente/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Pregunta_Frecuente web_Pregunta_Frecuente = await db.Web_Pregunta_Frecuente.FindAsync(id);
            if (web_Pregunta_Frecuente == null)
            {
                return HttpNotFound();
            }
            return View(web_Pregunta_Frecuente);
        }

        // GET: Web_Pregunta_Frecuente/Create
        public ActionResult Create()
        {
            ViewBag.FK_pregunta_frecuente_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre");
            return View();
        }

        // POST: Web_Pregunta_Frecuente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "pregunta_frecuente_id,nombre,descripcion,tipo,pregunta,respuesta,FK_pregunta_frecuente_estado_rips,fecha_modificacion")] Web_Pregunta_Frecuente web_Pregunta_Frecuente)
        {
            if (ModelState.IsValid)
            {
                db.Web_Pregunta_Frecuente.Add(web_Pregunta_Frecuente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_pregunta_frecuente_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", web_Pregunta_Frecuente.FK_pregunta_frecuente_estado_rips);
            return View(web_Pregunta_Frecuente);
        }

        // GET: Web_Pregunta_Frecuente/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Pregunta_Frecuente web_Pregunta_Frecuente = await db.Web_Pregunta_Frecuente.FindAsync(id);
            if (web_Pregunta_Frecuente == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_pregunta_frecuente_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", web_Pregunta_Frecuente.FK_pregunta_frecuente_estado_rips);
            return View(web_Pregunta_Frecuente);
        }

        // POST: Web_Pregunta_Frecuente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "pregunta_frecuente_id,nombre,descripcion,tipo,pregunta,respuesta,FK_pregunta_frecuente_estado_rips,fecha_modificacion")] Web_Pregunta_Frecuente web_Pregunta_Frecuente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_Pregunta_Frecuente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_pregunta_frecuente_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", web_Pregunta_Frecuente.FK_pregunta_frecuente_estado_rips);
            return View(web_Pregunta_Frecuente);
        }

        // GET: Web_Pregunta_Frecuente/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Pregunta_Frecuente web_Pregunta_Frecuente = await db.Web_Pregunta_Frecuente.FindAsync(id);
            if (web_Pregunta_Frecuente == null)
            {
                return HttpNotFound();
            }
            return View(web_Pregunta_Frecuente);
        }

        // POST: Web_Pregunta_Frecuente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_Pregunta_Frecuente web_Pregunta_Frecuente = await db.Web_Pregunta_Frecuente.FindAsync(id);
            db.Web_Pregunta_Frecuente.Remove(web_Pregunta_Frecuente);
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
