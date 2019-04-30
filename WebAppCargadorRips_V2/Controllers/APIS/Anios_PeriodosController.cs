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
    public class Anios_PeriodosController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        //PRECAUCIÓN Disable Lazy Loading
        //https://www.entityframeworktutorial.net/lazyloading-in-entity-framework.aspx
        // GET: api/Anios_Periodos
        public IQueryable<Anios_Periodos> GetAnios_Periodos()
        {
            //return db.Anios_Periodos.Select(a => a.fecha_maxima & a.fecha_minima).Where(e => e.FK_anios_estado_rips_id == 1);
            return db.Anios_Periodos.Where(e => e.FK_anios_estado_rips_id == 1);
        }

        // GET: api/Anios_Periodos/5
        [ResponseType(typeof(Anios_Periodos))]
        public async Task<IHttpActionResult> GetAnios_Periodos(long id)
        {
            Anios_Periodos anios_Periodos = await db.Anios_Periodos.FindAsync(id);
            if (anios_Periodos == null)
            {
                return NotFound();
            }

            return Ok(anios_Periodos);
        }

        // PUT: api/Anios_Periodos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAnios_Periodos(long id, Anios_Periodos anios_Periodos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != anios_Periodos.anios_periodos_id)
            {
                return BadRequest();
            }

            db.Entry(anios_Periodos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Anios_PeriodosExists(id))
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

        // POST: api/Anios_Periodos
        [ResponseType(typeof(Anios_Periodos))]
        public async Task<IHttpActionResult> PostAnios_Periodos(Anios_Periodos anios_Periodos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Anios_Periodos.Add(anios_Periodos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = anios_Periodos.anios_periodos_id }, anios_Periodos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Anios_PeriodosExists(long id)
        {
            return db.Anios_Periodos.Count(e => e.anios_periodos_id == id) > 0;
        }
    }
}