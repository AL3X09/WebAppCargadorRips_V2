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
    
    public partial class VW_Auditoria_RIPS
    {
        public long RN { get; set; }
        public Nullable<byte> estado_general_numero { get; set; }
        public string estado_general { get; set; }
        public Nullable<long> preradicado { get; set; }
        public Nullable<System.DateTime> preradicado_fecha { get; set; }
        public string preradicado_estado { get; set; }
        public Nullable<System.DateTime> validacion_fecha { get; set; }
        public string validacion_usuario { get; set; }
        public string validacion_estado { get; set; }
        public Nullable<int> radicado { get; set; }
        public Nullable<System.DateTime> radicado_fecha { get; set; }
        public string radicacion_estado { get; set; }
        public Nullable<long> prestador_id { get; set; }
        public string prestador { get; set; }
        public string prestador_habilitacion { get; set; }
        public string prestador_Sede { get; set; }
        public string prestador_nombre { get; set; }
        public string prestador_sede_nombre { get; set; }
        public Nullable<bool> prestador_red_adscrita { get; set; }
        public Nullable<bool> prestador_habilitado { get; set; }
        public Nullable<System.DateTime> periodo_inicio { get; set; }
        public Nullable<System.DateTime> periodo_fin { get; set; }
        public string categoria { get; set; }
        public string tipo_usuario_afilicion { get; set; }
        public string tipo_usuario_condicion { get; set; }
        public Nullable<bool> extranjero { get; set; }
        public Nullable<int> us { get; set; }
        public Nullable<int> af { get; set; }
        public Nullable<int> ac { get; set; }
        public Nullable<int> ap { get; set; }
        public Nullable<int> au { get; set; }
        public Nullable<int> ah { get; set; }
        public Nullable<int> am { get; set; }
        public Nullable<int> an { get; set; }
        public Nullable<int> at { get; set; }
        public Nullable<long> web_usuario_id { get; set; }
        public Nullable<long> web_validacion { get; set; }
        public Nullable<long> servicio_validacion { get; set; }
        public string carga_estado { get; set; }
        public Nullable<System.DateTime> carga_fecha { get; set; }
    }
}