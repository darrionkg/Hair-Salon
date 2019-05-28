using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
    private string _specialtyType;

    public Specialty()
    {

    }

    public Specialty(string specialtyType, int id = 0)
    {
      _specialtyType = specialtyType;
      _id = id;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public int GetId()
    {
      return _id;
    }

    public void SetId(int id)
    {
      _id = id;
    }

    public string GetSpecialtyType()
    {
      return _specialtyType;
    }

    public void SetSpecialtyType(string specialtyType)
    {
      _specialtyType = specialtyType;
    }

    public static List<Specialty> GetListOfSpecialties()
    {
      List<Specialty> allSpecialties = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
         Specialty newSpecialty = new Specialty();
         newSpecialty.SetId(rdr.GetInt32(0));
         newSpecialty.SetSpecialtyType(rdr.GetString(1));
         allSpecialties.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `specialties` (`id`,`specialty_type`) VALUES ('"+_id+"', '"+_specialtyType+"');";
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `specialties` WHERE id = '"+id+"';";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      Specialty foundSpecialty = new Specialty();
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistSpecialtyType = rdr.GetString(1);
        foundSpecialty.SetId(stylistId);
        foundSpecialty.SetSpecialtyType(stylistSpecialtyType);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundSpecialty;
    }

    public void Edit(string newSpecialty)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE specialties SET specialty_type = '"+newSpecialty+"' WHERE id = '"+_id+"';";
      cmd.ExecuteNonQuery();
      _specialtyType = newSpecialty;
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
        cmd.CommandText = @"DELETE FROM specialties WHERE id = '"+_id+"';";
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
        cmd.CommandText = @"DELETE FROM specialties;";
        cmd.ExecuteNonQuery();
        if (conn != null)
        {
          conn.Close();
        }
    }

    public List<Stylist> GetStylists()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT stylists.* FROM specialties
            JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
            JOIN stylists ON (stylists_specialties.stylist_id = stylists.id)
            WHERE specialties.id = '"+_id+"';";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<Stylist> stylists = new List<Stylist>{};
        while(rdr.Read())
        {
          int stylistId = rdr.GetInt32(0);
          string stylistName = rdr.GetString(1);
          string stylistDescription = rdr.GetString(2);
          DateTime hireDate = rdr.GetDateTime(3);
          Stylist newStylist = new Stylist(stylistName, stylistDescription, hireDate, stylistId);
          stylists.Add(newStylist);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return stylists;
    }

    public void AddStylist(Stylist stylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (specialty_id, stylist_id) VALUES ('"+_id+"', '"+stylist.GetId()+"');";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = this.GetId().Equals(newSpecialty.GetId());
        bool specialtyTypeEquality = this.GetSpecialtyType().Equals(newSpecialty.GetSpecialtyType());
        if(idEquality == true && specialtyTypeEquality == true)
        {
          return true;
        }
      return false;
      }
    }
  }
}
