using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIB.Web.Models
{
    public class BuscaPlaca
    {        
        public string placa { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string placaMercosul { get; set; }
        public string tipoPlaca { get; set; }
    }
}