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
using GBSExtranet.Repository;
using System.Globalization;
using System.Reflection;
namespace GBSExtranet.Api.ServiceLayer
{
    public class PropertyReviewService : BaseService
    {
        public List<PropertyReview> GetReviews(int HotelID,string culture)
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetReservationReviewAveragePoints", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", "ReviewTypeEvaluationName");
            cmd.Parameters.AddWithValue("@PartID", 1);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            // string FirstAveragePoint = "";
            int ReservationReviewDetailCount = 0;
            int TypeReviewCount = 0;
            List<PropertyReview> ListOfModel = new List<PropertyReview>();
            DataSet ReviewTypeCount = GetReviewTypeCount(HotelID);

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            if (ReviewTypeCount != null)
            {
                dt1 = ReviewTypeCount.Tables[0];
                dt2 = ReviewTypeCount.Tables[1];
            }



            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    PropertyReview ReviewsObj = new PropertyReview();
                    ReservationReviewDetailCount = Convert.ToInt32(dr["ReservationReviewDetailCount"]);
                    //ListOfModel.Add(ReviewsObj);
                }
            }
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in dt2.Rows)
                {
                    PropertyReview ReviewsObj = new PropertyReview();
                    TypeReviewCount = Convert.ToInt32(dr["TypeReviewCount"]);
                    //ListOfModel.Add(ReviewsObj);
                }
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyReview ReviewsObj = new PropertyReview();
                    ReviewsObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                    ReviewsObj.AveragePoint = Convert.ToDecimal(dr["AveragePoint"])*2;
                    ReviewsObj.ReviewTypeID = Convert.ToInt32(dr["ReviewTypeID"]);
                    ReviewsObj.ReviewTypeName = dr["ReviewTypeName"].ToString();
                    ReviewsObj.sort = Convert.ToInt32(dr["Sort"]);
                    ReviewsObj.PointSum = Convert.ToDecimal(dr["PointSum"]);
                    ReviewsObj.ReviewCount = Convert.ToInt32(dr["ReviewCount"]);
                    ReviewsObj.FirstAveragePoint = Convert.ToDecimal(dt.Rows[0]["AveragePoint"]);
                    string FirstAveragePoint = dt.Rows[0]["AveragePoint"].ToString();
                    ReviewsObj.FirstReviewTypeEvaluationName = dt.Rows[0]["ReviewTypeEvaluationName"].ToString();
                    if (FirstAveragePoint != "")
                    {

                        ReviewsObj.FirstAveragePoint = Convert.ToDecimal(FormatToNumber(FirstAveragePoint))*2;

                    }
                    double AveragePoint1 = Convert.ToDouble(Convert.ToDecimal(dr["PointSum"]) / Convert.ToInt32(dr["ReviewCount"]));
                    //ReviewsObj.AveragePoint = ;
                    ReviewsObj.AveragePoint = Math.Round(Convert.ToDecimal(AveragePoint1), 1);
                    if (AveragePoint1 != 0)
                    {
                        ReviewsObj.height = Convert.ToDouble(Math.Floor(AveragePoint1 * 100)/5);
                        ReviewsObj.height1 = ReviewsObj.height + "%";
                    }
                    ReviewsObj.ReviewTypeEvaluationName = dr["ReviewTypeEvaluationName"].ToString();
                    ReviewsObj.ReviewTypeCount1 = ReservationReviewDetailCount / TypeReviewCount;

                    ListOfModel.Add(ReviewsObj);
                }
            }


            return ListOfModel;
        }


        public string FormatToNumber(string Value)
        {
            Int16 MaxDecimalLength = 1;
            bool RemoveDecimalZeros = true;
            double NumericValue = 1;
            string numberStr = string.Empty;
            string InputNumberCultureCode = "en-Gb";
            string FormatNumberCultureCode = "en-Gb";

            System.Globalization.CultureInfo inputNumberCultureInfo = new System.Globalization.CultureInfo(InputNumberCultureCode);
            System.Globalization.CultureInfo formatNumberCultureInfo = new System.Globalization.CultureInfo(FormatNumberCultureCode);
            double d = 0;


            if (Value != null && !object.ReferenceEquals(Value, DBNull.Value) && double.TryParse(Value, System.Globalization.NumberStyles.Number, inputNumberCultureInfo, out d))
            {
                if (Value is double || Value is decimal || Value is int || Value is long)
                {
                    d = Convert.ToDouble(Value);
                }

                if (d == Math.Floor(d) && RemoveDecimalZeros)
                {
                    formatNumberCultureInfo.NumberFormat.NumberDecimalDigits = 0;
                }
                else
                {
                    formatNumberCultureInfo.NumberFormat.NumberDecimalDigits = MaxDecimalLength;
                }
                numberStr = d.ToString("n", formatNumberCultureInfo);

                //if (formatNumberCultureInfo.NumberFormat.NumberDecimalDigits > 0 && RemoveDecimalZeros)
                //{
                //    numberStr = numberStr.TrimEnd(0);
                //    numberStr = numberStr.TrimEnd(formatNumberCultureInfo.NumberFormat.NumberDecimalSeparator);
                //}

                numberStr = numberStr.Replace(formatNumberCultureInfo.NumberFormat.NumberGroupSeparator, string.Empty);
                NumericValue = double.Parse(numberStr, formatNumberCultureInfo);

            }

            return numberStr;

        }

        public List<PropertyReviewHeader> GetIndividualReviews(int HotelID,string culture)  
        {
            // string PropertyConditions = "";
            DataSet ds = new DataSet();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetReservationReviews", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", "CreateDateTime DESC");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            _sqlConnection.Close();

            List<PropertyReview> ListOfModel1 = new List<PropertyReview>();

            int TotalRecordCount = 0;


            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
                dt1 = ds.Tables[1];
            }



            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyReview ReviewsObj = new PropertyReview();
                    TotalRecordCount = Convert.ToInt32(dr["TotalRecordCount"]);
                    ListOfModel1.Add(ReviewsObj);
                }
            }
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    PropertyReview ReviewsObj = new PropertyReview();
                    ReviewsObj.ReservationReviewID = Convert.ToInt32(dr["ID"]);
                    ReviewsObj.ReservationID = Convert.ToInt32(dr["ReservationID"]);
                    ReviewsObj.TravellerTypeID = Convert.ToInt32(dr["TravellerTypeID"]);
                    ReviewsObj.TravelerTypeName = dr["TravelerTypeName"].ToString();
                    ReviewsObj.Review = dr["Review"].ToString();
                    ReviewsObj.AveragePoint = Convert.ToDecimal(dr["AveragePoint"])*2;
                    double AvgPoint = Convert.ToDouble(Convert.ToDecimal(dr["AveragePoint"]) / 5);
                    if (AvgPoint != 0)
                    {
                        ReviewsObj.width2 = Convert.ToDouble(Math.Floor(AvgPoint * 50));
                        ReviewsObj.width3 = ReviewsObj.width2 + "%";
                    }
                    ReviewsObj.ReviewStatusID = Convert.ToInt32(dr["ReviewStatusID"]);
                    ReviewsObj.ReviewStatusName = dr["ReviewStatusName"].ToString();
                    ReviewsObj.Anonymous = Convert.ToBoolean(dr["Anonymous"]);
                    ReviewsObj.Active = Convert.ToBoolean(dr["Active"]);
                    ReviewsObj.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    ReviewsObj.OpDateTime = Convert.ToDateTime(dr["OpDateTime"]);
                    ReviewsObj.IPAddress = dr["IPAddress"].ToString();
                    ReviewsObj.ReviewTypeID = Convert.ToInt32(dr["ReviewTypeID"]);
                    ReviewsObj.ReviewTypeName = dr["ReviewTypeName"].ToString();
                    ReviewsObj.ReviewTypeEvaluationName = dr["ReviewTypeEvaluationName"].ToString();
                    ReviewsObj.sort = Convert.ToInt32(dr["Sort"]);
                    ReviewsObj.Point = Convert.ToDecimal(dr["Point"]);
                    ReviewsObj.PositiveReview = dr["ReviewPositive"] == null || Convert.ToString(dr["ReviewPositive"]) == string.Empty ? "Nothing" : Convert.ToString(dr["ReviewPositive"]);
                    ReviewsObj.NegativeReview = dr["Reviewnegative"] == null || Convert.ToString(dr["Reviewnegative"]) == string.Empty ? "Nothing" : Convert.ToString(dr["Reviewnegative"]);
                    double AveragePoint1 = Convert.ToDouble(Convert.ToDecimal(dr["Point"]));

                    if (AveragePoint1 != 0)
                    {
                        ReviewsObj.width = Convert.ToDouble(Math.Floor(AveragePoint1 * 50)/5);
                        ReviewsObj.width1 = ReviewsObj.width + "%";
                    }
                    ReviewsObj.ReviewTypeScaleName = dr["ReviewTypeScaleName"].ToString();
                    ReviewsObj.UserID = Convert.ToInt32(dr["UserID"]);
                    ReviewsObj.UserName = dr["UserName"].ToString();
                    ReviewsObj.UserFullName = dr["UserFullName"].ToString();
                    ReviewsObj.PartID = Convert.ToInt32(dr["PartID"]);
                    ReviewsObj.Part = dr["Part"].ToString();
                    ReviewsObj.FirmID = Convert.ToInt32(dr["FirmID"]);
                    ReviewsObj.FirmName = dr["FirmName"].ToString();
                    ReviewsObj.CountryID = Convert.ToInt32(dr["CountryID"]);
                    ReviewsObj.CountryName = dr["CountryName"].ToString();
                    //ReviewsObj.ReviewCount1 = TotalRecordCount / ReviewTypeCount1;
                    ReviewsObj.ReviewInfo = dr["UserFullName"].ToString() + " - " + dr["CountryName"].ToString() + " - " + Convert.ToDateTime(dr["CreateDateTime"]);
                    ListOfModel1.Add(ReviewsObj);
                }
            }

            ListOfModel1 = ListOfModel1.FindAll(f=>f.ReservationReviewID != null && f.ReservationReviewID > 0).ToList();
            var groupedCustomerList = ListOfModel1
                                .GroupBy(u => u.ReservationReviewID)
                                .Select(grp => grp.ToList())
                                .ToList();

            List<PropertyReviewHeader> headers =  new List<PropertyReviewHeader>();
            if (groupedCustomerList != null)
            {
                foreach(var grp in groupedCustomerList)
                {
                    var header = new PropertyReviewHeader();
                    header.Name = grp[0].ReviewInfo;
                    header.PositiveComment = grp[0].PositiveReview;
                    header.NegativeComment = grp[0].NegativeReview;
                    header.IndividualReviews = grp;
                    headers.Add(header);
                }
            }

            return headers;
        }

        public DataSet GetReviewTypeCount(int HotelID)
        {
            DataSet ds = new DataSet();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_GetReviewTypeCount_TB_TypeReview_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            _sqlConnection.Close();
            return ds;
        }
    }
}