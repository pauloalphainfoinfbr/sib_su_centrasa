using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIB.Data;

namespace SIB.Relatorios
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!Page.IsPostBack)
                {

                    if (Session["id"] == null)
                    {
                        Response.Redirect("logOff.aspx");
                    }

                    int idUsuario = Convert.ToInt32(Session["id"]);

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
                                        select new { perm.ativo, perm.id_usuario, tel.hyperlink_nome }
                                    ).Where(r => r.id_usuario == idUsuario && r.ativo == true).ToList();

                    if (permissao != null)
                    {
                        foreach (var item in permissao)
                        {
                            if (!string.IsNullOrWhiteSpace(item.hyperlink_nome))
                            {
                                Control control = FindControl(item.hyperlink_nome);
                                ((HyperLink)control).Enabled = true;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            } 
        }
    }
}