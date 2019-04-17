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

            // Estilos desing para boostrap 
            //https://mdbootstrap.com/
            //EN DESUSO 

            //LIBRERIAS EXTERNAS

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
                      "~/Scripts/DataTables/jquery.dataTables.min.js",
                      "~/Scripts/DataTables/responsive.bootstrap4.min.js"
                      ));

            //DropZone
            bundles.Add(new StyleBundle("~/Stile/dropzonecss").Include(
                          "~/Scripts/dropzone/dropzone.min.css"
                          ));

            bundles.Add(new ScriptBundle("~/bundles/dropzonejs").Include(
                      "~/Scripts/dropzone/dropzone.min.js"
                      
                      ));

            //FIN LIBRERIAS EXTERNAS


            // JS para cada una de las paginas 
            //PAGINA HOME
            bundles.Add(new ScriptBundle("~/JSPaginas/home").Include(
                "~/Scripts/Paginas/home.js"));

            //PAGINA LISTADO RIPS
            bundles.Add(new ScriptBundle("~/bundles/listadoripsjs").Include(
                      "~/Scripts/Paginas/listadorips.js"
                      ));

            //PAGINA ROLES Y PERMISOS
            bundles.Add(new ScriptBundle("~/JSPaginas/RolHasPermiso").Include(
                "~/Scripts/Paginas/RolHasPermiso.js"));

            //PAGINA CARGA DE ARCHIVOS RIPS
            bundles.Add(new ScriptBundle("~/JSPaginas/cargarripsjs").Include(
                "~/Scripts/Paginas/cargarips.js"));
            // FIN JS para cada una de las paginas 

            // JS validaciones para cada una de las paginas 

            bundles.Add(new ScriptBundle("~/JSValidacion/validacargaarchivo").Include(
                "~/Scripts/Validaciones/validaCargaArchivo.js"));
            // FIN JS validaciones para cada una de las paginas



        }
    }
}
