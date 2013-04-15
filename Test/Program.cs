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
            //writeBalancedEquation("Na2CO3 + HCl -> NaCl + H2O + CO2");
            writeBalancedEquation("FeS2 + O2 -> Fe2O3 + SO2");
            //writeBalancedEquation("SnO2 + H2 -> Sn + H2O");
            //writeBalancedEquation("H2 + O2 -> H2O");
            writeBalancedEquation("Fe2(SO4)3 + KOH -> K2SO4 + Fe(OH)3");
            //writeBalancedEquation("KMnO4 + HCl = KCl + MnCl2 + H2O + Cl2");

            //writeParsableSymbols("2Co2 + H2O -> C6H12O6 + O2");

            //writeParsableSymbols("HCl + Na -> NaCl + H2");

            //writeParsableSymbols("CaCl2 + Ag(NO3) -> Ca(NO3)2 + AgCl");

            //writeParsableSymbols("CaCl2 + 3AgNO3 -> Ca(NO3)2 + AgCl");

            //writeParsableSymbols("CaCl2 + AgNO3 -> Ca(NxO3)2 + AgCl");

            //writeParsableSymbols("CH4+O2 --> CO2 +H2O");

            //writeParsableSymbols("Na+H2O > NaOH + 2+H");

            //writeParsableSymbols("SiCl4 + H2O = ZnCl2+H2");

            //writeParsableSymbols("Al[OH]3+ H2SO4 -> Al2[SO4]3 + H2O");

            Balancer myBalancer = new Balancer();

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press any key to end debugging...");
            Console.ReadKey();
        }

        private static void writeParsableSymbols(string myString)
        {
            Parser myParser = new Parser();
            ChemicalEquation myEquation;
            try
            {
                myEquation = myParser.Parse(myString);
                //ChemicalEquation chem = (ChemicalEquation)myEquation;
                foreach (string s in myEquation.ParsableSymbols)
                    Console.Write(s);
                Console.Write(Environment.NewLine);
            }
            catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
        }

        private static void writeBalancedEquation(string myString)
        {
            Parser myParser = new Parser();
            Balancer myBalancer = new Balancer();
            ChemicalEquation myEquation;
            //try
            //{
                myEquation = myParser.Parse(myString);
                myEquation = myBalancer.Balance(myEquation);
                Console.WriteLine(myEquation.ToString());
                Console.WriteLine(myEquation.ToHTML());
                Console.Write(Environment.NewLine);
            //}
            //catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
        }
    }
}
