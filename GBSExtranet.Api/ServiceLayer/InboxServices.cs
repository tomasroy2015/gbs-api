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
                    InboxMessages inboxObj = new InboxMessages();
                    inboxObj.MessageID = Convert.ToInt32(dr["MessageID"]);
                    inboxObj.Subject = dr["Subject"].ToString();
                    inboxObj.CreatedDate = dr["CreatedDate"].ToString();
                    inboxObj.ReadStatus = dr["ReadStatus"].ToString();
                    inboxObj.TotalMsg  = dr["TotalMsg"].ToString();
                    inboxObj.Unreadmsg = dr["Unreadmsg"].ToString();
                   
                    ListOfModel.Add(inboxObj);
                }
            }
            return ListOfModel;
        }

        public int DeleteInboxmessages(string ReceiverID,int MessageID, string cultureCode)
        {
            int status = 0;
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Deleteinboxmessages_TB_MessageBox_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReceiverID", ReceiverID);
            cmd.Parameters.AddWithValue("@MessageID", MessageID);
            status = cmd.ExecuteNonQuery();
            _sqlConnection.Close();
            return status;
        }

        public List<InboxMessages> GetUserEmails(string cultureCode)
        {
            List<InboxMessages> ListOfModel = new List<InboxMessages>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Getuseremailmessages_BizTbl_User_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    InboxMessages inboxObj = new InboxMessages();
                    inboxObj.ID = Convert.ToInt32(dr["ID"]);
                    inboxObj.Email = dr["Email"].ToString();
                    inboxObj.HotelName = dr["HotelName"].ToString();
                    inboxObj.UserName = dr["UserName"].ToString();
                    ListOfModel.Add(inboxObj);
                }
            }
            return ListOfModel;
        }

        public int Insertsendmessage(string SenderID, string ReceiverID, string Subject,string Message, string cultureCode)
        {
            int status = 0;
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_InsertSendmessages_TB_MessageBox_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SenderID", SenderID);
            cmd.Parameters.AddWithValue("@ReceiverID", ReceiverID);
            cmd.Parameters.AddWithValue("@Subject", Subject);
            cmd.Parameters.AddWithValue("@Message", Message);
            cmd.Parameters.AddWithValue("@cultureCode", cultureCode);
            cmd.Parameters.AddWithValue("@ReadStatus", 0);
            status = cmd.ExecuteNonQuery();
            _sqlConnection.Close();
            return status;
        }
    }
}