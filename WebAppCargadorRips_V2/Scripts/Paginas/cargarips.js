/**
 * @author ALEXANDER CIFUENTES SANCHEZ
 * @description este archivo se encarga de la pagina home y la pagina carga de rips
 * @ojo El archvio que controla la zona de carga se enceuntra en multifilecontrol
 * 26-10-2017
 */

var TipoEstructuraArray = []; //variable que almacena las estructuras existentes en la norma
var nombre = []; //variable almacena los nombre de los archivos a cargar
//var FechaMin;
var currentDate1 = new Date();
var currentDate2 = new Date();
var FechaDeReporte;

var today = new Date();
var dd = String(today.getDate()).padStart(2, '0');
var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
var yyyy = today.getFullYear();

var FechaInicioReporte = null;
var FechaFinReporte = null;


$(document).ready(function () {
    $("#mcarga").addClass("active");
    //valido que el explorador soporte la api de lectura de archivos
    if (!(window.File)) {
        //console.log('La API File no está soportada en este esplorador');
        swal.fire(
            'Precaución',
            'Su explorador no soporta la lectura de archivos, por favor cambie a un explorador mas reciente o active la función si la desactivado',
            'info'
        )
        return;
    }

    

    //escucho select tipousuario para efectuar respectivas acciones
    $('#tipoUsuario2').change(function (event) {

        var res = $(this).val().substring(4, 5);
        // llamo a la funcion categoria y le envio el tipo de usuario seleccionado y el id del tipo de usuario
        //con un +1 ya que no lista un registro ener cuidado con los registros
        callCategoria($(this).val(), parseInt(res) + 1);

    });

    //escucho valor del campo check IVE para efectuar respectivas acciones
    $('#IVE').change(function (event) {
        if ($(this).is(':checked')) {
            deshabilitarDatosGer();

        } else {
            habilitarDatosGer();
        }
    });
    //escucho valor del campo check NO POS para efectuar respectivas acciones
    $('#NOPOS').change(function (event) {
        if ($(this).is(':checked')) {
            //Envio el valor de subsidiado al select de tipo usuario y categoria con general
            $('#tipoUsuario2').val('tipo2');
            //dahabilito los select
            deshabilitarDatosGer2();

        } else {
            //habilito los selects
            habilitarDatosGer2();
            $('#tipoUsuario2').val('');
            $('#categoria').append(new Option("Seleccione...", ''));
            $('#categoria').val('');
            $("select").material_select();
        }
    });

    //inicializo la llamada a los datos de los calendarios
    //callFechasPeriodos();
    
   
});

//Función que permite traer las fechas definidas para los periodos de cada uno de los calendarios
function callFechasPeriodos(rol) {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Fechas/Listar",
        data: { rol: rol },
        success: function (response) {
            $.each(response, function (i, v) {
                if (v.fecha_id == 1) {
                    FechaMin = v.valor_fecha.substring(0, 10).replace(/-/g, '/');
                    FechaMax = new Date(currentDate2.getFullYear(), currentDate2.getMonth() - 0, 0);
                    calendarios(FechaMin, FechaMax);
                }

                //valido si el prestador tiene la fecha habilitada para realizar el cargue
                if (v.fecha_id != 1 && v.nombre_fecha.substring(0, 12).trim() =='fecha inicio' ) {
                    FechaInicioReporte = v.valor_fecha.substring(0, 10);
                    if (new Date(today) < new Date(FechaInicioReporte)) {
                        $('#btncargarinfo').remove();
                    }
                    
                }
                //valido si el prestador tiene la fecha habilitada para realizar el cargue
                if (v.fecha_id != 1 && v.nombre_fecha.substring(0, 10).trim() == 'fecha fin') {
                    FechaFinReporte = v.valor_fecha.substring(0, 10);
                    console.log(FechaFinReporte);
                    if (new Date(today) >= new Date(FechaFinReporte)) { 
                        $('#btncargarinfo').remove();
                    }
                }
                
            });
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        Swal.fire(
            'Error',
            'Error al traer la fecha para los calendarios, comuníquese con el administrador',
            'error',         
        )
        console.error(textStatus, errorThrown); // Algo fallo

        })

}

//Getting the last day for a given year and month:
function getLastDayOfYearAndMonth(year, month) {
    console.log(month);
    return (new Date((new Date(year, month + 1, 1)) - 1)).getDate();
}

//Función encargada de los calendarios
function calendarios(FechaMin, FechaMax) {
   
    $('#fechaInicio').datepicker({
        locale: 'es-es',
        format: 'yyyy/mm/dd',
        minDate: FechaMin,
        maxDate: FechaMax,
        close: function (e) {

            var fec1 = moment(e.target.value).format('YYYY-MM-DD');
            var fec2 = moment(fec1).endOf('month').format('YYYY/MM/DD');
            $("#fechaFin").datepicker().value(new Date(fec2));
            
        },
        disableDates: function (date) {
            var disabled = [2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31];
            if (disabled.indexOf(date.getDate()) == -1) {
                return true;
            } else {
                return false;
            }
        }
        
    })
    

    $('#fechaFin').datepicker({
        locale: 'es-es',
        format: 'yyyy/mm/dd',
        minDate: FechaMin,
        maxDate: FechaMax,
        disableDates: function (date) {
            if (date.getDate() ==  getLastDayOfYearAndMonth(date.getFullYear(), date.getMonth())) {
                return true;
            } else {
                return false;
            }
        }
    });
} 

//efectuo acciones necesarias para el buen funcionamiento de la aplicación
function deshabilitarDatosGer() {
    $('#tipoUsuario2').prop('disabled', true);
    $('#categoria').prop('disabled', true);
    //$("select").material_select();
    $('#NOPOS').prop('disabled', true);
    $('#fortipo').addClass("hide");
    $('#forcate').addClass("hide");
    //callTipoUsuario();
}

//efectuo acciones posteriores 
function habilitarDatosGer() {
    $('#tipoUsuario2').prop('disabled', false);
    $('#categoria').prop('disabled', false);
    //$("select").material_select();
    $('#NOPOS').prop('disabled', false);
    $('#fortipo').removeClass("hide");
    $('#forcate').removeClass("hide");

}
//efectuo acciones necesarias para el buen funcionamiento de la aplicación
function deshabilitarDatosGer2() {
    $('#tipoUsuario2').prop('disabled', true);
    $('#categoria').prop('disabled', true);
    //$("select").material_select();
    $('#IVE').prop('disabled', true);
    $('#fortipo').addClass("hide");
    $('#forcate').addClass("hide");

}
//efectuo acciones posteriores 
function habilitarDatosGer2() {
    $('#tipoUsuario2').prop('disabled', false);
    $('#categoria').prop('disabled', false);
    //$("select").material_select();
    $('#IVE').prop('disabled', false);
    $('#fortipo').removeClass("hide");
    $('#forcate').removeClass("hide");

}

//Función que me permite limpiar los campos
function limpiardivfiles() {
    $('#filename').html("");

    zonaarchvos();
    //$('#rips').val('');
    //$('#archivos').val('');
}

//funcion cancela la operación y retorna a la pagina principal
function cancelado() {
    Swal.fire({
        title: '¿Esta seguro?',
        text: 'Desea cancelar la operación de carga',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Cancelar',
        cancelButtonText: 'No',
        cancelButtonColor: '#d33',
        confirmButtonClass: 'btn green',
        cancelButtonClass: 'waves-effect waves-light btn red',
    }).then((result) => {

        if (result.value) {
            Swal.fire(
                'Cancelado',
                'Operación Cancelada',
                'error',
                setTimeout(function () {
                    //location.reload("/Home/Index");
                    window.location.href = baseURL + "Home";
                }, 2000)
            )
        }
        
    })
}


//funcion solicita datos para cargar al select de tipo de usuarios
function callTipoUsuario() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/TipoUsuario/Listar",
        success: function (data) {

            var option = $('<option></option>').attr("value", "").text("Seleccione...");
            $.each(data, function (i, v) {

                option = $('<option></option>').attr("value", "tipo" + v.numero).text(v.nombre);
                $("#tipoUsuario2").append(option);
            });
            
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.error(textStatus, errorThrown); // Algo fallo
        swal.fire(
            textStatus,
            'Hay un problema al traer los datos de tipo de usuario comuníquese con el administrador ',
            'error',
            setTimeout(function () {
            }, 2000)
        )
    });

}

//funcion solicita datos para cargar al select de categorias
function callCategoria(objeto, idtipousuario) {
    $('#categoria').empty();

    $.ajax({
        type: "GET",
        url: baseURL + "api/Categorias/Listar",
        data: { 'id': objeto },
        success: function (data) {
            var option = $('<option></option>').attr("value", "").text("Seleccione...");
            $.each(data, function (i, v) {
                option = $('<option></option>').attr("value", v.categoria_id).text(v.nombre);
                $("#categoria").append(option);
            });
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.error(textStatus, errorThrown); // Algo fallo
        swal.fire(
            textStatus,
            'Hay un problema al traer los datos de las Categorías comuníquese con el administrador ',
            'error',
            setTimeout(function () {
            }, 2000)
        )
    });
    $("#tipoUsuario").val(idtipousuario)
}


//Función que permite traer los campos para cada estructura de los archivod
function callEstructuraCampo() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Estructura/Listar",
        success: function (response) {

            $.each(response, function (i, v) {
                TipoEstructuraArray[v.estAcronimo] = v.cantidadDatos;
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        console.error(textStatus, errorThrown); // Algo fallo
    })
}

//Esta funcion envia el formulario para almacenarlo en la tabla 
//de validacion
function UploadValidacionConErrores(reserrores) {

    var formd = $('#formulariocargaarchivo')[0];
    var data = new FormData(formd);

    $.ajax({
        url: baseURL + "api/Rips/GuardarValidacionConErrores",
        type: "POST",
        data: data,
        processData: false,
        contentType: false,
        beforeSend: function () {
            swal.fire({
                title: 'Espere..',
                text: 'Almacenando información, esto puede tardar unos segundos.',
                animation: false,
                customClass: 'animated tada',
                allowOutsideClick: false,
                onOpen: () => {
                    swal.showLoading()
                }
            }).then((result) => {
                //console.log('cerrado modal')
                //window.location.href = "/Home";
            })
        },
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                //cambio el tititulo si es diferente de error
                //console.log(v);
                if (v.type == "error") {
                    titulo = "Mensaje";
                    swal.fire(
                        titulo,
                        v.value,
                        v.type
                    ).then((result) => {
                        //console.log('cerrado modal')
                        //location.reload();
                        enviarCorreoErrores(reserrores);
                        //window.location.href = baseURL +"Home";
                    })
                }
                /*modificado 30-11-2018
                 * else {
                  //Envio correo de errores
                  enviarCorreoErrores(reserrores)
                }*/

            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        console.error(textStatus, errorThrown); // Algo fallo
    });

}


//Esta funcion envia el correo al cargar los archivos en el servidor
//encontrados
function enviarCorreo(tipoCorreo, consec) {

    var email = $('#emailspan').text();
    var nombreuser = $('#nombreuserspan').text();
    $.ajax({
        type: "POST",
        url: baseURL + "api/Correo/SendEmail",
        data: { codPlantilla: tipoCorreo, usercorreo: email, usernombre: nombreuser, codigocarga: consec },
        beforeSend: function () {
            swal.fire({
                title: 'Espere..',
                text: 'Enviando correo por favor espere, no cierre el dialogo, esto puede tardar unos segundos.',
                animation: false,
                customClass: 'animated tada',
                allowOutsideClick: false,
                onOpen: () => {
                    swal.showLoading()
                }
            }).then((result) => {
                //console.log('cerrado modal')

            })
        },
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                //valido el codigo retornado por la api aqui pongo de codigo 1009 ya que es el envio de correo
                if (v.codigo == 1009) {
                    setTimeout(function () {
                        window.location.href = baseURL + "Home";
                    }, 9000)
                }
                //cambio el tititulo si es diferente de error
                if (v.type !== "error") {
                    titulo = "Mensaje";
                } else {
                    titulo = v.type;
                }
                swal.fire(
                    titulo,
                    v.value,
                    v.type
                )

            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        console.error(textStatus, errorThrown); // Algo fallo
    });

}

//Esta funcion envia el correo con los diferentes problemas
//encontrados
function enviarCorreoErrores(errores) {
    //console.log(errores.length);
    //envio a la api los errores
    var email = $('#emailspan').text();
    var nombreuser = $('#nombreuserspan').text();

    $.ajax({
        type: "POST",
        url: baseURL + "api/Correo/SendEmailErrors",
        data: { codPlantilla: 2, usercorreo: email, errores: errores, usernombre: nombreuser },
        beforeSend: function () {
            swal.fire({
                title: 'Espere..',
                text: 'Enviando correo con los errores por favor espere, no cierre el dialogo, esto puede tardar unos segundos.',
                animation: false,
                customClass: 'animated tada',
                allowOutsideClick: false,
                onOpen: () => {
                    swal.showLoading()
                }
            }).then((result) => {
                //console.log('cerrado modal')

            })
        },
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {

                //cambio el tititulo si es diferente de error
                if (v.type !== "error") {
                    titulo = "Mensaje";
                } else {
                    titulo = v.type;
                }
                swal.fire(
                    titulo,
                    v.value,
                    v.type
                ).then((result) => {
                    //console.log('cerrado modal')
                    window.location.href = baseURL + "Home";
                })

            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        console.error(textStatus, errorThrown); // Algo fallo
        //notifico al usuario del error al enviar correo y recargo la pagina
        swal.fire({
            title: 'Error',
            text: "Lo sentimos la plataforma no pudo enviar el correo por favor comuníquese con el administrador",
            type: 'error',
            showCancelButton: false,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Recargar'
        }).then((result) => {
            //if (result.value) {
            location.reload();
            //}
        })
    });
    //limpio la variable errores
    errores.length = 0;
}


//funcion envia el archivo a los servidores
function loadRIPS() {
    //debugger;

    var formd = $('#formulariocargaarchivo')[0];
    var data = new FormData(formd);

    $.ajax({
        url: baseURL + "api/Rips/Upload",
        type: "POST",
        data: data,
        processData: false,
        contentType: false,
        beforeSend: function () {
            swal.fire({
                title: 'Espere..',
                text: 'Enviando archivos por favor espere.',
                animation: false,
                customClass: 'animated tada',
                //timer: 5000,
                onOpen: () => {
                    swal.showLoading()
                }
            });
        },
        success: function (response) {
            $.each(response, function (i, v) {
                //valido el codigo retornado por la api
                if (v.codigo == 201) {
                    //envio correo
                    enviarCorreo(1, v.consec);
                }
                //cambio el tititulo si es diferente de error
                console.log(v.type);
                if (v.type !== "error") {
                    //titulo="Mensaje";
                } else {
                    titulo = v.type;
                    swal.fire(
                        titulo,
                        v.value,
                        v.type
                    )
                }
            });
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.error(textStatus, errorThrown); // Algo fallo
        swal.fire(
            textStatus,
            'Hay un problema al cargar los datos y archivos comuníquese con el administrador ',
            'error',
            setTimeout(function () {
            }, 2000)
        )
    });
    //debugger;
}