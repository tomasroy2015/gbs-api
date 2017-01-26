using GBSExtranet.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ServiceLayer
{
    public class InvoiceDetailService : BaseService
    {
        public List<InvoiceDetails> GetInvoiceDetails(string invoiceid, string cultureCode)
        {
            List<InvoiceDetails> ListOfModel = new List<InvoiceDetails>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetInvoiceDetail", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InvoiceID", invoiceid);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    InvoiceDetails invoiceObj = new InvoiceDetails();
                    invoiceObj.ID = Convert.ToInt32(dr["ID"]);
                    invoiceObj.InvoiceID = dr["InvoiceID"].ToString();
                    invoiceObj.ReservationID = dr["ReservationID"].ToString();
                    invoiceObj.ReservationOwnerFullName = dr["ReservationOwnerFullName"].ToString();
                    invoiceObj.Amount = dr["Amount"].ToString();
                    invoiceObj.CurrencySymbol = dr["CurrencySymbol"].ToString();
                    invoiceObj.ComissionRate = dr["ComissionRate"].ToString();
                    invoiceObj.ComissionAmount = dr["ComissionAmount"].ToString();
                    invoiceObj.CheckInDate = dr["CheckInDate"].ToString();
                    invoiceObj.CheckOutDate = dr["CheckOutDate"].ToString();
                    invoiceObj.Period = dr["Period"].ToString();
                    invoiceObj.FirmName = dr["FirmName"].ToString();
                    ListOfModel.Add(invoiceObj);
                }
            }
            return ListOfModel;
        }
    }
}