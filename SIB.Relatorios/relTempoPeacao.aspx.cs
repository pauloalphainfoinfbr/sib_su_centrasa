using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIB.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SIB.Relatorios
{
    public partial class relTempoPeacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            validaPermissao perm = new validaPermissao();

            if (!perm.verificaPermissao(Convert.ToInt32(Session["id"]), 14))
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
                    strScript += "$(\"#lstItemTempoPeacao\").attr(\"class\", \"active\");";
                    strScript += "});";
                    strScript += "</script>";

                    ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
                }

                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                txtDatInicio.Text = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
                txtDatFim.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //Combo Transportadora
                List<empresa> listaTransportadora = new List<empresa>();
                listaTransportadora = _dbContext.empresa.Where(r => r.id_tipo_empresa == 2).OrderBy(r => r.nome_fantasia).ToList();
                listaTransportadora.Add(new empresa() { nome_fantasia = "Todos" });

                ddlTransportadora.DataSource = listaTransportadora;
                ddlTransportadora.DataValueField = "id_empresa";
                ddlTransportadora.DataTextField = "nome_fantasia";
                ddlTransportadora.DataBind();

                Int32 id_usuario = Convert.ToInt32(Session["id"]);
                var usuario = _dbContext.usuario.Where(r => r.id_usuario == id_usuario).ToList()[0];

                if (usuario.id_tipo_usuario == 3)
                {
                    ddlTransportadora.SelectedValue = usuario.id_transportadora.ToString();
                    ddlTransportadora.Enabled = false;
                }
                else
                {
                    ddlTransportadora.SelectedIndex = ddlTransportadora.Items.Count - 1;
                }
            }
        }
    }
}