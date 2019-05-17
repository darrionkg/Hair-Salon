using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private string _name;
    private int _id;
    private int _stylistId;

    public Client()
    {

    }

    public Client(string name, int stylistId)
    {
      _name = name;
      _stylistId = stylistId;
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

    public int GetStylistId()
    {
      return _stylistId;
    }

    public void SetStylistId(int stylistId)
    {
      _stylistId = stylistId;
    }

    public static List<Client> GetListOfClients(Stylist stylist)
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = '"+stylist.GetId()+"';";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      Console.WriteLine(stylist.GetId());
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
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `clients` (`name`, `stylist_id`) VALUES ('"+_name+"', '"+_stylistId+"');";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }


}
