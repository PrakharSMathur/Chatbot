using BotApplicationPSM.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace BotApplicationPSM
{
    public class DBconnection
    {
        string connectionString = "Server=tcp:internsqlserver.database.windows.net,1433;Initial Catalog=healthbot;Persist Security Info=False;User ID=;Password=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //To add booking
        public int BookingAppt(Class1 employee)
        {
            /* Class1 employee = new Class1();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT TOP 1* FROM Appointments";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
               {*/
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                            string sqlQuery = "insert into MedBooking (pname,p_age,bgroup,pphone,Appdate,event1) values (@pname,@p_age,@bgroup,@pphone,@Appdate,@event1)";
                    SqlCommand cmd1 = new SqlCommand(sqlQuery, con);
                    cmd1.CommandType = CommandType.Text;

                    cmd1.Parameters.AddWithValue("@pname", employee.pname);
                    cmd1.Parameters.AddWithValue("@p_age", employee.p_age);
                    cmd1.Parameters.AddWithValue("@bgroup", employee.bgroup);
                    cmd1.Parameters.AddWithValue("@pphone", employee.pphone);
                    cmd1.Parameters.AddWithValue("@Appdate", employee.Appdate);
                    cmd1.Parameters.AddWithValue("@event1", employee.event1);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
                return 1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //To Delete the record on a particular employee  
        public int deleteBooking(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("deleteBooking", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookingId", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Class1 GetBooking()
        {
            Class1 employee = new Class1();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT TOP 1* FROM MedBooking";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
               {
                 while (rdr.Read())
                    {
                        employee.BookingId = Convert.ToInt32(rdr["BookingId"]);
                        employee.pname = rdr["pname"].ToString();
                        employee.p_age = Convert.ToInt32(rdr["p_age"]);
                        employee.bgroup = rdr["bgroup"].ToString();
                        employee.pphone = rdr["pphone"].ToString();
                        employee.Appdate = rdr["Appdate"].ToString();
                    }
                    
                }
                con.Close();
            }
            return employee;


        }
    }
}
