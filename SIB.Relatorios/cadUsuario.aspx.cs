using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SIB.Data;

namespace SIB.Relatorios
{
    public partial class cadUsuario : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            validaPermissao perm = new validaPermissao();
            if (!perm.verificaPermissao(Convert.ToInt32(Session["id"]), 1)) {
                Response.Redirect("Unauthorized.aspx");
            }

            if (!Page.IsPostBack)
            {
                if (!ClientScript.IsStartupScriptRegistered("script1"))
                {

                    string strScript = "<script type=\"text/javascript\">";
                    strScript += "$(function (e) {";
                    strScript += "$(\"li\").removeClass(\"active\");";
                    strScript += "$(\"#lstItemCadusu\").attr(\"class\", \"active\");";
                    strScript += "});";
                    strScript += "</script>";

                    ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
                }

                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();                

                ddlTransportadora.DataSource = _dbContext.empresa.Where(r => r.id_tipo_empresa == 2).OrderBy(r => r.nome_fantasia).ToList();
                ddlTransportadora.DataValueField = "id_empresa";
                ddlTransportadora.DataTextField = "nome_fantasia";
                ddlTransportadora.DataBind();                

                int idUsuario = !string.IsNullOrWhiteSpace(Request.QueryString["idUsuario"]) ? Convert.ToInt32(Request.QueryString["idUsuario"].ToString()) : 0;

                if (idUsuario > 0)
                {                    
                    if (_dbContext.usuario.Where(r => r.id_usuario == idUsuario).ToList().Count > 0)
                    {
                        var usuario = _dbContext.usuario.Where(r => r.id_usuario == idUsuario).ToList()[0];

                        if (usuario.id_tipo_usuario == 1){
                            rdbTipoUsuarioEmpresa.Checked = true;
                            ddlTransportadora.Enabled = false;
                        }
                        else if (usuario.id_tipo_usuario == 2){
                            rdbTipoUsuarioPocket.Checked = true;
                            ddlTransportadora.Enabled = false;
                        }
                        else if (usuario.id_tipo_usuario == 3){
                            rdbTipoUsuarioTransportadora.Checked = true;
                            ddlTransportadora.Enabled = true;
                        }
                        
                        ddlTransportadora.SelectedValue = usuario.id_transportadora != null ? usuario.id_transportadora.ToString() : "0";
                        txtNome.Text = usuario.nome_usuario;
                        txtLogin.Text = usuario.login;                        
                    }
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

            int idUsuario = !string.IsNullOrWhiteSpace(Request.QueryString["idUsuario"]) ? Convert.ToInt32(Request.QueryString["idUsuario"]) : 0;

            if ( (!rdbTipoUsuarioEmpresa.Checked && !rdbTipoUsuarioPocket.Checked && !rdbTipoUsuarioTransportadora.Checked)
               || string.IsNullOrWhiteSpace(txtNome.Text) 
               || string.IsNullOrWhiteSpace(txtLogin.Text)               
               )
            {
                lblMensagem.Text = "Todos os campos são obrigatórios.";
            }
            else if (rdbTipoUsuarioTransportadora.Checked && Convert.ToInt32(ddlTransportadora.SelectedValue) == 0){
                lblMensagem.Text = "Deve ser informada a transportadora do usuário.";
            }
            else if (idUsuario == 0 && (string.IsNullOrWhiteSpace(txtSenha.Text) || string.IsNullOrWhiteSpace(txtRepetirSenha.Text))){
                lblMensagem.Text = "Campo senha é obrigatório.";
            }
            else if (txtSenha.Text != txtRepetirSenha.Text){
                lblMensagem.Text = "As senhas não conferem.";
            }
            else if ( (_dbContext.usuario.Where(r => r.login == txtLogin.Text).ToList().Count > 0) && idUsuario == 0 ){
                lblMensagem.Text = "Já existe um usúario com este login cadastrado no sistema.";
            }
            else
            {                
                usuario objUsuario = new usuario();

                if (rdbTipoUsuarioEmpresa.Checked)
                    objUsuario.id_tipo_usuario = 1;
                else if (rdbTipoUsuarioPocket.Checked)
                    objUsuario.id_tipo_usuario = 2;
                else if (rdbTipoUsuarioTransportadora.Checked)
                    objUsuario.id_tipo_usuario = 3;

                objUsuario.nome_usuario = txtNome.Text;
                objUsuario.login = txtLogin.Text;
                objUsuario.id_empresa = 1;

                if (rdbTipoUsuarioTransportadora.Checked){
                    objUsuario.id_transportadora = Convert.ToInt32(ddlTransportadora.SelectedValue);
                }

                if (idUsuario == 0){
                    objUsuario.senha = FormsAuthentication.HashPasswordForStoringInConfigFile(txtSenha.Text, "SHA1");
                }
                else {
                    if (!string.IsNullOrWhiteSpace(txtSenha.Text)){
                        objUsuario.senha = FormsAuthentication.HashPasswordForStoringInConfigFile(txtSenha.Text, "SHA1");
                    }
                    else{
                        objUsuario.senha = _dbContext.usuario.Where(r => r.id_usuario == idUsuario).ToList()[0].senha;
                    }
                }                

                if (idUsuario == 0){
                    _dbContext.usuario.Add(objUsuario);
                }
                else{
                    objUsuario.id_usuario = idUsuario;

                    var usu_anterior = _dbContext.usuario.Where(r => r.id_usuario == idUsuario).ToList();
                    var entityKeyUser = _dbContext.usuario.Create().GetType().GetProperty("id_usuario").GetValue(usu_anterior[0]);
                    _dbContext.Entry(_dbContext.Set<Data.usuario>().Find(entityKeyUser)).CurrentValues.SetValues(objUsuario);
                }

                _dbContext.SaveChanges();
                Response.Redirect("edtUsuario.aspx");
            }
        }

        protected void rdbTipoUsuarioEmpresa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTipoUsuarioTransportadora.Checked)
                ddlTransportadora.Enabled = true;
            else
                ddlTransportadora.Enabled = false;
        }

        protected void rdbTipoUsuarioTransportadora_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTipoUsuarioTransportadora.Checked)
                ddlTransportadora.Enabled = true;
            else
                ddlTransportadora.Enabled = false;
        }

        protected void rdbTipoUsuarioPocket_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTipoUsuarioTransportadora.Checked)
                ddlTransportadora.Enabled = true;
            else
                ddlTransportadora.Enabled = false;
        }
    }
}