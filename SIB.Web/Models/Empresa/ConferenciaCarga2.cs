using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class ConferenciaCarga2
    {
        public Nullable<int> lona { get; set; }
        public Nullable<int> amarracaoCarreta { get; set; }
        public Nullable<int> cintas { get; set; }
        //public Nullable<int> devidamentePregado { get; set; }
        public Nullable<int> borrachasAssoalhoCarga { get; set; }
        public Nullable<int> bercoMetalicoBobineira { get; set; }
        public Nullable<int> cargaSecaGraneleiro { get; set; }
        public Nullable<int> catracaoAmarracao { get; set; }
        public Nullable<int> precisouMovimentarTampas { get; set; }
        public Nullable<int> devolucaoAlavanca { get; set; }
        public Nullable<int> tipoLona { get; set; }
        public Nullable<int> condicoesEmbalagemMaterial { get; set; }
        //public string qtdeCunha { get; set; }

        public string veiculoLiberado { get; set; }

    }
}