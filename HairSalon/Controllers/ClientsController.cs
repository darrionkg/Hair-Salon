using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{

    public class ClientsController : Controller
    {
      [HttpGet("/stylists/{id}/clients")]
      public ActionResult Index(Stylist stylist)
      {
        List<Client> clientList = stylist.GetListOfClients();
        return View(clientList);
      }

      [HttpGet("/stylists/{stylistId}/clients/new")]
      public ActionResult New(int stylistId)
      {
        Stylist foundStylist = Stylist.Find(stylistId);
        Console.WriteLine(foundStylist);
        return View(foundStylist);
      }

      // [HttpPost("/stylist/{stylistId}/clients/new")]
      // public ActionResult Create(string clientName, int stylistId)
      // {
      //   return RedirectToAction("/stylists/stylistId");
      // }

    //   // LIST OF CLIENTS
    //   [HttpGet("/stylists/{id}")]
    //   public ActionResult Show(int id)
    //   {
    //     Stylist foundStylist = Stylist.Find(id);
    //     //move GetListOfClients to Client Class
    //     List<Client> clientList = foundStylist.GetListOfClients();
    //     return View(clientList);
    //   }

    }

}
