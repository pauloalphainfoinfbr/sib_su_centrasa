using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.Entity;
using System.Data;
using SIB.Data;

namespace SIB.Relatorios
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["id"] != null)
                {
                    Session.Clear();
                }
            }
        }

        protected void entrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nomeUsuario.Text))
            {
                msg.InnerText = "Campo Nome de Usuário obrigatório.";
            }
            else if (string.IsNullOrWhiteSpace(senha.Text))
            {
                msg.InnerText = "Campo Senha obrigatório.";
            }
            else
            {
                try
                {
                    string senhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(senha.Text, "SHA1");

                    Data.UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
                    var usuario = _dbContext.usuario.Where(r => r.login == nomeUsuario.Text && r.senha == senhaCriptografada && (r.id_tipo_usuario == 1 || r.id_tipo_usuario == 3)).ToList();

                    if (usuario.Count == 0)
                    {
                        msg.InnerText = "Usuário ou senha inválidos.";
                    }
                    else
                    {
                        Session["nome"] = usuario[0].nome_usuario;
                        Session["id"] = usuario[0].id_usuario;
                        Session["id_empresa"] = usuario[0].id_empresa;

                        FormsAuthentication.RedirectFromLoginPage(usuario[0].login, false);
                        //Server.Transfer("Default.aspx", true);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {

        }
    }
}