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
    [RoutePrefix("api/Modulos")]
    public class Web_ModuloController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        [Route("Modulo_permiso")]
        [HttpGet]
        public async Task<IHttpActionResult> GetWeb_Modulo_Permiso(string rol)
        {
            //return db.SP_Get_Modulo_Permiso(id);
            var web_Modulo =  db.SP_Get_Modulo_Permiso(rol);
            if (web_Modulo == null)
            {
                return NotFound();
            }

            return Ok(web_Modulo);
        }


        // GET: api/Web_Modulo
        public IQueryable<Web_Modulo> GetWeb_Modulo()
        {
            return db.Web_Modulo;
        }

        // GET: api/Web_Modulo/5
        [ResponseType(typeof(Web_Modulo))]
        public async Task<IHttpActionResult> GetWeb_Modulo(long id)
        {
            Web_Modulo web_Modulo = await db.Web_Modulo.FindAsync(id);
            if (web_Modulo == null)
            {
                return NotFound();
            }

            return Ok(web_Modulo);
        }

        // PUT: api/Web_Modulo/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWeb_Modulo(long id, Web_Modulo web_Modulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != web_Modulo.modulo_id)
            {
                return BadRequest();
            }

            db.Entry(web_Modulo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Web_ModuloExists(id))
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

        // POST: api/Web_Modulo
        [ResponseType(typeof(Web_Modulo))]
        public async Task<IHttpActionResult> PostWeb_Modulo(Web_Modulo web_Modulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Web_Modulo.Add(web_Modulo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = web_Modulo.modulo_id }, web_Modulo);
        }

        // DELETE: api/Web_Modulo/5
        [ResponseType(typeof(Web_Modulo))]
        public async Task<IHttpActionResult> DeleteWeb_Modulo(long id)
        {
            Web_Modulo web_Modulo = await db.Web_Modulo.FindAsync(id);
            if (web_Modulo == null)
            {
                return NotFound();
            }

            db.Web_Modulo.Remove(web_Modulo);
            await db.SaveChangesAsync();

            return Ok(web_Modulo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Web_ModuloExists(long id)
        {
            return db.Web_Modulo.Count(e => e.modulo_id == id) > 0;
        }
    }
}