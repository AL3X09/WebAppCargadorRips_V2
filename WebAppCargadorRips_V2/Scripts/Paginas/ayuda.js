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

                    '<div class="">' +
                    '<div class="card-header card-header-info mt-3">' +
                    '<a class="card-link" data-toggle="collapse" href="#v.pregunta_frecuente">' +
                    '<div><a href="' + v.ruta + '" class="secondary-content title" target="_blank">' + v.descripcion + ' <i class="material-icons">send</i></a></div>' +
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
    var j = 0;
    var conte1 = "";
    var conte2 = "";
    //let conte2 = new Array();
        $.ajax({
        type: "GET",
        url: baseURL + "api/Preguntas/Listar",
            success: function (response) {

               

                $.each(response, function (i, v) {
                    ///console.log(conte2[10]);
                    if (j !== v.tipo) {
                    j = v.tipo;
                        conte1 += '<div id="sectionP' + i + '">' +
                            '<div class="card card-header-info mt-3">' +
                            '<div class="card-header" id="sectionP' + i + '">' +
                            '<h5 class="mb-0">' +
                            '<button class="btn btn-link card-title" data-toggle="collapse" data-target="#P' + i + '" aria-expanded="true" aria-controls="P' + i + '">' +
                            v.Nombre_Preguntas +
                            '</button>' +
                            '</h5>' +
                            '</div>' +
                            '<div id="P' + i + '" class="collapse" aria-labelledby="sectionP' + i + '" data-parent="#sectionP' + i + '">' +
                            '<div class="card-body" id="subpreguntas' +j+'">'+

                            //v.respuesta_preguntas +
                            

                             '</div>' +
                            ' </div>' +
                            '</div>' +
                            '</div>';

                        //'<div id="subpreguntas' + j + '"></div>' +
                }

               
                });

                $('#listpreguntas').html(
                    //console.log(conte)
                    conte1
                )
                
                

                $.each(response, function (k, y) {
                    
                    
                    conte2 += '<div class="card mt-3">' +
                        '<div class="card-header" id="sectionJ'+k+'">' +
                        '<h5 class="mb-0"><button class="btn btn-link  btn-round card-title" data-toggle="collapse" data-target="#J' + k + '" aria-expanded="true" aria-controls="J' + k + '">' +
                        '<h4 class="card-subtitle mb-2 text-muted">' + y.pregunta_frecuente +'</h4>'+
                       
                        '</button>' +
                        '</h5>' +
                        '</div>' +
                        '<div id="J'+k+'" class="collapse" aria-laben lledby="sectionJ'+k+'" data-parent="#sectionJ'+k+'">' +
                        '<div class="card-body">' +
                        y.respuesta_preguntas +
                        '</div>' +
                        '</div>' +
                        '</div>';
                    $('#subpreguntas' + y.tipo).append(conte2);
                    //$(conte2).append('#subpreguntas' + y.tipo);
                    //console.dir(document.getElementById("#subpreguntas'" + y.tipo + '"'));
                    
                    //$('#subpreguntas' + y.tipo).append("<strong>Hello</strong>");

                    //console.log(conte2);
                    conte2 = "";
                    
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
                    '<div class="">' +
                    '<div class="card-header card-header-info mt-3">' +
                    '<a class="card-link " data-toggle="collapse" href="#v.pregunta_frecuente">' +
                    '<div><a href="' + v.ruta + '" class="secondary-content title" target="_blank">' + v.descripcion + ' <i class="material-icons">send</i></a></div>' +
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
                    '<div class="">' +
                    '<div class="card-header card-header-info mt-3">' +
                    '<a class="card-link" data-toggle="collapse" href="#v.pregunta_frecuente">' +
                    '<div><a href="' + v.ruta + '" class="secondary-content title" target="_blank">' + v.descripcion + ' <i class="material-icons">send</i></a></div>' +
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
    getRedadscrita();
}

//llamo la api y alimento el ul de resoluciones y normas
function getRedadscrita() {

    $.ajax({
        type: "GET",
        url: baseURL + "api/DocsRedadscrita/Listar",
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {

                $('#listdocredascrita').append(
                    '<div class="">' +
                    '<div class="card-header card-header-info mt-3">' +
                    '<a class="card-link" data-toggle="collapse" href="#v.doc_redascrita">' +
                    '<div><a href="' + v.ruta + '" class="secondary-content title" target="_blank">' + v.descripcion + ' <i class="material-icons">send</i></a></div>' +
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
        url: baseURL + "api/Contactos/Listar",
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                $('#divcontactos').append(

                    '<div class="col-md-4">'+
                        '<div class="card card-profile">'+
                            '<div class="card-avatar">'+
                                '<a href="#!">'+
                                    '<img class="img" src="' + baseURL + v.imagen + '">'+
                                 '</a>'+
                            '</div>'+
                            '<div class="card-body">'+
                                    
                                    '<h4 class="card-title">' + v.nombres + ' ' + v.apellidos + '</h4>'+
                                    '<p class="card-description">'+
                                        '<p><strong>Descripción:</strong></p>' +
                                        '<p>' + v.descripcion + '</p>' +
                                        '<div class="card-action">' +
                                        '<p><strong>Correo:</strong></p>' +
                                        '<spam>' + v.correo + '</spam>' +
                                        '</div>' +
                                        '<div class="card-action">' +
                                        '<p> EXT:' + v.extension + '</p>' +
                                    '</p>'+
                             '</div>'+
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
            "Error al intertar traer la informacion del equipo",
            'error'
        );

        //envio a la api errores para que almacene el error
    });

}
