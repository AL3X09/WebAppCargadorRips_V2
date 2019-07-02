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
    
    public class Web_Nivel_PermisoController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_Nivel_Permiso
        public async Task<ActionResult> Index()
        {
            var web_Nivel_Permiso = db.Web_Nivel_Permiso.Include(w => w.Estado_RIPS);
            return View(await web_Nivel_Permiso.ToListAsync());
        }

        // GET: Web_Nivel_Permiso/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Nivel_Permiso web_Nivel_Permiso = await db.Web_Nivel_Permiso.FindAsync(id);
            if (web_Nivel_Permiso == null)
            {
                return HttpNotFound();
            }
            return View(web_Nivel_Permiso);
        }

        // GET: Web_Nivel_Permiso/Create
        public ActionResult Create()
        {
            ViewBag.nivelpermiso_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Web_Nivel_Permiso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "nivelpermiso_id,FK_nivelpermiso_rol,FK_nivelpermiso_modulo,nivelpermiso_crear,nivelpermiso_modificar,nivelpermiso_eliminar,FK_nivelpermiso_estado_rips,fecha_modificacion")] Web_Nivel_Permiso web_Nivel_Permiso)
        {
            if (ModelState.IsValid)
            {
                db.Web_Nivel_Permiso.Add(web_Nivel_Permiso);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.nivelpermiso_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Nivel_Permiso.nivelpermiso_id);
            return View(web_Nivel_Permiso);
        }



        // GET: Web_Nivel_Permiso/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Nivel_Permiso web_Nivel_Permiso = await db.Web_Nivel_Permiso.FindAsync(id);
            if (web_Nivel_Permiso == null)
            {
                return HttpNotFound();
            }
            ViewBag.nivelpermiso_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Nivel_Permiso.nivelpermiso_id);
            return View(web_Nivel_Permiso);
        }

        // POST: Web_Nivel_Permiso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "nivelpermiso_id,FK_nivelpermiso_rol,FK_nivelpermiso_modulo,nivelpermiso_crear,nivelpermiso_modificar,nivelpermiso_eliminar,FK_nivelpermiso_estado_rips,fecha_modificacion")] Web_Nivel_Permiso web_Nivel_Permiso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_Nivel_Permiso).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.nivelpermiso_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Nivel_Permiso.nivelpermiso_id);
            return View(web_Nivel_Permiso);
        }

        // GET: Web_Nivel_Permiso/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Nivel_Permiso web_Nivel_Permiso = await db.Web_Nivel_Permiso.FindAsync(id);
            if (web_Nivel_Permiso == null)
            {
                return HttpNotFound();
            }
            return View(web_Nivel_Permiso);
        }

        // POST: Web_Nivel_Permiso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_Nivel_Permiso web_Nivel_Permiso = await db.Web_Nivel_Permiso.FindAsync(id);
            db.Web_Nivel_Permiso.Remove(web_Nivel_Permiso);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Web_Nivel_Permiso/Create
        public ActionResult Nuevo()
        {
            //ViewBag.nivelpermiso_id = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            return View();
        }

        // POST: Web_Nivel_Permiso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Nuevo(FormCollection collection)
        {
            string fk_rol = Request.Form["fkrol"];

            var contador = collection.Count;
            int i = 2; // lo dejo en 2 para que no recorra el token y el rol que llegan el array.
            while (i < contador)
            {

                string keyNombre = collection.GetKey(i);

                if(keyNombre.Substring(0,3) == "mod")
                {
                    var KeyValor = collection.Get(i);
                    //var crea = String.Concat("select " + Request.Form["prcrea[0_" + KeyValor + "]"]);
                    var crea = (Request.Form["prcrea[0_" + KeyValor + "]"] != null)? "true":"false";
                    var modifica = (Request.Form["prmodifica[0_" + KeyValor + "]"] != null)? "true":"false";
                    var elimina = (Request.Form["prelimina[0_" + KeyValor + "]"] != null)? "true":"false";
                    db.SP_Insert_Nivel_Permiso_Modulo(fk_rol, KeyValor, crea, modifica, elimina);
                    await db.SaveChangesAsync();

                }

                i++;
            }

            return View();
            
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
