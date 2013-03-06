using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngryElectron.Domain;

namespace AngryElectron.Presentation.Web.Controllers
{
    public class ElementController : Controller
    {
        //
        // GET: /Element/

        public ActionResult Index()
        {
            //Element hydrogen = new Element("H");
            return View();
        }

    }
}
