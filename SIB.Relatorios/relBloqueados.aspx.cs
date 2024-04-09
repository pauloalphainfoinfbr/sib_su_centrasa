using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using SIB.Data;

namespace SIB.Relatorios
{
    public partial class relBloqueados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            validaPermissao perm = new validaPermissao();

            if (!perm.verificaPermissao(Convert.ToInt32(Session["id"]), 8))
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
                    strScript += "$(\"#lstItemRelBloqueados\").attr(\"class\", \"active\");";
                    strScript += "});";
                    strScript += "</script>";

                    ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
                }


                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                conn.Open();
                string sqlQuery = "";
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);


                txtDatInicio.Text = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
                txtDatFim.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //Tipo Veículo
                List<tipo_veiculo> listaTipoVeiculo = new List<tipo_veiculo>();
                listaTipoVeiculo = _dbContext.tipo_veiculo.OrderBy(r => r.desc_tipo_veiculo).ToList();
                listaTipoVeiculo.Add(new tipo_veiculo() { desc_tipo_veiculo = "Todos" });

                ddlTipoVeiculo.DataSource = listaTipoVeiculo;
                ddlTipoVeiculo.DataValueField = "id_tipo_veiculo";
                ddlTipoVeiculo.DataTextField = "desc_tipo_veiculo";
                ddlTipoVeiculo.DataBind();
                ddlTipoVeiculo.SelectedIndex = ddlTipoVeiculo.Items.Count - 1;

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