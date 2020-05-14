using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppCargadorRips_V2.Controllers
{
    //[Authorize]
    public class AyudaController : Controller
    {
        // GET: Ayuda
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ayuda
        public ActionResult AyudaExternal()
        {
            return View();
        }
        
    }
}