using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class Acessorios
    {
        [Required]
        public int adaptacaoCinta { get; set; }
        [Required]
        public int assoaFfrizado { get; set; }        
        [Required]
        public int bercoMetalico { get; set; }
        [Required]
        public int bobineira { get; set; }
        [Required]
        public int cintaFinaPossuiEtiqueta { get; set; }
        [Required]
        public int extintorTriangMacac { get; set; }
        [Required]
        public int liberado { get; set; }
        [Required]
        public int lonaProtecao { get; set; }
        [Required]
        public int tacografo { get; set; }
        [Required]
        public int tipoAssoalho { get; set; }
        
    }
}