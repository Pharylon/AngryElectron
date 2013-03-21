using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class ChemicalEquation : IEquation
    {
        //ChemicalEquation will hold two lists - the Reactants and Products. Each one will be a list that implements
        //IParsableSymbols. Therefore, list items may be an element or an ElementGroup.

        public List<IParsableSymbols> Reactants = new List<IParsableSymbols>();
        public List<IParsableSymbols> Products = new List<IParsableSymbols>();
    }
}
