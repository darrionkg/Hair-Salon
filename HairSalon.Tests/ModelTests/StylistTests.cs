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

    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=darrion_gering_test;";
    }

    [TestMethod]
    public void Constructor_CreatesConstructorWithProperties_Object()
    {
      Stylist testStylist = new Stylist("name", "stylist description");
      Assert.AreEqual(typeof(Stylist), testStylist.GetType());
    }

    [TestMethod]
    public void Constructor_AssignNameProperty_String()
    {
      Stylist testStylist = new Stylist("name", "stylist description");
      Stylist testStylist2 = new Stylist("name", "stylist description");
      Assert.AreEqual(testStylist2.GetName(), "name");
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      string name = "test stylist";
      Stylist newStylist = new Stylist(name, "description");
      string result = newStylist.GetName();
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_List()
    {
      List<Stylist> test = new List<Stylist> {};
      CollectionAssert.AreEqual(test, Stylist.GetListOfStylists());
    }


    [TestMethod]
    public void GetListOfStylists_ReturnsStylists_String()
    {
      Stylist stylist1 = new Stylist("Stylez", "The best at everything");
      stylist1.Save();
      Stylist stylist2 = new Stylist("Dave", "Im good");
      stylist2.Save();
      List<Stylist> testStylists = new List<Stylist> {stylist1, stylist2};
      List<Stylist> result = Stylist.GetListOfStylists();
      Assert.AreEqual(testStylists[0].GetName(), result[0].GetName());
    }

    [TestMethod]
    public void Save_SavesToDatabase_StylistList()
    {
      Stylist testStylist = new Stylist("test stylist", "test description");
      testStylist.Save();
      List<Stylist> result = Stylist.GetListOfStylists();
      List<Stylist> testList = new List<Stylist>{testStylist};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Stylist testStylist = new Stylist("test stylist", "test description");

      testStylist.Save();
      Stylist savedStylist = Stylist.GetListOfStylists()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_ReturnsCorrectStylistFromDatabase_Stylist()
    {
      Stylist testStylist = new Stylist("test stylist", "test description");
      testStylist.Save();

      Stylist foundStylist = Stylist.Find(testStylist.GetId());
      Console.WriteLine(foundStylist.GetId());
      Assert.AreEqual(testStylist.GetId(), foundStylist.GetId());
    }
  }
}
