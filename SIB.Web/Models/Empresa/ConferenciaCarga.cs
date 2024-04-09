using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class ConferenciaCarga
    {
        //public Nullable<int> materialMolhado { get; set; }
        public int? cintamentoFardos { get; set; }
        public int? chovendo { get; set; }
        public int? AvariaMaterial { get; set; }
        public int? TipoAvariaMaterial { get; set; }
        public int? tmpGuardaLatTravad { get; set; }
    }
}