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

})