using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private string _description;
    //private List<Client> _listOfClients;

    private static List<Stylist> _listOfStylists = new List<Stylist> {};

    public Stylist(string name, string description)
    {
      _id = _listOfStylists.Count;
      _name = name;
      _description = description;
      _listOfStylists.Add(this);
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string name)
    {
      _name = name;
    }

    public int GetId()
    {
      return _id;
    }

    public void SetId(int id)
    {
      _id = id;
    }

    public string GetDescription()
    {
      return _description;
    }

    public void SetDescription(string description)
    {
      _description = description;
    }

    public static List<Stylist> GetListOfStylists()
    {
      return _listOfStylists;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `stylists` (`name`, `description`, `hiredate`) VALUES (''"+_name+"'', ''"+_description+"'', CURRENT_TIMESTAMP);";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }



}
