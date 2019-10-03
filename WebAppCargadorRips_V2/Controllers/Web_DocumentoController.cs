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
    public class Web_DocumentoController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_Documento
        public async Task<ActionResult> Index()
        {
            var web_Documento = db.Web_Documento.Include(w => w.Estado_RIPS);
            return View(await web_Documento.ToListAsync());
        }

        // GET: Web_Documento/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Documento web_Documento = await db.Web_Documento.FindAsync(id);
            if (web_Documento == null)
            {
                return HttpNotFound();
            }
            return View(web_Documento);
        }

        // GET: Web_Documento/Create
        public ActionResult Create()
        {
            ViewBag.FK_web_documento_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre");
            return View();
        }

        // POST: Web_Documento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "documento_id,tipo,descripcion,ruta,FK_web_documento_estado_rips,fecha_modificacion")] Web_Documento web_Documento)
        {
            if (ModelState.IsValid)
            {
                db.Web_Documento.Add(web_Documento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_web_documento_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", web_Documento.FK_web_documento_estado_rips);
            return View(web_Documento);
        }

        // GET: Web_Documento/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Documento web_Documento = await db.Web_Documento.FindAsync(id);
            if (web_Documento == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_web_documento_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", web_Documento.FK_web_documento_estado_rips);
            return View(web_Documento);
        }

        // POST: Web_Documento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "documento_id,tipo,descripcion,ruta,FK_web_documento_estado_rips,fecha_modificacion")] Web_Documento web_Documento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_Documento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_web_documento_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", web_Documento.FK_web_documento_estado_rips);
            return View(web_Documento);
        }

        // GET: Web_Documento/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Documento web_Documento = await db.Web_Documento.FindAsync(id);
            if (web_Documento == null)
            {
                return HttpNotFound();
            }
            return View(web_Documento);
        }

        // POST: Web_Documento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_Documento web_Documento = await db.Web_Documento.FindAsync(id);
            db.Web_Documento.Remove(web_Documento);
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
