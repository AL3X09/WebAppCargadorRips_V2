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
    public class Tipo_UsuarioController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Tipo_Usuario
        public async Task<ActionResult> Index()
        {
            var tipo_Usuario = db.Tipo_Usuario.Include(t => t.Estado_RIPS);
            return View(await tipo_Usuario.ToListAsync());
        }

        // GET: Tipo_Usuario/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_Usuario = await db.Tipo_Usuario.FindAsync(id);
            if (tipo_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Usuario);
        }

        // GET: Tipo_Usuario/Create
        public ActionResult Create()
        {
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Tipo_Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "tipo_usuario_id,numero,nombre,afiliacion,condicion,fecha_modificacion,estado_rips_id")] Tipo_Usuario tipo_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Usuario.Add(tipo_Usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", tipo_Usuario.estado_rips_id);
            return View(tipo_Usuario);
        }

        // GET: Tipo_Usuario/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_Usuario = await db.Tipo_Usuario.FindAsync(id);
            if (tipo_Usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", tipo_Usuario.estado_rips_id);
            return View(tipo_Usuario);
        }

        // POST: Tipo_Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "tipo_usuario_id,numero,nombre,afiliacion,condicion,fecha_modificacion,estado_rips_id")] Tipo_Usuario tipo_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", tipo_Usuario.estado_rips_id);
            return View(tipo_Usuario);
        }

        // GET: Tipo_Usuario/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_Usuario = await db.Tipo_Usuario.FindAsync(id);
            if (tipo_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Usuario);
        }

        // POST: Tipo_Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Tipo_Usuario tipo_Usuario = await db.Tipo_Usuario.FindAsync(id);
            db.Tipo_Usuario.Remove(tipo_Usuario);
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
