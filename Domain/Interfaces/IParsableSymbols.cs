using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public interface IParsableSymbols
    {
        IEnumerable<String> ParsableSymbols { get; }
        List<String> ListOfElements { get; }
        int GetDeepElementCount(string key);
        String ToHTML();
    }
}
