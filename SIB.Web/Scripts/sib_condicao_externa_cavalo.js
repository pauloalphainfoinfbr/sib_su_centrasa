$(function () {
    $("#latariaPintura").attr("readonly", "readonly");
    $("#limpezaAspecVisual").attr("readonly", "readonly");
    $("#espelhosRetrovisores").attr("readonly", "readonly");
    $("#vidros").attr("readonly", "readonly");
    $("#parachoqueDanificado").attr("readonly", "readonly");

    $("#latariaPintura").click(function (e) {
        return false;
    });

    $("#limpezaAspecVisual").click(function (e) {
        return false;
    });

    $("#espelhosRetrovisores").click(function (e) {
        return false;
    });

    $("#vidros").click(function (e) {
        return false;
    });

    $("#parachoqueDanificado").click(function (e) {
        return false;
    });

    $("#pontoLatariaPintura").focusout(function (e) {
        if ($(this).val() == "") {
            alert('A pontuação do campo "Lataria/Pintura" deve ser informada. ');
        } else if (!$.isNumeric($(this).val())) {
            alert('A pontuação do campo "Lataria/Pintura" deve ser numérica. ');
            $(this).val("");
                        
        } else if (parseInt($(this).val()) > 20) {
            alert('O valor da pontuação campo "Lataria/Pintura" deve ser no máximo 20. ');
            $(this).val("");
            $(this).focus();
        } else {
            preencherPontuacaoMenor();

            if (parseInt($(this).val()) > 10) {
                $("#latariaPintura").removeAttr("checked");
                $("#latariaPintura").removeAttr("checked");
            } else {
                $("#latariaPintura").attr("checked", true);
            }
        }
    });

    $("#pontoLimpezaAspecVisual").focusout(function (e) {
        if ($(this).val() == "") {
            alert('A pontuação do campo "Limpeza/Aspecto Visual" deve ser informada. ');
        } else if (!$.isNumeric($(this).val())) {
            alert('A pontuação do campo "Limpeza/Aspecto Visual" deve ser numérica. ');
            $(this).val("");
            $(this).focus();
            
        } else if (parseInt($(this).val()) > 20) {
            alert('O valor da pontuação campo "Limpeza/Aspecto Visual" deve ser no máximo 20. ');
            $(this).val("");
            $(this).focus();
        } else {
            preencherPontuacaoMenor();

            if (parseInt($(this).val()) > 10) {
                $("#limpezaAspecVisual").removeAttr("checked");
            } else {
                $("#limpezaAspecVisual").attr("checked", true);
            }
        }
    });

    $("#pontoEspelhosRetrovisores").focusout(function (e) {
        if ($(this).val() == "") {
            alert('A pontuação do campo "Espelhos Retrovisores" deve ser informada. ');
        } else if (!$.isNumeric($(this).val())) {
            alert('A pontuação do campo "Espelhos Retrovisores" deve ser numérica. ');
            $(this).val("");
            $(this).focus();
            
        } else if (parseInt($("#pontoEspelhosRetrovisores").val()) > 20) {
            alert('O valor da pontuação campo "Espelhos Retrovisores" deve ser no máximo 20. ');
            $(this).val("");
            $(this).focus();
        } else {
            preencherPontuacaoMenor();

            if (parseInt($(this).val()) > 10) {
                $("#espelhosRetrovisores").removeAttr("checked");
            } else {
                $("#espelhosRetrovisores").attr("checked", true);
            }
        }
    });

    $("#pontoVidros").focusout(function (e) {
        if ($(this).val() == "") {
            alert('A pontuação do campo "Vidros" deve ser informada. ');
        } else if (!$.isNumeric($(this).val())) {
            alert('A pontuação do campo "Vidros" deve ser numérica. ');
            $("#pontoVidros").val("");
            
        } else if (parseInt($(this).val()) > 20) {
            alert('O valor da pontuação campo "Vidros" deve ser no máximo 20. ');
            $(this).val("");
            $(this).focus();
        } else {
            preencherPontuacaoMenor();

            if (parseInt($(this).val()) > 10) {
                $("#vidros").removeAttr("checked");
            } else {
                $("#vidros").attr("checked", true);
            }
        }
    });

    $("#pontoParachoqueDanificado").focusout(function (e) {
        if ($(this).val() == "") {
            alert('A pontuação do campo "Para-choque Danificado" deve ser informada. ');
        } else if (!$.isNumeric($(this).val())) {
            alert('A pontuação do campo "Para-choque Danificado" deve ser numérica. ');
            $(this).val("");

        } else if (parseInt($(this).val()) > 20) {
            alert('O valor da pontuação campo "Para-choque Danificado" deve ser no máximo 20. ');
            $(this).val("");
            $(this).focus();
        } else {
            preencherPontuacaoMenor();

            if (parseInt($(this).val()) > 10) {
                $("#parachoqueDanificado").removeAttr("checked");
            } else {
                $("#parachoqueDanificado").attr("checked", true);
            }
        }
    });

});

function preencherPontuacaoMenor() {

    pontoLatariaPintura = $("#pontoLatariaPintura").val();
    pontoLimpezaAspecVisual = $("#pontoLimpezaAspecVisual").val();
    pontoEspelhosRetrovisores = $("#pontoEspelhosRetrovisores").val();
    pontoVidros = $("#pontoVidros").val();
    pontoParachoqueDanificado = $("#pontoParachoqueDanificado").val();

    var postData = {        
        latariaPintura: pontoLatariaPintura,
        limpezaAspecVisual: pontoLimpezaAspecVisual,
        espelhosRetrovisores: pontoEspelhosRetrovisores,
        vidros: pontoVidros,
        parachoqueDanificado: pontoParachoqueDanificado
    }

    $.ajax({
        type: "POST",
        url: "CalculaPontuacaoMenor",
        data: postData,
        success: function (data) {            
            if (data != "") {
                var dados = $.parseJSON(data);
                $("#total").val(dados.total);

                if (parseInt(dados.pontuacaoMenor) >= 15) {
                    $("#status").val("Aprovado");
                } else if (parseInt(dados.pontuacaoMenor) > 10) {
                    $("#status").val("Aprovado com Restrição");
                } else {
                    $("#status").val("Reprovado");
                }
            }
        },
        error: function (e) {

        },
        dataType: "json",
        traditional: true,
    });
}
