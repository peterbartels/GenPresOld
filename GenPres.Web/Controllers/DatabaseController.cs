﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using GenPres.Assembler;
using GenPres.Business.Domain.Databases;
using GenPres.Business.Service;
using StructureMap;

namespace GenPres.Controllers
{
    public class DatabaseController : Controller
    {
        //
        // GET: /Database/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Settings.SettingsManager.Instance.Initialize(HttpContext.ApplicationInstance.Server.MapPath("~/"));
            GenPresApplication.Initialize();
        }

        public ActionResult GetDatabases()
        {
            IEnumerable<String> names = GetDatabaseServices().GetDatabases();
            IList<object> list = new List<object>();
            foreach (var name in names)
            {
                list.Add(new {DatabaseName = name});
            }           
            return this.Direct(list);
        }

        public ActionResult SaveDatabaseRegistration(String databaseName, String machine, String genPresConnectionString, String pdmsConnectionString, String genFormWebService)
        {
            SetSetting(machine, databaseName, genPresConnectionString, pdmsConnectionString, genFormWebService);
            return this.Direct(new {success = true, databaseName});
        }


        public Boolean SetSetting(String computerName, String name, String genPresConnectionString, String pdmsConnectionString, String genFormWebService)
        {
            var setting = CreateSettings(computerName, name, genPresConnectionString, pdmsConnectionString, genFormWebService);
            GetDatabaseServices().MapSettingsPath(HttpContext.ApplicationInstance.Server.MapPath("~/"));
            GetDatabaseServices().RegisterDatabaseSetting(setting);

            return true;
        }

        private static IDatabaseServices GetDatabaseServices()
        {
            return ObjectFactory.GetInstance<IDatabaseServices>();
        }

        private static IDatabaseSetting CreateSettings(String computerName, String name, String genPresConnectionString, String pdmsConnectionString, String genFormWebService)
        {
            var setting = ObjectFactory.GetInstance<IDatabaseSetting>();
            setting.GenPresConnectionString = genPresConnectionString;
            setting.PdmsConnectionString = pdmsConnectionString;
            setting.GenFormWebservice = genFormWebService;
            setting.Name = name;
            setting.Machine = computerName;

            return setting;
        }


    }
}
