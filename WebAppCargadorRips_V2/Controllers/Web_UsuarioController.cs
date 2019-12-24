using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppCargadorRips_V2.EF_Models;
using WebAppCargadorRips_V2.Models;
using System.Web.Security;
using Newtonsoft.Json;
using WebAppCargadorRips_V2.Controllers.APIS;

namespace WebAppCargadorRips_V2.Controllers
{
    [Authorize]
    public class Web_UsuarioController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Web_Usuario
        public async Task<ActionResult> Index()
        {
            var web_Usuario = db.Web_Usuario.Include(w => w.Web_Rol).Include(w => w.Estado_RIPS).Include(w => w.Prestador);
            return View(await web_Usuario.ToListAsync());
        }

        // GET: Web_Usuario/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Usuario web_Usuario = await db.Web_Usuario.FindAsync(id);
            if (web_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(web_Usuario);
        }

        // GET: Web_Usuario/Create
        public ActionResult Create()
        {
            ViewBag.FK_usuario_rol = new SelectList(db.Web_Rol, "rol_id", "nombre");
            ViewBag.FK_usuario_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo");
            ViewBag.FK_usuario_prestador = new SelectList(db.Prestador.Where(x => x.municipio == "BOGOTÁ D.C."), "prestador_id", "codigo");
            return View();
        }

        // POST: Web_Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "usuario_id,FK_usuario_prestador,correo,contrasenia,nombres,apellidos,telefono,razon_social,imagen,confirmacion_condiciones,confirmacion_correo,FK_usuario_rol,FK_usuario_estado_rips,fecha_modificacion")] Web_Usuario web_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.Web_Usuario.Add(web_Usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FK_usuario_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_Usuario.FK_usuario_rol);
            ViewBag.FK_usuario_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Usuario.FK_usuario_estado_rips);
            ViewBag.FK_usuario_prestador = new SelectList(db.Prestador.Where(x => x.municipio == "BOGOTÁ D.C."), "prestador_id", "codigo", web_Usuario.FK_usuario_prestador);
            return View(web_Usuario);
        }

        // GET: Web_Usuario/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Usuario web_Usuario = await db.Web_Usuario.FindAsync(id);
            if (web_Usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_usuario_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_Usuario.FK_usuario_rol);
            ViewBag.FK_usuario_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Usuario.FK_usuario_estado_rips);
            ViewBag.FK_usuario_prestador = new SelectList(db.Prestador.Where(x => x.municipio == "BOGOTÁ D.C."), "prestador_id", "codigo", web_Usuario.FK_usuario_prestador);
            return View(web_Usuario);
        }

        // POST: Web_Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "usuario_id,FK_usuario_prestador,correo,contrasenia,nombres,apellidos,telefono,razon_social,imagen,confirmacion_condiciones,confirmacion_correo,FK_usuario_rol,FK_usuario_estado_rips,fecha_modificacion")] Web_Usuario web_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(web_Usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FK_usuario_rol = new SelectList(db.Web_Rol, "rol_id", "nombre", web_Usuario.FK_usuario_rol);
            ViewBag.FK_usuario_estado_rips = new SelectList(db.Estado_RIPS, "estado_rips_id", "tipo", web_Usuario.FK_usuario_estado_rips);
            ViewBag.FK_usuario_prestador = new SelectList(db.Prestador.Where(x => x.municipio == "BOGOTÁ D.C."), "prestador_id", "codigo", web_Usuario.FK_usuario_prestador);
            return View(web_Usuario);
        }

        /*// GET: Web_Usuario/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Usuario web_Usuario = await db.Web_Usuario.FindAsync(id);
            if (web_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(web_Usuario);
        }

        // POST: Web_Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Web_Usuario web_Usuario = await db.Web_Usuario.FindAsync(id);
            db.Web_Usuario.Remove(web_Usuario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }*/

        /// <summary>
        /// Metodo invoca la vista para el perfil y realiza una sobre carga de información
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ActionResult Perfil(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Web_Usuario u = db.Web_Usuario.Find(id);

            ActualizarDatosViewModel model = new ActualizarDatosViewModel
                {
                    usuario_id = Convert.ToInt32(u.usuario_id),
                    codigo = u.Prestador.codigo_habilitacion,
                    nombres = u.nombres,
                    apellidos = u.apellidos,
                    razon_social = u.razon_social,
                    correo = u.correo,
                    telefono = u.telefono,
                    id_rol = Convert.ToInt32(u.FK_usuario_rol),
                    id_estado = Convert.ToInt32(u.FK_usuario_estado_rips)
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Perfil(ActualizarDatosViewModel model)
        {
            //Valido los campos del modelo
            if (ModelState.IsValid)
            {
                var result = new Web_UsuarioApiController().UpdateDatosUsuario(model);
                //Console.WriteLine(result);
                return View();
                //Web_UsuarioApiController datos = await datos.UpdateDatosUsuario(model);
                //await datos.UpdateDatosUsuario(model);
            }

            return View(model);
        }

        /// <summary>
        /// Metodo carga la vista para el cambio de contraseña de un usuario logeado
        /// </summary>
        /// GET: Perfil Usuario
        public ActionResult CambiarContrasenia()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CambiarContrasenia(CambiarContraseniaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MSG = new EnviarCorreoRecuperacionModel();
                try
                {
                        //Ejecuto los valores en el SP
                        //borrarSP_Updaterestacontra
                        var response = db.SP_ChangeContraseniaUser(model.idUsuario, model.contrasenia).First();
                        //
                        await db.SaveChangesAsync();

                        // el procedimiento envia un codigo de 201 como respuesta
                        if (response.codigo == 201)
                        {
                            //consulto el mensaje correspondiente para el esta caso
                            var msg = db.Web_Mensaje.Where(m => m.codigo.Equals(1018)).Select(m => new { codigo = m.codigo, tipo = m.tipo, mensaje = m.cuerpo }).First();
                            //creo un array a partir del json devuelto por la api para tratarlo desde aca y poder enviar los diferentes errores
                            var json = JsonConvert.SerializeObject(msg, Formatting.Indented);
                            //creo un json dinamico para enviarlo a la vista
                            dynamic dynJson = JsonConvert.DeserializeObject(json);
                            //envio mensaje
                            TempData["mensaje"] = dynJson;
                            //cierro sesiones
                            FormsAuthentication.SignOut();
                            //Cargo la vista                            
                            return RedirectToAction("Index", "Sesion");

                        }// fin if valida response
                        else
                        {
                            ModelState.AddModelError(string.Empty, response.mensaje);
                            //return View();
                        }

                }//fin try
                catch (Exception e)
                {
                    // envio error a la api logs errores
                    //envio a la carpeta logs
                    APIS.LogsController log = new APIS.LogsController(e.ToString());
                    log.createFolder();
                    //envio error mensaje al usuario
                    ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el momento por favor intente mas tarde");
                }
            }
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
