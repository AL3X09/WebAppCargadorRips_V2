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
    [RoutePrefix("api/Fechas")]
    public class FechasController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: api/Fechas
        public IQueryable<Fecha> GetFecha()
        {
            return db.Fecha;
        }

        // GET: api/Fechas/5
        [ResponseType(typeof(Fecha))]
        public async Task<IHttpActionResult> GetFecha(long id)
        {
            Fecha fecha = await db.Fecha.FindAsync(id);
            if (fecha == null)
            {
                return NotFound();
            }

            return Ok(fecha);
        }

        /// <summary>
        /// Se listan todas las categorias existentes en la base de datos.
        /// </summary>/// 
        // GET: api/Fechas/5
        [Route("Listar")]
        [ResponseType(typeof(Fecha))]
        public async Task<IHttpActionResult> GetFecha_rol(long rol)
        {
            var fecha =  db.Fecha.Where(x => x.rol_ide.Equals(rol)).Select(x => new {
                fecha_id = x.fecha_id,
                nombre_fecha = x.nombre_fecha,
                valor_fecha = x.valor_fecha
                }
            );

            if (fecha == null)
            {
                return NotFound();
            }

            return Ok(await fecha.ToListAsync());
        }

        // PUT: api/Fechas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFecha(long id, Fecha fecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fecha.fecha_id)
            {
                return BadRequest();
            }

            db.Entry(fecha).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FechaExists(id))
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

        // POST: api/Fechas
        [ResponseType(typeof(Fecha))]
        public async Task<IHttpActionResult> PostFecha(Fecha fecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fecha.Add(fecha);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = fecha.fecha_id }, fecha);
        }

        // DELETE: api/Fechas/5
        [ResponseType(typeof(Fecha))]
        public async Task<IHttpActionResult> DeleteFecha(long id)
        {
            Fecha fecha = await db.Fecha.FindAsync(id);
            if (fecha == null)
            {
                return NotFound();
            }

            db.Fecha.Remove(fecha);
            await db.SaveChangesAsync();

            return Ok(fecha);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FechaExists(long id)
        {
            return db.Fecha.Count(e => e.fecha_id == id) > 0;
        }
    }
}