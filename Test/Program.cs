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

            try
            {
                myEquation = myParser.Parse("HCl + Na -> NaCl + H2");
                writeEquation(myEquation);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(ex.ToString());
            }


            try
            {
                myEquation = myParser.Parse("CaCl2 + AgNO3 -> Ca(NO3)2 + AgCl");
                writeEquation(myEquation);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(ex.ToString());
            }


            try
            {
                myEquation = myParser.Parse("CaCl2 + 3AgNO3 -> Ca(NO3)2 + AgCl");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(ex.ToString());
            }


            try
            {
                myEquation = myParser.Parse("CaCl2 + AgNO3 -> Caz(NO3)2 + AgCl");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(ex.ToString());
            }


            Console.ReadKey();
        }

        private static void writeEquation(IEquation myEquation)
        {
            ChemicalEquation chem = (ChemicalEquation)myEquation;
            foreach (string s in chem.ParsableSymbols)
                Console.Write(s);
            Console.Write(Environment.NewLine);
        }

    }
}
