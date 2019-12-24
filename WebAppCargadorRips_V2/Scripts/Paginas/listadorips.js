//Archivo encargado unicamente de la lista rips 
$(document).ready(function () {
    //$("#mlistado").addClass("active");
    //$("#listaRips").removeClass("active");
    //$("#listaRips").DataTable();  
    //documentación de la biblioteca http://legacy.datatables.net/styling/custom_classes 

    //$.fn.dataTable.ext.classes.sPageButtonActive = 'active'; // Change Pagination Button Class
    //$.fn.dataTable.ext.classes.sPageButtonActive = 'waves-light btn small'; // Change Pagination Button Class
    //$.fn.dataTable.ext.classes.sPageButtonStaticDisabled = 'btn small disabled'; // Change Pagination Button Class
    cargaratabla(token);
})

//https://qawithexperts.com/article/asp.net/jquery-datatable-server-side-processing-with-web-api-in-aspn/72


function cargaratabla(token) {

    var table = $("#listaRips").dataTable({
        "bServerSide": true, //make server side processing to true
        "sAjaxSource": baseURL + "api/Rips/GetListadoRips?fktoken=" + token, //url of the Ajax source,i.e. web api method
        "sAjaxDataProp": "aaData", // data property of the returned ajax which contains table data
        "bProcessing": true,
        "sPaginationType": "full_numbers",//pagination type
        "aoColumns": [

            {
                'mData': "#", 'mRender': function (data, type, row) {

                    if (row.estado_id === 5) {
                        return '<img src="' + baseURL +'Img/tboton/info.png" alt="Información" height="42" width="42">';
                    } if (row.estado_id === 10) {
                        return '<img src="' + baseURL +'Img/tboton/success.png" alt="Generar PDF" height="42" width="42">';
                    } else {
                        return '<img src="' + baseURL +'Img/tboton/error.png" alt="Errores" height="42" width="42">';                        
                    }

                }
            },
            { "mData": "Preradicado" },
            { "mData": "Tipo_Usuario" },
            { "mData": "Categoria" },
            {
                'mData': "Extranjero", 'mRender': function (data, type, row) {

                    if (row.Extranjero === true) {
                        return '<input type="checkbox" checked="checked" disabled>';
                    } else {
                        return '<input type="checkbox" disabled>';
                    }

                }
            },
            {
                'mData': "Periodo_inicio", 'mRender': function (data, type, full) {
                    return data.substring(0, 10);
                }
            },
            {
                'mData': "Periodo_Fin", 'mRender': function (data, type, full) {
                    return data.substring(0, 10);
                }
            },
            {
                'mData': "Fecha_Carga", 'mRender': function (data, type, full) {
                    //console.log(data.substring(0,10))
                    return data.substring(0, 10);
                }
            },
            { "mData": "Estado" },
            { "mData": "Radicado" },
            {
                'mData': "Acciones", 'mRender': function (data, type, row) {

                    if (row.FK_estado_preradicacion === 17) {
                        return '<a href="' + baseURL + 'api/ActasGeneradas/Rechazdo?preradicado=' + row.Preradicado + '" target="_blank"><img src="../Img/tboton/apdf.png" alt="Generar PDF" height="42" width="42"></a>';
                        //return 'Pendiente';
                    } else if (row.Radicado === null) {
                        return 'Pendiente';
                    } 
                    else {
                      return '<a href="#" class="btn btn-secondary btn-sm" role="button" id="' + row.codigo + ' onclick="getErrores(' + row.codigo + ')" "><img src="../Img/tboton/apdf.png" alt="Generar PDF" height="42" width="42"></a>';
                    //return 'Pendiente';
                    }
                    
                }
            },
            
        ],
        "language": {
            'processing': 'Cargando...',
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            },

        },
        "searching": true,
        "ordering": true,
        "paging": true,
        "scrollX": true,
        "autoWidth": false,
        "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
    });

}