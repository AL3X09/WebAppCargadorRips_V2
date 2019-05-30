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
    public class Web_MensajeController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_Mensaje
        public async Task<ActionResult> Index()
        {
            var web_Mensaje = db.Web_Mensaje.Include(w => w.Estado_RIPS);
            return View(await web_Mensaje.ToListAsync());
        }

        // GET: Web_Mensaje/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Mensaje web_Mensaje = await db.Web_Mensaje.FindAsync(id);
            if (web_Mensaje == null)
            {
                return HttpNotFound();
            }
            return View(web_Mensaje);
        }

        // GET: Web_Mensaje/Create
        public ActionResult Create()
        {
            ViewBag.FK_mensaje_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Web_Mensaje/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "mensaje_id,codigo,nombre,tipo,cuerpo,FK_mensaje_estado_rips,fecha_modificacion")] Web_Mensaje web_Mensaje)
        {
            if (ModelState.IsValid)
            {
                db.Web_Mensaje.Add(web_Mensaje);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_mensaje_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Mensaje.FK_mensaje_estado_rips);
            return View(web_Mensaje);
        }

        // GET: Web_Mensaje/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Mensaje web_Mensaje = await db.Web_Mensaje.FindAsync(id);
            if (web_Mensaje == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_mensaje_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Mensaje.FK_mensaje_estado_rips);
            return View(web_Mensaje);
        }

        // POST: Web_Mensaje/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "mensaje_id,codigo,nombre,tipo,cuerpo,FK_mensaje_estado_rips,fecha_modificacion")] Web_Mensaje web_Mensaje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_Mensaje).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_mensaje_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Mensaje.FK_mensaje_estado_rips);
            return View(web_Mensaje);
        }

        // GET: Web_Mensaje/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Mensaje web_Mensaje = await db.Web_Mensaje.FindAsync(id);
            if (web_Mensaje == null)
            {
                return HttpNotFound();
            }
            return View(web_Mensaje);
        }

        // POST: Web_Mensaje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_Mensaje web_Mensaje = await db.Web_Mensaje.FindAsync(id);
            db.Web_Mensaje.Remove(web_Mensaje);
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
