using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Booking_Application.Models
{
   
    public class DbHandler
    {
        SqlConnection con;
        public void Connection()
        {
            string conn = ConfigurationManager.ConnectionStrings["Entities"].ConnectionString;
            con = new SqlConnection(conn);
        }
        public int Login_author(Tbl_author auth)        
        {
            Connection();
            string qry = "SELECT * FROM Tbl_author WHERE author_name=@author_name AND password=@pass";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@author_name",auth.author_name);
            cmd.Parameters.AddWithValue("@pass",auth.password);
            SqlDataReader sdr = cmd.ExecuteReader();
            if(sdr.HasRows)
            {
                sdr.Read();
                int id = (int)sdr["author_id"];
                return id;
            }
            else
            {
                return 0;
            }   
        }

    }
}