using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAppCargadorRips_V2.EF_Models;

namespace WebAppCargadorRips_V2.Controllers.APIS
{
    //[Authorize]
    [RoutePrefix("api/Log")]
    public class LogsController : ApiController
    {
        private static DateTime fechaActual = DateTime.Now;
        private RipsEntitiesConnection bd = new RipsEntitiesConnection();
        private static string pathlogs = HttpContext.Current.Server.MapPath("~/Logs/");
        private static string nombrefile = pathlogs + "/log_" + fechaActual.Year + ".txt";
        private string Texto;


        //Constructor
        public LogsController(string texto)
        {
            Texto = texto;
            //createFolder();
        }

        public LogsController()
        {
        }

        //creo el folder
        public void createFolder()
        {
            try
            {
                //consulto que exista el folder raiz
                if (!Directory.Exists(pathlogs))
                {
                    Directory.CreateDirectory(pathlogs);
                    var permisos = new FileIOPermission(FileIOPermissionAccess.AllAccess, pathlogs);
                    var permisosSET = new PermissionSet(PermissionState.None);
                    permisosSET.AddPermission(permisos);
                    if (permisosSET.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
                    {
                    }

                }
                createFile(Texto);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
        }

        //creo el archivo y texto
        public void createFile(string texto)
        {
            try
            {

                // Creo texto en el archivo
                FileStream fs = null;
                if (!File.Exists(nombrefile))
                {
                    using (fs = File.Create(nombrefile))
                    {

                    }
                }

                writeinFile(nombrefile, texto);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        //agrego texto al archivo y texto
        public void writeinFile(string file, string texto)
        {
            try
            {

                if (File.Exists(nombrefile))
                {
                    File.AppendAllText(nombrefile, string.Concat("\r\n-------------------------------------- ", fechaActual, " --------------------------\r\n", texto, "\r\n"));
                    //using (StreamWriter sw = new StreamWriter(file))
                    //{
                    //sw.Write(texto);
                    //sw.WriteLine(string.Concat("-------------------------------------- ", fechaActual, " --------------------------\r\n"));
                    //sw.WriteLine(texto);
                    //}
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        //http://techxposer.com/2017/12/25/downloading-pdf-file-server-client-using-asp-net-c/
        //https://www.dotnetspider.com/resources/21758-Downloading-Files-From-Server-To-CLient-Using.aspx
        ///<summary>
        /// Descargar Archivo LOGS
        ///</summary>
        [HttpGet]
        [Route("descargar")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Download_Logs() 
        {
            //HttpResponseMessage response = null;
            HttpResponse Response = HttpContext.Current.Response;

            //Create HTTP Response.
            //HttpResponseMessage Response1 = Request.CreateResponse(HttpStatusCode.OK);

            try
            {


                if (File.Exists(nombrefile))
                {
                    // Create New instance of FileInfo class to get the properties of the file being downloaded
                    FileInfo myfile = new FileInfo(nombrefile);

                    // Clear the content of the response
                    Response.ClearContent();

                    // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + myfile.Name);

                    // Add the file size into the response header 
                    Response.AddHeader("Content-Length", myfile.Length.ToString());

                    // Set the ContentType
                    Response.ContentType = "text/plain";

                    // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
                    Response.TransmitFile(myfile.FullName);

                    // End the response
                    Response.End();
                    //return Response;
                }
                else
                {
                    //string json = "{\"name\":\"Joe\"}";
                    Response.ClearContent();
                    Response.ContentType = "text/javascript";
                    //Response.Write(json);
                    Response.Write("<script language='javascript'>alert('ERROR "+ HttpStatusCode.NoContent+"');</script>");
                    //Response.End();
                }

            }
            catch (Exception e)
            {
                Response.ClearContent();
                Response.ContentType = "text/javascript";
                Response.Write("<script language=javascript>alert('ERROR " + e.ToString() + "');</script>");
                
            }

        }




        }
}
