﻿using System.Web.Mvc;
using GenPres.Assembler;
using GenPres.Data;
using GenPres.Data.Connections;

namespace GenPres.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //GenPresApplication.Initialize();
            //SessionManager.Instance.InitSessionFactory(DatabaseConnection.DatabaseName.GenPres, false);
            Settings.SettingsManager.Instance.Initialize(HttpContext.ApplicationInstance.Server.MapPath("~/"));
        }
    }
}