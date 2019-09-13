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
    public class Plantilla_Respuesta_PDFController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Plantilla_Respuesta_PDF
        public async Task<ActionResult> Index()
        {
            var plantilla_Respuesta_PDF = db.Plantilla_Respuesta_PDF.Include(p => p.Estado_RIPS);
            return View(await plantilla_Respuesta_PDF.ToListAsync());
        }

        // GET: Plantilla_Respuesta_PDF/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plantilla_Respuesta_PDF plantilla_Respuesta_PDF = await db.Plantilla_Respuesta_PDF.FindAsync(id);
            if (plantilla_Respuesta_PDF == null)
            {
                return HttpNotFound();
            }
            return View(plantilla_Respuesta_PDF);
        }

        // GET: Plantilla_Respuesta_PDF/Details/5
        public Object CuerpoPdf(string nombrep)
        {

            var plantilla_Respuesta_PDF = db.Plantilla_Respuesta_PDF.Where(r => r.categoria.ToString() == "Web" && r.nombre.ToString().Substring(0, 2) == nombrep.ToString() && r.FK_plantillas_respuesta_pdf_estado_rips == 1).Select(r => new{
                   r.cuerpo
               }).FirstOrDefault();

            return plantilla_Respuesta_PDF.cuerpo;
        }

        // GET: Plantilla_Respuesta_PDF/Create
        public ActionResult Create()
        {
            ViewBag.FK_plantillas_respuesta_pdf_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Plantilla_Respuesta_PDF/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "plantilla_respuesta_pdf_id,categoria,nombre,descripcion,cuerpo,FK_plantillas_respuesta_pdf_estado_rips,fecha_modificacion")] Plantilla_Respuesta_PDF plantilla_Respuesta_PDF)
        {
            if (ModelState.IsValid)
            {
                db.Plantilla_Respuesta_PDF.Add(plantilla_Respuesta_PDF);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_plantillas_respuesta_pdf_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", plantilla_Respuesta_PDF.FK_plantillas_respuesta_pdf_estado_rips);
            return View(plantilla_Respuesta_PDF);
        }

        // GET: Plantilla_Respuesta_PDF/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plantilla_Respuesta_PDF plantilla_Respuesta_PDF = await db.Plantilla_Respuesta_PDF.FindAsync(id);
            if (plantilla_Respuesta_PDF == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_plantillas_respuesta_pdf_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", plantilla_Respuesta_PDF.FK_plantillas_respuesta_pdf_estado_rips);
            return View(plantilla_Respuesta_PDF);
        }

        // POST: Plantilla_Respuesta_PDF/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "plantilla_respuesta_pdf_id,categoria,nombre,descripcion,cuerpo,FK_plantillas_respuesta_pdf_estado_rips,fecha_modificacion")] Plantilla_Respuesta_PDF plantilla_Respuesta_PDF)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plantilla_Respuesta_PDF).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_plantillas_respuesta_pdf_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", plantilla_Respuesta_PDF.FK_plantillas_respuesta_pdf_estado_rips);
            return View(plantilla_Respuesta_PDF);
        }

        // GET: Plantilla_Respuesta_PDF/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plantilla_Respuesta_PDF plantilla_Respuesta_PDF = await db.Plantilla_Respuesta_PDF.FindAsync(id);
            if (plantilla_Respuesta_PDF == null)
            {
                return HttpNotFound();
            }
            return View(plantilla_Respuesta_PDF);
        }

        // POST: Plantilla_Respuesta_PDF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Plantilla_Respuesta_PDF plantilla_Respuesta_PDF = await db.Plantilla_Respuesta_PDF.FindAsync(id);
            db.Plantilla_Respuesta_PDF.Remove(plantilla_Respuesta_PDF);
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
