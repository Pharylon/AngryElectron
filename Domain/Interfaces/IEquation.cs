using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public interface IEquation : IParsableSymbols
    {
        IParsableSymbols Reactants { get; }
        IParsableSymbols Products { get; }
        int MoleculeCount { get; }
        String ToString();
    }
}
