/**
 * @author ALEXANDER CIFUENTES SANCHEZ
 * @description este archivo se encarga del cotrol del campo de archivos
 * 22-04-2019
 * El archivo lector de los archivos planos es validaCargaArchivo.js
 */

//Inicioalizo el campo del multifile
function inicilizoMultifile() {
    $('#rips').MultiFile({
        accept: 'txt',
        onFileSelect: function (element, value, master_element) {
            $('#rips').MultiFile("reset");
        },
        afterFileSelect: function (element, value, master_element) {

        },
        onFileRemove: function (element, value, master_element) {

        },
        onFileDuplicate: function (element, value, master_element) {
            $('#F9-Log').append('<li>onFileDuplicate - ' + value + '</li>')
        },
        onFileTooMany: function (element, value, master_element) {
            $('#F9-Log').append('<li>onFileTooMany - ' + value + '</li>')
        },
        onFileTooBig: function (element, value, master_element) {
            $('#F9-Log').append('<li>onFileTooBig - ' + value + '</li>')
        }
    });
}

function zonaarchvos() {

    var dropZoneId = "drop-zone";
    var buttonId = "clickHere";
    var mouseOverClass = "mouse-over";
    var dropZone = $("#" + dropZoneId);
    var inputFile = dropZone.find("input");
    var finalFiles = [];

    var ooleft = dropZone.offset().left;
    var ooright = dropZone.outerWidth() + ooleft;
    var ootop = dropZone.offset().top;
    var oobottom = dropZone.outerHeight() + ootop;

    document.getElementById(dropZoneId).addEventListener("dragover", function (e) {
        e.preventDefault();
        e.stopPropagation();
        //dropZone.addClass(mouseOverClass);
        var x = e.pageX;
        var y = e.pageY;

        if (!(x < ooleft || x > ooright || y < ootop || y > oobottom)) {
            inputFile.offset({
                top: y - 15,
                left: x - 100
            });
        } else {
            inputFile.offset({
                top: -400,
                left: -400
            });
        }

    }, true);

    if (buttonId != "") {
        var clickZone = $("#" + buttonId);

        var oleft = clickZone.offset().left;
        var oright = clickZone.outerWidth() + oleft;
        var otop = clickZone.offset().top;
        var obottom = clickZone.outerHeight() + otop;

        //aqui se mueve el imput segun posision del mouse
        $("#" + buttonId).mousemove(function (e) {
            var x = e.pageX;
            var y = e.pageY;
            inputFile.offset({
                top: y - 15,
                left: x - 160
            });
            /*if (!(x < oleft || x > oright || y < otop || y > obottom)) {
                inputFile.offset({
                    top: y - 15,
                    left: x - 160
                });
            } else {
                inputFile.offset({
                    top: -400,
                    left: -400
                });
            }*/
        });
    }

    document.getElementById(dropZoneId).addEventListener("drop", function (e) {
        $("#" + dropZoneId).removeClass(mouseOverClass);
    }, true);


    inputFile.on('change', function (e) {
        //finalFiles = [];

        $('#filename').html("");

        var fileNum = this.files.length,
            initial = 0,
            counter = 0;

        $.each(this.files, function (idx, elm) {
            finalFiles[idx] = elm;

        });

        //Si existen archivos nuevos incio lectura
        if (fileNum > 0) {
            readFile();
        }

        for (initial; initial < fileNum; initial++) {
            counter = counter + 1;
            $('#filename').append('<div id="file_' + initial + '"><span class="fa-stack fa-lg"><i class="fa fa-file fa-stack-1x "></i><strong class="fa-stack-1x" style="color:#FFF; font-size:12px; margin-top:2px;">' + counter + '</strong></span> ' + this.files[initial].name + '</div>');
        }
    });

};