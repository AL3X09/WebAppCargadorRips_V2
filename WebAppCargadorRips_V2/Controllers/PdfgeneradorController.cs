using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppCargadorRips_V2.EF_Models;

namespace WebAppCargadorRips_V2.Controllers
{
    public class PdfgeneradorController : Controller
    {
        private RipsEntitiesConnection bd = new RipsEntitiesConnection();

        // GET: Pdfgenerador
        public ActionResult Index()
        {
            return View();
        }

        


        [HttpGet]
        public ActionResult Generar()
        {
            try
            {
                // TODO: Add update logic here
                //consulto el procedimiento para traer la información de la plantilla solicitada
                var result = new Plantilla_Respuesta_PDFController().CuerpoPdf("1-");
                //var t = result.CuerpoPdf("1-");
                //CuerpoPdf
                // instantiate a html to pdf converter object 
                HtmlToPdf converter = new HtmlToPdf();

                //var Text = string.Format(result.cuerpo);


                // create a new pdf document converting an url 
                PdfDocument doc = converter.ConvertHtmlString(result.ToString());

                // save pdf document 
                byte[] pdf = doc.Save();

                // close pdf document 
                doc.Close();

                // return resulted pdf document 
                FileResult fileResult = new FileContentResult(pdf, "application/pdf");
                fileResult.FileDownloadName = "Document.pdf";
                return fileResult;
            }
            catch (Exception e)
            {
                //Envio al archivo log 
                //LogsController log = new LogsController(e.ToString());
                //log.createFolder();
                //Envio mensaje de Error a la Vista
                //MSG.Add(new { type = "error", value = e.Message.ToString() });
                throw;
            }
        }


    }
}
