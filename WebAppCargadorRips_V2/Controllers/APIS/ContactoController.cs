using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAppCargadorRips_V2.EF_Models;

namespace WebAppCargadorRips_V2.Controllers.APIS
{
    //[Authorize]
    [RoutePrefix("api/Contactos")]
    public class ContactoController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        //constructor
        ContactoController()
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
            var result = db.SP_GetContactos();
            return result;
        }
    }
}
