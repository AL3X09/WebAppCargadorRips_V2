using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SelectPdf;

namespace WebAppCargadorRips_V2.Controllers.APIS
{
    public class PdfexampleController : ApiController
    {
        // GET: api/Pdfexample
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Pdfexample
        public void Page_Load(object sender, EventArgs e)
        {

           
        }


        // GET: api/Pdfexample/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Pdfexample
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Pdfexample/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pdfexample/5
        public void Delete(int id)
        {
        }
    }
}
