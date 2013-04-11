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
        List<string> listOfSymbols;

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
            listOfSymbols = myBalancer.generateListOfSymobols(myEquation);
        }

        [TestMethod]
        public void BalancerTest2()
        {
            listOfSymbols = myBalancer.generateListOfSymobols(myEquation);
            myBalancer.checkForValidEquation(listOfSymbols, myEquation);
        }
    }
}
