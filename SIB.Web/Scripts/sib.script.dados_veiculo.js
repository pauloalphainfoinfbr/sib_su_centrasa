$(function () {    
    buscaCondutor();
});

function buscaCondutor() {    
    $("#cpfCondutor").focusout(function (e) {

        cpfCondutor = $("#cpfCondutor").val();
        var postData = { numCPF: cpfCondutor }
        
        $.ajax({
            type: "POST",
            url: "BuscaCondutor",
            data: postData,
            success: function (data) {                                                    

                if (data != "") {
                    $("#nomeCondutor").val(data);                    
                }                
            },
            error: function (e) {
                
            },
            dataType: "json",
            traditional: true,
        });
    })
}