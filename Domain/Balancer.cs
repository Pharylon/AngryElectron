using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class Balancer : IBalancer
    {
        public IEquation Balance(IEquation unbalancedEquation)
        {
            List<IParsableSymbols> listOfSymbols = generateListOfSymobols(unbalancedEquation);
            throw new NotImplementedException();
        }

        private List<IParsableSymbols> generateListOfSymobols(IEquation unbalancedEquation)
        {
            throw new NotImplementedException();
        }
    }
}
