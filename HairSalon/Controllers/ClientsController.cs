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

      [HttpGet("/stylists/{stylistId}/clients/delete")]
      public ActionResult Delete(int stylistId)
      {
        Stylist foundStylist = Stylist.Find(stylistId);
        return View(foundStylist);
      }

      [HttpPost("/clients/deleteClient")]
      public ActionResult Delete(string clientId)
      {
        int intClientId = int.Parse(clientId);
        Client foundClient = Client.Find(intClientId);
        foundClient.Delete();
        return RedirectToAction("Index", "Home");
      }

      [HttpPost("/clients/deleteAll")]
      public ActionResult DeleteAll()
      {
        Client.DeleteAll();
        return Redirect("~/clients");
      }

      // [HttpGet("/clients")]
      // public ActionResult ViewAll()
      // {
      //   return View();
      // }
      //
      [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
      public ActionResult Show(int clientId)
      {
        Client foundClient = Client.Find(clientId);
        return View(foundClient);
      }

      [HttpPost("/clients/updateName")]
      public ActionResult Edit(int clientId, string newName)
      {
        Client foundClient = Client.Find(clientId);
        foundClient.EditName(newName);
        return RedirectToAction("Index", "Home");
      }

      [HttpPost("/clients/updateStylistId")]
      public ActionResult UpdateStylistId(int clientId, string newStylistId)
      {
        int intNewStylistId = int.Parse(newStylistId);
        Client foundClient = Client.Find(clientId);
        foundClient.EditStylistId(intNewStylistId);
        return RedirectToAction("Index", "Home");
      }

    }

}
