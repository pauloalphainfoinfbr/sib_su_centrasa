using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class BercoMetalico
    {
        [Required]
        public int necessarioMontarBerco { get; set; }
        [Required]
        public int possui { get; set; }
        [Required]
        public int bercoProprio { get; set; }
        [Required]
        public int liberado { get; set; }
        [Required]
        public int bobineira { get; set; }

    }
}