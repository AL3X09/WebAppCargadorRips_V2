using System.Web;
using System.Web.Optimization;

namespace WebAppCargadorRips_V2
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/popper.min.js",
                        "~/Scripts/bootstrap.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css"                      
                      ));

            //LIBRERIAS EXTERNAS

            // Estilos desing para boostrap 
            //https://mdbootstrap.com/
            bundles.Add(new StyleBundle("~/Content/mdbcss").Include(
                      "~/Content/MDB/mdb.css",
                      "~/Content/MDB/compiled.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsmdb").Include(
                     "~/Scripts/MDB/mdb.js"));

            // Estilos desing para boostrap
            //https://www.creative-tim.com/product/material-dashboard-dark
            bundles.Add(new StyleBundle("~/Stile/materialcss").Include(
                      "~/Content/Material/material-dashboard.css",
                      "~/Content/Material/demo.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/materialjs").Include(
                      "~/Scripts/Material/core/bootstrap-material-design.min.js",
                      "~/Scripts/Material/plugins/perfect-scrollbar.jquery.min.js",
                      "~/Scripts/Material/material-dashboard.js"
                      ));

            //DatePicker
            bundles.Add(new ScriptBundle("~/bundles/datepickerjs").Include(
                        "~/Scripts/gijgo/combined/gijgo.min.js",
                        "~/Scripts/gijgo/combined/messages/messages.es-es.min.js"
                      ));

            //Datatables
            bundles.Add(new ScriptBundle("~/bundles/datatablesjs").Include(
                      "~/Scripts/DataTables/jquery.dataTables.js"
                      //"~/Scripts/DataTables/responsive.bootstrap4.min.js"
                      ));

            //jquery multifile
            //http://lampspw.wallonie.be/dgo4/tinymvc/myfiles/plugins/multifile-2.2.1/docs.html#Customize
            bundles.Add(new ScriptBundle("~/bundles/multifilejs").Include(
                     "~/Scripts/Multifile/jquery.MultiFile.js"
                     ));

            bundles.Add(new StyleBundle("~/Stile/multifilecss").Include(
                     "~/Content/Multifile/freestyle.css"
                     ));

            //SweetAlert 2 (instalado de nuget)
            //https://sweetalert2.github.io/

            bundles.Add(new ScriptBundle("~/bundles/sweetalertjs").Include(
                    "~/SweetAlert2/sweetalert2.js"
                    ));

            //Tingle JS
            //https://robinparisi.github.io/tingle/
            bundles.Add(new StyleBundle("~/Stile/tinglecss").Include(
                     "~/Content/Tinglejs/tingle.css"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/tinglejs").Include(
                     "~/Scripts/Tinglejs/tingle.js"
                     ));

            //Izitoast JS
            //http://izitoast.marcelodolza.com//
            bundles.Add(new StyleBundle("~/Stile/izitoastcss").Include(
                     "~/Content/Izitoast/iziToast.min.css"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/izitoastjs").Include(
                     "~/Scripts/Izitoast/iziToast.min.js"
                     ));

            //DROPFY https://github.com/JeremyFagis/dropify

            bundles.Add(new StyleBundle("~/Stile/dropifycss").Include(
                      "~/Content/Dropify/dropify.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/dropifyjs").Include(
                      "~/Scripts/Dropify/dropify.min.js"
                      ));


            //FIN LIBRERIAS EXTERNAS


            // JS para cada una de las paginas 

            //PAGINA TABLERO
            bundles.Add(new ScriptBundle("~/JSPaginas/tablerojs").Include(
                "~/Scripts/Paginas/tablero.js"
                ));

            //PAGINA SESION
            bundles.Add(new ScriptBundle("~/bundles/loginval").Include(
                      "~/Scripts/Paginas/login.js"));
            //PAGINA LAYOUT
            bundles.Add(new ScriptBundle("~/JSPaginas/layoutjs").Include(
                "~/Scripts/Paginas/layout.js"));
            //PAGINA HOME
            bundles.Add(new ScriptBundle("~/JSPaginas/home").Include(
                "~/Scripts/Paginas/home.js"));

            //PAGINA LISTADO RIPS
            bundles.Add(new ScriptBundle("~/bundles/perfilusuariojs").Include(
                      "~/Scripts/Paginas/perfilusuario.js"
                      ));

            //PAGINA LISTADO RIPS
            bundles.Add(new ScriptBundle("~/bundles/listadoripsjs").Include(
                      "~/Scripts/Paginas/listadorips.js"
                      ));

            //PAGINA ROLES Y PERMISOS
            bundles.Add(new ScriptBundle("~/JSPaginas/RolHasPermiso").Include(
                "~/Scripts/Paginas/RolHasPermiso.js"));

            //PAGINA CARGA DE ARCHIVOS RIPS
            bundles.Add(new ScriptBundle("~/JSPaginas/cargarripsjs").Include(
                "~/Scripts/Paginas/cargarips.js",
                "~/Scripts/Paginas/multifilecontrol.js"
                ));

            //PAGINA AYUDA 
            bundles.Add(new ScriptBundle("~/JSPaginas/ayudajs").Include(
                "~/Scripts/Paginas/ayuda.js"
                ));
            // FIN JS para cada una de las paginas 

            // JS validaciones para cada una de las paginas 

            bundles.Add(new ScriptBundle("~/JSValidacion/validacargaarchivo").Include(
                "~/Scripts/Validaciones/validaCargaArchivo.js"));
            // FIN JS validaciones para cada una de las paginas



        }
    }
}
