﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIB.Data;

namespace SIB.Relatorios
{
    public class listaEmpresa
    {
        public int id_empresa { get; set; }
        public string nome_fantasia { get; set; }
        public string desc_cidade { get; set; }
        public string logradrouro { get; set; }
    }

    public partial class edtEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            validaPermissao perm = new validaPermissao();
            if (!perm.verificaPermissao(Convert.ToInt32(Session["id"]), 29))
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
                    strScript += "$(\"#lstItemEdtEmp\").attr(\"class\", \"active\");";
                    strScript += "});";
                    strScript += "</script>";

                    ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
                }

                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                //Popula o Gridview

                string sqlQuery = "SELECT " +
	                              "emp.id_empresa, " +
	                              "emp.nome_fantasia, " +
	                              "cid.desc_cidade, " +
	                              "ender.logradrouro " +
                                  "FROM " +
	                              "empresa emp " +
                                  "LEFT JOIN " +
	                              "endereco ender " +
                                  "ON " +
	                              "emp.id_empresa = ender.id_empresa " +
                                  "LEFT JOIN " +
	                              "cidade cid " +
                                  "ON " +
	                              "ender.id_cidade = cid.id_cidade " +
                                  "WHERE " +
	                              "emp.id_tipo_empresa = 1 ";

                List<listaEmpresa> listaEmpresa = _dbContext.Database.SqlQuery<listaEmpresa>(sqlQuery).ToList();

                gvEmpresa.DataSource = listaEmpresa;
                gvEmpresa.DataBind();
            }
        }

        protected void gvEmpresa_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvEmpresa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)gvEmpresa.Rows[index];
                int idEmpresa = Convert.ToInt32(row.Cells[2].Text);

                Response.Redirect("cadEmpresa.aspx?idEmpresa=" + idEmpresa);
            }
            else if (e.CommandName == "apagar")
            {
                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)gvEmpresa.Rows[index];
                int idEmrpesa = Convert.ToInt32(row.Cells[2].Text);
                
                RestricaoFk objRestricao = new RestricaoFk();

                if (!objRestricao.verificaRestricaoEmpresaEndereco("empresa", idEmrpesa))
                {
                    var endereco = _dbContext.empresa.Where(r => r.id_empresa == idEmrpesa).ToList()[0].endereco1.ToList()[0];

                    _dbContext.endereco.Remove(endereco);

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

        protected void gvEmpresa_RowCreated(object sender, GridViewRowEventArgs e)
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