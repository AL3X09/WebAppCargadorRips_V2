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
    public class Web_ModuloController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_Modulo
        public async Task<ActionResult> Index()
        {
            var web_Modulo = db.Web_Modulo.Include(w => w.Estado_RIPS);
            return View(await web_Modulo.ToListAsync());
        }

        // GET: Web_Modulo/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Modulo web_Modulo = await db.Web_Modulo.FindAsync(id);
            if (web_Modulo == null)
            {
                return HttpNotFound();
            }
            return View(web_Modulo);
        }

        // GET: Web_Modulo/Create
        public ActionResult Create()
        {
            ViewBag.FK_modulo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Web_Modulo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "modulo_id,nombre,FK_modulo_estado_rips,fecha_modificacion")] Web_Modulo web_Modulo)
        {
            if (ModelState.IsValid)
            {
                db.Web_Modulo.Add(web_Modulo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_modulo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Modulo.FK_modulo_estado_rips);
            return View(web_Modulo);
        }

        // GET: Web_Modulo/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Modulo web_Modulo = await db.Web_Modulo.FindAsync(id);
            if (web_Modulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_modulo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Modulo.FK_modulo_estado_rips);
            return View(web_Modulo);
        }

        // POST: Web_Modulo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "modulo_id,nombre,FK_modulo_estado_rips,fecha_modificacion")] Web_Modulo web_Modulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_Modulo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_modulo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Modulo.FK_modulo_estado_rips);
            return View(web_Modulo);
        }

        // GET: Web_Modulo/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Modulo web_Modulo = await db.Web_Modulo.FindAsync(id);
            if (web_Modulo == null)
            {
                return HttpNotFound();
            }
            return View(web_Modulo);
        }

        // POST: Web_Modulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_Modulo web_Modulo = await db.Web_Modulo.FindAsync(id);
            db.Web_Modulo.Remove(web_Modulo);
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
