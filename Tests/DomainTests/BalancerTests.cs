using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngryElectron.Domain;
using System.Collections.Generic;

namespace AngryElectron.Tests.Domain
{
    [TestClass]
    public class BalancerTest
    {
        Balancer myBalancer;
        IEquation myEquation;
        Parser myParser;

        [TestInitialize]
        public void Inititalize()
        {
            myBalancer = new Balancer();
            myParser = new Parser();
            myEquation = myParser.Parse("CaCl2 + Ag(NO3) -> Ca(NO3)2 + AgCl");
        }

        [TestMethod]
        public void BalancerTest1()
        {
            myEquation = myBalancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToString() == "K4Fe(CN6) + KMnO4 + H2SO4 --> KHSO4 + Fe2(SO4)3 +MnSO4 + HNO3 + CO2 + H2O");
        }

        [TestMethod]
        public void BalancerTest2()
        {
            //listOfSymbols = myBalancer.generateListOfSymobols(myEquation);
            //myBalancer.checkForValidEquation(listOfSymbols, myEquation);
        }
    }
}
