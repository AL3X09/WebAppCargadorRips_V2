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
    public class Web_AdministradoresController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_Administradores
        public async Task<ActionResult> Index()
        {
            var web_Administrador = db.Web_Administrador.Include(w => w.Web_Rol);
            return View(await web_Administrador.ToListAsync());
        }

        // GET: Web_Administradores/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Administrador web_Administrador = await db.Web_Administrador.FindAsync(id);
            if (web_Administrador == null)
            {
                return HttpNotFound();
            }
            return View(web_Administrador);
        }

        // GET: Web_Administradores/Create
        public ActionResult Create()
        {
            ViewBag.FK_web_administrador_rol = new SelectList(db.Web_Rol, "rol_id", "nombre");
            return View();
        }

        // POST: Web_Administradores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "administrador_id,usuario,contrasenia,nombres,apellidos,descripcion,correo,extension,imagen,FK_web_administrador_rol,FK_web_administrador_estado_rips,fecha_modificacion")] Web_Administrador web_Administrador)
        {
            if (ModelState.IsValid)
            {
                db.Web_Administrador.Add(web_Administrador);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_web_administrador_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_Administrador.FK_web_administrador_rol);
            return View(web_Administrador);
        }

        // GET: Web_Administradores/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Administrador web_Administrador = await db.Web_Administrador.FindAsync(id);
            if (web_Administrador == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_web_administrador_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_Administrador.FK_web_administrador_rol);
            return View(web_Administrador);
        }

        // POST: Web_Administradores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "administrador_id,usuario,contrasenia,nombres,apellidos,descripcion,correo,extension,imagen,FK_web_administrador_rol,FK_web_administrador_estado_rips,fecha_modificacion")] Web_Administrador web_Administrador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_Administrador).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_web_administrador_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_Administrador.FK_web_administrador_rol);
            return View(web_Administrador);
        }

        // GET: Web_Administradores/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Administrador web_Administrador = await db.Web_Administrador.FindAsync(id);
            if (web_Administrador == null)
            {
                return HttpNotFound();
            }
            return View(web_Administrador);
        }

        // POST: Web_Administradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_Administrador web_Administrador = await db.Web_Administrador.FindAsync(id);
            db.Web_Administrador.Remove(web_Administrador);
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
