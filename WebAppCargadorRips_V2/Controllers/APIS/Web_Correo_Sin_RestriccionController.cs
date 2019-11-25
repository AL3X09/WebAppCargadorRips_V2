using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAppCargadorRips_V2.EF_Models;

namespace WebAppCargadorRips_V2.Controllers.APIS
{
    public class Web_Correo_Sin_RestriccionController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: api/Web_Correo_Sin_Restriccion
        public IQueryable<Web_Correo_Sin_Restriccion> GetWeb_Correo_Sin_Restriccion()
        {
            
            db.Configuration.LazyLoadingEnabled = false;

            return db.Web_Correo_Sin_Restriccion;
        }

        // GET: api/Web_Correo_Sin_Restriccion/5
        [ResponseType(typeof(Web_Correo_Sin_Restriccion))]
        public async Task<IHttpActionResult> GetWeb_Correo_Sin_Restriccion(long id)
        {
            Web_Correo_Sin_Restriccion web_Correo_Sin_Restriccion = await db.Web_Correo_Sin_Restriccion.FindAsync(id);
            if (web_Correo_Sin_Restriccion == null)
            {
                return NotFound();
            }

            return Ok(web_Correo_Sin_Restriccion);
        }

        // PUT: api/Web_Correo_Sin_Restriccion/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWeb_Correo_Sin_Restriccion(long id, Web_Correo_Sin_Restriccion web_Correo_Sin_Restriccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != web_Correo_Sin_Restriccion.correo_sin_restriccion_id)
            {
                return BadRequest();
            }

            db.Entry(web_Correo_Sin_Restriccion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Web_Correo_Sin_RestriccionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Web_Correo_Sin_Restriccion
        [ResponseType(typeof(Web_Correo_Sin_Restriccion))]
        public async Task<IHttpActionResult> PostWeb_Correo_Sin_Restriccion(Web_Correo_Sin_Restriccion web_Correo_Sin_Restriccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Web_Correo_Sin_Restriccion csrtbl = new Web_Correo_Sin_Restriccion();    // make object of table
            csrtbl.correo_sin_restriccion_id = web_Correo_Sin_Restriccion.correo_sin_restriccion_id;
            csrtbl.correo = web_Correo_Sin_Restriccion.correo;
            csrtbl.FK_correo_sin_restriccion_estado_rips = web_Correo_Sin_Restriccion.correo_sin_restriccion_id;
            csrtbl.fecha_modificacion = web_Correo_Sin_Restriccion.fecha_modificacion;
            db.Web_Correo_Sin_Restriccion.Add(csrtbl);

            //db.Web_Correo_Sin_Restriccion.Add(web_Correo_Sin_Restriccion.correo_sin_restriccion_id,web_Correo_Sin_Restriccion.correo,web_Correo_Sin_Restriccion.fecha_modificacion,1);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = web_Correo_Sin_Restriccion.correo_sin_restriccion_id }, web_Correo_Sin_Restriccion);
        }

        // DELETE: api/Web_Correo_Sin_Restriccion/5
        [ResponseType(typeof(Web_Correo_Sin_Restriccion))]
        public async Task<IHttpActionResult> DeleteWeb_Correo_Sin_Restriccion(long id)
        {
            Web_Correo_Sin_Restriccion web_Correo_Sin_Restriccion = await db.Web_Correo_Sin_Restriccion.FindAsync(id);
            if (web_Correo_Sin_Restriccion == null)
            {
                return NotFound();
            }

            db.Web_Correo_Sin_Restriccion.Remove(web_Correo_Sin_Restriccion);
            await db.SaveChangesAsync();

            return Ok(web_Correo_Sin_Restriccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Web_Correo_Sin_RestriccionExists(long id)
        {
            return db.Web_Correo_Sin_Restriccion.Count(e => e.correo_sin_restriccion_id == id) > 0;
        }
    }
}