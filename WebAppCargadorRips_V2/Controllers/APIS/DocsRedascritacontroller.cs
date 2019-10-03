using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using WebAppCargadorRips_V2.EF_Models;

namespace WebAppCargadorRips_V2.Controllers.APIS
{
    [Authorize]
    [RoutePrefix("api/DocsRedadscrita")]
    public class DocsRedascritacontroller : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        //constructor
        DocsRedascritacontroller()
        {

        }

        ///<summary>
        /// Lista de los links manuales de la base de datos
        ///</summary>
        [HttpGet]
        [Route("Listar")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get()
        {
            var result = db.Web_Documento.Where(d => d.tipo == "Redadscrita" && d.FK_web_documento_estado_rips ==1)
               .Select(s => new 
               {
                   id = s.documento_id,
                   estado = s.FK_web_documento_estado_rips,
                   descripcion= s.descripcion,
                   tipo = s.tipo,
                   ruta = s.ruta

               });

            //JsonResult result = db.Web_Documento;
            //return result.Where(r=> r.tipo.ToString().Equals("Redadscrita"));
            return result;
        }
    }
}
