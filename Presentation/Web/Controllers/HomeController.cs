using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngryElectron.Domain;

namespace AngryElectron.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public string GetBalancedEquation(string unbalancedEquation)
        {
            ChemicalEquation myEquation;
            string returnString = string.Empty;
            try
            {
                myEquation = Parser.Parse(unbalancedEquation);
                myEquation = Balancer.Balance(myEquation);
                returnString = myEquation.ToHTML();
            }
            catch (Exception ex)
            {
                returnString = ex.Message;
            }
            return returnString;
        }
    }
}
