$(function () {
    $("#liberar").click(function (e) {
        
        //e.preventDefault();        
        debugger;
        
        var url = "http://localhost:51733/VeiculoLiberado/liberarVeiculo";        

        $.ajax({
            url: url,
            data: { url: url },
            type: "POST",
            success: function () {                
            //e.stopImmediatePropagation();
                $("#liberar").attr("disabled", "disabled");
                $("#liberar").val("Veículo liberado");

                $("#status").text("Sim");
            },
            error: function (e) {
                //alert("Um erro ocorreu ao requisitar os dados para popular a lista!");
            }            
        })                

    });
});