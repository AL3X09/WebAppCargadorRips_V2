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
    public class Web_UsuarioController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: api/Web_Usuario
        public IQueryable<Web_Usuario> GetWeb_Usuario()
        {
            return db.Web_Usuario;
        }

        // GET: api/Web_Usuario/5
        [ResponseType(typeof(Web_Usuario))]
        public async Task<IHttpActionResult> GetWeb_Usuario(long id)
        {
            Web_Usuario web_Usuario = await db.Web_Usuario.FindAsync(id);
            if (web_Usuario == null)
            {
                return NotFound();
            }

            return Ok(web_Usuario);
        }

        // PUT: api/Web_Usuario/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWeb_Usuario(long id, Web_Usuario web_Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != web_Usuario.usuario_id)
            {
                return BadRequest();
            }

            db.Entry(web_Usuario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Web_UsuarioExists(id))
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

        // POST: api/Web_Usuario
        [ResponseType(typeof(Web_Usuario))]
        public async Task<IHttpActionResult> PostWeb_Usuario(Web_Usuario web_Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Web_Usuario.Add(web_Usuario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = web_Usuario.usuario_id }, web_Usuario);
        }

        // DELETE: api/Web_Usuario/5
        [ResponseType(typeof(Web_Usuario))]
        public async Task<IHttpActionResult> DeleteWeb_Usuario(long id)
        {
            Web_Usuario web_Usuario = await db.Web_Usuario.FindAsync(id);
            if (web_Usuario == null)
            {
                return NotFound();
            }

            db.Web_Usuario.Remove(web_Usuario);
            await db.SaveChangesAsync();

            return Ok(web_Usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Web_UsuarioExists(long id)
        {
            return db.Web_Usuario.Count(e => e.usuario_id == id) > 0;
        }
    }
}