using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngryElectronDomain;

namespace AngryElectron.Controllers
{
    public class ElementController : Controller
    {
        //
        // GET: /Element/

        public ActionResult Index()
        {
            Element hydrogen = new Element("H");
            return View(hydrogen);
        }

    }
}
