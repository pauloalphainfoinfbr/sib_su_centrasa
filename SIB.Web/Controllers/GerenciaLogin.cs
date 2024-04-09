using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Transactions;
using SIB.Data;

namespace SIB.Web.Controllers
{
    public class GerenciaLogin
    {
        //public bool vefificaLogado(int id_user, HttpContextBase HttpContext)
        //{
        //    bool jaLogado = false;

        //    for (int i = 0; i < HttpContext.Application.Contents.Count; i++)
        //    {
        //        var userList = HttpContext.Application.Contents[i];

        //        if (id_user == ((SIB.Data.usuario)userList).id_usuario)
        //        {
        //            jaLogado = true;
        //            break;
        //        }
        //    }

        //    return jaLogado;
        //}

        public string retornaSessionID(string sessionID, HttpContextBase HttpContext)
        {
            string id_session = "";

            for (int i = 0; i < HttpContext.Application.Contents.Count; i++)
            {
                var userList = HttpContext.Application.Contents[i];

                if (sessionID == (string)userList)
                {
                    id_session = (string)userList;
                    break;
                }
            }

            return id_session;
        }        

        public void deslogaUsuarioApp(string sessionID, HttpContextBase HttpContext)
        {
            //Remove usuario do Application
            for (int i = 0; i < HttpContext.Application.Contents.Count; i++)
            {
                var user = HttpContext.Application.Contents[i];

                if (sessionID == (string)user)
                {
                    HttpContext.Application.RemoveAt(i);
                }
            }
        }
    }
}