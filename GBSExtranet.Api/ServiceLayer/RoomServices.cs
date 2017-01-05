//Balstechnology-AJ

using GBSExtranet.Api.Models;
using GBSExtranet.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ServiceLayer
{
    public class RoomServices : BaseService
    {
        public List<Room> GetRoomDetails(int hotelID, string cultureCode)
        {          
            List<Room> ListOfModel = new List<Room>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRooms]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", cultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@HotelID", hotelID);
            cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Room HotelObj = new Room();
                    HotelObj.RoomID = Convert.ToInt32(dr["ID"]);
                    HotelObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelObj.RoomTypeName = dr["RoomTypeName"].ToString();
                    HotelObj.SomkingTypeName = dr["SmokingTypeName"].ToString();
                    HotelObj.RoomSize = dr["RoomSize"].ToString();
                    HotelObj.RoomCount = dr["RoomCount"].ToString();
                    HotelObj.Image = dr["Name"].ToString();
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }

        
    }
}