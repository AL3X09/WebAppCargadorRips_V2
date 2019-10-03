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
    public class EstructurasController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Estructuras
        public async Task<ActionResult> Index()
        {
            var estructura = db.Estructura.Include(e => e.Estado_RIPS);
            return View(await estructura.ToListAsync());
        }

        // GET: Estructuras/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estructura estructura = await db.Estructura.FindAsync(id);
            if (estructura == null)
            {
                return HttpNotFound();
            }
            return View(estructura);
        }

        // GET: Estructuras/Create
        public ActionResult Create()
        {
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Estructuras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "estructura_id,numero,acronimo,nombre,tabla,fecha_modificacion,estado_rips_id")] Estructura estructura)
        {
            if (ModelState.IsValid)
            {
                db.Estructura.Add(estructura);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", estructura.estado_rips_id);
            return View(estructura);
        }

        // GET: Estructuras/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estructura estructura = await db.Estructura.FindAsync(id);
            if (estructura == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", estructura.estado_rips_id);
            return View(estructura);
        }

        // POST: Estructuras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "estructura_id,numero,acronimo,nombre,tabla,fecha_modificacion,estado_rips_id")] Estructura estructura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estructura).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", estructura.estado_rips_id);
            return View(estructura);
        }

        // GET: Estructuras/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estructura estructura = await db.Estructura.FindAsync(id);
            if (estructura == null)
            {
                return HttpNotFound();
            }
            return View(estructura);
        }

        // POST: Estructuras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Estructura estructura = await db.Estructura.FindAsync(id);
            db.Estructura.Remove(estructura);
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
