/**
 * Created by Alex on 15/06/2019.
 */

$(document).ready(function () {
    getRol();
})


//llamo la api y alimento el ul de manuales
function getRol(id_rol) {

    $.ajax({
        type: "GET",
        url: baseURL + "api/Rol/Listar",
        success: function (data) {

            var option = $('<option></option>').attr("value", "").text("Seleccione...");
            $.each(data, function (i, v) {

                option = $('<option></option>').attr("value", v.rol_id).text(v.nombre);
                $("#fkrol").append(option);
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

    getModulos();
}

//llamo la api y alimento el ul de manuales
function getModulos(id_rol) {
    
    $.ajax({
        type: "GET",
        url: baseURL + "api/Modulos/Listar",
        data: { rol: id_rol },
        success: function (response) {

            $.each(response, function (i, v) {

                $('#asigna_permiso').append(
                    '<div class="row">'+
            '<div class="col-md-12 col-sm-12">'+
                '<!--table '+i+'--->'+
                '<div class="row">'+
                    '<!--modulo--->'+
                    '<div class="col-md-3 col-sm-12">' +
                    '<div class="form-group">'+
                        '<label class="form-check-label">'+
                    '<input class="form-check-input" type="checkbox" name="modulo[0_' + v.modulo_id + ']" id="modulo' + v.modulo_id + '" value="' + v.modulo_id + '">' +
                     v.nombre +
                            '<span class="form-check-sign">'+
                                '<span class="check"></span>'+
                            '</span>'+
                        '</label>'+
                    '</div>'+
                        '</div>' +
                    '<!--/modulo--->'+
                    '<!--permisos--->'+
                    '<div class="col-md-2 col-sm-12">'+
                        '<label class="form-check-label">'+
                    '<input class="form-check-input" type="checkbox" name="prcrea[0_' + v.modulo_id +']" id="IVE_1" value="true">'+
                            'Crear'+
                            '<span class="form-check-sign">'+
                                '<span class="check"></span>'+
                            '</span>'+
                        '</label>'+
                    '</div>'+
                    '<div class="col-md-2 col-sm-12">'+
                        '<label class="form-check-label">'+
                    '<input class="form-check-input" type="checkbox" name="prmodifica[0_' + v.modulo_id +']" id="IVE_2" value="true">'+
                            'Modificar'+
                            '<span class="form-check-sign">'+
                                '<span class="check"></span>'+
                            '</span>'+
                        '</label>'+
                    '</div>'+
                    '<div class="col-md-2 col-sm-12">'+
                        '<label class="form-check-label">'+
                    '<input class="form-check-input" type="checkbox" name="prelimina[0_' + v.modulo_id +']" id="IVE_3" value="true">'+
                            'Eliminar'+
                            '<span class="form-check-sign">'+
                                '<span class="check"></span>'+
                            '</span>'+
                        '</label>'+
                    '</div>'+
                    '<!--/permisos--->'+
                '</div>'+
                '<!--/table i--->'+
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
            "Error al intertar traer el listado de loss modulos",
            'error'
        );

    });

}
