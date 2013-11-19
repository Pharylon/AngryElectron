using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public enum Side
    {
        LeftSide,
        RightSide,
    }

    public static class Extensions
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static string TrimInternal(this string myString)
        {
            while (myString.Contains("  ")) 
                myString = myString.Replace("  ", " ");
            return myString;
        }
    }

    class ChemicalComparer : IComparer<IChemical>
    {
        public int Compare(IChemical x, IChemical y)
        {
            if (y == x)
                return 0;
            else if (y.ToString() == "C")
                return 1;
            else if (y.ToString() == "H")
            {
                if (x.ToString() == "C")
                    return -1;
                else
                    return 1;
            }
            else if (y is Complex)
                return -1;
            else
                return string.Compare(x.ToString(), y.ToString());
        }
    }
}
