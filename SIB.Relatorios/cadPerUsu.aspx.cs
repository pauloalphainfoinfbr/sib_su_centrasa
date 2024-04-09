using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.Objects;
using SIB.Data;

namespace SIB.Relatorios
{
    public class listaTela
    {
        public int id_tela { get; set; }
        public string desc_tela { get; set; }
        public string hyperlink_nome { get; set; }
        public bool ativo { get; set; }
    }

    public partial class cadPerUsu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            validaPermissao perm = new validaPermissao();
            if (!perm.verificaPermissao(Convert.ToInt32(Session["id"]), 32))
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
                    strScript += "$(\"#lstItemPerUsu\").attr(\"class\", \"active\");";
                    strScript += "});";
                    strScript += "</script>";

                    ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
                }

                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                //Combo Usuario
                List<usuario> listaUsuario = new List<usuario>();
                listaUsuario = _dbContext.usuario.Where(r => r.id_tipo_usuario != 2).OrderBy(r => r.nome_usuario).ToList();
                listaUsuario.Add(new usuario() { nome_usuario = "Selecione o Usuário" });

                ddlUsuario.DataSource = listaUsuario;
                ddlUsuario.DataValueField = "id_usuario";
                ddlUsuario.DataTextField = "nome_usuario";
                ddlUsuario.DataBind();
                ddlUsuario.SelectedIndex = ddlUsuario.Items.Count - 1;                
            }
        }

        protected void cadastrar_Click(object sender, EventArgs e)
        {
            if (!ClientScript.IsStartupScriptRegistered("script1"))
            {

                string strScript = "<script type=\"text/javascript\">";
                strScript += "$(function (e) {";
                strScript += "$(\"li\").removeClass(\"active\");";
                strScript += "$(\"#lstItemPerUsu\").attr(\"class\", \"active\");";
                strScript += "});";
                strScript += "</script>";

                ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
            }

            if (Convert.ToInt32(ddlUsuario.SelectedValue) > 0 && Convert.ToInt32(ddlUsuario.SelectedIndex) != (ddlUsuario.Items.Count - 1))
            {
                int idUsuario = Convert.ToInt32(ddlUsuario.SelectedValue);
                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();                

                foreach (GridViewRow row in gvPermUsuario.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    
                    int idTela = Convert.ToInt32(row.Cells[1].Text);

                    if (_dbContext.permissao_usuario.Where(r => r.id_tela == idTela && r.id_usuario == idUsuario).ToList().Count == 0)
                    {
                        permissao_usuario objPermissao = new permissao_usuario();
                        objPermissao.id_usuario = idUsuario;
                        objPermissao.id_tela = idTela;

                        if (chk.Checked)
                            objPermissao.ativo = Convert.ToBoolean(1);
                        else
                            objPermissao.ativo = Convert.ToBoolean(0);

                        _dbContext.permissao_usuario.Add(objPermissao);
                    }
                    else
                    {
                        permissao_usuario objPermissao = new permissao_usuario();
                        objPermissao.id_usuario = idUsuario;
                        objPermissao.id_tela = idTela;

                        if (chk.Checked)
                            objPermissao.ativo = Convert.ToBoolean(1);
                        else
                            objPermissao.ativo = Convert.ToBoolean(0);

                        var permissao = _dbContext.permissao_usuario.Where(r => r.id_usuario == idUsuario && r.id_tela == idTela).ToList();
                            
                        objPermissao.id_permissao_usuario = permissao[0].id_permissao_usuario;

                        var entityKeyPermissao = _dbContext.permissao_usuario.Create().GetType().GetProperty("id_permissao_usuario").GetValue(permissao[0]);
                        _dbContext.Entry(_dbContext.Set<Data.permissao_usuario>().Find(entityKeyPermissao)).CurrentValues.SetValues(objPermissao);
                    }                                                                                                      
                }

                _dbContext.SaveChanges();

                Response.Redirect("Default.aspx");
            }
        }

        protected void gvPermUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ClientScript.IsStartupScriptRegistered("script1"))
            {

                string strScript = "<script type=\"text/javascript\">";
                strScript += "$(function (e) {";
                strScript += "$(\"li\").removeClass(\"active\");";
                strScript += "$(\"#lstItemPerUsu\").attr(\"class\", \"active\");";
                strScript += "});";
                strScript += "</script>";

                ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
            }

            if (Convert.ToInt32(ddlUsuario.SelectedValue) > 0 && Convert.ToInt32(ddlUsuario.SelectedIndex) != (ddlUsuario.Items.Count - 1))
            {
                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                string sqlQuery = "SELECT " +
                                        "t.id_tela, " +
                                        "t.desc_tela, " +
                                        "t.hyperlink_nome, " +
                                        "CASE WHEN pu.ativo IS NULL THEN CONVERT(BIT, 0) ELSE pu.ativo END as ativo " +
                                    "FROM " +
                                        "tela t " +
                                    "INNER JOIN " +
                                        "permissao_usuario pu " +
                                    "ON " +
                                        "t.id_tela = pu.id_tela " +
                                    "WHERE " +
                                        "id_usuario = " + ddlUsuario.SelectedValue;

                List<listaTela> objLista = new System.Collections.Generic.List<listaTela>();

                objLista = _dbContext.Database.SqlQuery<listaTela>(sqlQuery).ToList();

                if (objLista.Count > 0)
                {
                    gvPermUsuario.DataSource = objLista;
                    gvPermUsuario.DataBind();
                }
                else
                {
                    sqlQuery = "SELECT " +
                                "t.id_tela, " +
                                "t.desc_tela, " +
                                "t.hyperlink_nome, " +
                                "CONVERT(BIT, 0) as ativo " +
                            "FROM " +
                                "tela t ";
                    objLista = _dbContext.Database.SqlQuery<listaTela>(sqlQuery).ToList();

                    gvPermUsuario.DataSource = objLista;
                    gvPermUsuario.DataBind();
                }
            }
            else
            {
                gvPermUsuario.DataSource = null;
                gvPermUsuario.DataBind();
            }
        }

        protected void gvPermUsuario_RowCreated(object sender, GridViewRowEventArgs e)
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
    }
}