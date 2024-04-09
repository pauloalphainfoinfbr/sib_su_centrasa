$(function () {
    $("#medFumacaNegra").change(function (e) {

        if ($(this).val() == "0") {
            $("#climaFumacaNegra").removeAttr("disabled");
            $("#climaFumacaNegra").empty();

            $("#climaFumacaNegra")
                .append($("<option></option>")
                .attr("value", "0")                
                .text('Selecione uma Opção'));
            $("#climaFumacaNegra").attr("selected", "selected");

            $("#climaFumacaNegra")
                .append($("<option></option>")
                .attr("value", "Nublado")
                .text('Nublado'));

            $("#climaFumacaNegra")
                .append($("<option></option>")
                .attr("value", "Noite")
                .text('Noite'));

            $("#climaFumacaNegra")
                .append($("<option></option>")
                .attr("value", "Chuvoso")
                .text('Chuvoso'));
            
        } else {
            $("#climaFumacaNegra").prop("disabled", "disabled");
        }

    });
});