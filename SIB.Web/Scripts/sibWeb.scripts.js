$(document).ready(function () {
    $("#divInputBrasil").hide();
    $("#divInputMercosul").hide();
});

var controleTipoPlaca = function () {    
    if ($("input[name=tipoPlaca]:checked").val() === "brasil") {
        $("#divInputBrasil").show();
        $("#divInputMercosul").hide();
    } else {
        $("#divInputBrasil").hide();
        $("#divInputMercosul").show();
    }
}