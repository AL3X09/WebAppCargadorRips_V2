$(document).ready(function () {
    $("#madmin").addClass("active");
});

//llamo la api y alimento el ul de manuales
function getControlAdministración(id_rol) {

    $.ajax({
        type: "GET",
        url: baseURL + "api/PermisosModulos/Administrar",
        data: { rol: id_rol },
        success: function (response) {

            $.each(response, function (i, v) {

                $('#listadministrar').append(

                    '<div class="col-lg-3 col-md-3 mb-4 col-sm-12" id"' + v.nombre + '">' +
                    '<div class="card">' +
                    '<!-- Card image -->' +
                    '<div class="view overlay">' +
                    '<img class="card-img-top" src="' + v.imagen + '" alt="' + v.nombre + '">' +
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
                    '<!-- Button -->' +
                    (v.pr_crear == true ? '<a href = "' + baseURL + v.ruta + '" class= "btn btn-primary" id = "btnTacceso">Administrar</a >' : '') +
                    '</div>' +
                    '</div >'
                )


                if (v.pr_crear == false && document.getElementById("btnTacceso") != undefined) {
                    document.getElementById('btnTacceso').remove();
                }
                if (v.pr_modificar == false && document.getElementById("btnTmodificar") != undefined) {
                    document.getElementById('btnTmodificar').remove();
                }
                if (v.pr_eliminar == true && document.getElementById("btnTeliminar") != undefined) {
                    document.getElementById('btnTeliminar').remove();
                }

            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
        Swal.fire(
            '',
            "Error al intertar traer los datos del tablero de control",
            'error'
        );

    });

}