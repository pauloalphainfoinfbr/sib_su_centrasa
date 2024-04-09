using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System.Data.Entity.Validation;
using System.Web.Script.Services;
using System.Data.EntityModel;
using System.Data.Objects;
using System.Web.Services;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIB.Data;

namespace SIB.Relatorios
{
    public partial class cadEmpresa : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            validaPermissao perm = new validaPermissao();
            if (!perm.verificaPermissao(Convert.ToInt32(Session["id"]), 2))
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
                    strScript += "$(\"#lstItemCadEmp\").attr(\"class\", \"active\");";
                    strScript += "});";
                    strScript += "</script>";

                    ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
                }

                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();                

                var est = (
                            from
                                uf
                            in
                                _dbContext.estado
                            select new { uf.id_estado, uf.desc_estado }
                          ).ToList();                

                cmbEstado.DataSource = est;
                cmbEstado.DataTextField = "desc_estado";
                cmbEstado.DataValueField = "id_estado";
                cmbEstado.DataBind();
                cmbEstado.SelectedIndex = -1;  
                
                int idEmpresa = !string.IsNullOrWhiteSpace(Request.QueryString["idEmpresa"]) ? Convert.ToInt32(Request.QueryString["idEmpresa"].ToString()) : 0;                

                if(_dbContext.empresa.Where(r => r.id_empresa == idEmpresa).ToList().Count > 0)
                {
                    var empresa = _dbContext.empresa.Where(r => r.id_empresa == idEmpresa).ToList()[0];
                    
                    txtRazaoSocial.Text = empresa.razao_social;
                    txtNomeFantasia.Text = empresa.nome_fantasia;
                    txtEmail.Text = empresa.email;

                    int id_estado = 0;

                    if (empresa.endereco1.ToList().Count > 0)
                    {
                        endereco objEndereco = empresa.endereco1.ElementAt(0);
                        txtEnder.Text = objEndereco.logradrouro;
                        txtBairro.Text = objEndereco.bairro;
                        txtCep.Text = objEndereco.cep;
                        txtTelefone.Text = objEndereco.telefone;

                        int idCidade = Convert.ToInt32(objEndereco.id_cidade);

                        id_estado = Convert.ToInt32(_dbContext.cidade.Where(r => r.id_cidade == idCidade).ToList()[0].id_estado);
                    }                    

                    cmbEstado.SelectedValue = id_estado.ToString();

                    cmbCidade.DataSource = _dbContext.cidade.Where(r => r.id_estado == id_estado).OrderBy(r => r.desc_cidade).ToList();
                    cmbCidade.DataTextField = "desc_cidade";
                    cmbCidade.DataValueField = "id_cidade";
                    cmbCidade.DataBind();
                    cmbCidade.SelectedValue = empresa.endereco1.ToList().Count > 0 ? empresa.endereco1.ElementAt(0).id_cidade.ToString() : "0";  
                    
                    
                }
            }
        }
        protected void gravar_Click(object sender, EventArgs e)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

            int idEmpresa = !string.IsNullOrWhiteSpace(Request.QueryString["idEmpresa"]) ? Convert.ToInt32(Request.QueryString["idEmpresa"].ToString()) : 0;                

            if(string.IsNullOrWhiteSpace(txtRazaoSocial.Text) || string.IsNullOrWhiteSpace(txtNomeFantasia.Text)
            || Convert.ToInt32(cmbEstado.SelectedValue) < 1 || Convert.ToInt32(cmbCidade.SelectedValue) < 1
            || string.IsNullOrWhiteSpace(txtEnder.Text) || string.IsNullOrWhiteSpace(txtBairro.Text)
            || string.IsNullOrWhiteSpace(txtCep.Text) || string.IsNullOrWhiteSpace(txtTelefone.Text)
            )
            {
                lblMensagem.Text = "Todos os campos são obrigatórios.";
            }
            else
            {
                empresa emp = new empresa();
                emp.razao_social = txtRazaoSocial.Text;
                emp.nome_fantasia = txtNomeFantasia.Text;
                emp.email = txtEmail.Text;
                emp.id_tipo_empresa = 1;

                endereco end = new endereco();
                end.logradrouro = txtEnder.Text;
                end.bairro = txtBairro.Text;
                end.id_cidade = Convert.ToInt32(cmbCidade.SelectedValue);
                end.cep = txtCep.Text.Replace(".","").Replace("-","");
                end.telefone = txtTelefone.Text.Replace("(","").Replace(")","").Replace("-","");                                    

                if (_dbContext.empresa.Where(r => r.id_empresa == idEmpresa).ToList().Count == 0)
                {
                    emp.endereco1.Add(end);
                    _dbContext.empresa.Add(emp);
                }
                else
                {
                    emp.id_empresa = idEmpresa;
                    end.id_empresa = idEmpresa;

                    var emp_anterior = _dbContext.empresa.Where(r => r.id_empresa == idEmpresa).ToList();
                    var entityKeyEmpresa = _dbContext.empresa.Create().GetType().GetProperty("id_empresa").GetValue(emp_anterior[0]);
                    _dbContext.Entry(_dbContext.Set<Data.empresa>().Find(entityKeyEmpresa)).CurrentValues.SetValues(emp);

                    if (_dbContext.endereco.Where(r => r.id_empresa == idEmpresa).ToList().Count == 0)
                    {
                        _dbContext.endereco.Add(end);
                    }
                    else
                    {
                        end.id_endereco = _dbContext.endereco.Where(r => r.id_empresa == idEmpresa).ToList()[0].id_endereco;

                        var ender_anterior = _dbContext.endereco.Where(r => r.id_empresa == idEmpresa).ToList();
                        var entityKeyEndereco = _dbContext.endereco.Create().GetType().GetProperty("id_endereco").GetValue(ender_anterior[0]);
                        _dbContext.Entry(_dbContext.Set<Data.endereco>().Find(entityKeyEndereco)).CurrentValues.SetValues(end);
                    }
                }

                try
                {
                    _dbContext.SaveChanges();
                    Response.Redirect("edtEmpresa.aspx");
                }
                catch(DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
        }        

        protected void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

            int idEstado = Convert.ToInt32(cmbEstado.SelectedValue);

            cmbCidade.DataSource = _dbContext.cidade.Where(r => r.id_estado == idEstado).OrderBy(r => r.desc_cidade).ToList();
            cmbCidade.DataTextField = "desc_cidade";
            cmbCidade.DataValueField = "id_cidade";
            cmbCidade.DataBind();
        }
    }
}