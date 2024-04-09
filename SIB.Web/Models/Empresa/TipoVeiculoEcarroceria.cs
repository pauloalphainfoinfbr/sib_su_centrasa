using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class TipoVeiculoEcarroceria
    {
        [Required]
        public int tipoVeiculo { get; set; }
        [Required]
        public int tipoCarroceria { get; set; }
        
        public string placa2 { get; set; }
        public string placa3 { get; set; }
    }
}