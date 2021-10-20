using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BOMJ_Simulator.BusinessLogic;

namespace BOMJ_Simulator.Tests
{
    [TestClass]
    public class PersonTests
    {
        Person person = new Person { Eda = 100, Son = 100, Money = 100 };

        //Еда
        [TestMethod()]
        public void SubstractEda_20()
        {
            Assert.IsTrue(person.SubstractEda(20));
        }
        [TestMethod()]
        public void SubstractEda_120()
        {
            Assert.IsFalse(person.SubstractEda(120));
        }

        [TestMethod()]
        public void SubstractEda_minus20()
        {
            Assert.IsFalse(person.SubstractEda(-20));
        }

        //Сон
        [TestMethod()]
        public void SubstractSon_20()
        {
            Assert.IsTrue(person.SubstractSon(20));
        }

        [TestMethod()]
        public void SubstractSon_120()
        {
            Assert.IsFalse(person.SubstractSon(120));
        }

        [TestMethod()]
        public void SubstractSon_minus20()
        {
            Assert.IsFalse(person.SubstractSon(-20));
        }

        //Деньги
        [TestMethod()]
        public void SubstractMoney_20()
        {
            Assert.IsTrue(person.SubstractMoney(20));
        }

        [TestMethod()]
        public void SubstractMoney_120()
        {
            Assert.IsFalse(person.SubstractMoney(120));
        }

        [TestMethod()]
        public void SubstractMoney_minus20()
        {
            Assert.IsFalse(person.SubstractMoney(-20));
        }
    }
}