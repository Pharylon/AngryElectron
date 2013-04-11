using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class Solver
    {
        public IEquation Solve(string userInput) //Once the balancer works, this will send it to through the balancer before returning it.
        {
            Parser myParser = new Parser();
            IEquation myChemicalEquation =  myParser.Parse(userInput);
            return myChemicalEquation;
        }
    }
}
