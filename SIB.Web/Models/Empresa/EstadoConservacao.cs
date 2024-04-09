using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class EstadoConservacao
    {
        public int tacografo { get; set; }
        public int chassi { get; set; }
        public int extintor { get; set; }
        public int triangMacaco { get; set; } 
        public int parabrisaTrincado { get; set; }
        public int sinalSonoroRe { get; set; }
        public int carroceria { get; set; }

        //[Required]
        //[Range(1, 6)]
        //public int assoalho { get; set; }
        public int pneusEstepes { get; set; }
        public int faroisLanterna { get; set; }
        public int luzFreio { get; set; }
        public int setas { get; set; }
        public int cordasCintasCatracas { get; set; }
        public int carroceriaLimpa { get; set; }
        public int parafusoRodas { get; set; }
        public int trincaTolerancia { get; set; }
        public int retrovisorTrincado { get; set; }
        public int trincaRetrovisorTolerancia { get; set; }
        public int dormentesFixados { get; set; }
        public int laudoGanchoAdaptado { get; set; }
        public int todosPinos { get; set; }
        public int drenoBobineira { get; set; }
        public int dobradicasBoasCondicoes { get; set; }
        public int estruturaTampasFerroMadeira { get; set; }
        public int estadoGeral { get; set; }
        public int vazamentoOleo { get; set; }
        public int placaLegivel { get; set; }
        public int ganchoPatola { get; set; }
        public int fotosCondicoesBobineira { get; set; }
        public int possuiBorrachasAssoalho { get; set; }        

        public int tipoAssoalho { get; set; }
        public int EstadoAssoalho { get; set; }
        public int SituacaoAssoalho { get; set; }

        public int drenosDesobstruidos { get; set; }
        public int borrachaBobineiraInteirica { get; set; }
        public int borrachaFixaBobineira { get; set; }
        public int CondicoesBobineira { get; set; }
        public int travaSegTampas { get; set; }
        
        public string medFumacaNegra { get; set; }
    }
}