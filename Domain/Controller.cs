using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class Controller
    {
        ChemicalEquation myChemicalEquation;
        
        public ChemicalEquation Solve(string userInput) 
        {
            myChemicalEquation =  Parser.Parse(userInput);
            myChemicalEquation = Balancer.Balance(myChemicalEquation); 
            return myChemicalEquation;
        }
    }
}
