using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{

    public class StylistsController : Controller
    {
      [HttpGet("/owner")]
      public ActionResult OwnerSettings()
      {
        return View();
      }

      [HttpGet("/stylists")]
      public ActionResult Index()
      {
        List<Stylist> allStylists = Stylist.GetListOfStylists();
        //problem with adding multiple instances
        return View(allStylists);
      }

      [HttpPost("/stylists")]
      public ActionResult Create(string stylistName, string stylistDescription)
      {
        Stylist newStylist = new Stylist(stylistName, stylistDescription);
        newStylist.Save();
        return RedirectToAction("Index");
      }

      [HttpGet("/stylists/new")]
      public ActionResult New()
      {
        return View();
      }

      [HttpGet("/stylists/delete")]
      public ActionResult Delete()
      {

        return View();
      }

      [HttpPost("/stylists/deleteStylist")]
      public ActionResult Delete(string stylistId)
      {
        int intStylistId = int.Parse(stylistId);
        Stylist foundStylist = Stylist.Find(intStylistId);
        foundStylist.Delete();
        return RedirectToAction("Index");
      }

      [HttpPost("/stylists/deleteAll")]
      public ActionResult DeleteAll()
      {
        Stylist.DeleteAll();
        return RedirectToAction("Index");
      }

      // [HttpPost("/stylists/edit")]
      // public ActionResult Edit()
      // {
      //   return View();
      // }
  }

}
