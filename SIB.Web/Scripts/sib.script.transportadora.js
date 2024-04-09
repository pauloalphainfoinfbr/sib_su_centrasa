$(function () {
    buscaCliente();
});

function buscaCliente() {
    $("#buscaCliente").click(function (e) {
        e.preventDefault();                
            
        $("#cliente").empty();

        var postData = { nomeCliente: $("#nomeCliente").val() };

        $.ajax({
            url: "buscaCliente",
            data: postData,
            success: function (data) {
                $(data).each(function () {                        
                    $("#cliente").append('<option value="' + this.id_empresa + '">' + this.nome_fantasia + '</option>');
                })                                                            
            },
            error: function (e) {
                //alert("Um erro ocorreu ao requisitar os dados para popular a lista!");
            },
            dataType: "json",
            traditional: true,
        })
        
    });
}