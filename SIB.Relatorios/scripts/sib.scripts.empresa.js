$(function (e) {
    mudaEstado();
});

function mudaEstado() {
    $("#cD_cmbEstado").change(function (e) {
        e.preventDefault();
        var id = $("#cD_cmbEstado").val();

        $.ajax({
            type: "POST",
            url: "cadTransportadora.aspx/populaCidade",
            data: "{idUf : " + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("#cD_cmbCidade").empty();

                //$(data.d.Data).each(function (e) {
                //    idCid = this.id_cidade;
                //    desc = this.desc_cidade;
                //    $("#cD_cmbCidade").append('<option value="' + idCid + '" selected="selected">' + desc + '</option>');
                //});
                $("#cD_cmbCidade").append(data.d);
            },
            error: function (e) {
                alert("Não foi possivel acessar os dados.");
            }
        });
    });
}