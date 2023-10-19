using CRUDUsingMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CRUDUsingMVC.Repository
{
    public class StdRepository
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }

        public bool Create(StudentModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("AddNewstdDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Email", obj.Email);
            com.Parameters.AddWithValue("@RetypeEmail", obj.RetypeEmail);
            com.Parameters.AddWithValue("@Phone", obj.Phone);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }


        public List<StudentModel> GetAllStudent()
        {
            connection();
            List<StudentModel> stdList = new List<StudentModel>();


            SqlCommand com = new SqlCommand("Getstudentmvc", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {

                stdList.Add(

                    new StudentModel
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Email = Convert.ToString(dr["Email"]),
                        RetypeEmail = Convert.ToString(dr["RetypeEmail"]),
                        Phone = Convert.ToString(dr["Phone"]),
                    }
                    );
            }

            return stdList;

        }

        public bool UpdateStudent(StudentModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("UpdatestdDetails", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Id);
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Email", obj.Email);
            com.Parameters.AddWithValue("@RetypeEmail", obj.RetypeEmail);
            com.Parameters.AddWithValue("@Phone", obj.Phone);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }
        public bool DeleteStudent(int Id)
        {

            connection();
            SqlCommand com = new SqlCommand("DeletestdById", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", Id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }
    }
}
    
