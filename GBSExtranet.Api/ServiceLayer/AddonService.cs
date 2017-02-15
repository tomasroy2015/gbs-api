using GBSExtranet.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ServiceLayer
{
    public class AddonService : BaseService
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public int insertaddondetails(string HotelID, string Title, string Price, string Changetype)
        {
            int i = 0;
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Insertaddonservices_TB_Addons_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelId", HotelID);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@Changetype", Changetype);
            i = Convert.ToInt32(cmd.ExecuteNonQuery());
            return i;
            
        }
        public List<Addonservices> Displayaddonsdetails()
        {
            List<Addonservices> ListOfModel = new List<Addonservices>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Getaddonservices_TB_Addons_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Addonservices HotelObj = new Addonservices();
                    HotelObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelObj.HotelId = dr["HotelId"].ToString();
                    HotelObj.Title = dr["Title"].ToString();
                    HotelObj.Price = dr["Price"].ToString();
                    HotelObj.Changetype = dr["Changetype"].ToString();
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }
        public List<Addonservices> Displaydrpchangetype()
        {
            List<Addonservices> ListOfModel = new List<Addonservices>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Getchangetypeunit_TB_Unit_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Addonservices HotelObj = new Addonservices();
                    HotelObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelObj.Lanagecode = dr["CultureCode"].ToString();                 
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }
        public int Deleteaddons(int Id)
        {
            int i = 0;
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Deleteaddonservices_TB_Addons_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            i = Convert.ToInt32(cmd.ExecuteNonQuery());
            return i;

        }
    }
}