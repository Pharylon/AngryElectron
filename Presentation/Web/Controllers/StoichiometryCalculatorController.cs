using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngryElectron.Domain;

namespace AngryElectron.Presentation.Web.Controllers
{
    public class StoichiometryCalculatorController : Controller
    {
        //
        // GET: /Stoichiometry/

        public ActionResult Index()
        {

            return View();
        }

        public string GetBalancedEquation(string unbalancedEquation)
        {
            Balancer myBalancer;
            ChemicalEquation myEquation;
            Parser myParser;
            myBalancer = new Balancer();
            myParser = new Parser();
            myEquation = myParser.Parse(unbalancedEquation);
            myEquation = myBalancer.Balance(myEquation);
            return myEquation.ToHTML();
        }
    }
}
