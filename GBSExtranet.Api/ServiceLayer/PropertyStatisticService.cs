using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using System.ServiceModel;
using GBSExtranet.Api.ViewModel;
using Business;
using System.Data;
using System.Globalization;
namespace GBSExtranet.Api.ServiceLayer
{
    public class PropertyStatisticService : BaseService
    {
        public List<PropertyStatistics> DisplayPropertyStatistics(string culture, string dateFrom, string dateTo, int HotelID)
        {
            int HitCountPeriodID = 3;
            int partID = 1;
            //string dateFrom = DateTime.Now.ToString("yyyy-MM-dd");
            //string dateTo = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");
            string startDate = DateConvert(dateFrom);
            string endDate = DateConvert(dateTo);

            List<PropertyStatistics> list = new List<PropertyStatistics>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHitCounts", _sqlConnection);
            cmd.Parameters.AddWithValue("@PartID", partID);
            cmd.Parameters.AddWithValue("@RecordID", HotelID);
            cmd.Parameters.AddWithValue("@HitCountPeriodID", HitCountPeriodID);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@OrderBy", "Sort");
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            _sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyStatistics FirmObj = new PropertyStatistics();
                    FirmObj.PartID = dr["PartID"].ToString();
                    FirmObj.RecordID = dr["RecordID"].ToString();
                    FirmObj.ReservationCount = dr["ReservationCount"].ToString();
                    FirmObj.Date = dr["Date"].ToString();
                    FirmObj.HitCount = dr["HitCount"].ToString();
                    FirmObj.Month = dr["Month"].ToString();
                    FirmObj.MonthName = dr["MonthName"].ToString();
                    FirmObj.Day = dr["Day"].ToString();
                    FirmObj.DayName = dr["DayName"].ToString();
                    list.Add(FirmObj);
                }
            }
            return list;
        }

        public DataTable DisplaydatewisePropertyStatistics(string culture, int PartID, int HitCountPeriodID, string Startdate, string EndDate, int HotelID)
        {
            List<PropertyStatistics> list = new List<PropertyStatistics>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHitCounts", _sqlConnection);
            cmd.Parameters.AddWithValue("@PartID", Convert.ToInt32(PartID));
            cmd.Parameters.AddWithValue("@RecordID", Convert.ToInt64(HotelID));
            cmd.Parameters.AddWithValue("@HitCountPeriodID", Convert.ToInt32(HitCountPeriodID));
            cmd.Parameters.AddWithValue("@StartDate", Startdate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@OrderBy", "Sort");
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }

        public List<PropertyStatistics> MonthlyPropertyStatistics(string culture, string Year,int hotelID)
        {
            int HitCountPeriodID = 2;
            int PartID = 1;
            List<PropertyStatistics> list = new List<PropertyStatistics>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand("TB_SP_GetHitCounts", _sqlConnection);
            cmd.Parameters.AddWithValue("@PartID", Convert.ToInt32(PartID));
            cmd.Parameters.AddWithValue("@RecordID", Convert.ToInt64(hotelID));
            cmd.Parameters.AddWithValue("@HitCountPeriodID", HitCountPeriodID);
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(Year));
            cmd.Parameters.AddWithValue("@OrderBy", "Month");

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyStatistics FirmObj = new PropertyStatistics();
                    FirmObj.PartID = dr["PartID"].ToString();
                    FirmObj.RecordID = dr["RecordID"].ToString();
                    FirmObj.ReservationCount = dr["ReservationCount"].ToString();
                    FirmObj.HitCount = dr["HitCount"].ToString();
                    FirmObj.MonthName = dr["MonthName"].ToString();
                    list.Add(FirmObj);
                }
            }
            return list;

        }
        public string DateConvert(string date) 
        {
            string covertdate = "";
            //try
            //{
            //    DateTime dt23 = Convert.ToDateTime(date);
            //    CultureInfo arCI = new CultureInfo("ar-SA");
            //    string ar = dt23.ToString(new CultureInfo("ar-SA"));
            //    GregorianCalendar enCalendar = new GregorianCalendar();
            //    int year = enCalendar.GetYear(dt23);
            //    int month = enCalendar.GetMonth(dt23);
            //    int day = enCalendar.GetDayOfMonth(dt23);
            //    covertdate = (string.Format("{0}-{1}-{2}", year, month, day));
            //}
            //catch
            //{

                IFormatProvider culture = new CultureInfo("en-US", true);
                //DateTime dateVal = DateTime.ParseExact(date, "yyyy-MM-dd", culture);
                DateTime dt1 = DateTime.ParseExact(date, "dd/MM/yyyy", culture);
                CultureInfo arCI = new CultureInfo("en-US");
                string ar = date.ToString(new CultureInfo("en-US"));
                GregorianCalendar enCalendar = new GregorianCalendar();
                int year = enCalendar.GetYear(dt1);
                int month = enCalendar.GetMonth(dt1);
                int day = enCalendar.GetDayOfMonth(dt1);

                covertdate = (string.Format("{0}-{1}-{2}", year, month, day));

                // covertdate = dateVal.ToString("yyyy-MM-dd");

          //  }

            return (covertdate);

        }

        public List<PropertyStatistics> YearlyPropertyStatistics(string culture, int HotelID)
        {
            
            int HitCountPeriodID =1;
            int partID = 1;
            List<PropertyStatistics> list = new List<PropertyStatistics>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHitCounts", _sqlConnection);
            cmd.Parameters.AddWithValue("@PartID", partID);
            cmd.Parameters.AddWithValue("@RecordID", Convert.ToInt64(HotelID));
            cmd.Parameters.AddWithValue("@HitCountPeriodID", HitCountPeriodID);
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            // return dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyStatistics FirmObj = new PropertyStatistics();
                    FirmObj.PartID = dr["PartID"].ToString();
                    FirmObj.RecordID = dr["RecordID"].ToString();
                    FirmObj.ReservationCount = dr["ReservationCount"].ToString();
                    FirmObj.HitCount = dr["HitCount"].ToString();
                    FirmObj.Year = dr["Year"].ToString();
                    list.Add(FirmObj);
                }
            }
            return list;
        }


    }
}