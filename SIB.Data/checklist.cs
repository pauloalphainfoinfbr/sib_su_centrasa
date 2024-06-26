//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIB.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class checklist
    {
        public checklist()
        {
            this.checklist_fase = new HashSet<checklist_fase>();
        }
    
        public int id_checklist { get; set; }
        public Nullable<int> id_tipo_frete { get; set; }
        public Nullable<int> id_tipo_transporte { get; set; }
        public string placa { get; set; }
        public string cpf_condutor { get; set; }
        public Nullable<int> id_emp_transportadora { get; set; }
        public Nullable<System.DateTime> data_checklist { get; set; }
        public Nullable<System.DateTime> data_fim_checklist { get; set; }
        public Nullable<decimal> tonelada { get; set; }
        public Nullable<int> id_usuario { get; set; }
        public Nullable<int> id_tipo_veiculo { get; set; }
        public Nullable<int> id_tipo_carroceria { get; set; }
        public Nullable<int> id_tipo_assoalho { get; set; }
        public Nullable<bool> recuperado { get; set; }
        public string chk_placa2 { get; set; }
        public string chk_placa3 { get; set; }
        public Nullable<int> chk_numeroEntregas { get; set; }
        public Nullable<int> chk_parabrisaTrincado { get; set; }
        public Nullable<int> chk_carroceria { get; set; }
        public Nullable<int> chk_pneusEstepe { get; set; }
        public Nullable<int> chk_luzFreio { get; set; }
        public Nullable<int> chk_setas { get; set; }
        public Nullable<int> chk_estepe { get; set; }
        public Nullable<int> chk_faroisLanterna { get; set; }
        public Nullable<int> chk_assoalho { get; set; }
        public Nullable<int> chk_lona { get; set; }
        public string chk_material { get; set; }
        public Nullable<int> chk_epi { get; set; }
        public string chk_medidorFumacaNegra { get; set; }
        public Nullable<int> chk_veicDedicado { get; set; }
        public Nullable<int> chk_cargaDescarga { get; set; }
        public Nullable<int> chk_usinaDeposito { get; set; }
        public Nullable<int> chk_capaceteCarnJugular { get; set; }
        public Nullable<int> chk_oculosSeguranca { get; set; }
        public Nullable<int> chk_protetorAuditivo { get; set; }
        public Nullable<int> chk_perneira { get; set; }
        public Nullable<int> chk_botina { get; set; }
        public Nullable<int> chk_tacografo { get; set; }
        public Nullable<int> chk_carroceriaLimpa { get; set; }
        public Nullable<int> chk_sinalSonoroRe { get; set; }
        public Nullable<int> chk_necesMontarBerco { get; set; }
        public Nullable<int> chk_possui { get; set; }
        public Nullable<int> chk_metalico { get; set; }
        public Nullable<int> chk_bercoProprio { get; set; }
        public Nullable<int> chk_liberado { get; set; }
        public Nullable<int> chk_bobineira { get; set; }
        public Nullable<int> chk_DT { get; set; }
        public Nullable<int> chk_retornouUsina { get; set; }
        public string chk_tipoMaterial { get; set; }
        public string chk_numLote { get; set; }
        public string chk_cliente { get; set; }
        public string chk_observacao { get; set; }
        public Nullable<int> veiculo_liberado { get; set; }
        public Nullable<int> chk_chassi { get; set; }
        public Nullable<int> chk_extintor { get; set; }
        public Nullable<int> chk_triangMacaco { get; set; }
        public Nullable<System.DateTime> data_inicio_peacao { get; set; }
        public Nullable<int> chk_materialMolhado { get; set; }
        public Nullable<int> chk_materialChovendo { get; set; }
        public Nullable<int> chk_amarracaoCarreta { get; set; }
        public Nullable<int> chk_devidamentePregado { get; set; }
        public Nullable<System.DateTime> data_fim_peacao { get; set; }
        public Nullable<int> chk_cintas { get; set; }
        public Nullable<int> chk_cordasCintasCatracas { get; set; }
        public Nullable<int> chk_alturaVeiculo { get; set; }
        public Nullable<int> chk_situacaoAssoalho { get; set; }
        public Nullable<int> chk_avariaMaterial { get; set; }
        public Nullable<int> chk_tmpGuardaLatTravad { get; set; }
        public Nullable<int> chk_parafusoRodas { get; set; }
        public Nullable<int> chk_trincaTolerancia { get; set; }
        public Nullable<int> chk_dormentesFixados { get; set; }
        public Nullable<int> chk_laudoGanchoAdaptado { get; set; }
        public Nullable<int> chk_todosPinos { get; set; }
        public Nullable<int> chk_dobradicasBoasCondicoes { get; set; }
        public Nullable<int> chk_estruturaTampasFerroMadeira { get; set; }
        public Nullable<int> chk_drenoBobineira { get; set; }
        public Nullable<int> chk_bercoMetalicoBobineira { get; set; }
        public Nullable<int> chk_cargaSecaGraneleiro { get; set; }
        public Nullable<int> chk_catracaoTodaAmarracao { get; set; }
        public Nullable<int> chk_precisouMovimentarTampas { get; set; }
        public Nullable<int> chk_mangote { get; set; }
        public Nullable<int> chk_possuiLaudoOpacidade { get; set; }
        public Nullable<System.DateTime> data_realizacao_laudoOpacidade { get; set; }
        public Nullable<int> chk_retrovisorTrincado { get; set; }
        public Nullable<int> chk_trincaRetrovisorTolerancia { get; set; }
        public Nullable<int> chk_entregaAlavanca { get; set; }
        public Nullable<int> chk_devolucaoAlavanca { get; set; }
        public Nullable<int> chk_bloqueado { get; set; }
        public Nullable<int> chk_tipoAvaria { get; set; }
        public Nullable<int> chk_placaLegivel { get; set; }
        public Nullable<int> chk_estadoGeral { get; set; }
        public Nullable<int> chk_vazamentoOleo { get; set; }
        public Nullable<int> chk_ganchoPatola { get; set; }
        public Nullable<int> chk_tipoLona { get; set; }
        public Nullable<int> chk_qtdeCunha { get; set; }
        public Nullable<int> chk_cintamentoFardos { get; set; }
        public Nullable<int> chk_fotosCondicoesBobineira { get; set; }
        public Nullable<int> chk_drenosDesobstruidos { get; set; }
        public Nullable<int> chk_borrachaBobineiraInterica { get; set; }
        public Nullable<int> chk_borrachaFixaBobineira { get; set; }
        public Nullable<int> chk_condicoesBobineira { get; set; }
        public Nullable<int> chk_travaSegurancaTampas { get; set; }
        public string placa_mercosul { get; set; }
        public Nullable<int> chk_possui_borrachas_assoalho { get; set; }
        public Nullable<int> chk_condicoes_embalagem_material { get; set; }
        public Nullable<int> chk_borrachas_assoalho_carga { get; set; }
    
        public virtual usuario usuario { get; set; }
        public virtual condutor condutor { get; set; }
        public virtual ICollection<checklist_fase> checklist_fase { get; set; }
        public virtual tipo_assoalho tipo_assoalho { get; set; }
        public virtual tipo_frete tipo_frete { get; set; }
        public virtual tipo_transporte tipo_transporte { get; set; }
        public virtual veiculo veiculo { get; set; }
        public virtual empresa empresa { get; set; }
        public virtual tipo_carroceria tipo_carroceria { get; set; }
        public virtual tipo_veiculo tipo_veiculo { get; set; }
    }
}
