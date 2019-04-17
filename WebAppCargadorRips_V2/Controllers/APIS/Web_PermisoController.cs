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
    public class Web_PermisoController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: api/Web_Permiso
        public IQueryable<Web_Permiso> GetWeb_Permiso()
        {
            return db.Web_Permiso;
        }

        // GET: api/Web_Permiso/5
        [ResponseType(typeof(Web_Permiso))]
        public async Task<IHttpActionResult> GetWeb_Permiso(long id)
        {
            Web_Permiso web_Permiso = await db.Web_Permiso.FindAsync(id);
            if (web_Permiso == null)
            {
                return NotFound();
            }

            return Ok(web_Permiso);
        }

        // PUT: api/Web_Permiso/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWeb_Permiso(long id, Web_Permiso web_Permiso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != web_Permiso.permiso_id)
            {
                return BadRequest();
            }

            db.Entry(web_Permiso).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Web_PermisoExists(id))
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

        // POST: api/Web_Permiso
        [ResponseType(typeof(Web_Permiso))]
        public async Task<IHttpActionResult> PostWeb_Permiso(Web_Permiso web_Permiso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Web_Permiso.Add(web_Permiso);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = web_Permiso.permiso_id }, web_Permiso);
        }

        // DELETE: api/Web_Permiso/5
        [ResponseType(typeof(Web_Permiso))]
        public async Task<IHttpActionResult> DeleteWeb_Permiso(long id)
        {
            Web_Permiso web_Permiso = await db.Web_Permiso.FindAsync(id);
            if (web_Permiso == null)
            {
                return NotFound();
            }

            db.Web_Permiso.Remove(web_Permiso);
            await db.SaveChangesAsync();

            return Ok(web_Permiso);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Web_PermisoExists(long id)
        {
            return db.Web_Permiso.Count(e => e.permiso_id == id) > 0;
        }
    }
}