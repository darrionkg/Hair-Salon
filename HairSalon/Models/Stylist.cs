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

    public Stylist()
    {

    }

    public Stylist(string name, string description, int id = 0)
    {
      _name = name;
      _description = description;
      _id = id;
    }

    public Stylist(string name, string description, DateTime timestamp, int id = 0)
    {
      _name = name;
      _description = description;
      _timestamp = timestamp;
      _id = 0;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
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
      cmd.CommandText = @"INSERT INTO `stylists` (`id`, `name`, `description`, `hire_date`) VALUES ('"+_id+"', '"+ _name+"', '"+_description+"', CURRENT_TIMESTAMP);";
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
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
      cmd.CommandText = @"SELECT * FROM `stylists` WHERE id = '"+id+"';";
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
      return foundStylist;
    }

    public void EditName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = '"+newName+"' WHERE id = '"+_id+"';";
      cmd.ExecuteNonQuery();
      _name = newName;
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

    public static void DeleteAll()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE * FROM stylists;";
        cmd.ExecuteNonQuery();
        if (conn != null)
        {
          conn.Close();
        }
    }

    public List<Specialty> GetSpecialties()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT specialties.* FROM stylists JOIN stylists_specialties ON (stylists.id = stylists_specialties.stylist_id) JOIN specialties ON (stylists_specialties.specialty_id = specialties.id) WHERE stylists.id = '"+_id+"';";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Specialty> specialties = new List<Specialty>{};
            while(rdr.Read())
            {
                int specialtyId = rdr.GetInt32(0);
                string specialtyType = rdr.GetString(1);
                Specialty newSpecialty = new Specialty(specialtyType, specialtyId );
                specialties.Add(newSpecialty);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return specialties;
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
        if(nameEquality == true && idEquality == true && descriptionEquality == true)
        {
          return true;
        }
      return false;
      }
    }
  }
}
