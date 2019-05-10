using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTest : IDisposable
    {

        public void Dispose()
        {
            Stylist.ClearAll();
        }

        [TestMethod]
        public void Constructor_CreatesConstructorWithProperties_Object()
        {
          Stylist testStylist = new Stylist("name", "stylist description");
          Assert.AreEqual(typeof(Stylist), testStylist.GetType());
        }

        [TestMethod]
        public void Constructor_AssignsIds_Int()
        {
          Stylist testStylist = new Stylist("name", "stylist description");
          Stylist testStylist2 = new Stylist("name", "stylist description");
          Assert.AreEqual(testStylist2.GetId(), 1);
        }

        [TestMethod]
        public void Constructor_AssignNameProperty_String()
        {
          Stylist testStylist = new Stylist("name", "stylist description");
          Stylist testStylist2 = new Stylist("name", "stylist description");
          Assert.AreEqual(testStylist2.GetName(), "name");
        }



    }
}
