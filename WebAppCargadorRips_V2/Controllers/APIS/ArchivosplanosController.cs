using System;
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

namespace WebAppCargadorRips_V2.Controllers.APIS
{
    [RoutePrefix("api/Rips")]
    public class ArchivosplanosController : ApiController
    {
        private RipsEntitiesConnection bd = new RipsEntitiesConnection();

        //Constructor        
        public ArchivosplanosController()
        {
            //this.radicadoripsRepository = new RadicadoRipsRepository(new BD_CargadorRipsConnection());
        }

        ///<summary>
        /// Metodo asincrono carga un archivo con datos de su respectivo formulario
        ///</summary>
        [Route("GetListadoRips")]
        [HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
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
            var result = new List<VW_Listado_Estado_Rips>();//new List<VW_Listado_Estado_Rips>();
            //Search query when sSearch is not empty
            if (sSearch != "" && sSearch != null) //If there is search query
            {

                result = (from VLR in bd.VW_Listado_Estado_Rips
                          where (VLR.codigo.ToString().Contains(sSearch.ToString())
                          || VLR.tipo_usuario.ToString().ToLower().Contains(sSearch.ToString())
                          || VLR.categoria.ToString().ToLower().Contains(sSearch.ToString())
                          || VLR.periodo_fecha_inicio.Value.ToString().Contains(sSearch.ToString())
                          || VLR.periodo_fecha_fin.Value.ToString().Contains(sSearch.ToString())
                          || VLR.fecha_cargo.Value.ToString().Contains(sSearch.ToString())
                          || VLR.estado_web_validacion.ToString().ToLower().Contains(sSearch.ToString())
                          || VLR.estado_web_preradicacion.ToString().ToLower().Contains(sSearch.ToString())
                          || VLR.estado_servicio_validacion.ToString().ToLower().Contains(sSearch.ToString())
                          || VLR.estado_radicacion.ToString().ToLower().Contains(sSearch.ToString())
                          )
                          where VLR.FK_usuario == fktoken
                          orderby VLR.fecha_cargo descending
                          select VLR).ToList();
                // Call Funcion de ordenado  y proveer sorted Data, then Skip using iDisplayStart  
                result = SortFunction(iSortCol, sortOrder, result).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }
            else //Si no hay valores a buscar
            {
                result = (from VLR in bd.VW_Listado_Estado_Rips
                          where VLR.FK_usuario == fktoken
                          orderby VLR.fecha_cargo descending
                          select VLR).ToList();
                // Call Funcion de ordenado  y proveer sorted Data, then Skip using iDisplayStart  
                result = SortFunction(iSortCol, sortOrder, result).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }

            //get total value count
            var Cantidad = bd.VW_Listado_Estado_Rips.Where(v => v.FK_usuario == fktoken).Count();

            //se Creo un modelo para datatable paging and sending data & enter all the required values
            var VWListadoPaged = new SysDataTablePager<VW_Listado_Estado_Rips>(result, Cantidad, Cantidad, sEcho);

            return VWListadoPaged;
        }


        //Funcion de ordenado
        private List<VW_Listado_Estado_Rips> SortFunction(int iSortCol, string sortOrder, List<VW_Listado_Estado_Rips> list)
        {

            //Sorting for String columns
            if (iSortCol == 1 || iSortCol == 0)
            {
                Func<VW_Listado_Estado_Rips, long> orderingFunction = (c => iSortCol == 1 ? c.codigo : c.codigo); // compara la columna a ordenar

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
                Func<VW_Listado_Estado_Rips, long> orderingFunction = (c => iSortCol == 2 ? c.validacion_id : c.codigo); // compara la columna a ordenar

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
