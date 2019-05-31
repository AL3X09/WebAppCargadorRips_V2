/**
 * Created by Alex on 11/03/2017.
 */
//tener en cuenta se modifico el archivo materialize.js para que aceptara valores en español
//desde las lineas 806
//var src;
$(document).ready(function () {

    /**
    * FIN FUNCIONES ANONIMAS
    */
    
    document.getElementById("imguserperfil").addEventListener("click", openModalImagen);

});



//funcion abre el modal para cambiar la imagen, este metodo se llamad desde layoutjs
function openModalImagen(src1) {

    var src = document.getElementById("imguserperfil").src;
    //alert(src);

    var modalButtonOnly = new tingle.modal({
        closeMethods: [],
        footer: true,
        stickyFooter: true
    });

    modalButtonOnly.open();

    modalButtonOnly.setContent($('.tingle_addon_window').html());

    modalButtonOnly.addFooterBtn('Cambiar Avatar', 'btn btn-info pull-left', function () {
        //aqui logica para guardar
        updateAvatar();
        modalButtonOnly.close();
    });

    modalButtonOnly.addFooterBtn('Cancelar', 'btn btn-danger pull-right', function () {
        //aqui logica para cancelar
        modalButtonOnly.close();
    });
    if (document.getElementById("formuserimage") != undefined || document.getElementById("formuserimage") != null) {

        //$("#imagePreview").append('<input type="file" id="bla" class="dropify" data-default-file="'+v.ImagenUsuario+'">'); //esta linea la puse xq la libreria no reconoce el primer nodo no eliminar
        //$("#avatar").attr('data-default-file', v.ImagenUsuario);
        $("#avatar").dropify({
            "defaultFile": src,
            "messages": {
                default: 'Arrastre un archivo o haga clic aquí',
                replace: 'Arrastre un archivo o haga clic en reemplazar',
                remove: 'Eliminar',
                error: 'Lo sentimos, el archivo demasiado grande'
            },
        });

    }

}



// funcion actualiza la imagen
function updateAvatar() {

    var formd = $('#formuserimage')[0];
    var data = new FormData(formd);

    $.ajax({
        type: "POST",
        url: baseURL + "api/Usuarios/PostUploadAvatar",
        data: data,
        processData: false,
        contentType: false,
        beforeSend: function () {
            Swal.fire({
                title: 'Espere..',
                text: 'Enviando Avatar por favor espere, no cierre el dialogo, esto puede tardar unos segundos.',
                animation: false,
                customClass: 'animated tada',
                allowOutsideClick: false,
                onOpen: () => {
                    Swal.showLoading()
                }
            })
        },
        success: function (response) {

            $.each(response, function (i, v) {

                titulo = v.type;
                Swal.fire({
                    title: titulo,
                    text: v.value,
                    type: v.type
                }).then((result) => {
                    location.reload();
                });

            });
        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        console.error(textStatus, errorThrown); // Algo fallo
    })

}
