using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIB.Relatorios
{
    public partial class admEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ClientScript.IsStartupScriptRegistered("script1"))
            {

                string strScript = "<script type=\"text/javascript\">";
                strScript += "$(function (e) {";
                strScript += "$(\"li\").removeClass(\"active\");";
                strScript += "$(\"ul li:eq(13)\").attr(\"class\", \"active\");";
                strScript += "});";
                strScript += "</script>";

                ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (!ClientScript.IsStartupScriptRegistered("script1"))
            {

                string strScript = "<script type=\"text/javascript\">";
                strScript += "$(function (e) {";
                strScript += "$(\"li\").removeClass(\"active\");";
                strScript += "$(\"ul li:eq(13)\").attr(\"class\", \"active\");";
                strScript += "});";
                strScript += "</script>";

                ClientScript.RegisterStartupScript(GetType(), "script1", strScript);
            }

            if (Convert.ToInt32(ddlEmp.SelectedValue) == 1)
            {
                Response.Redirect("Home.aspx");
            }
        }
    }
}