/**
 * @author ALEXANDER CIFUENTES SANCHEZ
 * @description este archivo se encarga de validar los adjuntos para que el servidor no se cuelgue
 * en esta operación
 * 26-10-2017
 */

jQuery(function () {
    
    $('#formulariocargaarchivo').validate({
            rules: {                
                fechaInicio: {
                    required: true,
                },
                fechaFin: {
                    required: true,
                },
                rips: {
                    required: true,
                }
            },
            messages: {
                tipoUsuario2: "Seleccione un tipo de usuario",
                categoria: "Seleccione una categoria",
                fechaInicio: "Indique una fecha de inicio",
                fechaFin: "Indique una fecha de fin",
                rips: "Seleccione sus archivos",
            },
            errorPlacement: function (error, element) {
                error.addClass("text-danger");
                error.insertAfter(element);
            }
           
    });
});