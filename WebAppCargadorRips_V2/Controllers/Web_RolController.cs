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
    public class Web_RolController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_Rol
        public async Task<ActionResult> Index()
        {
            var web_Rol = db.Web_Rol.Include(w => w.Estado_RIPS);
            return View(await web_Rol.ToListAsync());
        }

        // GET: Web_Rol/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Rol web_Rol = await db.Web_Rol.FindAsync(id);
            if (web_Rol == null)
            {
                return HttpNotFound();
            }
            return View(web_Rol);
        }

        // GET: Web_Rol/Create
        public ActionResult Create()
        {
            ViewBag.FK_rol_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Web_Rol/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "rol_id,nombre,FK_rol_estado_rips,fecha_modificacion")] Web_Rol web_Rol)
        {
            if (ModelState.IsValid)
            {
                db.Web_Rol.Add(web_Rol);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_rol_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Rol.FK_rol_estado_rips);
            return View(web_Rol);
        }

        // GET: Web_Rol/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Rol web_Rol = await db.Web_Rol.FindAsync(id);
            if (web_Rol == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_rol_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "nombre", web_Rol.FK_rol_estado_rips);
            return View(web_Rol);
        }

        // POST: Web_Rol/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "rol_id,nombre,FK_rol_estado_rips,fecha_modificacion")] Web_Rol web_Rol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_Rol).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_rol_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Rol.FK_rol_estado_rips);
            return View(web_Rol);
        }

        // GET: Web_Rol/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Rol web_Rol = await db.Web_Rol.FindAsync(id);
            if (web_Rol == null)
            {
                return HttpNotFound();
            }
            return View(web_Rol);
        }

        // POST: Web_Rol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_Rol web_Rol = await db.Web_Rol.FindAsync(id);
            db.Web_Rol.Remove(web_Rol);
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
