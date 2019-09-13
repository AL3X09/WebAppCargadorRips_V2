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
    
    public partial class Estado_RIPS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estado_RIPS()
        {
            this.Web_Preradicacion = new HashSet<Web_Preradicacion>();
            this.Web_Validacion = new HashSet<Web_Validacion>();
            this.Correo = new HashSet<Correo>();
            this.Categoria = new HashSet<Categoria>();
            this.Estructura = new HashSet<Estructura>();
            this.Estructura_Campo = new HashSet<Estructura_Campo>();
            this.Tipo_Usuario = new HashSet<Tipo_Usuario>();
            this.Web_Mensaje = new HashSet<Web_Mensaje>();
            this.Web_Permiso = new HashSet<Web_Permiso>();
            this.Web_Rol = new HashSet<Web_Rol>();
            this.Web_Correo_Sin_Restriccion = new HashSet<Web_Correo_Sin_Restriccion>();
            this.Plantilla_Correo = new HashSet<Plantilla_Correo>();
            this.Web_Pregunta_Frecuente = new HashSet<Web_Pregunta_Frecuente>();
            this.Web_Usuario = new HashSet<Web_Usuario>();
            this.Web_Documento = new HashSet<Web_Documento>();
            this.Sexo = new HashSet<Sexo>();
            this.Fecha = new HashSet<Fecha>();
            this.Web_Nivel_Permiso = new HashSet<Web_Nivel_Permiso>();
            this.Web_Modulo = new HashSet<Web_Modulo>();
            this.Plantilla_Respuesta_PDF = new HashSet<Plantilla_Respuesta_PDF>();
        }
    
        public long estado_rips_id { get; set; }
        public byte numero { get; set; }
        public string tipo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Preradicacion> Web_Preradicacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Validacion> Web_Validacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Correo> Correo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categoria> Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Estructura> Estructura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Estructura_Campo> Estructura_Campo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tipo_Usuario> Tipo_Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Mensaje> Web_Mensaje { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Permiso> Web_Permiso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Rol> Web_Rol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Correo_Sin_Restriccion> Web_Correo_Sin_Restriccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plantilla_Correo> Plantilla_Correo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Pregunta_Frecuente> Web_Pregunta_Frecuente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Usuario> Web_Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Documento> Web_Documento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sexo> Sexo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fecha> Fecha { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Nivel_Permiso> Web_Nivel_Permiso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Modulo> Web_Modulo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plantilla_Respuesta_PDF> Plantilla_Respuesta_PDF { get; set; }
    }
}
