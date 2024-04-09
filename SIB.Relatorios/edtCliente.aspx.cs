using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIB.Data;

namespace SIB.Relatorios
{
    public class listaCliente
    {
        public int id_empresa { get; set; }
        public string codigo { get; set; }
        public string nome_fantasia { get; set; }        
    }

    public partial class edtCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            validaPermissao perm = new validaPermissao();
            if (!perm.verificaPermissao(Convert.ToInt32(Session["id"]), 31))
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
                    strScript += "$(\"ul li:eq(9)\").attr(\"class\", \"active\");";
                    strScript += "});";
                    strScript += "</script>";

                    ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
                }

                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                //Popula o Gridview

                string sqlQuery = "SELECT " +
                                  "emp.id_empresa, " +
                                  "emp.codigo, " +
                                  "emp.nome_fantasia " +
                                  "FROM " +
                                  "empresa emp " +
                                  "WHERE " +
                                  "emp.id_tipo_empresa = 3 ";

                List<listaCliente> listaEmpresa = _dbContext.Database.SqlQuery<listaCliente>(sqlQuery).ToList();

                gvCliente.DataSource = listaEmpresa;
                gvCliente.DataBind();
            }
        }

        protected void gvCliente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

            if (e.CommandName == "editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)gvCliente.Rows[index];
                int idEmpresa = Convert.ToInt32(row.Cells[2].Text);

                Response.Redirect("cadCliente.aspx?idCliente=" + idEmpresa);
            }
            else if (e.CommandName == "apagar")
            {                
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)gvCliente.Rows[index];
                int idEmrpesa = Convert.ToInt32(row.Cells[2].Text);

                RestricaoFk objRestricao = new RestricaoFk();

                if (!objRestricao.verificaRestricaoEmpresaEndereco("empresa", idEmrpesa))
                {
                    var empresa = _dbContext.empresa.Where(r => r.id_empresa == idEmrpesa).ToList()[0];

                    _dbContext.empresa.Remove(empresa);
                    _dbContext.SaveChanges();

                    Response.Redirect("edtEmpresa.aspx");
                }
                else
                {
                    lblMensagem.Text = "Não é possível excluir este cliente pois está vinculado a outro registro.";
                }
            }
        }

        protected void gvCliente_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void gvCliente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

            //Popula o Gridview

            string sqlQuery = "SELECT " +
                              "emp.id_empresa, " +
                              "emp.codigo, " +
                              "emp.nome_fantasia " +
                              "FROM " +
                              "empresa emp " +
                              "WHERE " +
                              "emp.id_tipo_empresa = 3 ";

            List<listaCliente> listaEmpresa = _dbContext.Database.SqlQuery<listaCliente>(sqlQuery).ToList();

            gvCliente.DataSource = listaEmpresa;
            gvCliente.PageIndex = e.NewPageIndex;
            gvCliente.DataBind();
        }
    }
}