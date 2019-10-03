using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppCargadorRips_V2.Controllers
{
    [Authorize]
    public class TableroController : Controller
    {
        // GET: Tablero
        public ActionResult Index()
        {
            return View();
        }

    }
}