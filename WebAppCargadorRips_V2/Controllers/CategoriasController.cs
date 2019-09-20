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
    public class CategoriasController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Categorias
        public async Task<ActionResult> Index()
        {
            var categoria = db.Categoria.Include(c => c.Estado_RIPS);
            return View(await categoria.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "categoria_id,numero,nombre,fecha_modificacion,estado_rips_id,tipo1,tipo2,tipo3,tipo4,tipo5,tipo6,tipo7,tipo8")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Categoria.Add(categoria);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", categoria.estado_rips_id);
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", categoria.estado_rips_id);
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "categoria_id,numero,nombre,fecha_modificacion,estado_rips_id,tipo1,tipo2,tipo3,tipo4,tipo5,tipo6,tipo7,tipo8")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.estado_rips_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", categoria.estado_rips_id);
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Categoria categoria = await db.Categoria.FindAsync(id);
            db.Categoria.Remove(categoria);
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
