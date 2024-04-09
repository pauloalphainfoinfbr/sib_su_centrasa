using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class TpSimNaoNa
    {
        public int ID { get; set; }
        public string Desc { get; set; }

        public List<TpSimNaoNa> getTpSimNaoNa()
        {
            return new List<TpSimNaoNa>
            {
                new TpSimNaoNa() { ID = 0, Desc = "Selecione" },
                new TpSimNaoNa() { ID = 1, Desc = "Sim" },
                new TpSimNaoNa() { ID = 2, Desc = "Não" },
                new TpSimNaoNa() { ID = 3, Desc = "NA" }
            };
        }
    }
}