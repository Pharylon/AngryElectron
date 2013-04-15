using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class Controller
    {
        public ChemicalEquation Solve(string userInput) 
        {
            Parser myParser = new Parser();
            Balancer myBalancer = new Balancer();
            ChemicalEquation myChemicalEquation;
            myChemicalEquation =  myParser.Parse(userInput);
            myChemicalEquation = myBalancer.Balance(myChemicalEquation); 
            return myChemicalEquation;
        }
    }
}
