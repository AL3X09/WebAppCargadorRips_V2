/**
 * Created by Alex on 11/03/2017.
 */
//tener en cuenta se modifico el archivo materialize.js para que aceptara valores en español
//desde las lineas 806

$(document).ready(function () {

    getManuales();

});

//llamo la api y alimento el ul de manuales
function getManuales() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Manuales/Listar",
        success: function (response) {

            $.each(response, function (i, v) {
                $('#listmanuales').append(
                    '<div class="card">' +
                    '<div class="card-header">' +
                    '<a class="card-link" data-toggle="collapse" href="#v.pregunta_frecuente">' +
                    '<div><a href="' + v.ruta + '" class="secondary-content" target="_blank">' + v.descripcion + ' <i class="material-icons">send</i></a></div>' +
                    '</a>' +
                    '</div>' +
                    '</div>'
                )
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        Swal.fire(
            '',
            "Error al intertar traer las presentaciones",
            'error'
        );

    });
    
    getPreguntasFrecuentes();
}

//llamo la api y alimento el ul de pregunta frecuente
function getPreguntasFrecuentes() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Preguntas/Listar",
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                $('#listpreguntas').append(
                    '<div id="sectionP' +i+ '">' +
                    '<div class="card">' +
                    '<div class="card-header" id="sectionP' + i + '">' +
                    '<h5 class="mb-0">' +
                    '<button class="btn btn-link" data-toggle="collapse" data-target="#P' + i + '" aria-expanded="true" aria-controls="P' + i + '">' +
                    v.pregunta_frecuente +
                    '</button>' +
                    '</h5>' +
                    '</div>' +
                    '<div id="P' + i + '" class="collapse" aria-labelledby="sectionP' + i + '" data-parent="#sectionP' +i+ '">' +
                    '<div class="card-body">' +
                    v.respuesta_preguntas +
                    '</div>' +
                    ' </div>' +
                    '</div>' +
                    '</div>'
                )
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        Swal.fire(
            '',
            "Error al intertar traer las preguntas frecuentes",
            'error'
        );

        //envio a la api errores para que almacene el error
    });
    getPresentaciones();
}


//llamo la api y alimento el ul de presentaciones
function getPresentaciones() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Presentaciones/Listar",
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                $('#listpresentaciones').append(
                    '<div class="card">' +
                    '<div class="card-header">' +
                    '<a class="card-link" data-toggle="collapse" href="#v.pregunta_frecuente">' +
                    '<div><a href="' + v.ruta + '" class="secondary-content" target="_blank">' + v.descripcion + ' <i class="material-icons">send</i></a></div>' +
                    '</a>'+
                    '</div>'+
                    '</div>'
                )
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        Swal.fire(
            '',
            "Error al intertar traer las presentaciones",
            'error'
        );

        //envio a la api errores para que almacene el error
    })
    getNormatividad();
}



//llamo la api y alimento el ul de resoluciones y normas
function getNormatividad() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Normatividad/Listar",
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                $('#listresolucionesnormas').append(
                    '<div class="card">' +
                    '<div class="card-header">' +
                    '<a class="card-link" data-toggle="collapse" href="#v.pregunta_frecuente">' +
                    '<div><a href="' + v.ruta + '" class="secondary-content" target="_blank">' + v.descripcion + ' <i class="material-icons">send</i></a></div>' +
                    '</a>' +
                    '</div>' +
                    '</div>'
                )
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        Swal.fire(
            '',
            "Error al intertar traer las Resoluciones y normas",
            'error'
        );

        //envio a la api errores para que almacene el error
    })
    getContactos();
}

//llamo la api y alimento las tarjetas de contactos(usuarios administradores)
function getContactos() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Usuarios/ListarContactos",
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                $('#divcontactos').append(

                    '<div class="col-md-4">' +
                    '<div class="card card-profile">' +
                    '<div class="card-avatar">' +
                    '<a href="#!">' +
                    '<img class="img" src="' + baseURL + v.imagen + '">' +
                    '</a>' +
                    '</div>' +
                    '<div class="card-body">' +
                    '<h6 class="card-category text-gray">CEO / Co-Founder</h6>' +
                    '<h4 class="card-title">' + v.nombre + ' ' + v.apellido + '</h4>' +
                    '<p class="card-description">' +
                    '<p>Descripción:</p>' +
                    '<p>' + v.descripcion + '</p>' +
                    '<div class="card-action">' +
                    '<p>Correo:</p>' +
                    '<spam>' + v.correo + '</spam>' +
                    '</div>' +
                    '<div class="card-action">' +
                    '<p> EXT:' + v.extencion + '</p>' +
                    '</p>' +
                    '</div>' +
                    '</div>' +
                    '</div>'

                )
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        Swal.fire(
            '',
            "Error al intertar traer la informacion del equipo",
            'error'
        );

        //envio a la api errores para que almacene el error
    });

}