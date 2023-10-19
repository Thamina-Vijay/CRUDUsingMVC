using CRUDUsingMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CRUDUsingMVC.Repository
{
    public class AccountRepository
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }

        public bool LoginDetails(LoginViewModel obj)
        {
            bool isvalidUser = false;
            connection();
            SqlCommand com = new SqlCommand("sp_LoginDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserName", obj.UserName);
            com.Parameters.AddWithValue("@Password", obj.Password);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            LoginViewModel Isvalid = new LoginViewModel();

            foreach (DataRow dr in dt.Rows)
            {
                Isvalid.UserName = Convert.ToString(dr["UserName"]);
                Isvalid.Password = Convert.ToString(dr["Password"]);
                Isvalid.RememberMe = Convert.ToBoolean(dr["RememberMe"]);
            }

            if (dt.Rows.Count > 0)
            {
                isvalidUser = true;
            }
            return isvalidUser;

            
        }


        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<RegisterViewModel> PopulateUserNames()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            List<RegisterViewModel> RegisterDetails = new List<RegisterViewModel>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT UserName, Id FROM RegisterDetails";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RegisterDetails.Add(new RegisterViewModel
                            {
                                UserName = sdr["UserName"].ToString(),
                                Id = Convert.ToInt32(sdr["Id"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return RegisterDetails;
        }

        public bool Register(RegisterViewModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("SP_RegisterDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserName", obj.UserName);
            com.Parameters.AddWithValue("@Password", obj.Password);
            com.Parameters.AddWithValue("@ConfirmPassword", obj.ConfirmPassword);

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

        public bool UpdateForgotPassword(ForgotPwdModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("SP_UpdateForgotPassword", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserId", obj.UserId);
            com.Parameters.AddWithValue("@NewPassword", obj.NewPassword);
            com.Parameters.AddWithValue("@ConfirmPassword", obj.ConfirmPassword);
            
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




