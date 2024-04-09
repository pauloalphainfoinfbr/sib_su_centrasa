using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class RetornouUsina
    {
        public Nullable<int> retornouUsina { get; set; }        
        public string numeroLote { get; set; }
        public string cliente { get; set; }
        //public string material { get; set; }
        public string observacao { get; set; }
    }
}