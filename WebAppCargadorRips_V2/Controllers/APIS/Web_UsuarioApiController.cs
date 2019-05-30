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
using WebAppCargadorRips_V2.Models;

namespace WebAppCargadorRips_V2.Controllers.APIS
{
    [RoutePrefix("api/Usuarios")]
    public class Web_UsuarioApiController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: api/Web_Usuario
        public IEnumerable<Object> GetWeb_Usuario()
        {
            return db.SP_GetAllInfoUsers();
        }

        // GET: api/Web_Usuario/5
        public async Task<IHttpActionResult> GetWeb_Usuario(long id)
        {
            var web_Usuario = db.SP_GetAllInfoUsers().Where(w => w.usuario_id.Equals(id)).ToArray();
            await db.SaveChangesAsync();

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // POST: api/UpdateDatosUsuario
        [Route("UpdateDatosUsuario")]
        [HttpPost]
        //[ResponseType(typeof(ActualizarDatosViewModel))]
        public async Task<Object> UpdateDatosUsuario(ActualizarDatosViewModel datos)
        {
            //creo una variable para manejar los mensajes
            var MSG = new List<object>();

            if (!ModelState.IsValid)
            {
               
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {

                try
                {
                    var result = db.SP_UpdateDatosUser(datos.usuario_id, datos.codigo, datos.nombres, datos.apellidos, datos.telefono, datos.razon_social, datos.correo, datos.id_rol, datos.id_estado).First();
                    await db.SaveChangesAsync();

                    return Json(result);
                }
                catch (Exception e)
                {
                    //envio mensaje a usuarios
                    var linq1 = db.Web_Mensaje.Where(s => s.codigo == 412).First();
                    MSG.Add(new { type = linq1.tipo, value = linq1.cuerpo, codigo = linq1.codigo });

                    //envio log a archivo de logs 
                    LogsController log = new LogsController(e.ToString());
                    log.createFolder();
                    //TODO enviar a la base de datos
                    return Json(MSG);
                }

            }
            //return NotFound();
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