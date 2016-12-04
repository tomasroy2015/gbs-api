using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using System.ServiceModel;
using GBSExtranet.Api.ViewModel;
using GBSExtranet.Repository;
using Business;
using System.Data;
using GBSExtranet.Api.ServiceLayer;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Collections;
using System.Globalization;
namespace GBSExtranet.Api.ServiceLayer
{
    public class HotelRateService : BaseService
    {
        public DataTable GetHotelRate(int HotelID, DateTime StartDate, DateTime EndDate, string HotelAccommodationTypeID, string PricePolicyTypeID)
        {
            //DateTime dtStart = DateTime.Now;
            //DateTime dtEnd = DateTime.Now.AddDays(30);
            //if (StartDate.Contains('.'))
            //{
            //    dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            //}
            //else
            //{
            //    dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //}
            //if (EndDate.Contains('.'))
            //{
            //    dtEnd = DateTime.ParseExact(EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            //}
            //else
            //{
            //    dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //}
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRate]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@HotelAccommodationTypeID", HotelAccommodationTypeID);
            cmd.Parameters.AddWithValue("@PricePolicyTypeID", PricePolicyTypeID);
            //cmd.Parameters.AddWithValue("@HotelRoomID", "RoomID");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            //List<RoomAvailabilityAndRateRepositoryExt> ListOfModel = new List<RoomAvailabilityAndRateRepositoryExt>();
            //string Roomname = "";

            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        RoomAvailabilityAndRateRepositoryExt HotelObj = new RoomAvailabilityAndRateRepositoryExt();
            //        HotelObj.HotelRateID = Convert.ToInt32(dr["ID"]);
            //        HotelObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
            //        HotelObj.DateID = dr["DateID"].ToString();
            //        HotelObj.Date = dr["Date"].ToString();
            //        HotelObj.RoomID = Convert.ToInt32(dr["HotelRoomID"]);
            //        HotelObj.AvailableRoomCount = Convert.ToInt32(dr["AvailableRoomCount"]);
            //        HotelObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
            //        HotelObj.SinglePrice = Convert.ToDecimal(dr["SinglePrice"]);
            //        HotelObj.DoublePrice = Convert.ToDecimal(dr["DoublePrice"]);
            //        HotelObj.RoomPrice = Convert.ToDecimal(dr["RoomPrice"]);
            //        HotelObj.RoomRateMissing = Convert.ToBoolean(dr["RoomRateMissing"]);
            //        HotelObj.Closed = Convert.ToBoolean(dr["Closed"]);
            //        ListOfModel.Add(HotelObj);
            //    }
            //}
            return dt;
        }
    }
}