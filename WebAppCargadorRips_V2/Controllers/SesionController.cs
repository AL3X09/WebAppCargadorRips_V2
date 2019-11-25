using CaptchaMvc.HtmlHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using WebAppCargadorRips_V2.Controllers.APIS;
using WebAppCargadorRips_V2.EF_Models;
using WebAppCargadorRips_V2.Models;

namespace WebAppCargadorRips_V2.Controllers
{
    public class SesionController : Controller
    {
        private RipsEntitiesConnection db = new RipsEntitiesConnection();

        // GET: Sesion
        public ActionResult Index()
        {
            //valido si en alguna vista envia msj despues de un redirect
            if (TempData["mensaje"] != null)
            {
                ViewBag.SomeData = TempData["mensaje"].ToString();
            }

            /***
             *valido si realizo el cambio de contraseña y de esta forma cerrar las sesiones
             *Por el momento solo para el cambio de contraseña external
             */
            if (TempData["cambiocontraseniasucces"] != null)
            {
                ViewBag.SomeData = TempData["cambiocontraseniasucces"].ToString();
                //cierro sesiones
                FormsAuthentication.SignOut();
                Session.Abandon();
            }

            return View();
        }

        // GET: Form Login
        public ActionResult ViewPartialLogin()
        {
            //Limpio campos
            ModelState.Clear();
            return View("Index");

        }

       


        /// <summary>
        /// Metodo que ejecuta el inicio de sesion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: /Account/ViewPartialLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ViewPartialLogin(LoginViewModel model)
        {

            //Valido los campos del modelo
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Valido el capcha
            if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                ModelState.AddModelError(string.Empty, "Error: captcha no es válido.");
            }
            //si el captcha es valido
            else
            {

                try
                {

                    //Ejecuto los valores
                    var response = db.SP_Ingreso_Usuario(model.Usuario, model.Password).FirstOrDefault();
                    //
                    await db.SaveChangesAsync();
                    //
                    if (response != null && response.codigo.Equals(200))
                    {
                        var obj = db.Web_Usuario.Where(u => u.Prestador.codigo.Equals(model.Usuario)).FirstOrDefault();
                        FormsAuthentication.SetAuthCookie(obj.usuario_id.ToString() + "|" + obj.FK_usuario_rol.ToString(), false);
                        return RedirectToAction("Index", "Tablero");
                    }
                    else if (response.codigo != 200)
                    {
                        ModelState.AddModelError(string.Empty, response.mensaje);
                    }
                    else
                    {
                        //Limpio campos
                        ModelState.Clear();
                        //envio un mensaje al usuario
                        ModelState.AddModelError(string.Empty, "La plataforma no esta respondiendo a su solicitud, por favor intente mas tarde.");
                    }

                }
                catch (Exception e)
                {
                    //envio error a la api logs errores
                    //y envio a la carpeta logs
                    APIS.LogsController log = new APIS.LogsController(e.ToString());
                    log.createFolder();
                    //Limpio campos
                    ModelState.Clear();
                    //envio error mensaje al usuario
                    //ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el momento por favor intente mas tarde. ");
                    ModelState.AddModelError(string.Empty, "Estamos"+e.ToString());
                }

            }//fin else captcha

            //retorno la vista en caso de que no se efectue el regsitro
            return View("Index", model);

        }

        public ActionResult ViewPartialRegistro()
        {
            //Limpio campos
            ModelState.Clear();
            return View("Index");
        }

        //Metodo que ejecuta el registro
        // POST: /Account/ViewPartialRegistro
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ViewPartialRegistro(RegisterViewModel usuario)
        {
            //Valido los campos del modelo
            if (ModelState.IsValid)
            {
                try
                {
                    //sección del recaptcha
                    //Valido el capcha
                    if (!this.IsCaptchaValid("Captcha is not valid"))
                    {
                        ModelState.AddModelError(string.Empty, "Error: captcha no es válido.");
                    }
                    //si el captcha es valido
                    else
                    {

                        //Ejecuto los valores en el SP
                        Object response = db.SP_Registro_Usuario(usuario.CodPrestador, usuario.Email, usuario.Pasword, usuario.Nombres, usuario.Apellidos, usuario.RazonSocial, usuario.Telefono, "/Img/avatarsusers/avatar.png", 1, 1, 1, 2).FirstOrDefault();
                        //
                        await db.SaveChangesAsync();

                        if (response.Equals(string.Empty))
                            response = HttpStatusCode.NotFound;
                        else
                        {
                            //creo un array a partir del json devuelto por la api para tratarlo desde aca y poder enviar los diferentes errores
                            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
                            //creo un json dinamico para enviarlo a la vista
                            dynamic dynJson = JsonConvert.DeserializeObject(json);
                            ViewBag.SomeData = dynJson;
                        }
                    }//fin else captcha

                }//fin try
                catch (Exception e)
                {
                    //envio error a la api logs errores
                    //envio a la carpeta logs
                    APIS.LogsController log = new APIS.LogsController(e.ToString());
                    log.createFolder();
                    //Limpio campos
                    ModelState.Clear();
                    //envio error mensaje al usuario
                    ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el momento por favor intente mas tarde.");
                    //ModelState.AddModelError(string.Empty, e.ToString());
                }

            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View("Index");
        }

        /// <summary>
        /// Metodo Recupero la contraseña desde la vista de logueo, enviado un encriptado al correo asociado al usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //Este metodo es usado cuando el usuario no recuerda y quiere recuperar la contraseña
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult RecuperarContrasenia()
        {
            //Limpio campos
            ModelState.Clear();
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecuperarContrasenia(RecuperarContraseniaViewModel model)
        {
            if (ModelState.IsValid)
            {
                //sección del recaptcha
                //Valido el capcha
                if (!this.IsCaptchaValid("Captcha is not valid"))
                {
                    ModelState.AddModelError(string.Empty, "Error: captcha no es válido.");
                }
                //si el captcha es valido
                else
                {

                    var MSG = new EnviarCorreoRecuperacionModel();

                    try
                    {
                        //Ejecuto los valores en el SP
                        var response = db.SP_GenerarCodigoRecuperacionContraseniaUser(model.Usuario, model.Email).First();
                        //almaceno cambios asincronamente
                        await db.SaveChangesAsync();

                        //se elimina xq pueden llgar multiples respuestas if (response.Equals(string.Empty))
                        //ModelState.AddModelError(string.Empty, "No se encontraron coincidencias, para restablecer la contraseña");
                        if (response.codigo == 200)
                        {
                            MSG.codPlantilla = 3;
                            MSG.usercorreo = response.correousuario;
                            MSG.id = response.codprestador;
                            MSG.token = response.token;

                            
                                // invoco el constructor
                                EnviarCorreoController enviocorreo = new EnviarCorreoController();
                                //llamo el metodo que realiza la acción de envio de correos
                                var postdatos = await enviocorreo.PostSendEmailRecuperacionContrasenia(MSG);
                                // valido la respuesta del metodo
                                if (postdatos.GetType().Name != null && postdatos.GetType().Name != "BadRequestResult")
                                {
                                    //Limpio campos
                                    ModelState.Clear();
                                    //consulto el mensaje correspondiente para el esta caso
                                    var msg = db.Web_Mensaje.Where(m => m.codigo.Equals(1017)).Select(m => new { codigo = m.codigo, tipo = m.tipo, mensaje = m.cuerpo }).First();
                                    //creo un array a partir del json devuelto por la api para tratramiento
                                    var json = JsonConvert.SerializeObject(msg, Formatting.Indented);
                                    //creo un json dinamico para enviarlo a la vista
                                    dynamic dynJson = JsonConvert.DeserializeObject(json);
                                    //envio mensaje
                                    TempData["mensaje"] = dynJson;
                                    return RedirectToAction("Index", "Sesion");
                                }
                                else
                                {
                                    // si la respuesta del correo fue erronea envio respuesta a la vista   
                                    ModelState.AddModelError(string.Empty, "No se pudo efectuar el restablecimiento de contraseña.");
                                }
                            
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, response.mensaje);
                        }



                    }//fin try
                    catch (Exception e)
                    {
                        // envio error a la api logs errores
                        //envio a la carpeta logs
                        APIS.LogsController log = new APIS.LogsController(e.ToString());
                        log.createFolder();
                        //envio error mensaje al usuario
                        ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el restablecimiento de contraseña, por favor intente mas tarde ");
                    }

                }//fin else captcha
                
            }
            return View("RecuperarContrasenia", model);
        }

        /// <summary>
        /// Metodo Valida el token del link gnerado por el correo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // GET: /Account/ValidaPasswordExternal
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        //[AllowAnonymous]
        public async Task<ActionResult> ValidaContraseniaExternal(validarContraseniaModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    //Ejecuto los valores en el SP
                    var response = db.SP_ValidarDatosRestablecimientoContrasenia(model.id, model.token).First();
                    //
                    await db.SaveChangesAsync();

                    //valido que el procedimiento se ejecute de manera correcta
                    if (response.codigo != 200)
                    {
                        //Retorno la vista principal no hay aviso alguno ya que peude que la pagina pueda estar bajo ataque
                        //cierro session
                        AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        return RedirectToAction("Index");

                    }
                    else if (response.codigo == 200)
                    {
                        //autentico al usuario session para que pueda modificar la contraseña y envio de manera permanente la cookie
                        FormsAuthentication.SetAuthCookie(response.idusuario.ToString(), true);
                        //creo un array a partir del json devuelto por la api para tratarlo desde aca y poder enviar los diferentes errores
                        var json = JsonConvert.SerializeObject(response.idusuario, Formatting.Indented);
                        //envio token a la vista
                        ViewBag.TokenP = response.idusuario;
                        return View("NuevaContrasenia", string.Empty);
                    }
                    else
                    {
                        // envio error a la api logs errores                        
                        //envio un mensaje al usuario
                        ModelState.AddModelError(string.Empty, "La plataforma no esta respondiendo a su solicitud, por favor intente mas tarde");
                        AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                    }

                }
                catch (Exception e)
                {
                    // envio error a la api logs errores
                    //envio a la carpeta logs
                    APIS.LogsController log = new APIS.LogsController(e.ToString());
                    log.createFolder();
                    //envio error mensaje al usuario
                    ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el momento por favor intente mas tarde");
                    //cierro session
                    AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                }
            }
            return RedirectToAction("Index");
        }

        //Este metodo es usado cuando el usuario va a ingresar la nueva contraseña
        // GET: /Account/ForgotPassword
        [Authorize]
        [OutputCache(NoStore = true, Duration = 0)] //https://diegobersano.com.ar/2014/08/13/outputcache-cache-de-acciones-en-asp-net-mvc/
        public ActionResult NuevaContrasenia()
        {
            return View();
        }


        // Post: /Account/NewPassword
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        //[OutputCache(NoStore = true, Duration = 0)]
        public async Task<ActionResult> NuevaContrasenia(CambiarContraseniaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MSG = new EnviarCorreoRecuperacionModel();
                try
                {
                    //sección del recaptcha
                    //Valido el capcha
                    if (!this.IsCaptchaValid("Captcha is not valid"))
                    {
                        ModelState.AddModelError(string.Empty, "Error: captcha no es válido.");
                    }
                    //si el captcha es valido
                    else
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
                            return RedirectToAction("Index");

                        }// fin if valida response
                        else
                        {
                            ModelState.AddModelError(string.Empty, response.mensaje);
                            return RedirectToAction("Index");
                        }

                    }//fin else captcha

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
            return View();
        }

        /*
         * Contraseña cifrada en pruebas
         * 
         */
        byte[] ComputeHash(byte[] data)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(data);
            }
        }

        // GET: Form Login
        public ActionResult ViewPartialLoginAdmin()
        {
            //Limpio campos
            ModelState.Clear();
            return View("IndexAdmin");

        }


        /// <summary>
        /// Metodo que ejecuta el inicio de sesion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// https://www.codeproject.com/Articles/844722/Hashing-Passwords-using-ASP-NETs-Crypto-Class
        /// https://codeday.me/es/qa/20190111/67544.html
        // POST: /Account/ViewPartialLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ViewPartialLoginAdmin([Bind(Include = "Usuario,Password")] LoginViewModelAdmin model)
        {

            //Valido los campos del modelo
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Valido el capcha

            if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                ModelState.AddModelError(string.Empty, "Error: captcha no es válido.");
            }
            //si el captcha es valido
            else
            {
               

                try
                {
                    var Password = Crypto.SHA256(model.Password);
                    var salt = Crypto.GenerateSalt();
                    var hashedPassword = Crypto.HashPassword(salt + Password);

                    //Ejecuto los valores
                    var response = db.SP_Ingreso_Usuario_Administrador(model.Usuario, model.Password).FirstOrDefault();
                    //
                    await db.SaveChangesAsync();
                    //
                    if (response != null && response.codigo.Equals(200))
                    {

                        var obj = db.Web_Administrador.Where(u => u.usuario.Equals(model.Usuario)).FirstOrDefault();
                       
                        //FormsAuthentication.SetAuthCookie(obj.administrador_id.ToString(), true);
                        FormsAuthentication.SetAuthCookie(obj.administrador_id.ToString()+ "|" + obj.FK_web_administrador_rol.ToString(), true);

                        //return RedirectToAction("IndexAdmin", "Tablero");
                        return RedirectToAction("Index", "Tablero");
                    }
                    else if (response.codigo != 200)
                    {
                        ModelState.AddModelError(string.Empty, response.mensaje);
                    }
                    else
                    {
                        //Limpio campos
                        ModelState.Clear();
                        //envio un mensaje al usuario
                        ModelState.AddModelError(string.Empty, "La plataforma no esta respondiendo a su solicitud, por favor intente mas tarde");
                    }

                }
                catch (Exception e)
                {
                    //envio error a la api logs errores
                    //y envio a la carpeta logs
                    APIS.LogsController log = new APIS.LogsController(e.ToString());
                    log.createFolder();
                    //Limpio campos
                    ModelState.Clear();
                    //envio error mensaje al usuario
                    ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el momento por favor intente mas tarde ");
                }

            }//fin else captcha

            //retorno la vista en caso de que no se efectue el regsitro
            return View("IndexAdmin", model);

        }

        /// <summary>
        /// Metodo para el cierre de sesión
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            //Cierro sesiones
            FormsAuthentication.SignOut();
            Session.Abandon();
            //Limpio campos
            ModelState.Clear();
            return RedirectToAction("Index");
        }


    }
}