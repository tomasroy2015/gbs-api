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
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Collections;
using GBSExtranet.Api.Models;
//BalsTechnology-SK   
namespace GBSExtranet.Api.ServiceLayer
{
    public class InvoiceService : BaseService
    {
        public ResponseObject GetInvoices(string CultureValue, int offset)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            ResponseObject data = new ResponseObject();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetInvoice_Paid", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            _sqlConnection.Close();

            List<InvoiceDetails> ListOfModel = new List<InvoiceDetails>();
            if (ds != null)
            {
                dt = ds.Tables[0];
                dt1 = ds.Tables[1];
            }
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    InvoiceDetails InvoiceObj = new InvoiceDetails();
                    InvoiceObj.Period = dr["Period"].ToString();
                    InvoiceObj.Amount = dr["Amount"].ToString();
                    InvoiceObj.InvoiceDate = dr["InvoiceDate"].ToString();
                    InvoiceObj.DueDate = dr["DueDate"].ToString();
                    InvoiceObj.InvoiceID = dr["ID"].ToString();
                    ListOfModel.Add(InvoiceObj);
                }
                data.totalRows = ListOfModel.Count;
                ListOfModel = ListOfModel.Skip(offset).Take(10).ToList();
                data.rows = ListOfModel.Cast<object>().ToList();
            }
            return data;
        }

        public ResponseObject GetpendingInvoices(string CultureValue, int offset)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            ResponseObject data = new ResponseObject();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetInvoice_Notpaid", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            _sqlConnection.Close();

            List<InvoiceDetails> ListOfModel = new List<InvoiceDetails>();

            if (ds != null)
            {
                dt = ds.Tables[0];
                dt1 = ds.Tables[1];
            }

            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    InvoiceDetails InvoiceObj = new InvoiceDetails();
                    InvoiceObj.Period = dr["Period"].ToString();
                    InvoiceObj.Amount = dr["Amount"].ToString();
                    InvoiceObj.InvoiceDate = dr["InvoiceDate"].ToString();
                    InvoiceObj.DueDate = dr["DueDate"].ToString();
                    InvoiceObj.InvoiceID = dr["ID"].ToString();
                    ListOfModel.Add(InvoiceObj);
                }
                data.totalRows = ListOfModel.Count;
                ListOfModel = ListOfModel.Skip(offset).Take(10).ToList();
                data.rows = ListOfModel.Cast<object>().ToList();
            }
            return data;
        }

        public List<InvoiceDetails> GetMonthlyRevenue(string Month, string Year, string cultureCode)
        {
            List<InvoiceDetails> ListOfModel = new List<InvoiceDetails>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_GetMonthlyRevenue_Reservation_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    InvoiceDetails mrevenue = new InvoiceDetails();
                    mrevenue.Date = Convert.ToInt32(dr["Date"]);
                    mrevenue.ReservationDate = dr["ReservationDate"].ToString();
                    mrevenue.CommissionAmount = Convert.ToInt32(dr["ComissionAmount"]);
                    mrevenue.PayableAmount = Convert.ToInt32(dr["PayableAmount"]);
                    mrevenue.GrossRevenue = Convert.ToInt32(dr["GrossRevenue"]);
                    //mrevenue.PayableAmount = dr["PayableAmount"].ToString();
                    //mrevenue.ComissionAmount = dr["ComissionAmount"].ToString();
                    //mrevenue.GrossRevenue = dr["GrossRevenue"].ToString();
                 
                    ListOfModel.Add(mrevenue);
                }
            }
            return ListOfModel;
        }
    }
}