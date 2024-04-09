
$(function () {
    alteraEstado();
    addItem("cidade", "lista", "PopulaCidades", "AdcCidade", "add");
    removeItem("cidade","lista","PopulaCidades", "remover");
    selecionaTodosLista();
    buscaCidade();
});

function alteraEstado() {    
    $("#UF").change(function (e) {        
        populaCombo("cidade", "lista", "PopulaCidades");
        $("#lista option").removeAttr("selected");

        $("#nomeCidade").val("");
    });
}

function addItem(comboPai, comboFilho, urlAction, urlDataSource, botaoAdd) {
    $("#" + botaoAdd).unbind("click").click(function (e) {
        e.preventDefault();
        
        if ($("#" + comboPai + " :selected").length > 0) {

            $("#nomeCidade").val("");

            Arr = new Array();
            i = 0;

            $("#" + comboPai + " :selected").each(function (e) {
                Arr[i] = $(this).val();
                i++;
            })
            
            var postData = { idItems: Arr, idUf: $("#UF :selected").val() };
            
            $.ajax({
                url: urlDataSource,
                data: postData,
                success: function (data) {
                    $(data).each(function () {
                        id = this.ID;
                        descricao = this.Descricao;
                        $("#" + comboFilho).append('<option value="' + id + '" selected="selected">' + descricao + '</option>');
                    })
                    populaCombo(comboPai, comboFilho, urlAction);
                    //Se um dia der problema, eu apenas descomento essa linhas
                    $("#" + comboFilho + " option").removeAttr("selected");
                    e.stopImmediatePropagation();

                },
                error: function (e) {
                    //alert("Um erro ocorreu ao requisitar os dados para popular a lista!");
                },
                dataType: "json",
                traditional: true,
            })            
        }        
    });
}


function removeItem(comboPai, comboFilho, urlAction, botaoRemove) {    
    $("#" + botaoRemove).unbind("click").click(function (e) {
        
        $("#" + comboFilho + " :selected").remove();
        
        if ($("#UF :selected").val() != "") {
            populaCombo(comboPai, comboFilho, urlAction);
        }

        $("#" + comboFilho + " option").removeAttr("selected");
        e.stopImmediatePropagation();        
    });
}

function selecionaTodosLista() {
    $("#Proximo").click(function (e) {
        $('#lista option').attr("selected", "selected");
    });

    $("#Voltar").click(function (e) {
        $('#lista option').attr("selected", "selected");
    });

    $("form").submit(function (e) {                
        $('#lista option').attr("selected", "selected");
    })
}

function populaCombo(comboPai, comboFilho, urlAction) {
    $("#" + comboPai).empty();
    $("#" + comboFilho + " option").attr("selected", "selected");

    codUf = $("#UF option:selected").val();
    Arr = new Array();
    var i = 0;

    $("#" + comboFilho + " option:selected").each(function () {
        Arr[i] = $(this).val();
        i++;
    })

    var postData = { idItensSelecionados: Arr, idUf: codUf };

    $.ajax({
        type: "POST",
        url: urlAction,
        data: postData,
        success: function (data) {
            $("#" + comboPai).empty();
            $(data).each(function () {
                $("#" + comboPai).append('<option value="' + this.ID + '">' + this.Descricao + '</option>');
            })
        },
        error: function (e) {
            //alert("Um erro ocorreu ao popular o Combo!");
        },
        dataType: "json",
        traditional: true,
    });
}


function buscaCidade() {
    $("#buscaCidade").click(function (e) {
        e.preventDefault();

        if ($("#UF option:selected").val() == "") {
            alert("Informe o Estado para efetuar a busca.");
        } else {
            $("#cidade").empty();

            var postData = { nomeCidade: $("#nomeCidade").val(), id_estado: $("#UF option:selected").val() };

            $.ajax({
                url: "buscaCidade",
                data: postData,
                success: function (data) {
                    $(data).each(function () {
                        $("#cidade").append('<option value="' + this.id_cidade + '">' + this.desc_cidade + '</option>');
                    })
                },
                error: function (e) {
                    //alert("Um erro ocorreu ao requisitar os dados para popular a lista!");
                },
                dataType: "json",
                traditional: true,
            })
        }
    });
}
