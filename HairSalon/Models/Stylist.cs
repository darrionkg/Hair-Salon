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
      cmd.CommandText = @"INSERT INTO `stylists` (`name`, `description`, `hire_date`) VALUES ('"+_name+"', '"+_description+"', CURRENT_TIMESTAMP);";
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
      Stylist foundStylist = new Stylist();
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string stylistDescription = rdr.GetString(2);
        DateTime timestamp = rdr.GetDateTime(3);
        foundStylist.SetId(stylistId);
        foundStylist.SetName(stylistName);
        foundStylist.SetDescription(stylistDescription);
        foundStylist.SetTimestamp(timestamp);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      Stylist test = new Stylist();
      return foundStylist;
    }

    public void Edit(string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET description = '"+newDescription+"' WHERE id = '"+_id+"';";
      cmd.ExecuteNonQuery();
      _description = newDescription;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM stylists WHERE id = '"+_id+"';";
        cmd.ExecuteNonQuery();
        if (conn != null)
        {
          conn.Close();
      }
    }


    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool nameEquality = this.GetName().Equals(newStylist.GetName());
        bool idEquality = this.GetId().Equals(newStylist.GetId());
        bool descriptionEquality = this.GetDescription().Equals(newStylist.GetDescription());
        bool timestampEquality = this.GetTimestamp().Equals(newStylist.GetTimestamp());
        if(nameEquality == true && idEquality == true && descriptionEquality == true && timestampEquality == true)
        {
          return nameEquality;
        }
      return false;
      }
    }
  }
}
