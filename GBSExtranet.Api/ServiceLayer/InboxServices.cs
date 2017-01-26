using GBSExtranet.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ServiceLayer
{
    public class InboxServices : BaseService
    {
        public List<InboxMessages> GetInboxmessages(string ReceiverID, string cultureCode)
        {
            List<InboxMessages> ListOfModel = new List<InboxMessages>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Getinboxmessages_TB_MessageBox_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReceiverID", ReceiverID);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    InboxMessages invoiceObj = new InboxMessages();
                    invoiceObj.MessageID = Convert.ToInt32(dr["MessageID"]);
                    invoiceObj.Subject = dr["Subject"].ToString();
                    invoiceObj.CreatedDate = dr["CreatedDate"].ToString();
                    invoiceObj.TotalMsg  = dr["TotalMsg"].ToString();
                    invoiceObj.Unreadmsg = dr["Unreadmsg"].ToString();
                   
                    ListOfModel.Add(invoiceObj);
                }
            }
            return ListOfModel;
        }
    }
}