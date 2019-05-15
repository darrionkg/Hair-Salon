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
    private DateTime _timestamp;
    private List<Client> _listOfClients = new List<Client> {};


    private static List<Stylist> _listOfStylists = new List<Stylist> {};

    public Stylist()
    {

    }

    public Stylist(string name, string description)
    {
      _id = _listOfStylists.Count;
      _name = name;
      _description = description;
      _listOfStylists.Add(this);
    }

    public Stylist(string name, string description, int id, DateTime timestamp)
    {
      _id = id;
      _name = name;
      _description = description;
      _timestamp = timestamp;
      _listOfStylists.Add(this);
    }

    public static void ClearAll()
    {
      _listOfStylists.Clear();
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

    public DateTime GetTimestamp()
    {
      return _timestamp;
    }

    public void SetTimestamp(DateTime timestamp)
    {
      _timestamp = timestamp;
    }

    public static List<Stylist> GetListOfStylists()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        Stylist newStylist = new Stylist();
        newStylist.SetId(rdr.GetInt32(0));
        newStylist.SetName(rdr.GetString(1));
        newStylist.SetDescription(rdr.GetString(2));
        newStylist.SetTimestamp(rdr.GetDateTime(3));
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `stylists` (`name`, `description`, `hiredate`) VALUES ('"+_name+"', '"+_description+"', CURRENT_TIMESTAMP);";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `stylists` WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      rdr.Read();
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string stylistDescription = rdr.GetString(2);
        DateTime timestamp = rdr.GetDateTime(3);
        Stylist foundStylist = new Stylist(stylistName, stylistDescription, stylistId, timestamp);
        return foundStylist;
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      Stylist test = new Stylist();
      return test;
    }

    public List<Client> GetListOfClients()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylistId = '_id';";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        Client newClient = new Client();
        newClient.SetId(rdr.GetInt32(0));
        newClient.SetName(rdr.GetString(1));
        newClient.SetStylistId(rdr.GetInt32(2));
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
  }
}
