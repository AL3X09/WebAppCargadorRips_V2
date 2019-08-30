using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get()
        {
            var result = db.SP_GetAllManuales();
            return result.Where(r=> r.tipo == "Redadscrita");
        }
    }
}
