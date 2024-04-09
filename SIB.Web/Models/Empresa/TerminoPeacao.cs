using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class TerminoPeacao
    {        
        public int qtdCunha { get; set; }        
        public int tipoBorracha { get; set; }
        public string placa { get; set; }
        public string observacao { get; set; }
        public Nullable<int> chkObsMaterial { get; set; }
        public DateTime dataFimPeacao { get; set; }
    }
}