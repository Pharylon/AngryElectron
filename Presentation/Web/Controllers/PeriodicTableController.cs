using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngryElectron.Domain;
using System.IO;

namespace AngryElectron.Presentation.Web.Controllers
{
    public class PeriodicTableController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Json = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/TableOfElements.json"));
            return View();
        }

    }
}
