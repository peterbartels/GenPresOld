﻿using System.Web.Mvc;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class TestsController : Controller
    {
        //
        // GET: /Tests/

        public ActionResult Index()
        {
            ViewBag.Title = "GenPres Tests";
            return View();
        }
        public ActionResult ValueField()
        {
            ViewBag.Title = "GenPres Tests";
            return View();
        }
    }
}