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
    public class Web_RolHasPermisoController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_RolHasPermiso
        public async Task<ActionResult> Index()
        {
            var web_RolHasPermiso = db.Web_RolHasPermiso.Include(w => w.Web_Modulo).Include(w => w.Web_Permiso).Include(w => w.Web_Rol).Include(w => w.Estado_RIPS);
            return View(await web_RolHasPermiso.ToListAsync());
        }

        // GET: Web_RolHasPermiso/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_RolHasPermiso web_RolHasPermiso = await db.Web_RolHasPermiso.FindAsync(id);
            if (web_RolHasPermiso == null)
            {
                return HttpNotFound();
            }
            return View(web_RolHasPermiso);
        }

        // GET: Web_RolHasPermiso/Create
        public ActionResult Create()
        {
            ViewBag.FK_rolhaspermiso_modulo = new SelectList(db.Web_Modulo, "modulo_id", "nombre");
            ViewBag.FK_rolhaspermiso_permiso = new SelectList(db.Web_Permiso, "permiso_id", "nombre");
            ViewBag.FK_rolhaspermiso_rol = new SelectList(db.Web_Rol, "rol_id", "nombre");
            ViewBag.FK_rolhaspermiso_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Web_RolHasPermiso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "rolhaspermiso_id,FK_rolhaspermiso_rol,FK_rolhaspermiso_modulo,FK_rolhaspermiso_permiso,FK_rolhaspermiso_estado_rips,fecha_modificacion")] Web_RolHasPermiso web_RolHasPermiso)
        {
            if (ModelState.IsValid)
            {
                db.Web_RolHasPermiso.Add(web_RolHasPermiso);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_rolhaspermiso_modulo = new SelectList(db.Web_Modulo, "modulo_id", "nombre", web_RolHasPermiso.FK_rolhaspermiso_modulo);
            ViewBag.FK_rolhaspermiso_permiso = new SelectList(db.Web_Permiso, "permiso_id", "nombre", web_RolHasPermiso.FK_rolhaspermiso_permiso);
            ViewBag.FK_rolhaspermiso_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_RolHasPermiso.FK_rolhaspermiso_rol);
            ViewBag.FK_rolhaspermiso_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_RolHasPermiso.FK_rolhaspermiso_estado_rips);
            return View(web_RolHasPermiso);
        }

        // GET: Web_RolHasPermiso/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_RolHasPermiso web_RolHasPermiso = await db.Web_RolHasPermiso.FindAsync(id);
            if (web_RolHasPermiso == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_rolhaspermiso_modulo = new SelectList(db.Web_Modulo, "modulo_id", "nombre", web_RolHasPermiso.FK_rolhaspermiso_modulo);
            ViewBag.FK_rolhaspermiso_permiso = new SelectList(db.Web_Permiso, "permiso_id", "nombre", web_RolHasPermiso.FK_rolhaspermiso_permiso);
            ViewBag.FK_rolhaspermiso_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_RolHasPermiso.FK_rolhaspermiso_rol);
            ViewBag.FK_rolhaspermiso_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_RolHasPermiso.FK_rolhaspermiso_estado_rips);
            return View(web_RolHasPermiso);
        }

        // POST: Web_RolHasPermiso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "rolhaspermiso_id,FK_rolhaspermiso_rol,FK_rolhaspermiso_modulo,FK_rolhaspermiso_permiso,FK_rolhaspermiso_estado_rips,fecha_modificacion")] Web_RolHasPermiso web_RolHasPermiso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_RolHasPermiso).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_rolhaspermiso_modulo = new SelectList(db.Web_Modulo, "modulo_id", "nombre", web_RolHasPermiso.FK_rolhaspermiso_modulo);
            ViewBag.FK_rolhaspermiso_permiso = new SelectList(db.Web_Permiso, "permiso_id", "nombre", web_RolHasPermiso.FK_rolhaspermiso_permiso);
            ViewBag.FK_rolhaspermiso_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_RolHasPermiso.FK_rolhaspermiso_rol);
            ViewBag.FK_rolhaspermiso_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_RolHasPermiso.FK_rolhaspermiso_estado_rips);
            return View(web_RolHasPermiso);
        }

        // GET: Web_RolHasPermiso/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_RolHasPermiso web_RolHasPermiso = await db.Web_RolHasPermiso.FindAsync(id);
            if (web_RolHasPermiso == null)
            {
                return HttpNotFound();
            }
            return View(web_RolHasPermiso);
        }

        // POST: Web_RolHasPermiso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_RolHasPermiso web_RolHasPermiso = await db.Web_RolHasPermiso.FindAsync(id);
            db.Web_RolHasPermiso.Remove(web_RolHasPermiso);
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
