using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDispose
  {
      public void Dispose()
    {
      Client.ClearAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=darrion_gering_test;";
    }

    [TestMethod]
    public void Constructor_CreatesConstructorWithProperties_Object()
    {
      Client testClient = new Client("name", "stylist description");
      Assert.AreEqual(typeof(Client), testClient.GetType());
    }

    [TestMethod]
    public void Constructor_AssignNameProperty_String()
    {
      Client testClient = new Client("name", "stylist description");
      Client testClient2 = new Client("name", "stylist description");
      Assert.AreEqual(testClient2.GetName(), "name");
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      string name = "test stylist";
      Client newClient = new Client(name, "description");
      string result = newClient.GetName();
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_List()
    {
      List<Client> test = new List<Client> {};
      CollectionAssert.AreEqual(test, Client.GetListOfClients());
    }


    [TestMethod]
    public void GetListOfClients_ReturnsClients_String()
    {
      Client stylist1 = new Client("Stylez", "The best at everything");
      stylist1.Save();
      Client stylist2 = new Client("Dave", "Im good");
      stylist2.Save();
      List<Client> testClients = new List<Client> {stylist1, stylist2};
      List<Client> result = Client.GetListOfClients();
      Assert.AreEqual(testClients[0].GetName(), result[0].GetName());
    }

    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      Client testClient = new Client("test stylist", "test description");
      testClient.Save();
      List<Client> result = Client.GetListOfClients();
      List<Client> testList = new List<Client>{testClient};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Client testClient = new Client("test stylist", "test description");

      testClient.Save();
      Client savedClient = Client.GetListOfClients()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_ReturnsCorrectClientFromDatabase_Client()
    {
      Client testClient = new Client("test stylist", "test description");
      testClient.Save();

      Client foundClient = Client.Find(testClient.GetId());
      Console.WriteLine(foundClient.GetId());
      Assert.AreEqual(testClient.GetId(), foundClient.GetId());
    }


  }
}
