using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
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
      Client testClient = new Client("name", 1);
      Assert.AreEqual(typeof(Client), testClient.GetType());
    }

    [TestMethod]
    public void Constructor_AssignNameProperty_String()
    {
      Client testClient = new Client("name", 1);
      Client testClient2 = new Client("name", 1);
      Assert.AreEqual(testClient2.GetName(), "name");
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      string name = "test client";
      Client newClient = new Client(name, 2);
      string result = newClient.GetName();
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_List()
    {
      Stylist stylistToTest = new Stylist("test", "description");
      List<Client> test = new List<Client> {};
      CollectionAssert.AreEqual(test, Client.GetListOfClients(stylistToTest));
    }

    [TestMethod]
    public void GetListOfClients_ReturnsClients_String()
    {
      Stylist stylistToTest = new Stylist("test", "description");
      stylistToTest.Save();
      Client client1 = new Client("Stylez", stylistToTest.GetId());
      client1.Save();
      Client client2 = new Client("Dave", stylistToTest.GetId());
      client2.Save();
      List<Client> testClients = new List<Client> {client1, client2};
      List<Client> result = Client.GetListOfClients(stylistToTest);
      Assert.AreEqual(testClients[0].GetName(), result[0].GetName());
    }

    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      Stylist stylistToTest = new Stylist("test", "description");
      stylistToTest.Save();
      Client testClient = new Client("test client", stylistToTest.GetId());
      testClient.Save();
      List<Client> testList = new List<Client>{testClient};
      List<Client> result = Client.GetListOfClients(stylistToTest);

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Stylist stylistToTest = new Stylist("test", "description");
      stylistToTest.Save();
      Client testClient = new Client("test client", stylistToTest.GetId());

      testClient.Save();
      Client savedClient = Client.GetListOfClients(stylistToTest)[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectClientFromDatabase_Client()
    {
      Client testClient = new Client("test client", 1);
      testClient.Save();

      Client foundClient = Client.Find(testClient.GetId());
      Console.WriteLine(foundClient.GetId());
      Assert.AreEqual(testClient.GetId(), foundClient.GetId());
    }

    [TestMethod]
    public void Delete_DeletesAClient()
    {
      Stylist stylistToTest = new Stylist("test", "description");
      stylistToTest.Save();
      Client testClient = new Client("test stylist", 1);
      testClient.Save();

      testClient.Delete();

      List<Client> allClients = Client.GetListOfClients(stylistToTest);
      List<Client> result = new List<Client> {};
      CollectionAssert.AreEqual(allClients, result);
    }

    [TestMethod]
    public void Edit_EditsAClientsName()
    {
      // Stylist stylistToTest = new Stylist("test", "description");
      // stylistToTest.Save();

      Client testClient = new Client("test client", 1);
      testClient.Save();
      Console.WriteLine(testClient.GetId());
      testClient.EditName("name", "new name");

      Assert.AreEqual(testClient.GetName(), "new name");
    }

    // [TestMethod]
    // public void Edit_EditsAClientsStylistId()
    // {
    //   Client testClient = new Client("test stylist", 1);
    //
    //   testClient.EditStylistId("stylist_id", 2);
    //
    //   Assert.AreEqual(testClient.GetStylistId(), 2);
    // }
  }
}
