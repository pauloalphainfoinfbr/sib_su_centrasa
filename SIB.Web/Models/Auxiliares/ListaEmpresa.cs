using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Auxiliares
{
    public class ListaEmpresa
    {
        Empresa TransportadoraList { get; set; }        

        public int idEmpresaSelecionada(int idEmpresaSelecionada)
        {
            Data.UsiminasIpatingaEntities _db = new Data.UsiminasIpatingaEntities();
            
            var transportadoraSelecionada = (
                                        from
                                            empresa
                                        in
                                            _db.empresa                                        
                                        select new { empresa.id_empresa }
                                      ).Where(r => r.id_empresa == idEmpresaSelecionada);

            return transportadoraSelecionada.ToList().Count > 0 ? transportadoraSelecionada.ToList()[0].id_empresa : 0;
        }

        public SelectList getEmpresa(int idTipoEmpresa)
        {
            Data.UsiminasIpatingaEntities _db = new Data.UsiminasIpatingaEntities();

            List<Empresa> empresas = new List<Empresa>();
            
            foreach (var conteudo in _db.empresa.OrderBy(e => e.nome_fantasia).Where(r => r.id_tipo_empresa == idTipoEmpresa))
            {
                empresas.Add(new Empresa() { ID = conteudo.id_empresa, Descricao = conteudo.nome_fantasia });
            }

            return new SelectList(empresas, "ID", "Descricao");
        }

        public SelectList getEmpresa(List<int> idClientesSelecionados)
        {
            Data.UsiminasIpatingaEntities _db = new Data.UsiminasIpatingaEntities();

            List<Empresa> empresas = new List<Empresa>();

            var empresasSelecionadas = idClientesSelecionados != null ? _db.empresa.Where(r => idClientesSelecionados.Contains(r.id_empresa)) : null;

            if (empresasSelecionadas != null)
            {
                foreach (var conteudo in empresasSelecionadas)
                {
                    empresas.Add(new Empresa() { ID = conteudo.id_empresa, Descricao = conteudo.nome_fantasia });
                }

                return new SelectList(empresas, "ID", "Descricao");
            }
            else
            {
                return new SelectList("", "ID", "Descricao");
            }
        }
    }
}