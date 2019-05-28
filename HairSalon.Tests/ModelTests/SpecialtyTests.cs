using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTest : IDisposable
  {
    public void Dispose()
    {
      Specialty.ClearAll();
    }

    public SpecialtyTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=darrion_gering_test;";
    }

    [TestMethod]
    public void Constructor_CreatesConstructorWithProperties_Object()
    {
      Specialty testSpecialty = new Specialty("specialty type");
      Assert.AreEqual(typeof(Specialty), testSpecialty.GetType());
    }

    [TestMethod]
    public void Constructor_AssignTypeProperty_String()
    {
      Specialty testSpecialty = new Specialty("specialty type");
      Specialty testSpecialty2 = new Specialty("specialty type");
      Assert.AreEqual(testSpecialty2.GetSpecialtyType(), "specialty type");
    }

    [TestMethod]
    public void GetSpecialtyType_ReturnsType_String()
    {
      string specialtyType = "test specialty type";
      Specialty newSpecialty = new Specialty(specialtyType);
      string result = newSpecialty.GetSpecialtyType();
      Assert.AreEqual(specialtyType, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_List()
    {
      List<Specialty> test = new List<Specialty> {};
      CollectionAssert.AreEqual(test, Specialty.GetListOfSpecialties());
    }


    [TestMethod]
    public void GetListOfSpecialties_ReturnsSpecialties_String()
    {
      Specialty specialtyType1 = new Specialty("barber");
      specialtyType1.Save();
      Specialty specialtyType2 = new Specialty("color");
      specialtyType2.Save();
      List<Specialty> testSpecialties = new List<Specialty> {specialtyType1, specialtyType2};
      List<Specialty> result = Specialty.GetListOfSpecialties();
      Assert.AreEqual(testSpecialties[0].GetSpecialtyType(), result[0].GetSpecialtyType());
    }

    [TestMethod]
    public void Save_SavesToDatabase_SpecialtyList()
    {
      Specialty testSpecialty = new Specialty("test specialty type");
      testSpecialty.Save();
      List<Specialty> result = Specialty.GetListOfSpecialties();
      List<Specialty> testList = new List<Specialty>{testSpecialty};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Specialty testSpecialty = new Specialty("test specialty type");

      testSpecialty.Save();
      Specialty savedSpecialty = Specialty.GetListOfSpecialties()[0];

      int result = savedSpecialty.GetId();
      int testId = testSpecialty.GetId();

      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectSpecialtyFromDatabase_Specialty()
    {
      Specialty testSpecialty = new Specialty("test specialty type");
      testSpecialty.Save();

      Specialty foundSpecialty = Specialty.Find(testSpecialty.GetId());
      Assert.AreEqual(testSpecialty.GetId(), foundSpecialty.GetId());
    }

    [TestMethod]
    public void Delete_DeletesASpecialty()
    {
      Specialty testSpecialty = new Specialty("test specialty type");
      testSpecialty.Save();

      testSpecialty.Delete();

      List<Specialty> allSpecialties = Specialty.GetListOfSpecialties();
      List<Specialty> result = new List<Specialty> {};
      CollectionAssert.AreEqual(allSpecialties, result);
    }

    [TestMethod]
    public void Edit_EditsASpecialtyDescription()
    {
      Specialty testSpecialty = new Specialty("test specialty type");
      testSpecialty.Save();

      testSpecialty.Edit("new specialty type");

      Assert.AreEqual(testSpecialty.GetSpecialtyType(), "new specialty type");
    }
  }
}
