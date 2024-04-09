using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using SIB.Data;

namespace SIB.Relatorios
{    
    public partial class relCheckList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                validaPermissao perm = new validaPermissao();

                if (!perm.verificaPermissao(Convert.ToInt32(Session["id"]), 11)){
                    Response.Redirect("Unauthorized.aspx");
                }

                if (!ClientScript.IsStartupScriptRegistered("script1"))
                {
                    string strScript = "<script type=\"text/javascript\">";
                    strScript += "$(function (e) {";
                    strScript += "$(\"li\").removeClass(\"active\");";
                    strScript += "$(\"#lstItemRelacaoCheckList\").attr(\"class\", \"active\");";
                    strScript += "});";
                    strScript += "</script>";

                    ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
                }                

                txtDatInicio.Text = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
                txtDatFim.Text = DateTime.Now.ToString("dd/MM/yyyy");
                
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                conn.Open();
                string sqlQuery = "";
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);

                //Transportadora
                DataTable dtTransportadora = new System.Data.DataTable();
                sqlQuery = "select id_empresa, nome_fantasia from empresa where id_tipo_empresa = 2 order by nome_fantasia";
                da.SelectCommand.CommandText = sqlQuery;
                da.Fill(dtTransportadora);
                DataRow drTransp = dtTransportadora.NewRow();
                drTransp["id_empresa"] = 0;
                drTransp["nome_fantasia"] = "Todos";
                dtTransportadora.Rows.Add(drTransp);

                ddlTransportadora.DataSource = dtTransportadora;
                ddlTransportadora.DataValueField = "id_empresa";
                ddlTransportadora.DataTextField = "nome_fantasia";
                ddlTransportadora.DataBind();
                ddlTransportadora.SelectedIndex = ddlTransportadora.Items.Count - 1;

                //Somente Usuários Que são da Transportadora
                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
                Int32 id_usuario = Convert.ToInt32(Session["id"]);
                var usuario = _dbContext.usuario.Where(r => r.id_usuario == id_usuario).ToList()[0];

                if (usuario.id_tipo_usuario == 3)
                {
                    ddlTransportadora.SelectedValue = usuario.id_transportadora.ToString();
                    ddlTransportadora.Enabled = false;
                }
            }
        }
    }
}