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
    [RoutePrefix("api/TipoUsuario")]
    public class TipoUsuarioController : ApiController
    {
        private RipsEntitiesConnection bd = new RipsEntitiesConnection();

        //Constructor
        TipoUsuarioController()
        {

        }

        //RECORDAR QUE SE DEBE HABILITAR CORS + INFO IR AL LINK
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api


        // GET api/TipoUsuario/ListarTipos
        [Route("Listar")]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get()
        {
            return bd.SP_ListarTipoUsuario();
        }
    }
}
