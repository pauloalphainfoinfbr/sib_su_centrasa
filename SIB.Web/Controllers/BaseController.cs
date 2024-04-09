using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIB.Data;

namespace SIB.Web.Controllers
{
    public class BaseController : Controller
    {
        public UsiminasIpatingaEntities _db = new UsiminasIpatingaEntities();

        public Data.usuario Usuario
        {
            get
            {
                if (Session["usuario"] == null)
                {
                    return new Data.usuario();
                }
                else
                {
                    return (Data.usuario)Session["usuario"];
                }
            }
            set
            {
                if (value != null) Session["usuario"] = value;
            }
        }

        public Data.condutor Condutor
        {
            get
            {
                if (Session["condutor"] == null)
                {
                    return new Data.condutor();
                }
                else
                {
                    return (Data.condutor)Session["condutor"];
                }
            }
            set
            {
                if (value != null) Session["condutor"] = value;
            }
        }
        public Data.veiculo Veiculo
        {
            get
            {
                if (Session["veic"] == null)
                {
                    return new Data.veiculo();
                }
                else
                {
                    return (Data.veiculo)Session["veic"];
                }
            }
            set
            {
                if (value != null) Session["veic"] = value;   
            }
        }

        public Data.checklist CheckList
        {
            get
            {
                if (Session["chk"] == null)
                {
                    return new Data.checklist();
                }
                else
                {
                    return (Data.checklist)Session["chk"];
                }
            }
            set
            {
                if (value != null)
                {
                    Session["chk"] = value;
                }
            }
        }

    }
}
