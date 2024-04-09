using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIB.Data;

namespace SIB.Relatorios
{
    public class listaUsuario
    {
        public Int32 id_usuario { get; set; }
        public string nome_usuario { get; set; }
        public string login { get; set; }
        public string transportadora { get; set; }
    }

    public partial class edtUsuario : System.Web.UI.Page
    {
        public static List<listaUsuario> lstUsuarios { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            validaPermissao perm = new validaPermissao();
            if (!perm.verificaPermissao(Convert.ToInt32(Session["id"]), 28))
            {
                Response.Redirect("Unauthorized.aspx");
            }

            if (!Page.IsPostBack)
            {
                if (!ClientScript.IsStartupScriptRegistered("script1"))
                {

                    string strScript = "<script type=\"text/javascript\">";
                    strScript += "$(function (e) {";
                    strScript += "$(\"li\").removeClass(\"active\");";
                    strScript += "$(\"#lstItemEdtUsu\").attr(\"class\", \"active\");";
                    strScript += "});";
                    strScript += "</script>";

                    ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
                }

                //Popula o GridView
                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                string sqlQuery = "SELECT " +
                                   "usu.id_usuario, " +
                                   "usu.nome_usuario, " +
                                   "usu.login, " +
                                   "CASE WHEN emp.nome_fantasia IS NOT NULL THEN emp.nome_fantasia WHEN (emp.nome_fantasia IS NULL and usu.id_tipo_usuario = 2) THEN 'VISTORIADOR' WHEN (emp.nome_fantasia IS NULL and usu.id_tipo_usuario = 1) THEN 'USUÁRIO INTERNO' END AS transportadora " +
                                "FROM " +
                                    "usuario usu " +
                                "LEFT JOIN " +
                                    "empresa emp " +
                                "ON " +
                                    "usu.id_transportadora = emp.id_empresa ";


                lstUsuarios = _dbContext.Database.SqlQuery<listaUsuario>(sqlQuery).ToList();               
                gvUsuario.DataSource = lstUsuarios;                
                gvUsuario.DataBind();
            }
        }

        protected void gvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)gvUsuario.Rows[index];
                int idUsuario = Convert.ToInt32(row.Cells[2].Text);

                Response.Redirect("cadUsuario.aspx?idUsuario=" + idUsuario);
            }
            else if (e.CommandName == "apagar")
            {
                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)gvUsuario.Rows[index];
                int idUsuario = Convert.ToInt32(row.Cells[2].Text);

                RestricaoFk objRestricao = new RestricaoFk();

                if(!objRestricao.verificaRestricao("usuario", idUsuario))
                {
                    var usuario = _dbContext.usuario.Where(r => r.id_usuario == idUsuario).ToList()[0];

                    _dbContext.usuario.Remove(usuario);
                    _dbContext.SaveChanges();

                    Response.Redirect("edtUsuario.aspx?idUsuario=" + idUsuario);
                }
                else
                {
                    lblMensagem.Text = "Não é possível excluir este usuário pois está vinculado a outro registro.";
                }
            }
        }

        protected void gvUsuario_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //Add CSS class on header row.
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.CssClass = "header";

            //Add CSS class on normal row.
            if (e.Row.RowType == DataControlRowType.DataRow &&
                      e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "normal";

            //Add CSS class on alternate row.
            if (e.Row.RowType == DataControlRowType.DataRow &&
                      e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "alternate";
        }

        protected void gvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuario.DataSource = lstUsuarios;
            gvUsuario.PageIndex = e.NewPageIndex;
            gvUsuario.DataBind();
        }           
    }
}