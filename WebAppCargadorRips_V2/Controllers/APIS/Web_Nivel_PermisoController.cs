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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebAppCargadorRips_V2.EF_Models;

namespace WebAppCargadorRips_V2.Controllers.APIS
{
    [Authorize]
    [RoutePrefix("api/PermisosModulos")]
    public class Web_Nivel_PermisoController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: api/Web_Nivel_Permiso
        public IQueryable<Web_Nivel_Permiso> GetWeb_Nivel_Permiso()
        {
            return db.Web_Nivel_Permiso;
        }

        [Route("Listar")]
        [HttpGet]
        // GET: api/Web_Nivel_Permiso/5
        [ResponseType(typeof(Web_Nivel_Permiso))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> GetWeb_Nivel_Permiso(long rol)
        {
            Web_Nivel_Permiso web_Nivel_Permiso = await db.Web_Nivel_Permiso.FirstOrDefaultAsync(np => np.FK_nivelpermiso_rol == rol);
                //Where( np => np.FK_nivelpermiso_rol== id )
            if (web_Nivel_Permiso == null)
            {
                return NotFound();
            }

            return Ok(web_Nivel_Permiso);
        }


        // GET: api/Web_Nivel_Permiso/5
        //[ResponseType(typeof(Web_Nivel_Permiso))]
        [Route("Tablero")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> GetWeb_Nivel_Permiso_Tablero(string rol)
        {
            var nivel_permiso_modulo = db.SP_Get_Modulo_Permiso(rol);

            if (nivel_permiso_modulo == null)
            {
                return NotFound();
            }

            return Ok(nivel_permiso_modulo.Where(r => r.orden == 1).OrderBy(r => r.nombre));
        }

        [Route("Administrar")]
        [HttpGet]
        [ResponseType(typeof(Web_Nivel_Permiso))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> GetWeb_Nivel_Permiso_Administrar(string rol)
        {
            var nivel_permiso_modulo = db.SP_Get_Modulo_Permiso(rol);

            if (nivel_permiso_modulo == null)
            {
                return NotFound();
            }

            return Ok(nivel_permiso_modulo.Where(r => r.orden == 12));
        }

        // PUT: api/Web_Nivel_Permiso/5
        [ResponseType(typeof(void))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> PutWeb_Nivel_Permiso(long id, Web_Nivel_Permiso web_Nivel_Permiso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != web_Nivel_Permiso.nivelpermiso_id)
            {
                return BadRequest();
            }

            db.Entry(web_Nivel_Permiso).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Web_Nivel_PermisoExists(id))
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

        // POST: api/Web_Nivel_Permiso
        [ResponseType(typeof(Web_Nivel_Permiso))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> PostWeb_Nivel_Permiso(Web_Nivel_Permiso web_Nivel_Permiso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Web_Nivel_Permiso.Add(web_Nivel_Permiso);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = web_Nivel_Permiso.nivelpermiso_id }, web_Nivel_Permiso);
        }

        // DELETE: api/Web_Nivel_Permiso/5
        [ResponseType(typeof(Web_Nivel_Permiso))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> DeleteWeb_Nivel_Permiso(long id)
        {
            Web_Nivel_Permiso web_Nivel_Permiso = await db.Web_Nivel_Permiso.FindAsync(id);
            if (web_Nivel_Permiso == null)
            {
                return NotFound();
            }

            db.Web_Nivel_Permiso.Remove(web_Nivel_Permiso);
            await db.SaveChangesAsync();

            return Ok(web_Nivel_Permiso);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Web_Nivel_PermisoExists(long id)
        {
            return db.Web_Nivel_Permiso.Count(e => e.nivelpermiso_id == id) > 0;
        }
    }
}