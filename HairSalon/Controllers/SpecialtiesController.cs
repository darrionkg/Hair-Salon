using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{

    public class SpecialtiesController : Controller
    {
      [HttpGet("/stylists/specialties")]
      public ActionResult Index()
      {
        List<Specialty> allSpecialties = Specialty.GetListOfSpecialties();
        return View(allSpecialties);
      }

      [HttpPost("/stylists/specialties")]
      public ActionResult Create(string specialtyType)
      {
        Specialty newSpecialty = new Specialty(specialtyType);
        newSpecialty.Save();
        List<Specialty> allSpecialties = Specialty.GetListOfSpecialties();
        return RedirectToAction("Index");
      }

      [HttpGet("/stylists/specialties/new")]
      public ActionResult New()
      {
        return View();
      }

      [HttpGet("/stylists/specialties/delete")]
      public ActionResult Delete()
      {
        return View();
      }

      [HttpPost("/stylists/specialties/deleteSpecialty")]
      public ActionResult Delete(string specialtyId)
      {
        int intSpecialtyId = int.Parse(specialtyId);
        Specialty foundSpecialty = Specialty.Find(intSpecialtyId);
        foundSpecialty.Delete();
        List<Specialty> allSpecialties = Specialty.GetListOfSpecialties();
        return RedirectToAction("Index", allSpecialties);
      }

      [HttpPost("/stylists/specialties/deleteAll")]
      public ActionResult DeleteAll()
      {
        Specialty.DeleteAll();
        return RedirectToAction("Index");
      }

      [HttpGet("/stylists/specialties/edit")]
      public ActionResult Edit()
      {
        return View();
      }

      [HttpPost("/stylists/specialties/editSpecialty")]
      public ActionResult Edit(string specialtyId, string newSpecialty)
      {
        int intSpecialtyId = int.Parse(specialtyId);
        Specialty foundSpecialty = Specialty.Find(intSpecialtyId);
        foundSpecialty.Edit(newSpecialty);
        List<Specialty> allSpecialties = Specialty.GetListOfSpecialties();
        return RedirectToAction("Index");
      }

      [HttpGet("/stylists/specialties/{id}")]
      public ActionResult Show(int id)
      {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Specialty selectedSpecialty = Specialty.Find(id);
          List<Stylist> specialtyStylists = selectedSpecialty.GetStylists();
          List<Stylist> allStylists = Stylist.GetListOfStylists();
          model.Add("specialty", selectedSpecialty);
          model.Add("specialtyStylists", specialtyStylists);
          model.Add("allStylists", allStylists);
          return View(model);
      }

      [HttpPost("/stylists/specialties/{specialtyId}/stylists/new")]
      public ActionResult AddStylist(int specialtyId, int stylistId)
      {
        Specialty specialty = Specialty.Find(specialtyId);
        Stylist stylist = Stylist.Find(stylistId);
        specialty.AddStylist(stylist);
        return RedirectToAction("Show",  new { id = specialtyId });
      }
    }

}
