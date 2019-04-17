/**
 * @author ALEXANDER CIFUENTES SANCHEZ
 * @description este archivo se encarga de la pagina home y la pagina carga de rips
 * 26-10-2017
 */





$(document).ready(function () {

    //valido que el explorador soporte la api de lectura de archivos
    if (!(window.File)) {
        //console.log('La API File no está soportada en este esplorador');
        swal(
            'Precaución',
            'Su explorador no soporta la lectura de archivos, por favor cambie a un explorador mas reciente o active la función si la desactivado',
            'info'
        )
        return;
    }

    //
    calendarios();

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
   
});

//Getting the last day for a given year and month:
function getLastDayOfYearAndMonth(year, month) {
    return (new Date((new Date(year, month + 1, 1)) - 1)).getDate();
}

function calendarios() {

    $('#fechaInicio').datepicker({
        locale: 'es-es',
        format: 'dd/mm/yyyy',
        minDate: '12/02/2017',
        maxDate: '12/05/2019',
        disableDates: function (date) {
            var disabled = [2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31];
            if (disabled.indexOf(date.getDate()) == -1) {
                return true;
            } else {
                return false;
            }
        }
    });
    $('#fechaFin').datepicker({
        locale: 'es-es',
        format: 'dd/mm/yyyy',
        minDate: '12/02/2017',
        maxDate: '12/05/2019',
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

function dropzone() {
    // Dropzone class:
    var myDropzone = new Dropzone("div#myId", { url: "/file/post" });
}

function produ() {

    if (validarCamposVacios() == true) {
        $.ajax({
            url: '/Armada/Controladores/controladorTbaspirantes.php?tarea=insertar',
            method: 'POST',
            data: formData.serialize(),
            beforeSend: function () {
                alert('almacenando');
            },
            success: function (data) {
                alert('error');
            }
        });
    }
} 


