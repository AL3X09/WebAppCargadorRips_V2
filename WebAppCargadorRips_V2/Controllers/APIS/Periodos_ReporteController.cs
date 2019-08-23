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
    [RoutePrefix("api/Anios_Periodos")]
    public class Periodos_ReporteController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: api/Periodos_Reporte
        public IQueryable<Periodos_Reporte> GetPeriodos_Reporte()
        {
            return db.Periodos_Reporte;
        }

        // GET: api/Periodos_Reporte/5
        [ResponseType(typeof(Periodos_Reporte))]
        public async Task<IHttpActionResult> GetPeriodos_Reporte(long id)
        {
            Periodos_Reporte periodos_Reporte = await db.Periodos_Reporte.FindAsync(id);
            if (periodos_Reporte == null)
            {
                return NotFound();
            }

            return Ok(periodos_Reporte);
        }

        /// <summary>
        /// Se listan todas las categorias existentes en la base de datos.
        /// </summary>
        /// <returns>Valores nulos</returns>
       // GET: api/Periodos_Reporte/5
        [Route("Listar")]        
        [ResponseType(typeof(Periodos_Reporte1))]
        public async Task<IHttpActionResult> GetPeriodos_Reporte_rol(long rol)
        {
            var periodos_Reporte = db.Periodos_Reporte.Where(x => x.FK_periodos_rol_id.Equals(rol));
            /*if (periodos_Reporte == null)
            {
                return NotFound();
            }*/

            return Ok(periodos_Reporte);
        }

        // PUT: api/Periodos_Reporte/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPeriodos_Reporte(long id, Periodos_Reporte periodos_Reporte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != periodos_Reporte.periodos_reporte_id)
            {
                return BadRequest();
            }

            db.Entry(periodos_Reporte).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Periodos_ReporteExists(id))
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

        // POST: api/Periodos_Reporte
        [ResponseType(typeof(Periodos_Reporte))]
        public async Task<IHttpActionResult> PostPeriodos_Reporte(Periodos_Reporte periodos_Reporte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Periodos_Reporte.Add(periodos_Reporte);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = periodos_Reporte.periodos_reporte_id }, periodos_Reporte);
        }

        // DELETE: api/Periodos_Reporte/5
        [ResponseType(typeof(Periodos_Reporte))]
        public async Task<IHttpActionResult> DeletePeriodos_Reporte(long id)
        {
            Periodos_Reporte periodos_Reporte = await db.Periodos_Reporte.FindAsync(id);
            if (periodos_Reporte == null)
            {
                return NotFound();
            }

            db.Periodos_Reporte.Remove(periodos_Reporte);
            await db.SaveChangesAsync();

            return Ok(periodos_Reporte);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Periodos_ReporteExists(long id)
        {
            return db.Periodos_Reporte.Count(e => e.periodos_reporte_id == id) > 0;
        }
    }
}