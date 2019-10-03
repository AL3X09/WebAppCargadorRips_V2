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
    public class CorreosController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Correos
        public async Task<ActionResult> Index()
        {
            var correo = db.Correo.Include(c => c.Estado_RIPS);
            return View(await correo.ToListAsync());
        }

        // GET: Correos/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = await db.Correo.FindAsync(id);
            if (correo == null)
            {
                return HttpNotFound();
            }
            return View(correo);
        }

        // GET: Correos/Create
        public ActionResult Create()
        {
            ViewBag.FK_correo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Correos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "correo_id,descripcion,correo1,contrasenia,from,host,puerto,FK_correo_estado_rips,fecha_modificacion")] Correo correo)
        {
            if (ModelState.IsValid)
            {
                db.Correo.Add(correo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_correo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", correo.FK_correo_estado_rips);
            return View(correo);
        }

        // GET: Correos/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = await db.Correo.FindAsync(id);
            if (correo == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_correo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", correo.FK_correo_estado_rips);
            return View(correo);
        }

        // POST: Correos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "correo_id,descripcion,correo1,contrasenia,from,host,puerto,FK_correo_estado_rips,fecha_modificacion")] Correo correo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(correo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_correo_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", correo.FK_correo_estado_rips);
            return View(correo);
        }

        // GET: Correos/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = await db.Correo.FindAsync(id);
            if (correo == null)
            {
                return HttpNotFound();
            }
            return View(correo);
        }

        // POST: Correos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Correo correo = await db.Correo.FindAsync(id);
            db.Correo.Remove(correo);
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
