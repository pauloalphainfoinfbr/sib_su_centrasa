using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class TpAvariaMaterial
    {
        public int ID { get; set; }
        public string Desc { get; set; }

        public List<TpAvariaMaterial> getTpAvariaMaterial()
        {
            return new List<TpAvariaMaterial>
            {
                new TpAvariaMaterial() { ID = 0, Desc = "Selecione" },
                new TpAvariaMaterial() { ID = 1, Desc = "Amassado" },
                new TpAvariaMaterial() { ID = 2, Desc = "Oxidado" },
                new TpAvariaMaterial() { ID = 3, Desc = "Molhado" },
                new TpAvariaMaterial() { ID = 4, Desc = "Empoeirado" },
                new TpAvariaMaterial() { ID = 5, Desc = "Empenada" },
                new TpAvariaMaterial() { ID = 6, Desc = "Rebarba" },
                new TpAvariaMaterial() { ID = 7, Desc = "Embalagem Danificada" }
            };
        }
    }
}