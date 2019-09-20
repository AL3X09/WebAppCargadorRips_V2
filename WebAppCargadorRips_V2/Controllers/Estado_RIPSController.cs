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
    public class Estado_RIPSController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Estado_RIPS
        public async Task<ActionResult> Index()
        {
            return View(await db.Estado_RIPS.ToListAsync());
        }

        // GET: Estado_RIPS/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado_RIPS estado_RIPS = await db.Estado_RIPS.FindAsync(id);
            if (estado_RIPS == null)
            {
                return HttpNotFound();
            }
            return View(estado_RIPS);
        }

        // GET: Estado_RIPS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estado_RIPS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "estado_rips_id,numero,tipo,nombre,descripcion,imagen,fecha_modificacion")] Estado_RIPS estado_RIPS)
        {
            if (ModelState.IsValid)
            {
                db.Estado_RIPS.Add(estado_RIPS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(estado_RIPS);
        }

        // GET: Estado_RIPS/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado_RIPS estado_RIPS = await db.Estado_RIPS.FindAsync(id);
            if (estado_RIPS == null)
            {
                return HttpNotFound();
            }
            return View(estado_RIPS);
        }

        // POST: Estado_RIPS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "estado_rips_id,numero,tipo,nombre,descripcion,imagen,fecha_modificacion")] Estado_RIPS estado_RIPS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estado_RIPS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(estado_RIPS);
        }

        // GET: Estado_RIPS/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado_RIPS estado_RIPS = await db.Estado_RIPS.FindAsync(id);
            if (estado_RIPS == null)
            {
                return HttpNotFound();
            }
            return View(estado_RIPS);
        }

        // POST: Estado_RIPS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Estado_RIPS estado_RIPS = await db.Estado_RIPS.FindAsync(id);
            db.Estado_RIPS.Remove(estado_RIPS);
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
