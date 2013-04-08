using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngryElectron.Domain;

namespace AngryElectron.Presentation.Web.Controllers
{
    public class PeriodicTableController : Controller
    {
        public ActionResult Index()
        {
            //TableOfElements theElements = new TableOfElements();
            //return View(theElements.Json);
            return View();
        }

    }
}
