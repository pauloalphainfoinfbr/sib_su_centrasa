using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class DadosVeiculo
    {
        public DateTime dataCheckList { get; set; }

        public string placa { get; set; }

        public string nomeCondutor { get; set; }

        public string cpfCondutor { get; set; }

        public Int32 transportadora { get; set; }
        
        public Nullable<int> ano { get; set; }
        public int transporte { get; set; }
        public int vinculo { get; set; }
        public int dedicado { get; set; }
        public int cargaDescarga { get; set; }
        public int usinaDeposito { get; set; }
        public int alturaVeic { get; set; }
        public int possuiLaudoOpacidade { get; set; }
        public string dataRealizacaoLaudoOpacidade { get; set; }
    }
}