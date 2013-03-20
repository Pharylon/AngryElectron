using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AngryElectron.Domain
{
    class Parser : IParser
    {
        public IEnumerable<string> ParsableSymbols
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEquation Parse(string reaction)
        {
            if (!validateString(reaction))
            {
                //Do whatever we do when the string isn't valid.
            }

            //This should take our validly formatted string and turn it int an array of strings, each one containing a symbol or operator.
            StringBuilder sb = new StringBuilder();
            foreach (char letter in reaction)
            {
                if ((Char.IsUpper(letter) || Char.IsNumber(letter) || letter == '+' || letter == '>') && sb.Length > 0)
                    sb.Append(",");
                sb.Replace(" ", "");
                sb.Append(letter);
            }
            sb.Replace(" ", "");
            reaction = sb.ToString();
            string[] reactionComponents = reaction.Split(',');


            //OK, now that we have our array of reaction parts, let's loop through it and build our chemical equation.
            foreach (string component in reactionComponents)
            {

            }



            //This is some regex code I found online for seperating capital letters. It has problems, though, and I don't know regular expressions. 
            //I may try to fix this later, though, so I'll keep it commented out here.
            //var regex = new Regex(@"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            //regex.Replace(reaction, ",");

            





            throw new NotImplementedException();
        }

        private bool validateString(string reaction)
        {
            return true; //Currently, no logic for this, it just says all inputs are valid.
        }
    }
}
