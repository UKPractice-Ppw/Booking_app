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
            string qry = "SELECT * FROM Tbl_author WHERE author_name=@author_name AND ";
            return 0;
        }

    }
}