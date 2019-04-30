/**
 * @author ALEXANDER CIFUENTES SANCHEZ
 * @description este archivo se encarga de validar los adjuntos para que el servidor no se cuelgue
 * en esta operación
 * 26-10-2017
 */

//Variable mantiene el modal desplegado
var modalButtonOnly = new tingle.modal({
    closeMethods: [],
    footer: true,
    stickyFooter: true
});
//variable mantiene los errores
var erroresEstructuraCampo = []; //new Array(100);
var erroresCaracteresEspeciales = []; //new Array(100);
//variable mantiene la posicion de lectura
var poslec = 0;
//Variable para buscar caracteres especiales en los archivos
var buscar;

jQuery(function () {
    
    $('#formulariocargaarchivo').validate({
            onsubmit: true,
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
        },
        submitHandler: function (e) {
            
            readFile();
        }
           
    });
});

function modalprogres() {

    modalButtonOnly.open();
    modalButtonOnly.setContent(document.querySelector('.tingle_addon_window').innerHTML);

}

function readFile() {
    //console.log("leiendoXD");
    let bandera = true;//variable buleana bandera que me permitira controlar las validaciones
    var fileInput = document.getElementById('rips');
    var fileDisplayArea = document.getElementById('fileDisplayArea');
    var cantidad = fileInput.childNodes[0].files.length;

    //almaceno el nombre de los archivos en un array para su posterior validacion, ya que no se pueden enviar repetidos


    for (let i = 0; i < cantidad; i++) {
        nombre.push((fileInput.childNodes[0].files[i]['name']).substring(2, 0));
    }

    //Valido si  los archivos de USUARIOS Y FACTURACIÓN se encuentran para ser cargados
    //ya que son obligatorios
    if (nombre.includes('US') == true && nombre.includes('AF') == true) {

        //Valido que el usuario no seleccione archivos iguales
        //aplico un ordenamiento burbuja para validar que no existan archivos repetidos
        for (let i = 0; i < cantidad - 1; i++) {
            for (let j = i + 1; j < cantidad; j++) {

                if (nombre[i] == nombre[j]) {
                    //Cambio el balor de la variable de validación
                    bandera = false;
                }
            }
        }

        //valido que la variable de validacion no cambio
        if (bandera === true) {
            modalprogres();
            // si la variable bandera no cambio envio lectura de archivos
            for (let i = 0; i < cantidad; i++) {

                var file = fileInput.childNodes[0].files[i];
                var textType = /text.*/;
                //var buscarpuntos=null;
                var namefile = null;
                if (file.type.match(textType)) { //si los archivos no son de formato txt no los permite leer
                    var reader = new FileReader();

                    reader.onload = function (e) {

                        namefile = fileInput.childNodes[0].files[i]['name'];

                        // Por lineas
                        var lines = this.result.split('\n');

                        //envio a una función las lienas del archivo a subir
                        readlines(lines, namefile, cantidad);

                    }
                    reader.readAsText(file);
                    //delete reader;
                } else {
                    swal.fire(
                        'Precaución',
                        'Parece que intenta cargar archivos no legibles, por favor elimine e intente nuevamente',
                        'info'
                    )
                    //fileDisplayArea.innerText = "Archivos No Soportados!"
                    nombre.length = 0; //Limpio el vector de nombres
                }
            }//fin for
            //cuando termina de leer todos los archivos llamo funcion para que realice las operaciones siguentes
            //terminaLectura();
        } else {
            //si cambio la variable bandera
            //evito la carga innesaria de los archivos
            //le indico al usuario que por favor revice la info a cargar
            swal.fire(
                'Precaución',
                'Parece que intenta cargar el mismo tipo de archivo, por favor elimine e intente nuevamente',
                'info'
            )
            nombre.length = 0;
            //limpiarCampos();
        }


    } else { //de lo contrario envio alerta para obligar cargar los archivos
        swal.fire(
            'Precaución',
            'No se encuentran los archivos de USUARIOS (US) y FACTURACIÓN(AF), por favor elimine los archivos e intente cargar nuevamente.',
            'info'
        )
        nombre.length = 0; //Limpio el vector de nombres

    } //fin else archivos obligatorios 



}


function readlines(lineas, namefile, cantidad) {
    var nombrecorto = namefile.substring(2, 0).toUpperCase();  

    //valido que el nombre del archivo sea el permitido de las estructuras definidas en la norma
    if (TipoEstructuraArray[nombrecorto] === undefined && nombrecorto !== 'CT') {
        modalButtonOnly.close();
        //limpio variable de posicion de lectura
        poslec = 0;
        swal.fire(
            'Error!',
            'Esta intentando cargar archivos con un nombre no permitido, por favor corrijalos!',
            'error'
        );
    }else {

        /*
         # valido si hay caracteres especiales
         # y el archivo es diferente a la estrutura AM envio errores
         */
        if (nombrecorto !== 'CT' && nombrecorto !== 'AM' && nombrecorto !== 'AT' && nombrecorto !== 'AP' && nombrecorto !== 'AU') {
            buscar = new RegExp(/[~`!#$%;\^&*+=\[\]\\'{}|\\"<>\?]/); //buscar caracteres especiales
        } else if (nombrecorto == 'AM') //validación para Medicamentos
        {
            buscar = new RegExp(/[~`!#$;\^&\[\]\\'{}|\\"<>\?]/); //buscar caracteres especiales            
        }
        /*else if (nombrecorto == 'AT') //validación para Otros Servicios
        {
            buscar = new RegExp(/[~`!$;\^&\[\]\\'{}|\\"<>\?]/); //buscar caracteres especiales            
        } */
        else if (nombrecorto == 'AP') //validación para Procedimientos
        {
            buscar = new RegExp(/[~`!#$%;\^&*\[\]\\'{}|\\"<>\?]/); //buscar caracteres especiales            
        }


        //valido liena por linea que no tenga caracteres especiales
        $.each(lineas, function (i, v) {

            var res = buscar.test(v);
            textoAreaDividido = v.split(",");
            numeroColumas = textoAreaDividido.length;
           
            /**
             * SE TIENE EN CUENTA QUE EL ARCHIVO CT NO LO ENVIAN MUCHOS PRESTADORES POR LO TANTO SE EXCLUYE DE LA
             * POSTERIRO VALIDACIÓN
             * 28/12/2017
             * EL VALIDADOR EXTRAE SU PROPIA VALIDACIÓN
             */
            //valido la cantidad de campos permitidos para cada estructura
            if (numeroColumas > 1) {
                if (nombrecorto !== 'CT' && TipoEstructuraArray[nombrecorto] !== numeroColumas && TipoEstructuraArray[nombrecorto] > 0) {
                    erroresEstructuraCampo.push("ERROR; El archivo " + namefile + " en la linea " + (i + 1) + " tiene una longitud de " + numeroColumas + ", la longitud permitida es de " + TipoEstructuraArray[nombrecorto]);
                }
            }
            //fin valido la cantidad de campos permitidos para cada estructura

            /*
                 # valido si hay caracteres especiales
                 # y el archivo es diferente a la estrutura AM envio errores
            */
            if (res == true && nombrecorto !== 'CT' && nombrecorto !== 'AT') {
                erroresCaracteresEspeciales.push("error" + (i + 1) + " El Archivo " + namefile + " contiene valores no permitidos en la linea " + (i + 1));
            }

        });

        //console.log(erroresEstructuraCampo);

        poslec = poslec + 1; //sumo la posición de lectura

        //valido que se hayan terminado de leer todos los archivos que adjunto el usuario
        if (poslec === cantidad) {
            //cierro el modal 
            modalButtonOnly.close();
            //Llamo funcion de terminacion de lectura
            terminaLectura();
            //limpio variable de posicion de lectura
            poslec = 0;
        }

       
       
    }

    
}

function terminaLectura() {
    //valido que si se presentan errores en la validacion

    if (erroresEstructuraCampo.length > 0) {
        //envio notificación del error
        swal.fire({
            title: 'Error',
            text: 'Sus archivos presentan errores de estructura!! se enviara un correo con los diferentes ' +
                'errores encontrados, por favor corrijalos e intente nuevamente.',
            type: 'warning',
            showCancelButton: false,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'OK!',
            allowOutsideClick: false
        }).then((result) => {
            //envio a la api de errores
            UploadValidacionConErrores(erroresEstructuraCampo.slice(0, 100));
        })

    } else if (erroresCaracteresEspeciales.length > 0) {
        //envio notificación del error
        swal.fire({
            title: 'Error',
            text: 'Sus archivos presentan caracteres especiales no permitidos, desea continuar con el cargue, o desea recibir un correo con los diferentes ' +
                  'caracteres encontrados.',
            type: 'warning',
            showCancelButton: true,
            cancelButtonText: 'Enviar correo',
            cancelButtonColor: '#d33',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Continuar',
            allowOutsideClick: false
        }).then((result) => {
            if (result.value) {
                //llamo funcio de carga de los archivos RIPS
                loadRIPS();
            } else {
                //envio a la api de errores
                enviarCorreoErrores(erroresCaracteresEspeciales.slice(0, 100));
               
            }
        })
    }
    else {
        //llamo funcio de carga de los archivos RIPS
        loadRIPS();
    }
}


