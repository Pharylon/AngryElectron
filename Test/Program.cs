using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngryElectron.Domain;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IEquation myEquation;
            Parser myParser = new Parser();
            //myEquation = myParser.Parse("HCl + Na -> NaCl + H2");
            myEquation = myParser.Parse("CaCl2 + AgNO3 -> Ca(NO3)2 + AgCl");
            ChemicalEquation chem = (ChemicalEquation)myEquation;
            foreach (string s in chem.ParsableSymbols)
                Console.Write(s);
            Console.ReadKey();
        }
    }
}
