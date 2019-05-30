using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppCargadorRips_V2.Controllers
{
    [Authorize]
    public class CargadorController : Controller
    {
        // GET: Carga
        public ActionResult Index()
        {
            return View();
        }
    }
}