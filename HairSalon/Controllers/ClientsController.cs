using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{

    public class ClientsController : Controller
    {
      [HttpGet("/stylists/{id}/clients")]
      public ActionResult Index(Stylist stylist, int id)
      {
        //Console.WriteLine(Client.GetListOfClients(stylist).Count);
        //List<Client> clientList = Client.GetListOfClients();
        Stylist foundStylist = Stylist.Find(id);
        return View(foundStylist);
      }

      [HttpGet("/stylists/{stylistId}/clients/new")]
      public ActionResult New(int stylistId)
      {
        Stylist foundStylist = Stylist.Find(stylistId);
        return View(foundStylist);
      }

      [HttpPost("/stylists/{id}/clients")]
      public ActionResult Create(string clientName, int id)
      {
        Stylist foundStylist = Stylist.Find(id);
        Client newClient = new Client(clientName, id);
        newClient.Save();
        return RedirectToAction("Index", foundStylist);
      }

      [HttpGet("/clients/delete")]
      public ActionResult Delete()
      {

        return View();
      }

      [HttpPost("/clients/deleteClient")]
      public ActionResult Delete(string clientId)
      {
        int intClientId = int.Parse(clientId);
        Client foundClient = Client.Find(intClientId);
        foundClient.Delete();
        return Redirect("~/stylists");
      }

      [HttpPost("/clients/deleteAll")]
      public ActionResult DeleteAll()
      {
        Client.DeleteAll();
        return Redirect("~/stylists");
      }

      [HttpGet("/clients")]
      public ActionResult ViewAll()
      {
        return View();
      }
    }

}
