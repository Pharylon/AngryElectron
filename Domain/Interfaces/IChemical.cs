using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public interface IChemical
    {
        IEnumerable<String> ParsableSymbols { get; }
        String ToHTML();
        int Coefficient { get; set; }
    }
}
