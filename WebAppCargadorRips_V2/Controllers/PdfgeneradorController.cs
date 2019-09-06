using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppCargadorRips_V2.Controllers
{
    public class PdfgeneradorController : Controller
    {
        // GET: Pdfgenerador
        public ActionResult Index()
        {
            return View();
        }



        // POST: Pdfgenerador/Edit/5 int id, FormCollection collection
        [HttpGet]
        public ActionResult Generar()
        {
            try
            {
                // TODO: Add update logic here

                // instantiate a html to pdf converter object 
                HtmlToPdf converter = new HtmlToPdf();

                var Text = @"<html>
 <body>
  <br/>
  Hello World from selectpdf.com.
  <br/>
  <br/>
  <br/>
  <br/>
  Hello World from selectpdf.com.
  <br/>
  <br/>
 </body>
</html>
";


                // create a new pdf document converting an url 
                PdfDocument doc = converter.ConvertHtmlString(Text);

                // save pdf document 
                byte[] pdf = doc.Save();

                // close pdf document 
                doc.Close();

                // return resulted pdf document 
                FileResult fileResult = new FileContentResult(pdf, "application/pdf");
                fileResult.FileDownloadName = "Document.pdf";
                return fileResult;
            }
            catch
            {
                return View();
            }
        }

       
    }
}
