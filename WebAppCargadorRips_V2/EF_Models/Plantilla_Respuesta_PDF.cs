//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppCargadorRips_V2.EF_Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Plantilla_Respuesta_PDF
    {
        public long plantilla_respuesta_pdf_id { get; set; }
        public string categoria { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string cuerpo { get; set; }
        public long FK_plantillas_respuesta_pdf_estado_rips { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
    
        public virtual Estado_RIPS Estado_RIPS { get; set; }
    }
}
