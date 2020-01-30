using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    //[Authorize]
    [RoutePrefix("api/ActasGeneradas")]
    public class ActasGeneradasController : ApiController
    {
        private static readonly RipsEntitiesConnection db = new RipsEntitiesConnection();
        private HttpResponse Response = HttpContext.Current.Response;
        private string usuarioZIP = db.Directorios.Select(s => s.usuario_directorios).First(); //objeto que tiene los valores de los directorios del servidor del servicio
        private string contraseñaZIP = db.Directorios.Select(s => s.contraseña_directorios).First(); //objeto que tiene los valores de los directorios del servidor del servicio
        private string directorioAprobada = db.Directorios.Select(s => s.directorio_actas_aprobadas).First(); //objeto que tiene los valores de los directorios del servidor del servicio
        private string directorioRechazada = db.Directorios.Select(s => s.directorio_actas_rechazadas).First(); //objeto que tiene los valores de los directorios del servidor del servicio


        //Constructor        
        public ActasGeneradasController()
        {

        }

        [Route("Rechazados")]
        [HttpGet]
        // GET: api/Web_Nivel_Permiso/5
        //[ResponseType(typeof(Web_Nivel_Permiso))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> GetActas_Rechazos(long prerradicado)
        {
            //HttpResponse Response = HttpContext.Current.Response;
            try
            {
                string nombrefile = string.Format(@"{0}\{1}{2}{3}", directorioRechazada, "Prerrad", prerradicado, " Errores.pdf");

                NetworkConnection.Impersonate(@"SDS", @usuarioZIP, @contraseñaZIP, delegate
                {

                    var permisos2 = new FileIOPermission(FileIOPermissionAccess.AllAccess, directorioRechazada);
                    var permisosSET2 = new PermissionSet(PermissionState.None);
                    permisosSET2.AddPermission(permisos2);

                    /*if (!Directory.Exists(nombrefile))
                    {

                        Response.Write(new { type = "error", value = "No se encuentran los archivos a descargar." });
                    }*/

                    if (File.Exists(nombrefile))
                    {
                        // Create New instance of FileInfo class to get the properties of the file being downloaded
                        FileInfo myfile = new FileInfo(nombrefile);

                        // Clear the content of the response
                        Response.ClearContent();

                        // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + myfile.Name.Replace(" ", ""));

                        // Add the file size into the response header
                        Response.AddHeader("Content-Length", myfile.Length.ToString());

                        // Set the ContentType
                        Response.ContentType = "application/pdf";

                        // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
                        Response.TransmitFile(myfile.FullName);

                        // End the response
                        Response.End();

                        // Buffer response so that page is sent
                        // after processing is complete.
                        Response.BufferOutput = true;
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('Error: No se encuentran los archivos a descargar.');</script>");
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                        //Response.Write("<script language=javascript>alert('ERROR');</script>");
                    }

                });

               

            }
            catch (Exception e){
                Response.Write("<script language=javascript>alert('Error: Se encontro un error en el servidor de descarga.');</script>");
                //Response.Write(new { type = "error", value = "Se encontro un error en el servidor de descarga, " + e.Message.ToString() });
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            }
            
            await db.SaveChangesAsync();
            return Ok(HttpStatusCode.OK);
            
        }

        [Route("Radicados")]
        [HttpGet]
        // GET: api/Web_Nivel_Permiso/5
        //[ResponseType(typeof(Web_Nivel_Permiso))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> GetActas_Aprobado(long radicado, long prerradicado)
        {
            //HttpResponse Response = HttpContext.Current.Response;
            try
            {
                string nombrefile = string.Format(@"{0}\{1}{2}{3}{4}{5}", directorioAprobada, "Rad", radicado, "-Prerrad", prerradicado, " Acta de Validación.pdf"); //directorioRechazada + @"\" + "Prerrad" + preradicado;

                NetworkConnection.Impersonate(@"SDS", @usuarioZIP, @contraseñaZIP, delegate
                {

                    var permisos2 = new FileIOPermission(FileIOPermissionAccess.AllAccess, directorioRechazada);
                    var permisosSET2 = new PermissionSet(PermissionState.None);
                    permisosSET2.AddPermission(permisos2);

                    if (File.Exists(nombrefile))
                    {
                        // Create New instance of FileInfo class to get the properties of the file being downloaded
                        FileInfo myfile = new FileInfo(nombrefile);

                        // Clear the content of the response
                        Response.ClearContent();

                        // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + myfile.Name.Replace(" ", ""));

                        // Add the file size into the response header
                        Response.AddHeader("Content-Length", myfile.Length.ToString());

                        // Set the ContentType
                        Response.ContentType = "application/pdf";

                        // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
                        Response.TransmitFile(myfile.FullName);

                        // End the response
                        Response.End();

                        // Buffer response so that page is sent
                        // after processing is complete.
                        Response.BufferOutput = true;
                    }

                    else
                    {
                        //Response.Write(new { type = "error", value = "No se encuentran los archivos a descargar." });
                        Response.Write("<script language=javascript>alert('Error: No se encuentran los archivos a descargar.');</script>");
                        //Response.Write(new { type = "error", value = "Se encontro un error en el archivo a descargar" + e.Message.ToString() });
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }

                });



            }
            catch (Exception e)
            {
                Response.Write("<script language=javascript>alert('Error: Se encontro un error en el servidor de descarga.');</script>");
                //Response.Write(new { type = "error", value = "Se encontro un error en el servidor de descarga, " + e.Message.ToString() });
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            }

            await db.SaveChangesAsync();
            return Ok(HttpStatusCode.OK);

        }



        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        */

    }
}