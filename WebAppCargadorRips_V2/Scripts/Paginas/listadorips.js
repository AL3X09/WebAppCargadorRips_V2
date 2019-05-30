﻿//Archivo encargado unicamente de la lista rips 
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
        "sAjaxSource": baseURL + "api/Rips/GetListadoRips?fktoken="+token, //url of the Ajax source,i.e. web api method
        "sAjaxDataProp": "aaData", // data property of the returned ajax which contains table data
        "bProcessing": true,
        "sPaginationType": "full_numbers",//pagination type
        "aoColumns": [
              
                { "mData": "codigo"},
                { "mData": "tipo_usuario"},
                { "mData": "categoria"},
                {'mData': "periodo_fecha_inicio", 'mRender': function(data, type, full) {
                    return data.substring(0,10);
                  }
                },
                {'mData': "periodo_fecha_fin", 'mRender': function(data, type, full) {
                    return data.substring(0,10);
                  }
                },
                { "mData": "estado_web_validacion"},
                { 
                    "mData": "estado_servicio_validacion", 'mRender': function (data, type, row) {
                        //console.log(row.desc_estado_servicio_validacion);
                        if (row.desc_estado_servicio_validacion == null) {
                            return "Pendiente";
                        } else {
                            return row.desc_estado_servicio_validacion;
                        }
                    }
                },
                { 
                    "mData": "estado_radicacion", 'mRender': function (data, type, row ) {
                        //console.log(row.desc_estado_servicio_validacion);
                        if (row.desc_estado_servicio_validacion == null) {
                            return "Pendiente";
                        } else {
                            return row.desc_estado_radicacion;
                        }
                    }
                },
                {
                    'mData': "fecha_cargo", 'mRender': function (data, type, full) {
                        //console.log(data.substring(0,10))
                        return data.substring(0, 10);
                    }
                },
                
        ],
        "language": {
            'processing': 'Cargando...',
            "sLengthMenu":     "Mostrar _MENU_ registros",
            "sZeroRecords":    "No se encontraron resultados",
            "sEmptyTable":     "Ningún dato disponible en esta tabla",
            "sInfo":           "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty":      "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered":   "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix":    "",
            "sSearch":         "Buscar:",
            "sUrl":            "",
            "sInfoThousands":  ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst":    "Primero",
                "sLast":     "Último",
                "sNext":     "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending":  ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            },
			
          },
          "searching": true,
          "ordering": true,
        "paging": true,
        "scrollX": true,
        "autoWidth": false,
        "lengthMenu": [[10, 25, 50, 100, 500], [10, 25, 50, 100, 500]],
    });
    //
    $("select").val('10');
    //$('select').addClass("browser-default");
    //$('select').material_select();

    /*$('#listaRips tbody').on('click', 'tr', function () {
        $(this).toggleClass('table-success');
        console.log(table);
        var data = table[0].rows($(this).parents('tr')).data();
        alert(data[0] + "'s salary is: " + data[5]);
    });*/
    $('#listaRips').on('click', 'tr', function (e) {
        $(this).toggleClass('table-success');
        e.preventDefault();
        console.log(e);
        var row = $(this).closest('tr'),
            data = oTable._(row),
            id = data[0].id;
        
        //do something with your id
    });
        
}