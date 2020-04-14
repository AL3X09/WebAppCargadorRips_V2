﻿using System;
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
    [RoutePrefix("api/Preguntas")]
    public class PreguntasFrecuentesController : ApiController
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        //constructor
        PreguntasFrecuentesController()
        {

        }

        //RECORDAR QUE SE DEBE HABILITAR CORS + INFO IR AL LINK
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api

        ///<summary>
        /// Lista de las preguntas existentes en el aplicativo
        ///</summary>
        [HttpGet]
        [Route("Listar")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get()
        {
            var result = db.SP_GetAllPreguntasFrecuentes();
            return result;
        }
    }
}
