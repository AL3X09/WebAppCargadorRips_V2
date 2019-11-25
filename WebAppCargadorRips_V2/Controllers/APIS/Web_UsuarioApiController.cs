using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebAppCargadorRips_V2.EF_Models;
using WebAppCargadorRips_V2.Models;

namespace WebAppCargadorRips_V2.Controllers.APIS
{
    [Authorize]
    [RoutePrefix("api/Usuarios")]
    public class Web_UsuarioApiController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();
        private string path = HttpContext.Current.Server.MapPath("~/Img/avatarsusers/"); //crea la carpeta apropiada
        private string pathimagen = "Img/avatarsusers/"; // esta liena debe ser igual a la linea anterior ya que es la que tiene el nombre del donde se alamcenara la imagen

        // GET: api/Web_Usuario
        public IEnumerable<Object> GetWeb_Usuario()
        {
            return db.SP_GetAllInfoUsers();
        }

        // GET: api/Web_Usuario/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> GetWeb_Usuario(long id, long rol)
        {
            object[] web_Usuario;

            web_Usuario = db.SP_GetAllInfoUsers().Where(w => w.id_rol.Equals(rol) && w.usuario_id.Equals(id)).ToArray();
            
            //si el retorno del usuario llega en cero busca si es un usario de otro rol
            if (web_Usuario.Count() == 0 )
            {
                web_Usuario = await db.Web_Administrador.Where(a => a.administrador_id == id && a.FK_web_administrador_rol.Equals(rol)).Select(a => new {
                    id = a.administrador_id,
                    nombres = a.nombres,
                    apellidos = a.apellidos,
                    descripcion = a.descripcion,
                    correo = a.correo,
                    extencion = a.extension,
                    imagen = a.imagen,
                    id_rol = a.FK_web_administrador_rol
                }).ToArrayAsync();
                
                //await db.SaveChangesAsync();
            }
            
            else if (web_Usuario == null)
            {
                return NotFound();
            }

            return Ok(web_Usuario);
            
        }

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // POST: api/UpdateDatosUsuario
        [Route("GetOtroUser")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // GET: api/Web_Usuario/5
        public async Task<IHttpActionResult> GetWeb_Usuario_Admin(long id)
        {
            var web_Admin = await db.Web_Administrador.Where(a => a.administrador_id == id).Select(a => new {
                             id = a.administrador_id,
                             nombres = a.nombres,
                             apellidos = a.apellidos,
                             descripcion = a.descripcion,
                             correo = a.correo,
                             extencion = a.extension,
                             imagen = a.imagen,
                             id_rol = a.FK_web_administrador_rol
            }).ToListAsync();

            if (web_Admin == null)
            {
                return NotFound();
            }

            return Ok(web_Admin);
        }*/

        // PUT: api/Web_Usuario/5
        [ResponseType(typeof(void))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        [EnableCors(origins: "*", headers: "*", methods: "*")]
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

        /// <summary>
        /// Actualizar la imagen de un usuario especifico
        /// </summary>
        [Route("PostUploadAvatar")]
        [HttpPost]
        [Authorize]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> UpdateAvatar()
        {
            //consulto que exista el folder raiz
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                var permisos = new FileIOPermission(FileIOPermissionAccess.AllAccess, path);
                var permisosSET = new PermissionSet(PermissionState.None);
                permisosSET.AddPermission(permisos);
                if (permisosSET.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
                {
                }

            }

            //variables que se estan reciviendo del front
            // obtengo las variables enviadas por el formulario
            var nombreimagen = HttpContext.Current.Request.Files["avatar"];

            var idUser = HttpContext.Current.Request.Params["usuario_id"];
            var codigousuario = HttpContext.Current.Request.Params["codigo"];
            //almaceno el path donde se guarda la imagen
            var contentType = "." + nombreimagen.ContentType.Substring(6);
            var pathguardo = pathimagen + codigousuario + contentType;
            //creo una variable para manejar los mensajes
            var MSG = new List<object>();

            //valido la información recivida del formulario
            if (String.IsNullOrEmpty(nombreimagen.FileName) && nombreimagen.ContentLength == 0)
            {
                MSG.Add(new { type = "error", value = "Debe cargar una imagen.", codigo = 0 });
            }
            //Valido que el formulario sea enviado con el formato permitido.
            else if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                //Armo mensaje y envio al cliente
                MSG.Add(new { type = "error", value = "Formato de envio no permitido", codigo = 0 });

                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.UnsupportedMediaType)

                    );
                //TODO envio error a la base de datos

            }
            else
            {

                //almaceno la información en la base de datos
                try
                {
                    var result = db.SP_UpdateAvatarUser(Int32.Parse(idUser), pathguardo).First();

                    //si la respuesta del porcedimeinto es satisfactoria realizo el almacenamiento de los archivos
                    if (result.codigo == 201)
                    {
                        //variables que almacenan temporalmente los archivos para no perderlos
                        var streamProvider = new MultipartFormDataStreamProvider(path);
                        await Request.Content.ReadAsMultipartAsync(streamProvider);

                        //carga de archivos a la carpeta
                        

                            foreach (MultipartFileData archivo in streamProvider.FileData)
                            {
                                string fileName = "";
                                if (string.IsNullOrEmpty(archivo.Headers.ContentDisposition.FileName))
                                {
                                    fileName = Guid.NewGuid().ToString();
                                }
                                fileName = archivo.Headers.ContentDisposition.FileName;
                                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                                {
                                    fileName = fileName.Trim('"');
                                }
                                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                                {
                                    fileName = Path.GetFileName(fileName);
                                }
                                if (archivo != null && fileName != "")
                                {
                                    fileName = codigousuario + contentType;
                                    if (File.Exists(Path.Combine(path, fileName)))
                                    {
                                        File.Replace(archivo.LocalFileName, Path.Combine(path, fileName), null);
                                    }
                                    else
                                    {
                                        File.Move(archivo.LocalFileName, Path.Combine(path, fileName));
                                    }

                                }

                            }

                            var linq1 = db.Web_Mensaje.Where(s => s.codigo == 1011).First();

                            MSG.Add(new { type = linq1.tipo, value = linq1.cuerpo, codigo = result.codigo });

                       
                    }//fin if respuesta satisfactoria
                    else
                    {
                        //envio mensaje al usuario final
                        MSG.Add(new { type = result.tipo, value = result.mensaje, codigo = result.codigo });
                    }

                }
                catch (Exception e)
                {
                    //envio log a archivo de logs 
                    LogsController log = new LogsController(e.ToString());
                    log.createFolder();
                    MSG.Add(new { type = "error", value = e.ToString() });
                    //todo enviar error a la  base de datos
                }//end catch

            }//end else

            return Json(MSG);
        }

        ///<summary>
        /// Lista de los Contactos existentes en el universo.
        /// </summary>
        /// <returns>Todos los valores disponibles en el universo</returns>
        [HttpGet]
        [Route("ListarContactos")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetContactos()
        {
            var result = from e in db.Web_Administrador
                         where e.FK_web_administrador_estado_rips == 1
                         select new
                         {
                             id = e.administrador_id,
                             nombre = e.nombres,
                             apellido = e.apellidos,
                             descripcion = e.descripcion,
                             correo = e.correo,
                             extencion = e.extension,
                             imagen = e.imagen
                         };
            return result;
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