using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SIB.Data;

namespace SIB.Relatorios
{
    public class listaVeiculos
    {
        public string manifestado { get; set; }
        public string liberado { get; set; }
        public Int32 id_checklist { get; set; }
        public int entrou_carregar { get; set; }
        public int id_fase { get; set; }
        public string placa { get; set; }
        public string veic_manifestado { get; set; }
        public string liberado_viagem { get; set; }
        public string data_veic_manifestado { get; set; }
        public string data_liberado_viagem { get; set; }
        public string desc_tipo_veiculo { get; set; }
        public string motivos { get; set; }
        public string nome_fantasia { get; set; }
        public string berco_metalico { get; set; }
        public string cinta_fina_etiqueta { get; set; }
        public string bobineira { get; set; }
        public string status { get; set; }
        public string data { get; set; }
        public string inicio_inspecao { get; set; }
        public string inicio_peacao { get; set; }
        public string termino_peacao { get; set; }
        public string rastreador { get; set; }
    }

    public class statusVeiculo
    {
        public bool veiculo_manifestado {get; set;}
        public bool liberado_viagem { get; set; }
    }

    public partial class pesquisaTransportadora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            validaPermissao perm = new validaPermissao();
            if (!perm.verificaPermissao(Convert.ToInt32(Session["id"]), 33))
            {
                Response.Redirect("Unauthorized.aspx");
            }

            if (!ClientScript.IsStartupScriptRegistered("script1"))
            {
                string strScript = "<script type=\"text/javascript\">";
                strScript += "$(function (e) {";
                strScript += "$(\"li\").removeClass(\"active\");";
                strScript += "$(\"#lstItemPesquisaVeiculos\").attr(\"class\", \"active\");";
                strScript += "});";
                strScript += "</script>";

                ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
            }

            chkLiberado.Enabled = false;
            chkManifestado.Enabled = false;
            btnGravar.Enabled = false;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

            string sqlQuery = "SELECT " +                                                                
                                "chk.id_checklist, " +                                
                                "(SELECT MAX(fase.id_fase) FROM checklist_fase fase WHERE fase.id_checklist = chk.id_checklist) as id_fase, " +
                                "chk.placa, " +                                                                                                                                
                                "tv.desc_tipo_veiculo, " +
                                "REPLACE(dbo.fn_ListaMotivosReprovacaoUsiminasIP(chk.id_checklist), '<BR>', ' | ') as motivos, " +
                                "emp.nome_fantasia, " +
                                "CASE chk_metalico WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END as berco_metalico, " +                                
                                "CASE chk_bobineira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END as bobineira, " +
                                "CASE chk.veiculo_liberado WHEN 2 THEN 'NÃO/VISTORIA' WHEN 1 THEN 'SIM/VISTORIA' ELSE '' END as status, " +
                                "CONVERT(VARCHAR, chk.data_checklist, 103) as data, " +
                                "CONVERT(VARCHAR, chk.data_checklist, 103) + ' ' + CONVERT(VARCHAR, chk.data_checklist, 108) as inicio_inspecao, " +
                                "CONVERT(VARCHAR, chk.data_inicio_peacao, 103) + ' ' + CONVERT(VARCHAR, chk.data_inicio_peacao, 108) as inicio_peacao, " +
                                "CONVERT(VARCHAR, chk.data_fim_peacao, 103) + ' ' + CONVERT(VARCHAR, chk.data_fim_peacao, 108) as termino_peacao " +
                            "FROM " +
                                "checklist chk " +
                            "INNER JOIN " +
                                "veiculo veic " +
                            "ON " +
                                "chk.placa = veic.placa " +
                            "INNER JOIN " +
                                "tipo_veiculo tv " +
                            "ON " +
                                "tv.id_tipo_veiculo = chk.id_tipo_veiculo " +
                            "INNER JOIN " +
                                "checklist_fase cf " +
                            "ON " +
                                "cf.id_checklist = chk.id_checklist " +
                            "LEFT OUTER JOIN " +
                                "empresa emp " +
                            "ON " +
                                "emp.id_empresa = chk.id_emp_transportadora " +
                            "WHERE " +
                                "chk.placa = '" + txtPlaca.Text.Replace("-", "") + "' " +
                            "AND " +
                                "chk.id_checklist = (SELECT MAX(chek.id_checklist) FROM checklist chek WHERE chek.placa = '" + txtPlaca.Text.Replace("-", "") + "' ) " +
                            "AND " +
                                "(DATEDIFF(DAY, chk.data_checklist, GETDATE()) <= 5 AND (SELECT MAX(clf.id_fase) FROM checklist_fase clf WHERE clf.id_checklist = chk.id_checklist) = 1) " +
                            "UNION " +
                            "SELECT " +
                                "chk.id_checklist, " +                                
                                "(SELECT MAX(fase.id_fase) FROM checklist_fase fase WHERE fase.id_checklist = chk.id_checklist) as id_fase, " +
                                "chk.placa, " +
                                "tv.desc_tipo_veiculo, " +
                                "REPLACE(dbo.fn_ListaMotivosReprovacaoUsiminasIP(chk.id_checklist), '<BR>', ' | ') as motivos, " +
                                "emp.nome_fantasia, " +
                                 "CASE chk_metalico WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END as berco_metalico, " +
                                "CASE chk_bobineira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END as bobineira, " +
                                "CASE chk.veiculo_liberado WHEN 2 THEN 'NÃO/VISTORIA' WHEN 1 THEN 'SIM/VISTORIA' ELSE '' END as status, " +
                                "CONVERT(VARCHAR, chk.data_checklist, 103) as data, " +
                                "CONVERT(VARCHAR, chk.data_checklist, 103) + ' ' + CONVERT(VARCHAR, chk.data_checklist, 108) as inicio_inspecao, " +
                                "CONVERT(VARCHAR, chk.data_inicio_peacao, 103) + ' ' + CONVERT(VARCHAR, chk.data_inicio_peacao, 108) as inicio_peacao, " +
                                "CONVERT(VARCHAR, chk.data_fim_peacao, 103) + ' ' + CONVERT(VARCHAR, chk.data_fim_peacao, 108) as termino_peacao " +
                            "FROM " +
                                "checklist chk " +
                            "INNER JOIN " +
                                "veiculo veic " +
                            "ON " +
                                "chk.placa = veic.placa " +
                            "INNER JOIN " +
                                "tipo_veiculo tv " +
                            "ON " +
                                "tv.id_tipo_veiculo = chk.id_tipo_veiculo " +
                            "INNER JOIN " +
                                "checklist_fase cf " +
                            "ON " +
                                "cf.id_checklist = chk.id_checklist " +
                            "LEFT OUTER JOIN " +
                                "empresa emp " +
                            "ON " +
                                "emp.id_empresa = chk.id_emp_transportadora " +
                            "WHERE " +
                                "chk.placa = '" + txtPlaca.Text.Replace("-", "") + "' " +
                            "AND " +
                                "chk.id_checklist = (SELECT MAX(c.id_checklist) FROM checklist c WHERE c.placa = '" + txtPlaca.Text.Replace("-", "") + "') " +
                            "AND " +
                                "cf.id_fase >= 2 ";                            
            
            gvVeiculos.DataSource = _dbContext.Database.SqlQuery<listaVeiculos>(sqlQuery).ToList();
            gvVeiculos.DataBind();

            if (_dbContext.Database.SqlQuery<listaVeiculos>(sqlQuery).ToList().Count > 0)
            {
                int id_fase = _dbContext.Database.SqlQuery<listaVeiculos>(sqlQuery).ToList()[0].id_fase;
                int entrou_carregar = _dbContext.Database.SqlQuery<listaVeiculos>(sqlQuery).ToList()[0].entrou_carregar;

                string veic_manifestado = _dbContext.Database.SqlQuery<listaVeiculos>(sqlQuery).ToList()[0].veic_manifestado;
                string veic_liberado = _dbContext.Database.SqlQuery<listaVeiculos>(sqlQuery).ToList()[0].liberado_viagem;

                if (entrou_carregar > 0 && string.IsNullOrWhiteSpace(veic_manifestado))
                {
                    chkManifestado.Enabled = true;
                    btnGravar.Enabled = true;
                }

                if (id_fase > 2 && string.IsNullOrWhiteSpace(veic_liberado))
                {
                    chkLiberado.Enabled = true;
                    btnGravar.Enabled = true;
                }
            }
        }

        protected void gvVeiculos_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

            string sqlQuery = "SELECT " +                                                                
                                "chk.id_checklist, " +                                
                                "(SELECT MAX(fase.id_fase) FROM checklist_fase fase WHERE fase.id_checklist = chk.id_checklist) as id_fase, " +
                                "chk.placa, " +
                                "tv.desc_tipo_veiculo, " +
                                "REPLACE(dbo.fn_ListaMotivosReprovacaoUsiminasIP(chk.id_checklist), '<BR>', ' | ') as motivos, " +
                                "emp.nome_fantasia, " +
                                "chk_metalico as berco_metalico, " +                                
                                "chk_bobineira as bobineira, " +
                                "CASE chk.veiculo_liberado WHEN 0 THEN 'NÃO/VISTORIA' WHEN 1 THEN 'SIM/VISTORIA' ELSE '' END as status, " +
                                "CONVERT(VARCHAR, chk.data_checklist, 103) as data, " +
                                "CONVERT(VARCHAR, chk.data_checklist, 103) + ' ' + CONVERT(VARCHAR, chk.data_checklist, 108) as inicio_inspecao, " +
                                "CONVERT(VARCHAR, chk.data_inicio_peacao, 103) + ' ' + CONVERT(VARCHAR, chk.data_inicio_peacao, 108) as inicio_peacao, " +
                                "CONVERT(VARCHAR, chk.data_fim_peacao, 103) + ' ' + CONVERT(VARCHAR, chk.data_fim_peacao, 108) as termino_peacao " +
                            "FROM " +
                                "checklist chk " +
                            "INNER JOIN " +
                                "veiculo veic " +
                            "ON " +
                                "chk.placa = veic.placa " +
                            "INNER JOIN " +
                                "tipo_veiculo tv " +
                            "ON " +
                                "tv.id_tipo_veiculo = chk.id_tipo_veiculo " +
                            "INNER JOIN " +
                                "checklist_fase cf " +
                            "ON " +
                                "cf.id_checklist = chk.id_checklist " +
                            "LEFT OUTER JOIN " +
                                "empresa emp " +
                            "ON " +
                                "emp.id_empresa = chk.id_emp_transportadora " +
                            "WHERE " +
                                "chk.placa = '" + txtPlaca.Text.Replace("-", "") + "' " +
                            "AND " +
                                "chk.id_checklist = (SELECT MAX(chek.id_checklist) FROM checklist chek WHERE chek.placa = '" + txtPlaca.Text.Replace("-", "") + "' ) " +
                            "AND " +
                                "(DATEDIFF(DAY, chk.data_checklist, GETDATE()) < 5 AND (SELECT MAX(clf.id_fase) FROM checklist_fase clf WHERE clf.id_checklist = chk.id_checklist) = 1) " +
                            "UNION " +
                            "SELECT " +                                
                                "chk.id_checklist, " +                                
                                "(SELECT MAX(fase.id_fase) FROM checklist_fase fase WHERE fase.id_checklist = chk.id_checklist) as id_fase, " +
                                "chk.placa, " +
                                "tv.desc_tipo_veiculo, " +
                                "REPLACE(dbo.fn_ListaMotivosReprovacaoUsiminasIP(chk.id_checklist), '<BR>', ' | ') as motivos, " +
                                "emp.nome_fantasia, " +
                                "chk_metalico as berco_metalico, " +                                
                                "chk_bobineira as bobineira, " +
                                "CASE chk.veiculo_liberado WHEN 0 THEN 'NÃO/VISTORIA' WHEN 1 THEN 'SIM/VISTORIA' ELSE '' END as status, " +
                                "CONVERT(VARCHAR, chk.data_checklist, 103) as data, " +
                                "CONVERT(VARCHAR, chk.data_checklist, 103) + ' ' + CONVERT(VARCHAR, chk.data_checklist, 108) as inicio_inspecao, " +
                                "CONVERT(VARCHAR, chk.data_inicio_peacao, 103) + ' ' + CONVERT(VARCHAR, chk.data_inicio_peacao, 108) as inicio_peacao, " +
                                "CONVERT(VARCHAR, chk.data_fim_peacao, 103) + ' ' + CONVERT(VARCHAR, chk.data_fim_peacao, 108) as termino_peacao " +
                            "FROM " +
                                "checklist chk " +
                            "INNER JOIN " +
                                "veiculo veic " +
                            "ON " +
                                "chk.placa = veic.placa " +
                            "INNER JOIN " +
                                "tipo_veiculo tv " +
                            "ON " +
                                "tv.id_tipo_veiculo = chk.id_tipo_veiculo " +
                            "INNER JOIN " +
                                "checklist_fase cf " +
                            "ON " +
                                "cf.id_checklist = chk.id_checklist " +
                            "LEFT OUTER JOIN " +
                                "empresa emp " +
                            "ON " +
                                "emp.id_empresa = chk.id_emp_transportadora " +
                            "WHERE " +
                                "chk.placa = '" + txtPlaca.Text.Replace("-","") + "' " +
                            "AND " +
                                "chk.id_checklist = (SELECT MAX(c.id_checklist) FROM checklist c WHERE c.placa = '" + txtPlaca.Text.Replace("-", "") + "') " +
                            "AND " +
                                "cf.id_fase >= 2 ";
 
            Int32 id_checklist = _dbContext.Database.SqlQuery<listaVeiculos>(sqlQuery).ToList()[0].id_checklist;

            checklist objChecklist = _dbContext.checklist.Where(r => r.id_checklist == id_checklist).ToList()[0];

            //if (chkManifestado.Checked)
            //{
            //    objChecklist.veic_manifestado = true;
            //    objChecklist.data_veic_manifestado = DateTime.Now;
            //}

            //if (chkLiberado.Checked)
            //{
            //    objChecklist.liberado_viagem = true;
            //    objChecklist.data_liberado_viagem = DateTime.Now;
            //}

            var check_anterior = _dbContext.checklist.Where(r => r.id_checklist == id_checklist).ToList();
            var entityKeyCheckList = _dbContext.checklist.Create().GetType().GetProperty("id_checklist").GetValue(check_anterior[0]);
            _dbContext.Entry(_dbContext.Set<Data.checklist>().Find(entityKeyCheckList)).CurrentValues.SetValues(objChecklist);

            _dbContext.SaveChanges();

            gvVeiculos.DataSource = _dbContext.Database.SqlQuery<listaVeiculos>(sqlQuery).ToList();
            gvVeiculos.DataBind();

            chkManifestado.Enabled = false;
            chkLiberado.Enabled = false;
            btnGravar.Enabled = false;
        }

        protected void gvVeiculos_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void gvVeiculos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrWhiteSpace(e.Row.Cells[5].Text) || e.Row.Cells[5].Text != " ")
                {
                    if (e.Row.Cells[5].Text == "N&#195;O/VISTORIA")
                    {
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }
}