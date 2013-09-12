using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngryElectron.Domain;
using System.Collections.Generic;

namespace AngryElectron.Tests.Domain
{
    [TestClass]
    public class BalancerTest
    {
        ChemicalEquation myEquation;

        [TestMethod]
        public void BasicBalancerTest()
        {
            myEquation = Parser.Parse("FeS2 + O2 -> Fe2O3 + SO2");
            myEquation = Balancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToString() == "4FeS2 + 11O2 -> 2Fe2O3 + 8SO2");
        }

        [TestMethod]
        public void BalanceH2O()
        {
            myEquation = Parser.Parse("H2O -> H + O");
            myEquation = Balancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToString() == "H2O -> 2H + O");
        }

        [TestMethod]
        public void BalancerHTMLTest()
        {
            myEquation = Parser.Parse("FeS2 + O2 -> Fe2O3 + SO2");
            myEquation = Balancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToHTML() == "4FeS<sub>2</sub> + 11O<sub>2</sub> -> 2Fe<sub>2</sub>O<sub>3</sub> + 8SO<sub>2</sub>");
        }

        [TestMethod]
        public void BalancerWithComplexTest()
        {
            myEquation = Parser.Parse("CaCl2 + Ag(NO3) -> Ca(NO3)2 + AgCl");
            myEquation = Balancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToString() == "CaCl2 + 2Ag(NO3) -> Ca(NO3)2 + 2AgCl");
        }

        [TestMethod]
        public void BalanceLongProblem()
        {
            myEquation = Parser.Parse("B2Br6 + HNO3 -> B(NO3)3 + HBr");
            myEquation = Balancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToString() == "B2Br6 + 6HNO3 -> 2B(NO3)3 + 6HBr");
        }

        [TestMethod]
        public void BalancerHTMLWithComplexTest()
        {
            myEquation = Parser.Parse("CaCl2 + Ag(NO3) -> Ca(NO3)2 + AgCl");
            myEquation = Balancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToHTML() == "CaCl<sub>2</sub> + 2Ag(NO<sub>3</sub>) -> Ca(NO<sub>3</sub>)<sub>2</sub> + 2AgCl");
        }

        [TestMethod]
        public void BalancerWithLoneElementTest()
        {
            myEquation = Parser.Parse("HCl + Na = NaCl + H2");
            myEquation = Balancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToString() == "2HCl + 2Na -> 2NaCl + H2");
        }

        [TestMethod]
        public void BalancerWithLoneElementTestReversed()
        {
            myEquation = Parser.Parse("NaCl + H2 = HCl + Na");
            myEquation = Balancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToString() == "2NaCl + H2 -> 2HCl + 2Na");
        }

        [TestMethod]
        public void BalancerHTMLWithLoneElementTest()
        {
            myEquation = Parser.Parse("HCl + Na = NaCl + H2");
            myEquation = Balancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToHTML() == "2HCl + 2Na -> 2NaCl + H<sub>2</sub>");
        }

        [TestMethod]
        public void UnusualInputTest()
        {
            myEquation = Parser.Parse("CaCl2 + Ag[NO3] ==> Ca[NO3]2 + AgCl");
            myEquation = Balancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToString() == "CaCl2 + 2Ag(NO3) -> Ca(NO3)2 + 2AgCl");
        }

        [TestMethod]
        public void IsBalancedTest()
        {
            myEquation = Parser.Parse("FeS2 + O2 -> Fe2O3 + SO2");
            myEquation = Balancer.Balance(myEquation);
            Assert.IsTrue(myEquation.IsBalanced);
        }
    }
}
