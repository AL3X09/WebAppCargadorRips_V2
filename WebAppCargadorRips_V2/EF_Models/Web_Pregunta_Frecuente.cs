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
    
    public partial class Web_Pregunta_Frecuente
    {
        public long pregunta_frecuente_id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public string pregunta { get; set; }
        public string respuesta { get; set; }
        public long FK_pregunta_frecuente_estado_rips { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
    
        public virtual Estado_RIPS Estado_RIPS { get; set; }
    }
}
