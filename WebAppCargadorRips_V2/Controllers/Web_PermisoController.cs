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
    public class Web_PermisoController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_Permiso
        public async Task<ActionResult> Index()
        {
            var web_Permiso = db.Web_Permiso.Include(w => w.Estado_RIPS);
            return View(await web_Permiso.ToListAsync());
        }

        // GET: Web_Permiso/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Permiso web_Permiso = await db.Web_Permiso.FindAsync(id);
            if (web_Permiso == null)
            {
                return HttpNotFound();
            }
            return View(web_Permiso);
        }

        // GET: Web_Permiso/Create
        public ActionResult Create()
        {
            ViewBag.FK_permiso_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre");
            return View();
        }

        // POST: Web_Permiso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "permiso_id,nombre,FK_permiso_estado_rips,fecha_modificacion")] Web_Permiso web_Permiso)
        {
            if (ModelState.IsValid)
            {
                db.Web_Permiso.Add(web_Permiso);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_permiso_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", web_Permiso.FK_permiso_estado_rips);
            return View(web_Permiso);
        }

        // GET: Web_Permiso/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Permiso web_Permiso = await db.Web_Permiso.FindAsync(id);
            if (web_Permiso == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_permiso_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", web_Permiso.FK_permiso_estado_rips);
            return View(web_Permiso);
        }

        // POST: Web_Permiso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "permiso_id,nombre,FK_permiso_estado_rips,fecha_modificacion")] Web_Permiso web_Permiso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_Permiso).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_permiso_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", web_Permiso.FK_permiso_estado_rips);
            return View(web_Permiso);
        }

        // GET: Web_Permiso/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Permiso web_Permiso = await db.Web_Permiso.FindAsync(id);
            if (web_Permiso == null)
            {
                return HttpNotFound();
            }
            return View(web_Permiso);
        }

        // POST: Web_Permiso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_Permiso web_Permiso = await db.Web_Permiso.FindAsync(id);
            db.Web_Permiso.Remove(web_Permiso);
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
