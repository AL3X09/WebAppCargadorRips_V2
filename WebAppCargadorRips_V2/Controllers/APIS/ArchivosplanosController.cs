﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAppCargadorRips_V2.EF_Models;
using WebAppCargadorRips.Models;
using System.IO;
using System.Security.Permissions;
using System.Security;
using Ionic.Zip;
using WebAppCargadorRips_V2.Models;
using System.Web.Http.Cors;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace WebAppCargadorRips_V2.Controllers.APIS
{
    [Authorize]
    [RoutePrefix("api/Rips")]
    public class ArchivosplanosController : ApiController
    {
        private static RipsEntitiesConnection bd = new RipsEntitiesConnection();
        private string path = HttpContext.Current.Server.MapPath("~/RIPSCargados/");
        private string pathresult = null; //Nombre de la carpeta que almacena los archivos txt tempo por usaurio
        private string nombreZIP = null; //nombre del zip resultante
        private string usuarioZIP = bd.Directorios.Select(s => s.usuario_directorios).First(); //objeto que tiene los valores de los directorios del servidor del servicio
        private string contraseñaZIP = bd.Directorios.Select(s => s.contraseña_directorios).First(); //objeto que tiene los valores de los directorios del servidor del servicio
        private string directorioZIP = bd.Directorios.Select(s => s.directorio_entrada).First(); //objeto que tiene los valores de los directorios del servidor del servicio

        //Constructor        
        public ArchivosplanosController()
        {
            
        }

        ///<summary>
        /// Metodo asincrono carga un archivo con datos del respectivo formulario y genera el consecutivo 
        /// de preradicación
        ///</summary>
        [Route("Upload")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> UploadSingleFile()
        {

            //variables que se estan reciviendo del front
            // obtengo las variables enviadas por el formulario
            var tipoUsuario = HttpContext.Current.Request.Params["tipoUsuario"];
            var categoria = HttpContext.Current.Request.Params["categoria"];
            var IVE = HttpContext.Current.Request.Params["IVE"];
            var NOPOS = HttpContext.Current.Request.Params["NOPOS"];
            var fechaInicio = HttpContext.Current.Request.Params["fechaInicio"];
            var fechaFin = HttpContext.Current.Request.Params["fechaFin"];
            var idUsuario = HttpContext.Current.Request.Params["idUsuario"];
            //creo una variable para manejar los mensajes
            var MSG = new List<object>();

            //valido la información recivida del formulario
            if (!String.IsNullOrEmpty(tipoUsuario))
            {
                tipoUsuario = tipoUsuario;
            }

            if (!String.IsNullOrEmpty(IVE))
            {
                tipoUsuario = "1";
                categoria = "6";
            }

            if (!String.IsNullOrEmpty(NOPOS))
            {
                /*tipoUsuario = "2";
                categoria = "1";*/
                tipoUsuario = "2";
                categoria = "5";
            }

            //Valido que el formulario sea enviado con el formato permitido.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                //Armo mensaje y envio al cliente
                MSG.Add(new { type = "error", value = "Formato de envio no permitido" });

                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.UnsupportedMediaType)
                    );
                //TODO envio error a la base de datos

            }

            //almaceno la información solo del formulario en la base de datos
            try
            {
                //inserto en la tabla web_validacion
                //3 es estado aprobado sin errores
                var result = bd.SP_Web_Insert_Datos_Rips_a_Validar(tipoUsuario, categoria,false,fechaInicio, fechaFin, idUsuario, "3").First();

                //si la respuesta del porcedimeinto de insercion a la tabla validacion, es satisfactoria realizo el almacenamiento de los archivos
                if (result.codigo == 201)
                {

                    try
                    {
                        //Inserto en la tabla web_preradicado
                        var preradicadoResult = bd.SP_Web_Insert_Rips_a_Preradicar(Convert.ToInt64(idUsuario), result.ultimoIdInsert).First();
                        //Si el SP de insert de preradicado retorno el una respuesta satisfactoria cargo el archivo
                        if (preradicadoResult.codigo == 201)
                        {
                            //intemto crear y guardar los archivos en el forlder para insertalo
                            try
                            {

                                //creo el nombre del path
                                pathresult = path + @"\" + preradicadoResult.ultimoIdInsertPreradicado;
                                //consulto que exista el folder raiz
                                if (!Directory.Exists(pathresult))
                                {
                                    Directory.CreateDirectory(pathresult);
                                    var permisos = new FileIOPermission(FileIOPermissionAccess.AllAccess, pathresult);
                                    var permisosSET = new PermissionSet(PermissionState.None);
                                    permisosSET.AddPermission(permisos);

                                    //variables que almacenan temporalmente los archivos para no perderlos
                                    var streamProvider = new MultipartFormDataStreamProvider(path);
                                    await Request.Content.ReadAsMultipartAsync(streamProvider);

                                    using (ZipFile zip = new ZipFile())
                                    {
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
                                                fileName = fileName.Substring(0, 2) + ".txt";
                                                File.Move(archivo.LocalFileName, Path.Combine(pathresult, fileName));

                                            }

                                        }

                                        //comprimo los archivos
                                        //https://stackoverflow.com/questions/24391794/c-sharp-move-files-to-zip-folder
                                        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                                        zip.AddDirectory(pathresult);
                                        zip.Save(pathresult + ".zip");
                                        nombreZIP = pathresult + ".zip";
                                    }// FIN using zip library


                                    /**
                                    * Envio la carpeta zip al reosritotio local del servicio
                                    **/
                                    try
                                    {

                                        NetworkConnection.Impersonate(@"SDS", @usuarioZIP, @contraseñaZIP, delegate
                                        {

                                            var permisos2 = new FileIOPermission(FileIOPermissionAccess.AllAccess, directorioZIP);
                                            var permisosSET2 = new PermissionSet(PermissionState.None);
                                            permisosSET2.AddPermission(permisos2);

                                            if (!Directory.Exists(directorioZIP + @"\"))
                                            {

                                                Directory.CreateDirectory(directorioZIP + @"\");
                                            }

                                            if (File.Exists(string.Format(@"{0}\{1}.zip", directorioZIP, preradicadoResult.ultimoIdInsertPreradicado)))
                                            {
                                                File.Delete(string.Format(@"{0}\{1}.zip", directorioZIP, preradicadoResult.ultimoIdInsertPreradicado));
                                            }

                                            File.Copy(@nombreZIP, string.Format(@"{0}\{1}.zip", directorioZIP, preradicadoResult.ultimoIdInsertPreradicado));

                                        });

                                    }
                                    catch (Exception e)
                                    {
                                        //Envio al archivo log 
                                        LogsController log = new LogsController(e.ToString());
                                        log.createFolder();
                                        //Envio mensaje de Error a la Vista
                                        MSG.Add(new { type = "error", value = e.Message.ToString() });
                                        throw;
                                    }
                                    /**
                                     * FIN Envio la carpeta zip al reosritotio local del servicio
                                    **/

                                    /**
                                        * libero de archivos en servidor para limpiar memoria
                                        * OJO CON ESTA LINEA: ESTA ELIMINA ARCHIVOS TEMPORALES PODRIA ELIMINAR
                                        * DE OTROS USUARIOS SEGUN RECURRENCIA DE USUARIOS
                                     **/
                                    Directory.Delete(pathresult, true);


                                }// FIN if !Directory.Exists(pathresult)
                                var linq1 = bd.Web_Mensaje.Where(s => s.codigo == 1009).First();

                                MSG.Add(new { type = linq1.tipo, value = linq1.cuerpo, codigo = preradicadoResult.codigo, consec = preradicadoResult.ultimoIdInsertPreradicado });

                            }// FIN try
                            //error al cargar en el servidor del servicio integrado de WIN
                            catch (Exception e) // si hay un error al crear y guardar el fichero cambio el estado del registro en la tabla Auditoria.Web_Validacion
                            {
                                //Cambio el estado la tabla web_preradicado a disponible
                                var UpdatepreradicadoResult = bd.SP_Web_Update_Estado_Disponible_Preradicado(result.ultimoIdInsert, preradicadoResult.ultimoIdInsertPreradicado).First();
                                //Cambio el estado la tabla web_validacion estado error de carga 
                                var UpdatewebvalidacionError = bd.SP_Web_Update_Estado_Error_Carga_WebValidacion(result.ultimoIdInsert).First();

                                //envio log a archivo de logs 
                                LogsController log = new LogsController(e.ToString());
                                log.createFolder();
                                /**
                                 * libero de archivos temporales 
                                 * OJO CON ESTA LINEA: ESTA ELIMINA ARCHIVOS TEMPORALES PODRIA ELIMINAR
                                 * DE OTROS USUARIOS SEGUN RECURRENCIA DE USUARIOS
                                 **/
                                Directory.Delete(pathresult, true);
                                File.SetAttributes(nombreZIP, FileAttributes.Normal);
                                File.Delete(nombreZIP);
                                //Envio mensaje de error a la vista
                                MSG.Add(new { type = "error", value = "No se cargaron los archivos correctamente en el servidor, " + e.Message.ToString() });

                            }

                        }
                        else
                        {
                            MSG.Add(new { type = "error", value = "No se almaceno la informacón correctamente en la base de datos " });
                            throw new HttpResponseException(
                            Request.CreateResponse(HttpStatusCode.UnsupportedMediaType)
                            );
                        }
                    }
                    catch (Exception e) // si hay un error al crear y guardar el fichero cambio el estado del registro en la tabla Auditoria.Web_Validacion
                    {
                        LogsController log = new LogsController(e.ToString());
                        log.createFolder();
                        //Envio mensaje de error a la vista
                        MSG.Add(new { type = "error", value = "No se cargaron los archivos correctamente en el servidor, " + e.Message.ToString() });
                    }

                }//fin if respuesta satisfactoria
                else
                {
                    //envio mensaje al usuario final
                    MSG.Add(new { type = result.tipo, value = result.mensaje });
                }

            }
            catch (Exception e)
            {
                //Envio al archivo log 
                LogsController log = new LogsController(e.ToString());
                log.createFolder();
                //Envio mensaje de Error a la Vista
                MSG.Add(new { type = "error", value = e.Message.ToString() });
                
            }

            return Json(MSG);
        }

        ///<summary>
        /// Metodo asincrono lista los archivos del prestador
        ///</summary>
        [Route("GetListadoRips")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> GetListadoRipstAsync(int fktoken)
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            string sEcho = nvc["sEcho"].ToString();
            string sSearch = nvc["sSearch"].ToString();
            int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
            //this provides display length of table it can be 10,25, 50
            int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
            //iSortCol gives your Column numebr of for which sorting is required
            int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
            //provides your sort order (asc/desc)
            string sortOrder = nvc["sSortDir_0"].ToString();
            var result = new List<VW_Auditoria_RIPS>();//new List<VW_Listado_Estado_Rips>();
            var Cantidad = 0;
            //Console.WriteLine(Convert.ToDateTime(sSearch).ToString());
            //Search query when sSearch is not empty
            if (sSearch != "" && sSearch != null) //If there is search query
            {
                

                result = (from VLR in bd.VW_Auditoria_RIPS
                          where VLR.web_usuario_id == fktoken
                          where (
                          VLR.preradicado.Value.ToString().Contains(sSearch.ToString())
                          || VLR.radicado.Value.ToString().Contains(sSearch.ToString())
                          || VLR.tipo_usuario_afilicion.ToString().ToLower().Contains(sSearch.ToString().ToLower())
                          || VLR.categoria.ToString().ToLower().Contains(sSearch.ToString().ToLower())
                          || VLR.estado_general.ToString().ToLower().Contains(sSearch.ToString().ToLower())
                          || EntityFunctions.TruncateTime(VLR.preradicado_fecha).ToString().Contains(sSearch.ToString())
                          || EntityFunctions.TruncateTime(VLR.periodo_inicio).ToString().Contains(sSearch.ToString())
                          || EntityFunctions.TruncateTime(VLR.periodo_fin).ToString().Contains(sSearch.ToString())

                          )
                          group VLR by new { VLR.periodo_inicio, VLR.extranjero } into grp
                          select grp.OrderBy(y => y.preradicado).FirstOrDefault()).ToList();

                //get total value count
                Cantidad = result.Count();
                // Call Funcion de ordenado  y proveer sorted Data, then Skip using iDisplayStart  
                result = SortFunction(iSortCol, sortOrder, result).Skip(iDisplayStart).Take(iDisplayLength).ToList();

                /*orderby VLR.preradicado ascending
                          select VLR).ToList();*/
            }
            else //Si no hay valores a buscar
            {
                result = (from VLR in bd.VW_Auditoria_RIPS
                          where VLR.web_usuario_id == fktoken
                          group VLR by new { VLR.periodo_inicio, VLR.extranjero} into grp
                          select grp.OrderByDescending(y => y.periodo_inicio).FirstOrDefault()
                          ).ToList();
                //).Where(s => s.periodo_inicio == result.Max(x=> x.periodo_inicio)).First();
                //https://stackoverflow.com/questions/38204748/linq-query-to-order-a-list-based-on-date-and-get-distinct
                //select VLR).GroupBy(r => new { r.tipo_usuario_afilicion }).Max(k => k.periodo_inicio).ToList();
                //select VLR).OrderByDescending(r => r.periodo_inicio).ToList();
                // Call Funcion de ordenado  y proveer sorted Data, then Skip using iDisplayStart  select VLR).Max(k => k.periodo_inicio).ToList();

                //get total value count
                Cantidad = result.Count();

                result = SortFunction(iSortCol, sortOrder, result).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }

            

            //se Creo un modelo para datatable paging and sending data & enter all the required values
            var VWListadoPaged = new SysDataTablePager<VW_Auditoria_RIPS>(result, Cantidad, Cantidad, sEcho);

            return VWListadoPaged;
        }


        //Funcion de ordenado
        private List<VW_Auditoria_RIPS> SortFunction(int iSortCol, string sortOrder, List<VW_Auditoria_RIPS> list)
        {

            //Sorting for String columns
            if (iSortCol == 1 || iSortCol == 0)
            {
                Func<VW_Auditoria_RIPS, long?> orderingFunction = (c => iSortCol == 1 || iSortCol == 0 ? c.preradicado : c.preradicado); // compara la columna a ordenar

                //if (sortOrder == "desc")
                if (sortOrder == "desc")
                    {
                    list = list.OrderByDescending(orderingFunction).ToList();
                }
                else
                {
                    list = list.OrderBy(orderingFunction).ToList();

                }
            }
            //Sorting for Int columns TODO
            else if (iSortCol == 2)
            {
                Func<VW_Auditoria_RIPS, string> orderingFunction = (c => iSortCol == 2 ? c.tipo_usuario_afilicion : c.tipo_usuario_afilicion); // compara la columna a ordenar

                if (sortOrder == "desc")
                {
                    list = list.OrderByDescending(orderingFunction).ToList();
                }
                else
                {
                    list = list.OrderBy(orderingFunction).ToList();

                }
            }

            //Sorting for Int columns TODO
            else if (iSortCol == 3)
            {
                Func<VW_Auditoria_RIPS, string> orderingFunction = (c => iSortCol == 2 ? c.categoria : c.categoria); // compara la columna a ordenar

                if (sortOrder == "desc")
                {
                    list = list.OrderByDescending(orderingFunction).ToList();
                }
                else
                {
                    list = list.OrderBy(orderingFunction).ToList();

                }
            }

            //Sorting for Int columns extranjero
            else if (iSortCol == 4)
            {
                Func<VW_Auditoria_RIPS, bool?> orderingFunction = (c => iSortCol == 2 ? false : c.extranjero); // compara la columna a ordenar

                if (sortOrder == "desc")
                {
                    list = list.OrderByDescending(orderingFunction).ToList();
                }
                else
                {
                    list = list.OrderBy(orderingFunction).ToList();

                }
            }

            //Sorting for Int columns TODO
            else if (iSortCol == 5)
            {
                Func<VW_Auditoria_RIPS, DateTime?> orderingFunction = (c => iSortCol == 2 ? c.periodo_inicio : c.periodo_inicio); // compara la columna a ordenar

                if (sortOrder == "desc")
                {
                    list = list.OrderByDescending(orderingFunction).ToList();
                }
                else
                {
                    list = list.OrderBy(orderingFunction).ToList();

                }
            }

            //Sorting for Int columns TODO
            else if (iSortCol == 6)
            {
                Func<VW_Auditoria_RIPS, DateTime?> orderingFunction = (c => iSortCol == 2 ? c.periodo_fin : c.periodo_fin); // compara la columna a ordenar

                if (sortOrder == "desc")
                {
                    list = list.OrderByDescending(orderingFunction).ToList();
                }
                else
                {
                    list = list.OrderBy(orderingFunction).ToList();

                }
            }

            //Sorting for Int columns TODO
            else if (iSortCol == 7)
            {
                Func<VW_Auditoria_RIPS, DateTime?> orderingFunction = (c => iSortCol == 2 ? c.preradicado_fecha : c.preradicado_fecha); // compara la columna a ordenar

                if (sortOrder == "desc")
                {
                    list = list.OrderByDescending(orderingFunction).ToList();
                }
                else
                {
                    list = list.OrderBy(orderingFunction).ToList();

                }
            }

            //Sorting for Int columns TODO
            else if (iSortCol == 8)
            {
                Func<VW_Auditoria_RIPS, string> orderingFunction = (c => iSortCol == 2 ? c.estado_general : c.estado_general); // compara la columna a ordenar

                if (sortOrder == "desc")
                {
                    list = list.OrderByDescending(orderingFunction).ToList();
                }
                else
                {
                    list = list.OrderBy(orderingFunction).ToList();

                }
            }

            return list;
        }

    }
}
