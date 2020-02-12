/**
 * Created by Alex on 15/06/2019.
 * Si busca el JS que controla la sesiones y el tablero se encuntra en el Layout.js
 */

$(document).ready(function () {
    today = new Date()
    if (today.getMinutes() < 10) {
        pad = "0"
    }
    else
        pad = "";
    //document.write;
    if ((today.getHours() >= 6) && (today.getHours() <= 9)) {
        $("#saludo").text("¡Buenos días!");
    }
    if ((today.getHours() >= 10) && (today.getHours() <= 11)) {
        $("#saludo").text("¡Buenos días!");
    }
    if ((today.getHours() >= 12) && (today.getHours() <= 19)) {
        $("#saludo").text("¡Buenas tardes!");
    }
    if ((today.getHours() >= 20) && (today.getHours() <= 23)) {
        $("#saludo").text("¡Buenas noches!");
    }
    if ((today.getHours() >= 0) && (today.getHours() <= 3)) {
        $("#saludo").text("¡Buenas noches!");
    }
    if ((today.getHours() >= 4) && (today.getHours() <= 5)) {
        $("#saludo").text("¡Buenas noches!");
    }
})


