/**
 * Created by Alex on 15/06/2019.
 */

$(document).ready(function () {
    //getDatosTablero();
})


//llamo la api y alimento el ul de manuales
function getControlTablero(id_rol) {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Modulos/Modulo_permiso",
        data: { rol: 1 },
        success: function (response) {

            $.each(response, function (i, v) {

                $('#listamandos').appendTo(
                    '<div class="col-lg-4 col-sm-1">' +
                    '<div class="card">' +
                    '<!-- Card image -->' +
                    '<div class="view overlay">' +
                    '<img class="card-img-top" src="' + v.imagen + '" alt="Card image cap">' +
                    '<a href="#!">' +
                    '<div class="mask rgba-white-slight"></div>' +
                    '</a>' +
                    '</div>' +

                    '<!-- Card content -->' +
                    '<div class="card-body">' +

                    '<!-- Title -->' +
                    '<h4 class="card-title">' +
                    v.nombre +
                    '</h4>' +
                    '<!-- Text -->' +
                    '<p class="card-text">' + v.descripcion + '</p>' +
                    '<!-- Button -->'+
                    //if (v.pr_crear == true) {
                    (v.pr_crear == true > 0 ? ' < a href = "' + baseURL + v.ruta + '" class= "btn btn-primary" id = "btnTacceso" > Acceder</a >' : '') +
                        //'<a href="' + baseURL + v.ruta + '" class="btn btn-primary" id="btnTacceso">Acceder</a>' +
                    //}
                    '</div>' +
                    '</div >'

                )

                if (v.pr_crear == false) {
                    document.getElementById('btnTacceso').remove();
                    $('#btnTcrear').remove();
                }
                if (v.pr_modificar == true) {
                    $('#btnTmodificar').remove();
                }
                if (v.pr_eliminar == true) {
                    $('#btnTeliminar').remove();
                }
                
                //console.log(v);
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        Swal.fire(
            '',
            "Error al intertar traer los permisos",
            'error'
        );

    });

}

//llamo la api para alimentar el tablero
function getDatosTablero(rol_id) {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Web_Modulo",
        success: function (response) {

            $.each(response, function (i, v) {
                $('#listamandos').html(
                    '<div class="col-lg-4 col-sm-1">' +
                    '<div class="card">'+
                '<!-- Card image -->'+
                '<div class="view overlay">'+
                    '<img class="card-img-top" src="'+v.imagen+'" alt="Card image cap">'+
                    '<a href="#!">'+
                        '<div class="mask rgba-white-slight"></div>'+
                    '</a>'+
                '</div>'+

                '<!-- Card content -->'+
                '<div class="card-body">'+

                    '<!-- Title -->'+
                    '<h4 class="card-title">'+
                        v.nombre +
                    '</h4>'+
                    '<!-- Text -->'+
                    '<p class="card-text">'+v.descripcion+'</p>'+
                    '<!-- Button -->' +
                    '<a href="' + baseURL + v.ruta + '" class="btn btn-primary" id="btnTacceso">Acceder</a>' +

                '</div>'+
            '</div >'

                )
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        Swal.fire(
            '',
            "Error al intertar traer la información",
            'error'
        );

        });

    getControlTablero(rol_id);
    
}
