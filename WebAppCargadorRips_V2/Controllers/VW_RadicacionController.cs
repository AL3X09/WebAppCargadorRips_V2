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
    public class VW_RadicacionController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: VW_Radicacion
        public async Task<ActionResult> Index()
        {
            return View(await db.VW_Radicacion.ToListAsync());
        }

        // GET: VW_Radicacion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VW_Radicacion vW_Radicacion = await db.VW_Radicacion.FindAsync(id);
            if (vW_Radicacion == null)
            {
                return HttpNotFound();
            }
            return View(vW_Radicacion);
        }

        // GET: VW_Radicacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VW_Radicacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "radicado,servicio_validacion_id,servicio_radicacion_id,estado_validacion,estado_radicado,estado_carga,categoria,afiliacion,condicion,entidad_reporta,prestador,administradora_id,periodo_inicio,periodo_fin,us,af,ac,ap,au,ah,an,am,at,validacion_fecha,radicacion_fecha,carga_fecha,usuario_validacion,radicacion_usuario")] VW_Radicacion vW_Radicacion)
        {
            if (ModelState.IsValid)
            {
                db.VW_Radicacion.Add(vW_Radicacion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vW_Radicacion);
        }

        // GET: VW_Radicacion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VW_Radicacion vW_Radicacion = await db.VW_Radicacion.FindAsync(id);
            if (vW_Radicacion == null)
            {
                return HttpNotFound();
            }
            return View(vW_Radicacion);
        }

        // POST: VW_Radicacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "radicado,servicio_validacion_id,servicio_radicacion_id,estado_validacion,estado_radicado,estado_carga,categoria,afiliacion,condicion,entidad_reporta,prestador,administradora_id,periodo_inicio,periodo_fin,us,af,ac,ap,au,ah,an,am,at,validacion_fecha,radicacion_fecha,carga_fecha,usuario_validacion,radicacion_usuario")] VW_Radicacion vW_Radicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vW_Radicacion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vW_Radicacion);
        }

        // GET: VW_Radicacion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VW_Radicacion vW_Radicacion = await db.VW_Radicacion.FindAsync(id);
            if (vW_Radicacion == null)
            {
                return HttpNotFound();
            }
            return View(vW_Radicacion);
        }

        // POST: VW_Radicacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VW_Radicacion vW_Radicacion = await db.VW_Radicacion.FindAsync(id);
            db.VW_Radicacion.Remove(vW_Radicacion);
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
