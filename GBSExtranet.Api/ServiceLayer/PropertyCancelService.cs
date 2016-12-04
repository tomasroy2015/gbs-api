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

namespace GBSExtranet.Api.ServiceLayer
{
    public class PropertyCancelService : BaseService
    {
        public int UpdatePropertyCancelPolicy(PropertyCancelPolicy model, int UserID)
        {            
            _sqlConnection.Open();
            int status = 0;
            SqlCommand cmd = new SqlCommand("B_Ex_UpdateHotelCancelPolicy_TB_HotelCancelPolicy_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", model.HotelID);
            cmd.Parameters.AddWithValue("@CanceltypeID", model.CancelTypeID);
            cmd.Parameters.AddWithValue("@PenaltyRateType", model.PenaltyRateTypeID);
            cmd.Parameters.AddWithValue("@RefundableDayCount", model.RefundableDayCount);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            status = Convert.ToInt32(cmd.ExecuteNonQuery());
            _sqlConnection.Close();
            return status;

        }

        public List<PropertyCancelPolicy> GetHotelCancelPolicyinfo(int HotelID, string CultureValue)
        {

            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelCancelPolicy", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            List<PropertyCancelPolicy> ListOfModel = new List<PropertyCancelPolicy>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyCancelPolicy HotelcancelpolicyObj = new PropertyCancelPolicy();

                    HotelcancelpolicyObj.CancelTypeID = Convert.ToInt32(dr["CancelTypeID"]);
                    HotelcancelpolicyObj.CancelTypeName = dr["CancelTypeName"].ToString();
                    HotelcancelpolicyObj.Refundable = dr["Refundable"].ToString();
                    if (dr["RefundableDayCount"].ToString() != string.Empty)
                        HotelcancelpolicyObj.RefundableDayCount = Convert.ToInt32(dr["RefundableDayCount"]);
                    string PenaltyRateType = dr["PenaltyRateTypeID"].ToString();
                    if (PenaltyRateType != "")
                    {
                        HotelcancelpolicyObj.PenaltyRateTypeID = Convert.ToInt32(PenaltyRateType);
                    }
                    else
                    {
                        HotelcancelpolicyObj.PenaltyRateTypeID = 0;
                    }
                    HotelcancelpolicyObj.PenaltyRateTypeName = dr["PenaltyRateTypeName"].ToString();
                    ListOfModel.Add(HotelcancelpolicyObj);
                }

            }
            return ListOfModel;
        }


        public List<PropertyCancelPolicy> GetHotelCancelPolicy(string CultureValue)
        {

            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelCancelPolicy", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            List<PropertyCancelPolicy> ListOfModel = new List<PropertyCancelPolicy>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyCancelPolicy HotelcancelpolicyObj = new PropertyCancelPolicy();
                    HotelcancelpolicyObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                    HotelcancelpolicyObj.CancelTypeID = Convert.ToInt32(dr["CancelTypeID"]);
                    HotelcancelpolicyObj.CancelTypeName = dr["CancelTypeName"].ToString();
                    HotelcancelpolicyObj.Refundable = dr["Refundable"].ToString();

                    if (dr["RefundableDayCount"].ToString() != string.Empty)
                        HotelcancelpolicyObj.RefundableDayCount =  Convert.ToInt32(dr["RefundableDayCount"]);

                    string PenaltyRateType = dr["PenaltyRateTypeID"].ToString();
                    if (PenaltyRateType != "")
                    {
                        HotelcancelpolicyObj.PenaltyRateTypeID = Convert.ToInt32(PenaltyRateType);
                    }
                    else
                    {
                        HotelcancelpolicyObj.PenaltyRateTypeID = 0;
                    }
                    HotelcancelpolicyObj.PenaltyRateTypeName = dr["PenaltyRateTypeName"].ToString();
                    ListOfModel.Add(HotelcancelpolicyObj);
                }

            }
            return ListOfModel;
        }
          
          
    }
}