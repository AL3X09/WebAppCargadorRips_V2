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
    public class DirectoriosController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Directorios
        public async Task<ActionResult> Index()
        {
            return View(await db.Directorios.ToListAsync());
        }

        // GET: Directorios/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directorios directorios = await db.Directorios.FindAsync(id);
            if (directorios == null)
            {
                return HttpNotFound();
            }
            return View(directorios);
        }

        // GET: Directorios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Directorios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "directorios_id,directorio_entrada,directorio_salida,directorio_error,directorio_rechazo,usuario_directorios,contraseña_directorios")] Directorios directorios)
        {
            if (ModelState.IsValid)
            {
                db.Directorios.Add(directorios);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(directorios);
        }

        // GET: Directorios/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directorios directorios = await db.Directorios.FindAsync(id);
            if (directorios == null)
            {
                return HttpNotFound();
            }
            return View(directorios);
        }

        // POST: Directorios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "directorios_id,directorio_entrada,directorio_salida,directorio_error,directorio_rechazo,usuario_directorios,contraseña_directorios")] Directorios directorios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(directorios).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(directorios);
        }

        // GET: Directorios/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directorios directorios = await db.Directorios.FindAsync(id);
            if (directorios == null)
            {
                return HttpNotFound();
            }
            return View(directorios);
        }

        // POST: Directorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Directorios directorios = await db.Directorios.FindAsync(id);
            db.Directorios.Remove(directorios);
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
