using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using SIB.Data;

namespace SIB.Web.Models.Auxiliares
{
    public class ListaCheckListDestino
    {        
        Cidade CidadeList {get; set;}

        public SelectList getCidadeCheckListDestino(List<int> checkDestinos, int idFase)
        {
            Data.UsiminasIpatingaEntities _db = new Data.UsiminasIpatingaEntities();
           
            if (checkDestinos != null)
            {
                List<Cidade> cidades = new List<Cidade>();

                if (idFase == 1)
                {                   
                    var lista = (
                                    from
                                        c
                                    in
                                        _db.cidade
                                    select new { c.id_cidade, c.desc_cidade }).Where(c => checkDestinos.Contains(c.id_cidade)).OrderBy(cd => cd.desc_cidade).ToList();

                    foreach (var conteudo in lista)
                    {
                        cidades.Add(new Cidade { ID = conteudo.id_cidade, Descricao = conteudo.desc_cidade });
                    }

                    return new SelectList(cidades, "ID", "Descricao");
                }
                else
                {
                    var lista = (
                                from
                                    c
                                in
                                    _db.cidade
                                select new { c.id_cidade, c.desc_cidade }).Where(c => checkDestinos.Contains(c.id_cidade)).OrderBy(cd => cd.desc_cidade).ToList();

                    foreach (var conteudo in lista)
                    {
                        cidades.Add(new Cidade { ID = conteudo.id_cidade, Descricao = conteudo.desc_cidade });
                    }

                    return new SelectList(cidades, "ID", "Descricao");
                }
            }
            else
            {
                return new SelectList("", "ID", "Descricao");
            }                        
        }        

        public SelectList getCidadesSelecionadas(List<int> idCidadesSelecionadas)
        {
            Data.UsiminasIpatingaEntities _db = new Data.UsiminasIpatingaEntities();

            List<Cidade> cidades = new List<Cidade>();

            var lista = idCidadesSelecionadas != null ? _db.cidade.Where(r => idCidadesSelecionadas.Contains(r.id_cidade)).OrderBy(r => r.desc_cidade).ToList() : null;

            if (lista != null)
            {
                foreach (var conteudo in lista)
                {
                    cidades.Add(new Cidade { ID = conteudo.id_cidade, Descricao = conteudo.desc_cidade });
                }

                return new SelectList(cidades, "ID", "Descricao");
            }
            else
            {
                return new SelectList("", "ID", "Descricao");
            }

        }
    }
}