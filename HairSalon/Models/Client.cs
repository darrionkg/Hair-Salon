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

    public Client(string name, string stylistName)
    {
      _name = name;
      _stylistName = stylistName;
    }

  }


}
