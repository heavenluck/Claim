using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClaimProject.Config
{
    public class ClaimConnection
    {
        private MySqlConnection conn;

        public MySqlConnection Conn { get => conn; set => conn = value; }

        string strConnString = "Server=10.6.3.175;User Id=adminclaim; Password=admin25;charset=tis620; Database=db_claim; Pooling=false";  //Depoly
        //string strConnString = "Server=localhost;User Id=root; Password=admin25;charset=tis620; Database=db_claim; Pooling=false";  //Test

        public ClaimConnection()
        {
            Conn = new MySqlConnection(strConnString);
        }

        public void Open()
        {
            
            if (Conn != null && Conn.State == ConnectionState.Closed)//to check if conn is already open or not
            {
                Conn.Open();
            }
            else
            {
                Conn.Close();
                conn.Close();
                Conn.Open();
            }
        }

        public void Close()
        {
            Conn.Close();
            conn.Close();
        }
    }

}