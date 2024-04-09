using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Auxiliares
{
    public class ListaCidade
    {
        Cidade CidadeList { get; set; }

        public SelectList getCidadesList()
        {
            Data.UsiminasIpatingaEntities _db = new Data.UsiminasIpatingaEntities();

            List<Cidade> cidades = new List<Cidade>();

            foreach (var conteudo in _db.cidade)
            {
                cidades.Add(new Cidade { ID = conteudo.id_cidade, Descricao = conteudo.desc_cidade });
            }

            return new SelectList(cidades, "ID", "Descricao");
        }
    }   
}