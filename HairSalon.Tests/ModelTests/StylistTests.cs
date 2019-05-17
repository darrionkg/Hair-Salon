using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=darrion_gering_test;";
    }

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

    [TestMethod]
    public void GetAll_ReturnsEmptyList_List()
    {
      List<Stylist> test = new List<Stylist> {};
      CollectionAssert.AreEqual(test, Stylist.GetListOfStylists());
    }

    [TestMethod]
    public void GetListOfStylists_ReturnsStylists_List()
    {
      Stylist stylist1 = new Stylist("Stylez", "The best at everything");
      stylist1.Save();
      Stylist stylist2 = new Stylist("Dave", "Im good");
      stylist2.Save();
      List<Stylist> testStylists = new List<Stylist> {};
      testStylists.Add(stylist1);
      testStylists.Add(stylist2);
      List<Stylist> result = Stylist.GetListOfStylists();
      CollectionAssert.AreEqual(testStylists, result);
    }
  }
}
