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
    
    public partial class Sexo
    {
        public long sexo_id { get; set; }
        public byte numero { get; set; }
        public string acronimo { get; set; }
        public string nombre { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
        public long estado_rips_id { get; set; }
    
        public virtual Estado_RIPS Estado_RIPS { get; set; }
    }
}
