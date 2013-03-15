using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterContactTracker
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PersonId { get; set; }
        public int AddressId { get; set; }

        public Person(string fname, string lname, int pid, int aid)
        {
            FirstName = fname;
            LastName = lname;
            PersonId = pid;
            AddressId = aid;
        }
        public override string ToString()
        {
            return LastName + "," + FirstName ;
        }
    }
}
