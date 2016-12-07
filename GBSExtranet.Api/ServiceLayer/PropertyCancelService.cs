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
        public int SavePolicy(PropertyCancellationPolicy model,string culture, long? UserID) 
        {
            var dbExist = _db.HotelCancellationPolicies.Where(f => f.HotellD == model.HotellD && f.CancelTypeID == model.CancelTypeID).FirstOrDefault();
            
            if (dbExist != null)
            {
                _db.HotelCancellationPolicies.Remove(dbExist);
                _db.SaveChanges();
            }

            GBSExtranet.Api.Models.HotelCancellationPolicy dbModel = new Models.HotelCancellationPolicy();
            dbModel.ArrivalRateID = model.ArrivalRateID;
            dbModel.ArrivalTypeID = model.ArrivalTypeID;
            dbModel.CancelRateID = model.CancelRateID;
            dbModel.CancelTypeID = model.CancelTypeID;
            dbModel.HotellD = model.HotellD;
            dbModel.IsPeriodExists = model.IsPeriodExists;
            dbModel.IsPrivateDisplay = model.IsPromotionDisplay;
            dbModel.IsPublicDisplay = model.IsPublicDisplay;
            dbModel.PrepaymentTypeID = model.PrepaymentTypeID;
            if (culture == "en")
            {
                dbModel.PaymentDescription_en = model.PaymentDescription;
                dbModel.PolicyDescription_en = model.PolicyDescription;
            }
            if (culture == "ar")
            {
                dbModel.PaymentDescription_ar = model.PaymentDescription;
                dbModel.PolicyDescription_ar = model.PolicyDescription;
            }
            if (culture == "de")
            {
                dbModel.PaymentDescription_de = model.PaymentDescription;
                dbModel.PolicyDescription_de = model.PolicyDescription;
            }
            if (culture == "es")
            {
                dbModel.PaymentDescription_es = model.PaymentDescription;
                dbModel.PolicyDescription_es = model.PolicyDescription;
            }
            if (culture == "fr")
            {
                dbModel.PaymentDescription_fr = model.PaymentDescription;
                dbModel.PolicyDescription_fr = model.PolicyDescription;
            }
            if (culture == "it")
            {
                dbModel.PaymentDescription_it = model.PaymentDescription;
                dbModel.PolicyDescription_it = model.PolicyDescription;
            }
            if (culture == "ja")
            {
                dbModel.PaymentDescription_ja = model.PaymentDescription;
                dbModel.PolicyDescription_ja = model.PolicyDescription;
            }
            if (culture == "pt")
            {
                dbModel.PaymentDescription_pt = model.PaymentDescription;
                dbModel.PolicyDescription_pt = model.PolicyDescription;
            }
            if (culture == "ru")
            {
                dbModel.PaymentDescription_ru = model.PaymentDescription;
                dbModel.PolicyDescription_ru = model.PolicyDescription;
            }
            if (culture == "tr")
            {
                dbModel.PaymentDescription_tr = model.PaymentDescription;
                dbModel.PolicyDescription_tr = model.PolicyDescription;
            }
            if (culture == "zh")
            {
                dbModel.PaymentDescription_zh = model.PaymentDescription;
                dbModel.PolicyDescription_zh = model.PolicyDescription;
            }
             dbModel.OpDateTime = DateTime.Now;
             dbModel.OpUserID = UserID;
             
             _db.HotelCancellationPolicies.Add(dbModel);
             
            _db.SaveChanges();
             return 0;
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

        public List<CancelTypeSummary> GetCancelTypeSummary(int hotelID, string culture) 
        {
            var summaryList = new List<CancelTypeSummary>();
            var types = _db.TB_TypeCancel.ToList().Where(f => f.PartID == Convert.ToInt32(EnumPart.Hotel) && f.Active == true).ToList().OrderBy(o=>o.Sort);
            if (types != null)
            {
                foreach (var type in types)
                {
                    var summary = new CancelTypeSummary();
                    summary.CancelTypeID = type.ID;
                    summary.CancelTypeName = new Tools().GetDynamicSortProperty(type, "Name_" + culture).ToString();
                    summary.IsRefundable = type.Refundable == null ? false : (type.Refundable == false? false:true);
                    var data = _db.HotelCancellationPolicies.ToList().Where(h => h.HotellD == hotelID && h.CancelTypeID == type.ID).FirstOrDefault();
                    if (data != null)
                    {
                        summary.CancelSummaryText = new Tools().GetDynamicSortProperty(data, "PolicyDescription_" + culture).ToString();
                        summary.PrepaymentSummaryText = new Tools().GetDynamicSortProperty(data, "PaymentDescription_" + culture).ToString();
                    }
                    summaryList.Add(summary);
                }
                
            }
            return summaryList;
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

        public List<TypePrepayment> GetPrepaymentNames(string culture)
        {
            GBSDbContext _db = new GBSDbContext();
            List<TypePrepayment> list = new List<TypePrepayment>();
            var names = _db.TypePrepayments.ToList().OrderBy(o => o.Sort);
            if (names != null)
            {
                foreach (var p in names)
                {
                    var obj = new TypePrepayment();
                    obj.ID = p.ID;
                    obj.Name = new Tools().GetDynamicSortProperty(p, "Name_" + culture) as string;
                    obj.IsAfterReservation = p.IsAfterReservation;
                    list.Add(obj);
                }
            }
            return list;
        }
    }
}