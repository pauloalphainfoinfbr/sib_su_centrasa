using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SIB.Data;

namespace SIB.Relatorios
{
    public class validaPermissao
    {
        public bool verificaPermissao(Int32 idUsuario, Int32 idTela)
        {
            UsiminasIpatingaEntities _dbContex = new UsiminasIpatingaEntities();

            var permissao = (
                                from
                                    perm
                                in
                                    _dbContex.permissao_usuario
                                join
                                    tel
                                in
                                    _dbContex.tela
                                on
                                    perm.id_tela
                                equals
                                    tel.id_tela
                                select new { perm.ativo, perm.id_usuario, tel.hyperlink_nome, perm.id_tela }
                            ).Where(r => r.id_usuario == idUsuario && r.id_tela == idTela && r.ativo == true).ToList();

            if (permissao.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}