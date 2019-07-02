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
    [RoutePrefix("Fechas_periodos")]
    public class Anios_PeriodosController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Anios_Periodos
        public async Task<ActionResult> Index()
        {
            var anios_Periodos = db.Anios_Periodos.Include(a => a.Estado_RIPS);
            return View(await anios_Periodos.ToListAsync());
        }
        
        // GET: Anios_Periodos/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anios_Periodos anios_Periodos = await db.Anios_Periodos.FindAsync(id);
            if (anios_Periodos == null)
            {
                return HttpNotFound();
            }
            return View(anios_Periodos);
        }

        // GET: Anios_Periodos/Create
        public ActionResult Create()
        {
            ViewBag.FK_anios_estado_rips_id = new SelectList(db.Estado_RIPS.Where(e =>e.tipo == "activacion"), "estado_rips_id", "nombre");
            return View();
        }

        // POST: Anios_Periodos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "anios_periodos_id,fecha_minima,fecha_maxima,fecha_modificacion,FK_anios_estado_rips_id")] Anios_Periodos anios_Periodos)
        {
            if (ModelState.IsValid)
            {
                db.Anios_Periodos.Add(anios_Periodos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_anios_estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", anios_Periodos.FK_anios_estado_rips_id);
            return View(anios_Periodos);
        }

        // GET: Anios_Periodos/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anios_Periodos anios_Periodos = await db.Anios_Periodos.FindAsync(id);
            if (anios_Periodos == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_anios_estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", anios_Periodos.FK_anios_estado_rips_id);
            return View(anios_Periodos);
        }

        // POST: Anios_Periodos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "anios_periodos_id,fecha_minima,fecha_maxima,fecha_modificacion,FK_anios_estado_rips_id")] Anios_Periodos anios_Periodos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anios_Periodos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_anios_estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", anios_Periodos.FK_anios_estado_rips_id);
            return View(anios_Periodos);
        }

        // GET: Anios_Periodos/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anios_Periodos anios_Periodos = await db.Anios_Periodos.FindAsync(id);
            if (anios_Periodos == null)
            {
                return HttpNotFound();
            }
            return View(anios_Periodos);
        }

        // POST: Anios_Periodos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Anios_Periodos anios_Periodos = await db.Anios_Periodos.FindAsync(id);
            db.Anios_Periodos.Remove(anios_Periodos);
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
