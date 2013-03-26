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
            writeParsableSymbols("HCl + Na -> NaCl + H2");

            writeParsableSymbols("CaCl2 + AgNO3 -> Ca(NO3)2 + AgCl");

            writeParsableSymbols("CaCl2 + 3AgNO3 -> Ca(NO3)2 + AgCl");

            writeParsableSymbols("CaCl2 + AgNO3 -> Caz(NO3)2 + AgCl");


            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press any key to end debugging...");
            Console.ReadKey();
        }

        private static void writeParsableSymbols(string myString)
        {
            Parser myParser = new Parser();
            IEquation myEquation = new ChemicalEquation();
            try 
            { 
                myEquation = myParser.Parse(myString);
                ChemicalEquation chem = (ChemicalEquation)myEquation;
                foreach (string s in chem.ParsableSymbols)
                    Console.Write(s);
                Console.Write(Environment.NewLine);
            }
            catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
        }
    }
}
