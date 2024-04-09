using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class VeiculoPossui
    {
        public int cintas { get; set; }
        public string DT { get; set; }
        public string tonelagem { get; set; }
        public string qtdeEntregas { get; set; }
        public string entregaAlavancas { get; set; }
        //public Nullable<int> alavancaDevolvida { get; set; }
        public string[] materiais { get; set; }
    }
}
