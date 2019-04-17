﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class RipsEntitiesConnection : DbContext
    {
        public RipsEntitiesConnection()
            : base("name=RipsEntitiesConnection")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Servicio_Radicacion> Servicio_Radicacion { get; set; }
        public virtual DbSet<Servicio_Validacion> Servicio_Validacion { get; set; }
        public virtual DbSet<Servicio_Validacion_Error> Servicio_Validacion_Error { get; set; }
        public virtual DbSet<Web_Preradicacion> Web_Preradicacion { get; set; }
        public virtual DbSet<Web_Validacion> Web_Validacion { get; set; }
        public virtual DbSet<Correo> Correo { get; set; }
        public virtual DbSet<Directorios> Directorios { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Estructura> Estructura { get; set; }
        public virtual DbSet<Estructura_Campo> Estructura_Campo { get; set; }
        public virtual DbSet<Sexo> Sexo { get; set; }
        public virtual DbSet<Tipo_Usuario> Tipo_Usuario { get; set; }
        public virtual DbSet<Web_Mensaje> Web_Mensaje { get; set; }
        public virtual DbSet<Web_Permiso> Web_Permiso { get; set; }
        public virtual DbSet<Web_Rol> Web_Rol { get; set; }
        public virtual DbSet<DIVIPOLA> DIVIPOLA { get; set; }
        public virtual DbSet<Estado_RIPS> Estado_RIPS { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Plantilla_Correo> Plantilla_Correo { get; set; }
        public virtual DbSet<Prestador> Prestador { get; set; }
        public virtual DbSet<Web_Administrador> Web_Administrador { get; set; }
        public virtual DbSet<Web_Correo_Sin_Restriccion> Web_Correo_Sin_Restriccion { get; set; }
        public virtual DbSet<Web_Documento> Web_Documento { get; set; }
        public virtual DbSet<Web_Pregunta_Frecuente> Web_Pregunta_Frecuente { get; set; }
        public virtual DbSet<Web_Usuario> Web_Usuario { get; set; }
        public virtual DbSet<VW_Radicacion> VW_Radicacion { get; set; }
        public virtual DbSet<VW_Servicio_Validacion> VW_Servicio_Validacion { get; set; }
        public virtual DbSet<VW_Listado_Estado_Rips> VW_Listado_Estado_Rips { get; set; }
        public virtual DbSet<Web_Modulo> Web_Modulo { get; set; }
        public virtual DbSet<Web_RolHasPermiso> Web_RolHasPermiso { get; set; }
    
        public virtual ObjectResult<SP_Web_Insert_Datos_Rips_a_Validar_Result> SP_Web_Insert_Datos_Rips_a_Validar(string tipoUsuario, string categoria, string periodoFechaInicio, string periodoFechaFin, string idUser, string fkEstado)
        {
            var tipoUsuarioParameter = tipoUsuario != null ?
                new ObjectParameter("TipoUsuario", tipoUsuario) :
                new ObjectParameter("TipoUsuario", typeof(string));
    
            var categoriaParameter = categoria != null ?
                new ObjectParameter("Categoria", categoria) :
                new ObjectParameter("Categoria", typeof(string));
    
            var periodoFechaInicioParameter = periodoFechaInicio != null ?
                new ObjectParameter("PeriodoFechaInicio", periodoFechaInicio) :
                new ObjectParameter("PeriodoFechaInicio", typeof(string));
    
            var periodoFechaFinParameter = periodoFechaFin != null ?
                new ObjectParameter("PeriodoFechaFin", periodoFechaFin) :
                new ObjectParameter("PeriodoFechaFin", typeof(string));
    
            var idUserParameter = idUser != null ?
                new ObjectParameter("IdUser", idUser) :
                new ObjectParameter("IdUser", typeof(string));
    
            var fkEstadoParameter = fkEstado != null ?
                new ObjectParameter("FkEstado", fkEstado) :
                new ObjectParameter("FkEstado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Web_Insert_Datos_Rips_a_Validar_Result>("SP_Web_Insert_Datos_Rips_a_Validar", tipoUsuarioParameter, categoriaParameter, periodoFechaInicioParameter, periodoFechaFinParameter, idUserParameter, fkEstadoParameter);
        }
    
        public virtual ObjectResult<SP_Web_Insert_Rips_a_Preradicar_Result> SP_Web_Insert_Rips_a_Preradicar(Nullable<long> idUser, Nullable<long> webvalidacion_id)
        {
            var idUserParameter = idUser.HasValue ?
                new ObjectParameter("IdUser", idUser) :
                new ObjectParameter("IdUser", typeof(long));
    
            var webvalidacion_idParameter = webvalidacion_id.HasValue ?
                new ObjectParameter("webvalidacion_id", webvalidacion_id) :
                new ObjectParameter("webvalidacion_id", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Web_Insert_Rips_a_Preradicar_Result>("SP_Web_Insert_Rips_a_Preradicar", idUserParameter, webvalidacion_idParameter);
        }
    
        public virtual ObjectResult<SP_Web_Update_Estado_Disponible_Preradicado_Result> SP_Web_Update_Estado_Disponible_Preradicado(Nullable<long> idWebvalidacion, Nullable<long> preradicacionid)
        {
            var idWebvalidacionParameter = idWebvalidacion.HasValue ?
                new ObjectParameter("IdWebvalidacion", idWebvalidacion) :
                new ObjectParameter("IdWebvalidacion", typeof(long));
    
            var preradicacionidParameter = preradicacionid.HasValue ?
                new ObjectParameter("Preradicacionid", preradicacionid) :
                new ObjectParameter("Preradicacionid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Web_Update_Estado_Disponible_Preradicado_Result>("SP_Web_Update_Estado_Disponible_Preradicado", idWebvalidacionParameter, preradicacionidParameter);
        }
    
        public virtual ObjectResult<SP_Web_Update_Estado_Error_Carga_WebValidacion_Result> SP_Web_Update_Estado_Error_Carga_WebValidacion(Nullable<long> idWebvalidacion)
        {
            var idWebvalidacionParameter = idWebvalidacion.HasValue ?
                new ObjectParameter("IdWebvalidacion", idWebvalidacion) :
                new ObjectParameter("IdWebvalidacion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Web_Update_Estado_Error_Carga_WebValidacion_Result>("SP_Web_Update_Estado_Error_Carga_WebValidacion", idWebvalidacionParameter);
        }
    
        public virtual ObjectResult<SP_CantidadRipsRecibidos_Result> SP_CantidadRipsRecibidos(Nullable<int> fKuser)
        {
            var fKuserParameter = fKuser.HasValue ?
                new ObjectParameter("FKuser", fKuser) :
                new ObjectParameter("FKuser", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_CantidadRipsRecibidos_Result>("SP_CantidadRipsRecibidos", fKuserParameter);
        }
    
        public virtual ObjectResult<SP_ChangeContraseniaUser_Result> SP_ChangeContraseniaUser(Nullable<int> token, string contrasenia)
        {
            var tokenParameter = token.HasValue ?
                new ObjectParameter("token", token) :
                new ObjectParameter("token", typeof(int));
    
            var contraseniaParameter = contrasenia != null ?
                new ObjectParameter("contrasenia", contrasenia) :
                new ObjectParameter("contrasenia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ChangeContraseniaUser_Result>("SP_ChangeContraseniaUser", tokenParameter, contraseniaParameter);
        }
    
        public virtual ObjectResult<SP_GenerarCodigoRecuperacionContraseniaUser_Result> SP_GenerarCodigoRecuperacionContraseniaUser(string codigo, string correo)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GenerarCodigoRecuperacionContraseniaUser_Result>("SP_GenerarCodigoRecuperacionContraseniaUser", codigoParameter, correoParameter);
        }
    
        public virtual ObjectResult<SP_GetAllInfoUsers_Result> SP_GetAllInfoUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetAllInfoUsers_Result>("SP_GetAllInfoUsers");
        }
    
        public virtual ObjectResult<SP_GetAllManuales_Result> SP_GetAllManuales()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetAllManuales_Result>("SP_GetAllManuales");
        }
    
        public virtual ObjectResult<SP_GetAllNormatividad_Result> SP_GetAllNormatividad()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetAllNormatividad_Result>("SP_GetAllNormatividad");
        }
    
        public virtual ObjectResult<SP_GetAllPlantillasCorreo_Result> SP_GetAllPlantillasCorreo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetAllPlantillasCorreo_Result>("SP_GetAllPlantillasCorreo");
        }
    
        public virtual ObjectResult<SP_GetAllPreguntasFrecuentes_Result> SP_GetAllPreguntasFrecuentes()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetAllPreguntasFrecuentes_Result>("SP_GetAllPreguntasFrecuentes");
        }
    
        public virtual ObjectResult<SP_GetAllPresentacionesAyuda_Result> SP_GetAllPresentacionesAyuda()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetAllPresentacionesAyuda_Result>("SP_GetAllPresentacionesAyuda");
        }
    
        public virtual ObjectResult<SP_GetDatosUsuario_Result> SP_GetDatosUsuario(string codigo)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetDatosUsuario_Result>("SP_GetDatosUsuario", codigoParameter);
        }
    
        public virtual ObjectResult<SP_GetEstadoRipsRecibidos_Result> SP_GetEstadoRipsRecibidos(Nullable<int> prestador)
        {
            var prestadorParameter = prestador.HasValue ?
                new ObjectParameter("Prestador", prestador) :
                new ObjectParameter("Prestador", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetEstadoRipsRecibidos_Result>("SP_GetEstadoRipsRecibidos", prestadorParameter);
        }
    
        public virtual ObjectResult<SP_GetEstructuraCamposArchivos_Result> SP_GetEstructuraCamposArchivos()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetEstructuraCamposArchivos_Result>("SP_GetEstructuraCamposArchivos");
        }
    
        public virtual ObjectResult<SP_GetPlantillaCorreo_Result> SP_GetPlantillaCorreo(Nullable<int> codigoPlantilla)
        {
            var codigoPlantillaParameter = codigoPlantilla.HasValue ?
                new ObjectParameter("CodigoPlantilla", codigoPlantilla) :
                new ObjectParameter("CodigoPlantilla", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetPlantillaCorreo_Result>("SP_GetPlantillaCorreo", codigoPlantillaParameter);
        }
    
        public virtual ObjectResult<SP_Ingreso_Usuario_Result> SP_Ingreso_Usuario(string codprestador, string contrasenia)
        {
            var codprestadorParameter = codprestador != null ?
                new ObjectParameter("codprestador", codprestador) :
                new ObjectParameter("codprestador", typeof(string));
    
            var contraseniaParameter = contrasenia != null ?
                new ObjectParameter("contrasenia", contrasenia) :
                new ObjectParameter("contrasenia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Ingreso_Usuario_Result>("SP_Ingreso_Usuario", codprestadorParameter, contraseniaParameter);
        }
    
        public virtual ObjectResult<SP_Ingreso_Usuario_Administrador_Result> SP_Ingreso_Usuario_Administrador(string usuario, string contrasenia)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("Usuario", usuario) :
                new ObjectParameter("Usuario", typeof(string));
    
            var contraseniaParameter = contrasenia != null ?
                new ObjectParameter("Contrasenia", contrasenia) :
                new ObjectParameter("Contrasenia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Ingreso_Usuario_Administrador_Result>("SP_Ingreso_Usuario_Administrador", usuarioParameter, contraseniaParameter);
        }
    
        public virtual ObjectResult<SP_ListarTipoUsuario_Result> SP_ListarTipoUsuario()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ListarTipoUsuario_Result>("SP_ListarTipoUsuario");
        }
    
        public virtual ObjectResult<SP_Registro_Usuario_Result> SP_Registro_Usuario(string codigoPrestador, string correo, string contrasenia, string nombres, string apellidos, string razonSocial, string telefono, string imagen, Nullable<int> confirmacionCondiciones, Nullable<int> confirmacionCorreo, Nullable<int> estado, Nullable<int> rol)
        {
            var codigoPrestadorParameter = codigoPrestador != null ?
                new ObjectParameter("CodigoPrestador", codigoPrestador) :
                new ObjectParameter("CodigoPrestador", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var contraseniaParameter = contrasenia != null ?
                new ObjectParameter("Contrasenia", contrasenia) :
                new ObjectParameter("Contrasenia", typeof(string));
    
            var nombresParameter = nombres != null ?
                new ObjectParameter("Nombres", nombres) :
                new ObjectParameter("Nombres", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("Apellidos", apellidos) :
                new ObjectParameter("Apellidos", typeof(string));
    
            var razonSocialParameter = razonSocial != null ?
                new ObjectParameter("RazonSocial", razonSocial) :
                new ObjectParameter("RazonSocial", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("Telefono", telefono) :
                new ObjectParameter("Telefono", typeof(string));
    
            var imagenParameter = imagen != null ?
                new ObjectParameter("Imagen", imagen) :
                new ObjectParameter("Imagen", typeof(string));
    
            var confirmacionCondicionesParameter = confirmacionCondiciones.HasValue ?
                new ObjectParameter("ConfirmacionCondiciones", confirmacionCondiciones) :
                new ObjectParameter("ConfirmacionCondiciones", typeof(int));
    
            var confirmacionCorreoParameter = confirmacionCorreo.HasValue ?
                new ObjectParameter("ConfirmacionCorreo", confirmacionCorreo) :
                new ObjectParameter("ConfirmacionCorreo", typeof(int));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(int));
    
            var rolParameter = rol.HasValue ?
                new ObjectParameter("Rol", rol) :
                new ObjectParameter("Rol", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Registro_Usuario_Result>("SP_Registro_Usuario", codigoPrestadorParameter, correoParameter, contraseniaParameter, nombresParameter, apellidosParameter, razonSocialParameter, telefonoParameter, imagenParameter, confirmacionCondicionesParameter, confirmacionCorreoParameter, estadoParameter, rolParameter);
        }
    
        public virtual ObjectResult<SP_Registro_Usuario_Administrador_Result> SP_Registro_Usuario_Administrador(string usuario, string contrasenia, string correo, string nombres, string apellidos, string descripcion, string extension, string imagen, Nullable<int> rol, Nullable<int> estado)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("Usuario", usuario) :
                new ObjectParameter("Usuario", typeof(string));
    
            var contraseniaParameter = contrasenia != null ?
                new ObjectParameter("Contrasenia", contrasenia) :
                new ObjectParameter("Contrasenia", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var nombresParameter = nombres != null ?
                new ObjectParameter("Nombres", nombres) :
                new ObjectParameter("Nombres", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("Apellidos", apellidos) :
                new ObjectParameter("Apellidos", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var extensionParameter = extension != null ?
                new ObjectParameter("Extension", extension) :
                new ObjectParameter("Extension", typeof(string));
    
            var imagenParameter = imagen != null ?
                new ObjectParameter("Imagen", imagen) :
                new ObjectParameter("Imagen", typeof(string));
    
            var rolParameter = rol.HasValue ?
                new ObjectParameter("Rol", rol) :
                new ObjectParameter("Rol", typeof(int));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Registro_Usuario_Administrador_Result>("SP_Registro_Usuario_Administrador", usuarioParameter, contraseniaParameter, correoParameter, nombresParameter, apellidosParameter, descripcionParameter, extensionParameter, imagenParameter, rolParameter, estadoParameter);
        }
    
        public virtual ObjectResult<SP_UpdateAvatarUser_Result> SP_UpdateAvatarUser(Nullable<int> token, string imagen)
        {
            var tokenParameter = token.HasValue ?
                new ObjectParameter("token", token) :
                new ObjectParameter("token", typeof(int));
    
            var imagenParameter = imagen != null ?
                new ObjectParameter("Imagen", imagen) :
                new ObjectParameter("Imagen", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_UpdateAvatarUser_Result>("SP_UpdateAvatarUser", tokenParameter, imagenParameter);
        }
    
        public virtual ObjectResult<SP_UpdateDatosUser_Result> SP_UpdateDatosUser(Nullable<int> token, string codigo, string nombres, string apellidos, string telefono, string razonsocial, string correo, Nullable<int> fkrol, Nullable<int> fkestado)
        {
            var tokenParameter = token.HasValue ?
                new ObjectParameter("token", token) :
                new ObjectParameter("token", typeof(int));
    
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var nombresParameter = nombres != null ?
                new ObjectParameter("nombres", nombres) :
                new ObjectParameter("nombres", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("apellidos", apellidos) :
                new ObjectParameter("apellidos", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("telefono", telefono) :
                new ObjectParameter("telefono", typeof(string));
    
            var razonsocialParameter = razonsocial != null ?
                new ObjectParameter("razonsocial", razonsocial) :
                new ObjectParameter("razonsocial", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var fkrolParameter = fkrol.HasValue ?
                new ObjectParameter("fkrol", fkrol) :
                new ObjectParameter("fkrol", typeof(int));
    
            var fkestadoParameter = fkestado.HasValue ?
                new ObjectParameter("fkestado", fkestado) :
                new ObjectParameter("fkestado", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_UpdateDatosUser_Result>("SP_UpdateDatosUser", tokenParameter, codigoParameter, nombresParameter, apellidosParameter, telefonoParameter, razonsocialParameter, correoParameter, fkrolParameter, fkestadoParameter);
        }
    
        public virtual ObjectResult<SP_ValidarDatosRestablecimientoContrasenia_Result> SP_ValidarDatosRestablecimientoContrasenia(string codigo, string token)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("Codigo", codigo) :
                new ObjectParameter("Codigo", typeof(string));
    
            var tokenParameter = token != null ?
                new ObjectParameter("Token", token) :
                new ObjectParameter("Token", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ValidarDatosRestablecimientoContrasenia_Result>("SP_ValidarDatosRestablecimientoContrasenia", codigoParameter, tokenParameter);
        }
    }
}
