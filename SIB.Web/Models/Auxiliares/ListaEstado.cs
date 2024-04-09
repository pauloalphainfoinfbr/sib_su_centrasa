using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Auxiliares
{
    public class ListaEstado
    {
        Estado EstadoList { get; set; }

        public SelectList getEstadoList()
        {
            Data.UsiminasIpatingaEntities _db = new Data.UsiminasIpatingaEntities();

            List<Estado> estados = new List<Estado>();

            foreach (var conteudo in _db.estado.OrderBy(e => e.desc_estado))
            {
                estados.Add(new Estado { ID = conteudo.id_estado, Descricao = conteudo.desc_estado });
            }

            return new SelectList(estados, "ID", "Descricao");
        }
    }
}