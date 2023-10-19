using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using CRUDUsingMVC.Models;
using CRUDUsingMVC.ViewModel;
using System.Configuration;

namespace CRUDUsingMVC.Repositories
{
    public class ContentRepository
    {
        //private readonly DBContext db = new DBContext();

        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }

        public byte[] DisplayImage(int ID)
        {
            connection();
            con.Open();
            SqlCommand com = new SqlCommand("GetEmployeesByID", con);
            com.Parameters.AddWithValue("@ID", ID);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            byte[] productImage;
            productImage = (byte[])ds.Tables[0].Rows[0]["Image"];
            con.Close();
            return productImage;
        }

        public List<ContentViewModel> GetAllImage()
        {
            connection();
            List<ContentViewModel> EmpList = new List<ContentViewModel>();


            SqlCommand com = new SqlCommand("SP_GETALLIMAGES", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {

                EmpList.Add(

                    new ContentViewModel
                    {

                        ID = Convert.ToInt32(dr["ID"]),
                        Title = Convert.ToString(dr["Title"]),
                        Description = Convert.ToString(dr["Description"]),
                        Contents = Convert.ToString(dr["Contents"])
                        //Image = Convert.ToByte(dr["Image"])

                    }
                    );


            }

            return EmpList;


        }

        public int UploadImageInDataBase(HttpPostedFileBase file, UserInfoModel UserInfoModel)
        {
            UserInfoModel.Image = ConvertToBytes(file);
            var UserInfo = new UserInfoModel
            {
                Name = UserInfoModel.Name,
                Image = UserInfoModel.Image,
                 Email= UserInfoModel.Email,
                RetypeEmail = UserInfoModel.RetypeEmail,
                //Phone = UserInfoModel.Phone
            };

            connection();
            SqlCommand com = new SqlCommand("SP_UserInfo", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", UserInfo.Name);
            com.Parameters.AddWithValue("@Image", UserInfo.Image);
            com.Parameters.AddWithValue("@Email", UserInfo.Email);
            com.Parameters.AddWithValue("@RetypeEmail", UserInfo.RetypeEmail);
            //com.Parameters.AddWithValue("@Phone", UserInfo.Phone);

            SqlParameter outputPara = new SqlParameter();
            outputPara.ParameterName = "@Image_Id";
            outputPara.Direction = System.Data.ParameterDirection.Output;
            outputPara.SqlDbType = System.Data.SqlDbType.Int;
            com.Parameters.Add(outputPara);
            
            con.Open();
            int i = com.ExecuteNonQuery();

            string RetrievedImageId = outputPara.Value.ToString();

            con.Close();
            if (i >= 1)
            {

                UserInfoModel.Id = Convert.ToInt32(RetrievedImageId);
                //DisplayImage(Convert.ToInt32(RetrievedImageId));
                return 1;
               
            }
            else
            {
                return 0;
            }

        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        internal int UploadImageInDataBase(HttpPostedFileBase file, StudentModel model)
        {
            throw new NotImplementedException();
        }

        internal int UploadImageInDataBase(HttpPostedFileBase file, ContentViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}