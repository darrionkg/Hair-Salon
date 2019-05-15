using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{

    public class StylistsController : Controller
    {
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
  }

}
