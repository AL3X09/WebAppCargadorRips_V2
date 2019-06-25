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
    [RoutePrefix("api/Rol")]
    public class Web_RolController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();


        // GET: api/Web_Rol
        public IQueryable<Web_Rol> GetWeb_Rol()
        {
            return db.Web_Rol;
        }

        // GET: api/Web_Rol
        [Route("Listar")]
        public async Task<IHttpActionResult> Get_Web_Rol()
        {
            var web_Rol = db.Web_Rol.Select(r => new { rol_id = r.rol_id, nombre = r.nombre, fkrolestado = r.FK_rol_estado_rips }).Where(r => r.fkrolestado == 1).OrderBy(m => m.nombre);
            return Ok(web_Rol);

            //return db.Web_Rol.Select(r => new { rol_id = r.rol_id, nombre = r.nombre, fkrolestado = r.FK_rol_estado_rips });
        }

        // GET: api/Web_Rol/5
        [ResponseType(typeof(Web_Rol))]
        public async Task<IHttpActionResult> GetWeb_Rol(long id)
        {
            Web_Rol web_Rol = await db.Web_Rol.FindAsync(id);
            if (web_Rol == null)
            {
                return NotFound();
            }

            return Ok(web_Rol);
        }

        // PUT: api/Web_Rol/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWeb_Rol(long id, Web_Rol web_Rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != web_Rol.rol_id)
            {
                return BadRequest();
            }

            db.Entry(web_Rol).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Web_RolExists(id))
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

        // POST: api/Web_Rol
        [ResponseType(typeof(Web_Rol))]
        public async Task<IHttpActionResult> PostWeb_Rol(Web_Rol web_Rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Web_Rol.Add(web_Rol);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = web_Rol.rol_id }, web_Rol);
        }

        // DELETE: api/Web_Rol/5
        [ResponseType(typeof(Web_Rol))]
        public async Task<IHttpActionResult> DeleteWeb_Rol(long id)
        {
            Web_Rol web_Rol = await db.Web_Rol.FindAsync(id);
            if (web_Rol == null)
            {
                return NotFound();
            }

            db.Web_Rol.Remove(web_Rol);
            await db.SaveChangesAsync();

            return Ok(web_Rol);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Web_RolExists(long id)
        {
            return db.Web_Rol.Count(e => e.rol_id == id) > 0;
        }
    }
}