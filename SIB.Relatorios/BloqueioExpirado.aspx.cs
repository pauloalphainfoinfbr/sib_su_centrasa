using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIB.Data;

namespace SIB.Relatorios
{
    public class listaChecklist
    {
        public Int32 id_checklist { get; set; }
        public string placa { get; set; }
        public string data_checklist { get; set; }
        public string nome_condutor { get; set; }
        public string transportadora { get; set; }
        public string Icon { get; set; }
    }

    public partial class BloqueioExpirado : System.Web.UI.Page
    {
        public static List<listaChecklist> lstChecklists { get; set; }

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
                    strScript += "$(\"#lstItemBloqueados\").attr(\"class\", \"active\");";
                    strScript += "});";
                    strScript += "</script>";

                    ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
                }

                //Popula o GridView
                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                populaGridChecklist();
            }
        }

        public void populaGridChecklist()
        {
            //Popula o GridView
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

            string sqlQuery = @"SELECT
	                                c.id_checklist,
	                                c.placa, 
	                                CONVERT(VARCHAR, c.data_checklist, 103) AS data_checklist,
	                                cond.nome_condutor,
	                                e.nome_fantasia AS transportadora,
                                    CASE c.chk_bloqueado WHEN 1 THEN '~/icones/Lock-Unlock-icon.png' ELSE '~/icones/Lock-Lock-icon.png' END AS Icon 
                                FROM checklist c
                                INNER JOIN condutor cond ON cond.cpf_condutor = c.cpf_condutor
                                INNER JOIN empresa e ON e.id_empresa = c.id_emp_transportadora
                                WHERE c.chk_bloqueado = 1 
                                ORDER BY c.data_checklist desc";


            lstChecklists = _dbContext.Database.SqlQuery<listaChecklist>(sqlQuery).ToList();
            gvChecklist.DataSource = lstChecklists;
            gvChecklist.DataBind();
        }

        protected void gvChecklist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "desbloquear")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)gvChecklist.Rows[index];
                int idChecklist = Convert.ToInt32(row.Cells[1].Text);

                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                var checklistExpirado = _dbContext.checklist.Where(c => c.id_checklist == idChecklist).First();
                checklistExpirado.chk_bloqueado = 2;

                var check_anterior = _dbContext.checklist.Where(r => r.id_checklist == idChecklist).ToList();
                var entityKeyCheckList = _dbContext.checklist.Create().GetType().GetProperty("id_checklist").GetValue(check_anterior[0]);
                _dbContext.Entry(_dbContext.Set<Data.checklist>().Find(entityKeyCheckList)).CurrentValues.SetValues(checklistExpirado);

                _dbContext.SaveChanges();

                populaGridChecklist();

                //Response.Redirect("cadUsuario.aspx?idUsuario=" + idUsuario);
            }
        }

        protected void gvChecklist_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void gvChecklist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvChecklist.DataSource = lstChecklists;
            gvChecklist.PageIndex = e.NewPageIndex;
            gvChecklist.DataBind();
        }
    }
}