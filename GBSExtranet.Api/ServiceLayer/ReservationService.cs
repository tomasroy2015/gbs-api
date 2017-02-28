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

namespace GBSExtranet.Api.ServiceLayer
{
    public class ReservationService : BaseService
    {
        public List<Reservation> GetReservations(Int64 ReservationID, long userID, bool systemadmin, string CultureValue)
        {
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetResevations_TB_Reservations_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "Name ASC,ID ASC");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@ReservationID", ReservationID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            List<Reservation> ListOfModel = new List<Reservation>();

            Int64 OpUserID = userID;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Reservation ReservationObj = new Reservation();
                    ReservationObj.ReservationID = Convert.ToInt32(dr["ReservationID"]);
                    ReservationObj.ReservationOwner = dr["FullName"].ToString();
                    ReservationObj.StatusName = dr["StatusName"].ToString();
                    DateTime dt1 = Convert.ToDateTime(dr["ReservationDate"]);
                    DateTime dt2 = Convert.ToDateTime(dr["CheckInDate"]);
                    DateTime dt3 = Convert.ToDateTime(dr["CheckOutDate"]);
                    ReservationObj.ReservationDate = (dt1.ToString("d"));
                    ReservationObj.CheckInDate = (dt2.ToString("d"));
                    ReservationObj.ReservationOperation = dr["ReservationOperationName"].ToString();
                    DateTime CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"]);
                    ReservationObj.CheckOutDate = (dt3.ToString("d"));

                    GBSExtranet.Api.Models.GBSHotelsEntities insertentity = new GBSExtranet.Api.Models.GBSHotelsEntities();

                    string rowValue = "";
                    string rowValues = "";
                    if (((dt.Rows.Count > 0) && ((CheckOutDate.AddDays(7) >= DateTime.Now)) && Convert.ToInt32(dr["ReservationID"]) == ReservationID))
                    {
                        DataTable tbl = BizApplication.GetUserOperations(_context, "Date DESC", CultureValue, "", Convert.ToString(ReservationID), Convert.ToString(17), null);
                        if (tbl.Rows.Count > 0)
                        {
                            DataRow row = tbl.Rows[0];
                            rowValue = row["Date"].ToString();
                        }

                        DataTable tbl1 = BizApplication.GetUserOperations(_context, "Date DESC", CultureValue, Convert.ToString(OpUserID), Convert.ToString(ReservationID), Convert.ToString(17), null);
                        if (tbl1.Rows.Count > 0)
                        {
                            DataRow rows = tbl1.Rows[0];
                            rowValues = rows["Date"].ToString();
                        }

                        if (systemadmin == true || (Business.BizApplication.GetUserOperations(_context, "Date DESC", CultureValue, Convert.ToString(OpUserID), Convert.ToString(ReservationID), Convert.ToString(2), null).Rows.Count == 0 ||
                        BizApplication.GetUserOperations(_context, "Date DESC", CultureValue, "", Convert.ToString(ReservationID), Convert.ToString(17), null).Rows.Count == 1 &&
                        (DateTime.ParseExact(rowValue, "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture)) > (DateTime.ParseExact(rowValue, "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture))))
                        {
                            ReservationObj.CreditCardProvider = dr["CCTypeName"].ToString();
                            string NameOnCreditcard = dr["CCFullName"].ToString();
                            ReservationObj.NameontheCreditCard = Decrypt128New(NameOnCreditcard, "2164285821854754", "5436265039712626");
                            string CreditcardNumber = dr["CCNo"].ToString();
                            ReservationObj.CreditCardNumber = Decrypt128New(CreditcardNumber, "6164285828955421", "6485880454987489");
                            string CVCCode = dr["CCCVC"].ToString();
                            ReservationObj.CVCCode = Decrypt128New(CVCCode, "5267912096542731", "6359629697944359");
                            string CardExpriryDate = dr["CCExpiration"].ToString();
                            ReservationObj.ExpirationDate = Decrypt128New(CardExpriryDate, "5216428540391821", "6961584652179891");
                            // divCCDisplayWarning.Visible = true;
                            if (systemadmin != true)
                            {
                                GBSExtranet.Api.Models.BizTbl_UserOperation UserObj = new GBSExtranet.Api.Models.BizTbl_UserOperation();
                                UserObj.UserID = OpUserID;
                                UserObj.Date = DateTime.Now;
                                UserObj.OperationTypeID = 2;
                                UserObj.IPAddress = "";
                                UserObj.PartID = 1;
                                UserObj.RecordID = ReservationID;
                                UserObj.UserSessionID = null;
                                insertentity.BizTbl_UserOperation.Add(UserObj);
                                insertentity.SaveChanges();
                                // Business.BizUser.AddUserOperation(BizDB, Convert.ToString(OpUserID), Convert.ToString(DateTime.Now),Business.BizCommon.Operation.CreditCardInfoViewed, "", Convert.ToString(ReservationID), "", "");
                            }

                        }
                        else
                        {
                            // MasterPage.AddMessage(BizDB, "CreditCardAlreadyDisplayedWarning", CultureValue, BizCommon.MessageType.Info);

                        }
                    }

                    ListOfModel.Add(ReservationObj);

                }
            }
            return ListOfModel;
        }


        public ResponseObject GetReservationStatement(string hotelID,string CultureValue, int offset)
        {
            List<Reservation> ListOfModel = new List<Reservation>();
            DataTable dt = new DataTable();
            ResponseObject data = new ResponseObject();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_GetReservationStatement_Reservation_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelId", hotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Reservation invoiceObj = new Reservation();
                    invoiceObj.ID = Convert.ToInt32(dr["ID"]);
                    invoiceObj.Name = dr["Name"].ToString();
                    invoiceObj.Surname = dr["Surname"].ToString();
                    invoiceObj.ComissionRate = dr["ComissionRate"].ToString();
                    invoiceObj.ActualAmount = dr["ActualAmount"].ToString();
                    invoiceObj.PayableAmounts = dr["PayableAmount"].ToString();
                    invoiceObj.ComissionAmount = dr["ComissionAmount"].ToString();
                    invoiceObj.CheckInDate = dr["CheckInDate"].ToString();
                    invoiceObj.CheckOutDate = dr["CheckOutDate"].ToString();
                    ListOfModel.Add(invoiceObj);
                }
                data.totalRows = ListOfModel.Count;
                ListOfModel = ListOfModel.Skip(offset).Take(20).ToList();
                data.rows = ListOfModel.Cast<object>().ToList();
            }
            return data;
        }
        public ResponseObject GetReservationStatementByDate(string hotelID, DateTime StartDate, DateTime Enddate, string CultureValue, int offset)
        {
            List<Reservation> ListOfModel = new List<Reservation>();
            DataTable dt = new DataTable();
            ResponseObject data = new ResponseObject();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_GetReservationStatementBYDate_Reservation_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelId", hotelID);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@Enddate", Enddate);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Reservation invoiceObj = new Reservation();
                    invoiceObj.ID = Convert.ToInt32(dr["ID"]);
                    invoiceObj.Name = dr["Name"].ToString();
                    invoiceObj.Surname = dr["Surname"].ToString();
                    invoiceObj.ComissionRate = dr["ComissionRate"].ToString();
                    invoiceObj.ActualAmount = dr["ActualAmount"].ToString();
                    invoiceObj.PayableAmounts = dr["PayableAmount"].ToString();
                    invoiceObj.ComissionAmount = dr["ComissionAmount"].ToString();
                    invoiceObj.CheckInDate = dr["CheckInDate"].ToString();
                    invoiceObj.CheckOutDate = dr["CheckOutDate"].ToString();
                    ListOfModel.Add(invoiceObj);
                }
                data.totalRows = ListOfModel.Count;
                ListOfModel = ListOfModel.Skip(offset).Take(20).ToList();
                data.rows = ListOfModel.Cast<object>().ToList();
            }
            return data;
        }   
        public string Decrypt128New(string cipherText, string key, string IV)
        {
            //  string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                encryptor.Key = Encoding.UTF8.GetBytes(key);
                encryptor.IV = Encoding.UTF8.GetBytes(IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public class Encryption64
        {

            private byte[] key = { };
            //private byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

            private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            public string Decrypt(string stringToDecrypt, string sEncryptionKey)
            {
                byte[] inputByteArray = new byte[stringToDecrypt.Length];
                try
                {
                    key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    inputByteArray = Convert.FromBase64String(stringToDecrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV),
                                                                      CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    return encoding.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }



        }
    }
}