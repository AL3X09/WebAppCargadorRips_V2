/**
 * Created by Alex on 11/03/2017.
 * https://www.it-swarm.net/es/asp.net/asp.net-mvc-establecer-identidad-personalizada-o-iprincipal/967225311/
 */
const getUrl = window.location;
var token;
const baseURL = getUrl.protocol + "//" + getUrl.host + "/"; // lineas servidor local
//const baseURL = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1] + "/"; // lineas servidor publicación

$(document).ready(function () {
    
    // Initialize menu
    $().ready(function () {
        $sidebar = $('.sidebar');

        $sidebar_img_container = $sidebar.find('.sidebar-background');

        $full_page = $('.full-page');

        $sidebar_responsive = $('body > .navbar-collapse');

        window_width = $(window).width();

        fixed_plugin_open = $('.sidebar .sidebar-wrapper .nav li.active a p').html();

        if (window_width > 767 && fixed_plugin_open == 'Dashboard') {
            if ($('.fixed-plugin .dropdown').hasClass('show-dropdown')) {
                $('.fixed-plugin .dropdown').addClass('open');
            }

        }

        $('.fixed-plugin a').click(function (event) {
            // Alex if we click on switch, stop propagation of the event, so the dropdown will not be hide, otherwise we set the  section active
            if ($(this).hasClass('switch-trigger')) {
                if (event.stopPropagation) {
                    event.stopPropagation();
                } else if (window.event) {
                    window.event.cancelBubble = true;
                }
            }
        });

        $('.fixed-plugin .active-color span').click(function () {
            $full_page_background = $('.full-page-background');

            $(this).siblings().removeClass('active');
            $(this).addClass('active');

            var new_color = $(this).data('color');

            if ($sidebar.length != 0) {
                $sidebar.attr('data-color', new_color);
            }

            if ($full_page.length != 0) {
                $full_page.attr('filter-color', new_color);
            }

            if ($sidebar_responsive.length != 0) {
                $sidebar_responsive.attr('data-color', new_color);
            }
        });

        $('.fixed-plugin .background-color .badge').click(function () {
            $(this).siblings().removeClass('active');
            $(this).addClass('active');

            var new_color = $(this).data('background-color');

            if ($sidebar.length != 0) {
                $sidebar.attr('data-background-color', new_color);
            }
        });

        $('.fixed-plugin .img-holder').click(function () {
            $full_page_background = $('.full-page-background');

            $(this).parent('li').siblings().removeClass('active');
            $(this).parent('li').addClass('active');


            var new_image = $(this).find("img").attr('src');

            if ($sidebar_img_container.length != 0 && $('.switch-sidebar-image input:checked').length != 0) {
                $sidebar_img_container.fadeOut('fast', function () {
                    $sidebar_img_container.css('background-image', 'url("' + new_image + '")');
                    $sidebar_img_container.fadeIn('fast');
                });
            }

            if ($full_page_background.length != 0 && $('.switch-sidebar-image input:checked').length != 0) {
                var new_image_full_page = $('.fixed-plugin li.active .img-holder').find('img').data('src');

                $full_page_background.fadeOut('fast', function () {
                    $full_page_background.css('background-image', 'url("' + new_image_full_page + '")');
                    $full_page_background.fadeIn('fast');
                });
            }

            if ($('.switch-sidebar-image input:checked').length == 0) {
                var new_image = $('.fixed-plugin li.active .img-holder').find("img").attr('src');
                var new_image_full_page = $('.fixed-plugin li.active .img-holder').find('img').data('src');

                $sidebar_img_container.css('background-image', 'url("' + new_image + '")');
                $full_page_background.css('background-image', 'url("' + new_image_full_page + '")');
            }

            if ($sidebar_responsive.length != 0) {
                $sidebar_responsive.css('background-image', 'url("' + new_image + '")');
            }
        });

        $('.switch-sidebar-image input').change(function () {
            $full_page_background = $('.full-page-background');

            $input = $(this);

            if ($input.is(':checked')) {
                if ($sidebar_img_container.length != 0) {
                    $sidebar_img_container.fadeIn('fast');
                    $sidebar.attr('data-image', '#');
                }

                if ($full_page_background.length != 0) {
                    $full_page_background.fadeIn('fast');
                    $full_page.attr('data-image', '#');
                }

                background_image = true;
            } else {
                if ($sidebar_img_container.length != 0) {
                    $sidebar.removeAttr('data-image');
                    $sidebar_img_container.fadeOut('fast');
                }

                if ($full_page_background.length != 0) {
                    $full_page.removeAttr('data-image', '#');
                    $full_page_background.fadeOut('fast');
                }

                background_image = false;
            }
        });

        $('.switch-sidebar-mini input').change(function () {
            $body = $('body');

            $input = $(this);

            if (md.misc.sidebar_mini_active == true) {
                $('body').removeClass('sidebar-mini');
                md.misc.sidebar_mini_active = false;

                $('.sidebar .sidebar-wrapper, .main-panel').perfectScrollbar();

            } else {

                $('.sidebar .sidebar-wrapper, .main-panel').perfectScrollbar('destroy');

                setTimeout(function () {
                    $('body').addClass('sidebar-mini');

                    md.misc.sidebar_mini_active = true;
                }, 300);
            }

            // we simulate the window Resize so the charts will get updated in realtime.
            var simulateWindowResize = setInterval(function () {
                window.dispatchEvent(new Event('resize'));
            }, 180);

            // we stop the simulation of Window Resize after the animations are completed
            setTimeout(function () {
                clearInterval(simulateWindowResize);
            }, 1000);

        });
    });

});

function vistaDescargarValidador() {
    window.open('https://docs.google.com/forms/d/e/1FAIpQLSdZOk-bHZ8lhyZ_s5iqbznbGUYgXoYny3kDhqHmkSQHRyYyIw/viewform?embedded=true');
}

function getAllME(token, rol) {
   
    token = token;
    
    $.ajax({
        type: "GET",
        url: baseURL + "api/Web_UsuarioApi/GetWeb_Usuario",
        data: { id: token, rol: rol },
        success: function (response) {
            //console.log(response);
            $.each(response, function (i, v) {
                
                var apellido;
                if (v.apellidos == null) {
                    apellido = "";
                } else {
                    apellido = v.apellidos;
                }
                $("#imguser").attr("src", baseURL + v.imagen);
                $("#imguserperfil").attr("src", baseURL + v.imagen);
                $("#nombreuserspan").html(v.nombres + " " + apellido);
                $("#prestador").html(v.nombres + " " + apellido);
                $("#codigoprest").html(v.codigo);
                $('#emailspan').html(v.correo);
                if (v.id_rol === 1) {
                    $("#tokenacces").append(
                        '<li class="nav-item " id="madmin">' +
                        '<a class="nav-link" href="/Administracion"> ' +
                        '<i class= "material-icons" > dashboard</i> ' +
                        '<p> Administración</p> ' +
                        '</a>' +
                        '</li>');
                }                
                
                if (document.getElementById("imguser") != undefined || document.getElementById("imguser") != null) {
                    $("#imguser").attr("src", baseURL + v.imagen);
                }

                if (document.getElementById("divtabEstado") != undefined || document.getElementById("divtabEstado") != null) {
                    cargaratabla(v.usuario_id);
                }
                
                if (v.codigo === undefined) {
                    $("#btn_perfil").remove();
                    $("#btn_contrase").remove();
                }

                // si el prestador no esta habilitado notifico visualmente
                if (v.habilitado === false) {

                    iziToast.warning({
                        title: 'Alerta',
                        message: 'Usted se encuentra Inhabilitado en REPS!!',
                        position: 'topCenter',
                        timeout: 50000,
                        resetOnHover: true,
                        drag: true,
                        close: true,
                    });
                }
                getControlTablero(v.id_rol);
                //callFechasPeriodos(v.id_rol);
                if (typeof getControlAdministración !== "undefined") {
                    getControlAdministración(v.id_rol);
                }
                //identificar si la función existe
                if (typeof callFechasPeriodos !== "undefined") {
                    callFechasPeriodos(v.id_rol);
                }
               
            });

        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        console.error(textStatus, errorThrown); // Algo fallo
        //window.location.href = baseURL + "Cuenta";
    })

}

// función usada para las alertas
function ShowAlert(tipo, msj) {
    console.log(tipo);
    console.log(msj);
    var toast = "iziToast." + tipo + "({" +
        "timeout: 20000," +
        "title: '" + tipo.toUpperCase() + "'," +
        "message: '" + msj + "'," +
        "position: 'topCenter'," +
        "})";

    eval(toast);

}

//llamo la api y alimento el ul de manuales
function getControlTablero(id_rol) {

    $.ajax({
        type: "GET",
        url: baseURL + "api/PermisosModulos/Tablero",
        data: { rol: id_rol },
        success: function (response) {

            $.each(response, function (i, v) {

                $('#listamandos').append(

                    '<div class="col-lg-3 col-sm-12" id"' + v.nombre + '">' +
                    '<div class="card">' +
                    '<!-- Card image -->' +
                    '<div class="view overlay">' +
                    '<img class="card-img-top" src="' + baseURL + v.imagen + '" alt="' + v.nombre + '" width="50">' +
                    '<a href="#!">' +
                    '<div class="mask rgba-white-slight"></div>' +
                    '</a>' +
                    '</div>' +
                    '<!-- Card content -->' +
                    '<div class="card-body">' +
                    '<!-- Title -->' +
                    '<h4 class="card-title font-weight-bold">' +
                    v.nombre +
                    '</h4>' +
                    '<!-- Text -->' +
                    '<p class="card-text text-justify">' + v.descripcion + '</p>' +
                    '<!-- Button -->' +
                    (v.nombre.search("validador") > 1 ? '<div id="validador">' :  '<div>') +
                    (v.pr_crear == true ? '<a href = "' + (v.nombre.search("validador") > 1 ? v.ruta : baseURL + v.ruta ) + '" class= "btn btn-info" id = "btnTacceso">Acceder</a >' : '') +
                    '</div>' +
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

//llamo la api para descargar el acta de errores segun preradicado
function getErrores(prerra) {

    $.ajax({
        type: "GET",
        url: baseURL + "api/ActasGeneradas/Rechazdo",
        data: { preradicado: prerra },
        success: function (response) {

            console.log(response);
                
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