using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIB.Data;

namespace SIB.Relatorios
{
    public partial class CadCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            validaPermissao perm = new validaPermissao();
            if (!perm.verificaPermissao(Convert.ToInt32(Session["id"]), 4))
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
                    strScript += "$(\"ul li:eq(4)\").attr(\"class\", \"active\");";
                    strScript += "});";
                    strScript += "</script>";

                    ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
                }

                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                int idEmpresa = !string.IsNullOrWhiteSpace(Request.QueryString["idCliente"]) ? Convert.ToInt32(Request.QueryString["idCliente"].ToString()) : 0;

                if (_dbContext.empresa.Where(r => r.id_empresa == idEmpresa).ToList().Count > 0)
                {
                    var empresa = _dbContext.empresa.Where(r => r.id_empresa == idEmpresa).ToList()[0];

                    txtCodigo.Text = empresa.codigo;
                    txtNome.Text = empresa.nome_fantasia;
                }
            }
        }

        protected void cadastrar_Click(object sender, EventArgs e)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

            int idEmpresa = !string.IsNullOrWhiteSpace(Request.QueryString["idCliente"]) ? Convert.ToInt32(Request.QueryString["idCliente"].ToString()) : 0;

            Int32 codigo;

            if(string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNome.Text))
            {
                lblMensagem.Text = "Todos os campos são obrigatórios.";
            }
            else if (txtCodigo.Text.Length > 10)
            {
                lblMensagem.Text = "O código deve conter no máximo 10 números.";
            }
            else if(!Int32.TryParse(txtCodigo.Text, out codigo))
            {
                lblMensagem.Text = "O código deve conter apenas números.";
            }            
            else
            {
                empresa objEmpresa = new empresa();
                objEmpresa.id_tipo_empresa = 3;
                objEmpresa.nome_fantasia = txtNome.Text;
                objEmpresa.codigo = txtCodigo.Text;

                if (_dbContext.empresa.Where(r => r.id_empresa == idEmpresa).ToList().Count == 0)
                {
                    _dbContext.empresa.Add(objEmpresa);
                }
                else
                {
                    objEmpresa.id_empresa = idEmpresa;

                    var emp_anterior = _dbContext.empresa.Where(r => r.id_empresa == idEmpresa).ToList();
                    var entityKeyEmpresa = _dbContext.empresa.Create().GetType().GetProperty("id_empresa").GetValue(emp_anterior[0]);
                    _dbContext.Entry(_dbContext.Set<Data.empresa>().Find(entityKeyEmpresa)).CurrentValues.SetValues(objEmpresa);
                }

                _dbContext.SaveChanges();

                Response.Redirect("edtCliente.aspx");
            }
        }
    }
}