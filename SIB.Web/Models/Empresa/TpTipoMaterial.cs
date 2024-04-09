using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Empresa
{
    public class TpTipoMaterial
    {
        public string ID { get; set; }
        public string Desc { get; set; }

        public List<TpTipoMaterial> getTpAvariaMaterial()
        {
            return new List<TpTipoMaterial>
            {
                new TpTipoMaterial() { ID = "0", Desc = "Selecione" },
                new TpTipoMaterial() { ID = "Bobina", Desc = "Bobina" },
                new TpTipoMaterial() { ID = "Chapa", Desc = "Chapa" },
                new TpTipoMaterial() { ID = "Fardo", Desc = "Fardo" }
            };
        }
    }
}