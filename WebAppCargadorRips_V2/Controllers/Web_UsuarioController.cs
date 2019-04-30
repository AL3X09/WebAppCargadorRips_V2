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
    public class Web_UsuarioController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_Usuario
        public async Task<ActionResult> Index()
        {
            var web_Usuario = db.Web_Usuario.Include(w => w.Web_Rol).Include(w => w.Estado_RIPS).Include(w => w.Prestador);
            return View(await web_Usuario.ToListAsync());
        }

        // GET: Web_Usuario/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Usuario web_Usuario = await db.Web_Usuario.FindAsync(id);
            if (web_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(web_Usuario);
        }

        // GET: Web_Usuario/Create
        public ActionResult Create()
        {
            ViewBag.FK_usuario_rol = new SelectList(db.Web_Rol, "rol_id", "nombre");
            ViewBag.FK_usuario_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            ViewBag.FK_usuario_prestador = new SelectList(db.Prestador.Where(x => x.municipio == "BOGOTÁ D.C."), "prestador_id", "codigo");
            return View();
        }

        // POST: Web_Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "usuario_id,FK_usuario_prestador,correo,contrasenia,nombres,apellidos,telefono,razon_social,imagen,confirmacion_condiciones,confirmacion_correo,FK_usuario_rol,FK_usuario_estado_rips,fecha_modificacion")] Web_Usuario web_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.Web_Usuario.Add(web_Usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_usuario_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_Usuario.FK_usuario_rol);
            ViewBag.FK_usuario_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Usuario.FK_usuario_estado_rips);
            ViewBag.FK_usuario_prestador = new SelectList(db.Prestador.Where(x => x.municipio == "BOGOTÁ D.C."), "prestador_id", "codigo", web_Usuario.FK_usuario_prestador);
            return View(web_Usuario);
        }

        // GET: Web_Usuario/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Usuario web_Usuario = await db.Web_Usuario.FindAsync(id);
            if (web_Usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_usuario_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_Usuario.FK_usuario_rol);
            ViewBag.FK_usuario_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Usuario.FK_usuario_estado_rips);
            ViewBag.FK_usuario_prestador = new SelectList(db.Prestador.Where(x => x.municipio == "BOGOTÁ D.C."), "prestador_id", "codigo", web_Usuario.FK_usuario_prestador);
            return View(web_Usuario);
        }

        // POST: Web_Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "usuario_id,FK_usuario_prestador,correo,contrasenia,nombres,apellidos,telefono,razon_social,imagen,confirmacion_condiciones,confirmacion_correo,FK_usuario_rol,FK_usuario_estado_rips,fecha_modificacion")] Web_Usuario web_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_Usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_usuario_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_Usuario.FK_usuario_rol);
            ViewBag.FK_usuario_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Usuario.FK_usuario_estado_rips);
            ViewBag.FK_usuario_prestador = new SelectList(db.Prestador.Where(x => x.municipio == "BOGOTÁ D.C."), "prestador_id", "codigo", web_Usuario.FK_usuario_prestador);
            return View(web_Usuario);
        }

        // GET: Web_Usuario/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Usuario web_Usuario = await db.Web_Usuario.FindAsync(id);
            if (web_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(web_Usuario);
        }

        // POST: Web_Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_Usuario web_Usuario = await db.Web_Usuario.FindAsync(id);
            db.Web_Usuario.Remove(web_Usuario);
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
