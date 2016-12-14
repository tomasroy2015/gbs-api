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

namespace GBSExtranet.Api.ServiceLayer
{
    public class PropertyReservationService:BaseService
    {
        public List<PropertyReservation> GetReservationByProperty(int hotelID, string CultureValue,int offset) 
        {
             DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetPropertyReservations_TB_Reservation_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@HotelID", hotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            int count = 0;

            List<PropertyReservation> ListOfModel = new List<PropertyReservation>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyReservation ProObj = new PropertyReservation();
                    count = count + 1;
                    ProObj.RoomIndex = count;

                    ProObj.ID = Convert.ToInt32(dr["ID"]);
                    ProObj.HotelName = dr["HotelName"].ToString();
                    ProObj.ReservationID = Convert.ToInt64(dr["ReservationID"]);
                    ProObj.ReservationDate = Convert.ToDateTime(dr["ReservationDate"]);
                    ProObj.OwnerFullName = dr["FullName"].ToString();
                    ProObj.FullName = dr["FullName"].ToString() + " " + dr["GuestFullName"].ToString();

                    ProObj.Email = dr["Email"].ToString();
                    ProObj.Phone = dr["Phone"].ToString();
                    ProObj.Address = dr["Address"].ToString();
                    ProObj.PostCode = dr["PostCode"].ToString();
                    ProObj.City = dr["City"].ToString();

                    ProObj.SalutationTypeName = dr["SalutationTypeName"].ToString();
                    ProObj.ReservationOwner = dr["FullName"].ToString() + "  (" + dr["SalutationTypeName"].ToString() + ")";
                    ProObj.GuestFullName = dr["GuestFullName"].ToString();
                    ProObj.CheckInDate = Convert.ToDateTime(dr["CheckInDate"]);
                    // DateTime CheckOutDate= Convert.ToDateTime(dr["CheckOutDate"]);
                    ProObj.CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"]);
                    ProObj.Note = dr["Note"].ToString();
                    ProObj.Status = dr["StatusName"].ToString();
                    ProObj.Rooms = dr["RoomTypeName"].ToString();

                    ProObj.PayableAmount = dr["Currencysymbol"].ToString() + " " + Convert.ToDouble(dr["PayableAmount"]);
                    ProObj.PayableAmountSum = ProObj.PayableAmountSum + Convert.ToDouble(dr["PayableAmount"]);

                    ProObj.PayableAmountValue = Convert.ToDouble(dr["PayableAmount"]);
                    ProObj.ComissionSum = +Convert.ToDouble(dr["Comission"]);
                    ProObj.ComissionValue = Convert.ToDouble(dr["Comission"]);

                    ProObj.Commission = dr["Currencysymbol"].ToString() + " " + Convert.ToString(dr["Comission"]);
                    ProObj.Currencysymbol = dr["Currencysymbol"].ToString();
                    ProObj.NightCount = Convert.ToInt32(dr["NightCount"]);
                    ProObj.HotelEmail = dr["HotelEmail"].ToString();
                    ProObj.HotelPhone = dr["HotelPhone"].ToString();
                    ProObj.HotelAddress = dr["HotelAddress"].ToString();
                    ProObj.HotelPostCode = dr["HotelPostCode"].ToString();
                    ProObj.HotelCityName = dr["HotelCityName"].ToString();
                    ProObj.CountryName = dr["CountryName"].ToString();
                    ProObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                    ProObj.PeopleCount = Convert.ToInt32(dr["PeopleCount"]);
                    ProObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                    ProObj.Property = dr["HotelName"].ToString() + "  (" + Convert.ToInt32(dr["HotelID"]) + ")";
                    ProObj.AccommodationName = dr["HotelAccommodationTypeName"].ToString();
                    ProObj.TravellerTypeName = dr["TravellerTypeName"].ToString();

                    string Estimatedtime = dr["EstimatedArrivalTime"].ToString();
                    if (Estimatedtime == "Not Certain")
                    {
                        ProObj.EstimatedArrivalTime = "Not Certain";
                    }
                    else
                    {
                        ProObj.EstimatedArrivalTime = dr["EstimatedArrivalTime"].ToString();
                    }
                    ProObj.CancelPolicy = dr["HotelCancelTypeName"].ToString();
                    ProObj.PenaltyRateName = dr["PenaltyRateTypeName"].ToString();
                    ProObj.RefundableDayCount = Convert.ToInt32(dr["RefundableDayCount"]);
                    ProObj.FirmID = Convert.ToInt32(dr["FirmID"]);
                    ProObj.UserID = Convert.ToInt32(dr["UserID"]);
                    ProObj.ReservationCultureID = Convert.ToInt32(dr["ReservationCultureID"]);
                    ProObj.ReservationCultureCode = dr["ReservationCultureCode"].ToString();
                    ProObj.ReservationCultureSystemCode = dr["ReservationCultureSystemCode"].ToString();
                    if (ProObj.CancelPolicy == "Refundable")
                    {
                        String CancelPolicyText = GetCancellationPolicy(CultureValue);
                        ProObj.CancelPolicyDesc = CancelPolicyText.Replace("#Days#", dr["RefundableDayCount"].ToString()).Replace("#Penalty#", dr["PenaltyRateTypeName"].ToString());
                    }
                    else
                    {
                        ProObj.CancelPolicyDesc = "";
                    }
                    ProObj.HotelRoomID = Convert.ToInt64(dr["HotelRoomID"].ToString());
                    Encryption64 objEncryptreservation = new Encryption64();
                    string EncryptReservationID = dr["ReservationID"].ToString();
                    EncryptReservationID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptReservationID, "58421043")));
                    ProObj.EncryptReservationID = EncryptReservationID;

                    string Encryptcc = "CC";
                    Encryptcc = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(Encryptcc, "58421043")));
                    ProObj.Encryptcc = Encryptcc;

                    string Encrypthistory = "History";
                    Encrypthistory = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(Encrypthistory, "58421043")));
                    ProObj.Encrypthistory = Encrypthistory;

                    string EncryptChangeDate = "ChangeDate";
                    EncryptChangeDate = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptChangeDate, "58421043")));
                    ProObj.EncryptChangeDate = EncryptChangeDate;

                    ProObj.creditCardUsed = Convert.ToBoolean(dr["creditCardUsed"]);
                    ProObj.statusID = Convert.ToInt32(dr["statusID"]);
                    ProObj.reservationOperationID = dr["reservationOperationID"].ToString();

                    ProObj.BedOptionNo = dr["BedOptionNo"].ToString();

                    if (DateTime.Now.Date <= ProObj.CheckOutDate.AddDays(AddDaysvalue(CultureValue, "ReservationOperationCloseOutDayCount")))
                    {
                        if (ProObj.creditCardUsed == true && ProObj.statusID == 1 && (GetFirmRequest(2, ProObj.ReservationID) == 0))
                        {
                            ProObj.lbtnCC = true;
                        }
                        else
                        {
                            ProObj.lbtnCC = false;
                        }

                        if (ProObj.creditCardUsed == true && (DateTime.Now.Date < ProObj.CheckInDate) && ProObj.statusID == 1 && (GetFirmRequest(3, ProObj.ReservationID, "1") == 0) && (ProObj.reservationOperationID == "1" || ProObj.reservationOperationID == "5"))
                        {
                            ProObj.lbtnReportAsInvalidCC = true;
                        }
                        else
                        {
                            ProObj.lbtnReportAsInvalidCC = false;
                        }
                        DateTime Date1 = GetDate("Date DESC", CultureValue, ProObj.ReservationID, 16);
                        TimeSpan diff = DateTime.Now - Date1;
                        double hoursDiff = diff.TotalHours;
                        //if ((DateTime.Now.Date < ProObj.CheckInDate) && (ProObj.statusID == 1) && (ProObj.reservationOperationID == "4") && ((GetDate("Date DESC",CultureValue,ProObj.ReservationID,16).Hour - DateTime.Now.Hour) >= 24))
                        if ((DateTime.Now.Date < ProObj.CheckInDate) && (ProObj.statusID == 1) && (ProObj.reservationOperationID == "4") && (hoursDiff >= 24))
                        {
                            ProObj.lbtnCancel = true;
                        }
                        else
                        {
                            ProObj.lbtnCancel = false;
                        }

                        if ((DateTime.Now.Date >= ProObj.CheckInDate.AddDays(1)) && (ProObj.statusID == 1) && (GetFirmRequest(2, ProObj.ReservationID) == 0))
                        {
                            ProObj.lbtnMarkAsNoUse = true;
                        }
                        else
                        {
                            ProObj.lbtnMarkAsNoUse = false;
                            //ProObj.lbtnMarkAsNoUse = true;
                        }
                        if ((DateTime.Now.Date >= ProObj.CheckInDate.AddDays(1)) && (ProObj.statusID == 1) && (GetFirmRequest(1, ProObj.ReservationID) == 0) && (GetFirmRequest(2, ProObj.ReservationID) == 0))
                        {
                            ProObj.lbtnChangeDate = true;
                        }
                        else
                        {
                            ProObj.lbtnChangeDate = false;
                        }
                    }
                    else
                    {
                        ProObj.lbtnCC = false;
                        ProObj.lbtnReportAsInvalidCC = false;
                        ProObj.lbtnCancel = false;
                        ProObj.lbtnChangeDate = false;
                        ProObj.lbtnMarkAsNoUse = false;
                    }
                    ListOfModel.Add(ProObj);
                }
                ListOfModel = ListOfModel.Skip(offset).Take(10).ToList();
            }
            return ListOfModel;
        }

        public List<PropertyReservation> GetReservations(string CultureValue)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetPropertyReservations_TB_Reservation_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "CheckInDate ASC,ID DESC");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            ArrayList HotelReservations = new ArrayList();

            List<PropertyReservation> ListOfModel = new List<PropertyReservation>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!HotelReservations.Contains(Convert.ToInt64(dr["ReservationID"])))
                    {
                        HotelReservations.Add(Convert.ToInt64(dr["ReservationID"]));


                        PropertyReservation ProObj = new PropertyReservation();
                        //  count = count + 1;
                        ProObj.RoomIndex = 1;

                        ProObj.ID = Convert.ToInt32(dr["ID"]);
                        ProObj.HotelName = dr["HotelName"].ToString();
                        ProObj.ReservationID = Convert.ToInt64(dr["ReservationID"]);
                        ProObj.ReservationDate = Convert.ToDateTime(dr["ReservationDate"]);
                        ProObj.OwnerFullName = dr["FullName"].ToString();
                        ProObj.FullName = dr["FullName"].ToString() + " " + dr["GuestFullName"].ToString();
                        ProObj.Email = dr["Email"].ToString();
                        ProObj.Phone = dr["Phone"].ToString();
                        ProObj.Address = dr["Address"].ToString();
                        ProObj.PostCode = dr["PostCode"].ToString();
                        ProObj.City = dr["City"].ToString();
                        ProObj.SalutationTypeName = dr["SalutationTypeName"].ToString();
                        ProObj.ReservationOwner = dr["FullName"].ToString() + "  (" + dr["SalutationTypeName"].ToString() + ")";
                        ProObj.GuestFullName = dr["GuestFullName"].ToString();
                        ProObj.CheckInDate = Convert.ToDateTime(dr["CheckInDate"]);
                        // var date = (ProObj.CheckInDate.ToString("d"));
                        // ProObj.CheckInDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);
                        // ProObj.CheckInDate.TimeOfDay.ToString("");

                        //    ProObj.CheckInDate = DateTime.Parse(ProObj.CheckInDate.ToShortDateString());

                        ProObj.CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"]);
                        //  var dates = (ProObj.CheckInDate.ToString("d"));
                        // ProObj.CheckOutDate = DateTime.ParseExact(dates, "dd-MM-yyyy", null);
                        // ProObj.CheckOutDate.TimeOfDay.ToString("");
                        //    ProObj.CheckOutDate = DateTime.Parse(ProObj.CheckOutDate.ToShortDateString());

                        ProObj.Note = dr["Note"].ToString();
                        ProObj.Status = dr["StatusName"].ToString();

                        //ProObj.Rooms = dr["RoomTypeName"].ToString();
                        ProObj.Rooms = GetReservationsRoomTypes(dr["ReservationID"].ToString(),CultureValue);

                        ProObj.PayableAmount = dr["Currencysymbol"].ToString() + " " + Convert.ToDouble(dr["PayableAmount"]);
                        ProObj.PayableAmountSum = ProObj.PayableAmountSum + Convert.ToDouble(dr["PayableAmount"]);

                        ProObj.PayableAmountValue = Convert.ToDouble(dr["PayableAmount"]);
                        ProObj.ComissionSum = +Convert.ToDouble(dr["Comission"]);
                        ProObj.ComissionValue = Convert.ToDouble(dr["Comission"]);

                        ProObj.Commission = dr["Currencysymbol"].ToString() + " " + Convert.ToString(dr["Comission"]);
                        ProObj.Currencysymbol = dr["Currencysymbol"].ToString();
                        ProObj.NightCount = Convert.ToInt32(dr["NightCount"]);
                        ProObj.HotelEmail = dr["HotelEmail"].ToString();
                        ProObj.HotelPhone = dr["HotelPhone"].ToString();
                        ProObj.HotelAddress = dr["HotelAddress"].ToString();
                        ProObj.HotelPostCode = dr["HotelPostCode"].ToString();
                        ProObj.HotelCityName = dr["HotelCityName"].ToString();
                        ProObj.CountryName = dr["CountryName"].ToString();
                        ProObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                        ProObj.PeopleCount = Convert.ToInt32(dr["PeopleCount"]);
                        ProObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                        ProObj.Property = dr["HotelName"].ToString() + "  (" + Convert.ToInt32(dr["HotelID"]) + ")";
                        ProObj.AccommodationName = dr["HotelAccommodationTypeName"].ToString();
                        ProObj.TravellerTypeName = dr["TravellerTypeName"].ToString();

                        string Estimatedtime = dr["EstimatedArrivalTime"].ToString();
                        if (Estimatedtime == "Not Certain")
                        {
                            ProObj.EstimatedArrivalTime = "Not Certain";
                        }
                        else
                        {
                            ProObj.EstimatedArrivalTime = dr["EstimatedArrivalTime"].ToString();
                        }

                        ProObj.CancelPolicy = dr["HotelCancelTypeName"].ToString();
                        ProObj.PenaltyRateName = dr["PenaltyRateTypeName"].ToString();
                        ProObj.RefundableDayCount = Convert.ToInt32(dr["RefundableDayCount"]);
                        ProObj.FirmID = Convert.ToInt32(dr["FirmID"]);
                        ProObj.UserID = Convert.ToInt32(dr["UserID"]);
                        ProObj.ReservationCultureID = Convert.ToInt32(dr["ReservationCultureID"]);
                        ProObj.ReservationCultureCode = dr["ReservationCultureCode"].ToString();
                        ProObj.ReservationCultureSystemCode = dr["ReservationCultureSystemCode"].ToString();
                        if (ProObj.CancelPolicy == "Refundable")
                        {
                            String CancelPolicyText = GetCancellationPolicy(CultureValue);
                            ProObj.CancelPolicyDesc = CancelPolicyText.Replace("#Days#", dr["RefundableDayCount"].ToString()).Replace("#Penalty#", dr["PenaltyRateTypeName"].ToString());
                        }
                        else
                        {
                            ProObj.CancelPolicyDesc = "";
                        }
                        ProObj.HotelRoomID = Convert.ToInt64(dr["HotelRoomID"].ToString());
                        Encryption64 objEncryptreservation = new Encryption64();
                        string EncryptReservationID = dr["ReservationID"].ToString();
                        EncryptReservationID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptReservationID, "58421043")));
                        ProObj.EncryptReservationID = EncryptReservationID;

                        string Encryptcc = "CC";
                        Encryptcc = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(Encryptcc, "58421043")));
                        ProObj.Encryptcc = Encryptcc;

                        string Encrypthistory = "History";
                        Encrypthistory = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(Encrypthistory, "58421043")));
                        ProObj.Encrypthistory = Encrypthistory;

                        string EncryptChangeDate = "ChangeDate";
                        EncryptChangeDate = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptChangeDate, "58421043")));
                        ProObj.EncryptChangeDate = EncryptChangeDate;

                        ProObj.creditCardUsed = Convert.ToBoolean(dr["creditCardUsed"]);
                        ProObj.statusID = Convert.ToInt32(dr["statusID"]);
                        ProObj.reservationOperationID = dr["reservationOperationID"].ToString();

                        ProObj.BedOptionNo = dr["BedOptionNo"].ToString();

                        if (DateTime.Now.Date <= ProObj.CheckOutDate.AddDays(AddDaysvalue(CultureValue, "ReservationOperationCloseOutDayCount")))
                        {
                            if (ProObj.creditCardUsed == true && ProObj.statusID == 1 && (GetFirmRequest(2, ProObj.ReservationID) == 0))
                            {
                                ProObj.lbtnCC = true;
                            }
                            else
                            {
                                ProObj.lbtnCC = false;
                            }

                            if (ProObj.creditCardUsed == true && (DateTime.Now.Date < ProObj.CheckInDate) && ProObj.statusID == 1 && (GetFirmRequest(3, ProObj.ReservationID, "1") == 0) && (ProObj.reservationOperationID == "1" || ProObj.reservationOperationID == "5"))
                            {
                                ProObj.lbtnReportAsInvalidCC = true;
                            }
                            else
                            {
                                ProObj.lbtnReportAsInvalidCC = false;
                            }
                            DateTime Date1 = GetDate("Date DESC", CultureValue, ProObj.ReservationID, 16);
                            TimeSpan diff = DateTime.Now - Date1;
                            double hoursDiff = diff.TotalHours;
                            //if ((DateTime.Now.Date < ProObj.CheckInDate) && (ProObj.statusID == 1) && (ProObj.reservationOperationID == "4") && ((GetDate("Date DESC",CultureValue,ProObj.ReservationID,16).Hour - DateTime.Now.Hour) >= 24))
                            if ((DateTime.Now.Date < ProObj.CheckInDate) && (ProObj.statusID == 1) && (ProObj.reservationOperationID == "4") && (hoursDiff >= 24))
                            {
                                ProObj.lbtnCancel = true;
                            }
                            else
                            {
                                ProObj.lbtnCancel = false;
                            }

                            if ((DateTime.Now.Date >= ProObj.CheckInDate.AddDays(1)) && (ProObj.statusID == 1) && (GetFirmRequest(2, ProObj.ReservationID) == 0))
                            {
                                ProObj.lbtnMarkAsNoUse = true;
                            }
                            else
                            {
                                ProObj.lbtnMarkAsNoUse = false;
                                //ProObj.lbtnMarkAsNoUse = true;
                            }
                            if ((DateTime.Now.Date >= ProObj.CheckInDate.AddDays(1)) && (ProObj.statusID == 1) && (GetFirmRequest(1, ProObj.ReservationID) == 0) && (GetFirmRequest(2, ProObj.ReservationID) == 0))
                            {
                                ProObj.lbtnChangeDate = true;
                            }
                            else
                            {
                                ProObj.lbtnChangeDate = false;
                            }
                        }
                        else
                        {
                            ProObj.lbtnCC = false;
                            ProObj.lbtnReportAsInvalidCC = false;
                            ProObj.lbtnCancel = false;
                            ProObj.lbtnChangeDate = false;
                            ProObj.lbtnMarkAsNoUse = false;
                        }
                        ListOfModel.Add(ProObj);

                    }

                }
            }
            return ListOfModel;
        }


        public List<PropertyReservation> GetReservationsForView(long ReservationID,string culture)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetPropertyReservations_TB_Reservation_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            ArrayList HotelReservations = new ArrayList();

            List<PropertyReservation> ListOfModel = new List<PropertyReservation>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (ReservationID == (Convert.ToInt64(dr["ReservationID"])))
                    {
                        //    HotelReservations.Add(Convert.ToInt64(dr["ReservationID"]));


                        PropertyReservation ProObj = new PropertyReservation();
                        //  count = count + 1;
                        ProObj.RoomIndex = 1;

                        ProObj.ID = Convert.ToInt32(dr["ID"]);
                        ProObj.HotelName = dr["HotelName"].ToString();
                        ProObj.ReservationID = Convert.ToInt64(dr["ReservationID"]);
                        ProObj.ReservationDate = Convert.ToDateTime(dr["ReservationDate"]);
                        ProObj.PropertyConditionslist = PropertyConditions(Convert.ToInt32(dr["HotelID"]), Convert.ToDateTime(dr["ReservationDate"]),culture);
                        ProObj.OwnerFullName = dr["FullName"].ToString();
                        ProObj.FullName = dr["FullName"].ToString() + " " + dr["GuestFullName"].ToString();
                        ProObj.Email = dr["Email"].ToString();
                        ProObj.Phone = dr["Phone"].ToString();
                        ProObj.Address = dr["Address"].ToString();
                        ProObj.PostCode = dr["PostCode"].ToString();
                        ProObj.City = dr["City"].ToString();
                        ProObj.SalutationTypeName = dr["SalutationTypeName"].ToString();
                        ProObj.ReservationOwner = dr["FullName"].ToString() + "  (" + dr["SalutationTypeName"].ToString() + ")";
                        ProObj.GuestFullName = dr["GuestFullName"].ToString();
                        ProObj.CheckInDate = Convert.ToDateTime(dr["CheckInDate"]);
                        // var date = (ProObj.CheckInDate.ToString("d"));
                        // ProObj.CheckInDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);
                        // ProObj.CheckInDate.TimeOfDay.ToString("");

                        //    ProObj.CheckInDate = DateTime.Parse(ProObj.CheckInDate.ToShortDateString());

                        ProObj.CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"]);
                        //  var dates = (ProObj.CheckInDate.ToString("d"));
                        // ProObj.CheckOutDate = DateTime.ParseExact(dates, "dd-MM-yyyy", null);
                        // ProObj.CheckOutDate.TimeOfDay.ToString("");
                        //    ProObj.CheckOutDate = DateTime.Parse(ProObj.CheckOutDate.ToShortDateString());

                        ProObj.Note = dr["Note"].ToString();
                        ProObj.Status = dr["StatusName"].ToString();

                        ProObj.Rooms = dr["RoomTypeName"].ToString();
                        // ProObj.Rooms = GetReservationsRoomTypes(dr["ReservationID"].ToString());

                        ProObj.PayableAmount = dr["Currencysymbol"].ToString() + " " + Convert.ToDouble(dr["PayableAmount"]);
                        ProObj.PayableAmountSum = ProObj.PayableAmountSum + Convert.ToDouble(dr["PayableAmount"]);

                        ProObj.PayableAmountValue = Convert.ToDouble(dr["PayableAmount"]);
                        ProObj.ComissionSum = +Convert.ToDouble(dr["Comission"]);
                        ProObj.ComissionValue = Convert.ToDouble(dr["Comission"]);
                        ProObj.RoomPrice = dr["Amount"].ToString();
                        ProObj.Commission = dr["Currencysymbol"].ToString() + " " + Convert.ToString(dr["Comission"]);
                        ProObj.Currencysymbol = dr["Currencysymbol"].ToString();
                        ProObj.NightCount = Convert.ToInt32(dr["NightCount"]);
                        ProObj.HotelEmail = dr["HotelEmail"].ToString();
                        ProObj.HotelPhone = dr["HotelPhone"].ToString();
                        ProObj.HotelAddress = dr["HotelAddress"].ToString();
                        ProObj.HotelPostCode = dr["HotelPostCode"].ToString();
                        ProObj.HotelCityName = dr["HotelCityName"].ToString();
                        ProObj.CountryName = dr["CountryName"].ToString();
                        ProObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                        ProObj.PeopleCount = Convert.ToInt32(dr["PeopleCount"]);
                        ProObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                        ProObj.Property = dr["HotelName"].ToString() + "  (" + Convert.ToInt32(dr["HotelID"]) + ")";
                        ProObj.AccommodationName = dr["HotelAccommodationTypeName"].ToString();
                        ProObj.TravellerTypeName = dr["TravellerTypeName"].ToString();

                        string Estimatedtime = dr["EstimatedArrivalTime"].ToString();
                        if (Estimatedtime == "Not Certain")
                        {
                            ProObj.EstimatedArrivalTime = "Not Certain";
                        }
                        else
                        {
                            ProObj.EstimatedArrivalTime = dr["EstimatedArrivalTime"].ToString();
                        }

                        ProObj.CancelPolicy = dr["HotelCancelTypeName"].ToString();
                        ProObj.PenaltyRateName = dr["PenaltyRateTypeName"].ToString();
                        ProObj.RefundableDayCount = Convert.ToInt32(dr["RefundableDayCount"]);
                        ProObj.FirmID = Convert.ToInt32(dr["FirmID"]);
                        ProObj.UserID = Convert.ToInt32(dr["UserID"]);
                        ProObj.ReservationCultureID = Convert.ToInt32(dr["ReservationCultureID"]);
                        ProObj.ReservationCultureCode = dr["ReservationCultureCode"].ToString();
                        ProObj.ReservationCultureSystemCode = dr["ReservationCultureSystemCode"].ToString();
                        if (Convert.ToBoolean(dr["Refundable"]) == true)
                        {
                            String CancelPolicyText = new PropertyCancelService().GetCancelTypeSummary(ProObj.HotelID, culture).Where(f => f.IsRefundable == true).FirstOrDefault().CancelSummaryText;
                            ProObj.CancelPolicyDesc = CancelPolicyText.Replace("#Days#", dr["RefundableDayCount"].ToString()).Replace("#Penalty#", dr["PenaltyRateTypeName"].ToString());
                        }
                        else
                        {
                            ProObj.CancelPolicyDesc = "Non Refundable";
                        }
                        ProObj.HotelRoomID = Convert.ToInt64(dr["HotelRoomID"].ToString());
                        Encryption64 objEncryptreservation = new Encryption64();
                        string EncryptReservationID = dr["ReservationID"].ToString();
                        EncryptReservationID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptReservationID, "58421043")));
                        ProObj.EncryptReservationID = EncryptReservationID;

                        string Encryptcc = "CC";
                        Encryptcc = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(Encryptcc, "58421043")));
                        ProObj.Encryptcc = Encryptcc;

                        string Encrypthistory = "History";
                        Encrypthistory = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(Encrypthistory, "58421043")));
                        ProObj.Encrypthistory = Encrypthistory;

                        string EncryptChangeDate = "ChangeDate";
                        EncryptChangeDate = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptChangeDate, "58421043")));
                        ProObj.EncryptChangeDate = EncryptChangeDate;

                        ProObj.creditCardUsed = Convert.ToBoolean(dr["creditCardUsed"]);
                        ProObj.statusID = Convert.ToInt32(dr["statusID"]);
                        ProObj.reservationOperationID = dr["reservationOperationID"].ToString();

                        ProObj.BedOptionNo = dr["BedOptionNo"].ToString();

                        if (DateTime.Now.Date <= ProObj.CheckOutDate.AddDays(AddDaysvalue(culture, "ReservationOperationCloseOutDayCount")))
                        {
                            if (ProObj.creditCardUsed == true && ProObj.statusID == 1 && (GetFirmRequest(2, ProObj.ReservationID) == 0))
                            {
                                ProObj.lbtnCC = true;
                            }
                            else
                            {
                                ProObj.lbtnCC = false;
                            }

                            if (ProObj.creditCardUsed == true && (DateTime.Now.Date < ProObj.CheckInDate) && ProObj.statusID == 1 && (GetFirmRequest(3, ProObj.ReservationID, "1") == 0) && (ProObj.reservationOperationID == "1" || ProObj.reservationOperationID == "5"))
                            {
                                ProObj.lbtnReportAsInvalidCC = true;
                            }
                            else
                            {
                                ProObj.lbtnReportAsInvalidCC = false;
                            }
                            DateTime Date1 = GetDate("Date DESC", culture, ProObj.ReservationID, 16);
                            TimeSpan diff = DateTime.Now - Date1;
                            double hoursDiff = diff.TotalHours;
                            //if ((DateTime.Now.Date < ProObj.CheckInDate) && (ProObj.statusID == 1) && (ProObj.reservationOperationID == "4") && ((GetDate("Date DESC",CultureValue,ProObj.ReservationID,16).Hour - DateTime.Now.Hour) >= 24))
                            if ((DateTime.Now.Date < ProObj.CheckInDate) && (ProObj.statusID == 1) && (ProObj.reservationOperationID == "4") && (hoursDiff >= 24))
                            {
                                ProObj.lbtnCancel = true;
                            }
                            else
                            {
                                ProObj.lbtnCancel = false;
                            }

                            if ((DateTime.Now.Date >= ProObj.CheckInDate.AddDays(1)) && (ProObj.statusID == 1) && (GetFirmRequest(2, ProObj.ReservationID) == 0))
                            {
                                ProObj.lbtnMarkAsNoUse = true;
                            }
                            else
                            {
                                ProObj.lbtnMarkAsNoUse = false;
                                //ProObj.lbtnMarkAsNoUse = true;
                            }
                            if ((DateTime.Now.Date >= ProObj.CheckInDate.AddDays(1)) && (ProObj.statusID == 1) && (GetFirmRequest(1, ProObj.ReservationID) == 0) && (GetFirmRequest(2, ProObj.ReservationID) == 0))
                            {
                                ProObj.lbtnChangeDate = true;
                            }
                            else
                            {
                                ProObj.lbtnChangeDate = false;
                            }
                        }
                        else
                        {
                            ProObj.lbtnCC = false;
                            ProObj.lbtnReportAsInvalidCC = false;
                            ProObj.lbtnCancel = false;
                            ProObj.lbtnChangeDate = false;
                            ProObj.lbtnMarkAsNoUse = false;
                        }
                        ProObj.RoomRateDetailslist = getRoomRateDetails(dr["ID"].ToString(), Convert.ToDateTime(dr["CheckInDate"]), Convert.ToDateTime(dr["CheckOutDate"]),culture);
                        ProObj.SelectedBedText = GetRoomBedInfoDetails(dr["ReservationID"].ToString(), dr["HotelRoomID"].ToString(), dr["BedOptionNo"].ToString(),culture);
                        ListOfModel.Add(ProObj);

                    }
                   
                }
            }
            
            return ListOfModel;
        }

        public string GetRoomBedInfoDetails(string ReservationID, string HotelRoomID, string BedOptionNo, string CultureValue)
        {
            DataTable HotelReservationsHistory = BizReservation.GetHotelReservationHistory(_context, "LogDateTime DESC", CultureValue, "", ReservationID);
            DataRow[] roomBedInfo = BizHotel.GetHotelRoomBeds(_context, string.Empty, CultureValue, "", Convert.ToString(HotelRoomID)).Select("OptionNo = " + BedOptionNo);

            DataRow[] hotelReservationHistoryRows = HotelReservationsHistory.Select("HotelReservationID = " + ReservationID);
            DataTable hotelReservationHistory = new DataTable();
            DataRow[] reservationHistoryDR = null;
            DataView reservationHistoryDW = null;
            string oldValueText = "";
            //var text = BizMessage.GetMessage(_context, "OldValue", CultureValue);
            //if (text != null)
            //    oldValueText = text.ToString();

           // string oldValueText = Resources.Resources.OldValue;

            if (hotelReservationHistoryRows.Count() > 0)
            {
                hotelReservationHistory = hotelReservationHistoryRows.CopyToDataTable();
            }



            string roomBedText = string.Empty;
            foreach (DataRow optionalBed in roomBedInfo)
            {
                roomBedText += optionalBed["BedTypeNameWithCount"] + ", ";
            }

            string BedPreference = string.Empty;
            BizUtil.TrimEnd(ref roomBedText, ", ");



            BedPreference = roomBedText;

            if (hotelReservationHistoryRows.Count() > 0)
            {
                reservationHistoryDR = hotelReservationHistory.Select("BedOptionNo <> " + BedOptionNo);
                if (reservationHistoryDR.Count() > 0)
                {
                    reservationHistoryDW = reservationHistoryDR.CopyToDataTable().AsDataView();
                    reservationHistoryDW.Sort = "LogDateTime DESC";
                    DataRow[] oldRoomBedInfo = BizHotel.GetHotelRoomBeds(_context, string.Empty, CultureValue, "", Convert.ToString(HotelRoomID)).Select("OptionNo = " + reservationHistoryDW[0]["BedOptionNo"]);
                    string oldRoomBedText = string.Empty;
                    foreach (DataRow optionalBed in oldRoomBedInfo)
                    {
                        oldRoomBedText += optionalBed["BedTypeNameWithCount"] + ", ";
                    }

                    BizUtil.TrimEnd(ref oldRoomBedText, ", ");
                    // roomBedText.TrimEnd(',');
                    BedPreference += BizCommon.HTMLEmptyStr + "<i>(" + oldRoomBedText + ")</i>";
                }
            }

            return BedPreference;
        }

        public List<PropertyReservation> getRoomRateDetails(string HotelReservationID, DateTime StartDate, DateTime EndDate, string CultureValue)
        {
            string CheckInDatevalue = Convert.ToString(StartDate);
            string CheckOutDatevalue = Convert.ToString(EndDate.AddDays(-1));

            DataTable roomRate = BizReservation.GetHotelReservationRate(_context, "Date", CultureValue, "", HotelReservationID, CheckInDatevalue, CheckOutDatevalue);

            List<PropertyReservation> ObjList = new List<PropertyReservation>();
            if (roomRate != null)
            {
                if (roomRate.Rows.Count > 0)
                {
                    foreach (DataRow dr in roomRate.Rows)
                    {
                        PropertyReservation Obj = new PropertyReservation();

                        Obj.Day = Convert.ToString(dr["Day"]);
                        Obj.MonthName = Convert.ToString(dr["MonthName"]);
                        Obj.CurrencySymbol = Convert.ToString(dr["CurrencySymbol"]);
                        Obj.RoomPrice = Convert.ToString(dr["RoomPrice"]);
                        ObjList.Add(Obj);
                    }
                }
            }
            return ObjList;
        }
        public List<string> PropertyConditions(int HotelID, DateTime ReservationDate,string culture)
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelAttributes", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@AttributeTypeID", 2);
            cmd.Parameters.AddWithValue("@Date", ReservationDate);
            cmd.Parameters.AddWithValue("@Active", 1);
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", "AttributeHeaderSort,AttributeName");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            _sqlConnection.Close();

            List<string> ListOfModel = new List<string>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyReservation obj = new PropertyReservation();
                    // string value = dr["AttributeName"].ToString();

                    string ConditionDiscription = dr["AttributeName"].ToString();
                    if (dr["UnitValue"].ToString() != "")
                    {
                        ConditionDiscription += " " + "(" + dr["UnitName"].ToString() + ":" + " " + dr["UnitValue"].ToString() + ")";
                    }
                    if (dr["Charge"].ToString() != "")
                    {
                        ConditionDiscription += " " + "-" + dr["HotelUnitName"].ToString() + " " + dr["Charge"].ToString() + " " + dr["CurrencyName"].ToString();
                    }

                    ListOfModel.Add(ConditionDiscription);
                }
            }


            return ListOfModel;
        }

        public List<string> ReservationPromotions(int ReservationID,string culture)
        {
            // string PropertyConditions = "";
            DataTable ReservationPromotions = BizReservation.GetHotelReservationPromotions(_context, "ValidForAllRoomTypes DESC,PromotionSort", culture, ReservationID);

            List<string> ListOfModel = new List<string>();
            if (ReservationPromotions.Rows.Count > 0)
            {
                ReservationPromotions = ReservationPromotions.DefaultView.ToTable(true, "PromotionID", "PromotionType", "PromotionName", "GeneralPromotion", "PromotionDescription", "PromotionDiscountPercentage", "DiscountPercentage", "DayCount", "PromotionCount", "ValidForAllRoomTypes", "Region", "RegionName", "HotelRoomID", "RoomTypeName");
                foreach (DataRow dr in ReservationPromotions.Rows)
                {
                    PropertyReservation obj = new PropertyReservation();
                    // string value = dr["AttributeName"].ToString();
                    string PromotionDiscription = "";
                    if (dr["discountPercentage"].ToString() != "" && Convert.ToInt32(dr["discountPercentage"]) > 0)
                    {
                        PromotionDiscription = dr["PromotionName"].ToString() + " <i>(Discount % " + dr["discountPercentage"].ToString() + "  )</i>";
                    }
                    ListOfModel.Add(PromotionDiscription);
                }
            }


            return ListOfModel;
        }
        public string GetReservationsRoomTypes(string ReservationID, string CultureValue)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetPropertyReservations_TB_Reservation_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "HotelRoomID");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@ReservationID", ReservationID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            List<PropertyReservation> ListOfModel = new List<PropertyReservation>();
            string RoomType = "";
            ArrayList HotelReservations = new ArrayList();
            if (dt.Rows.Count > 0)
            {
                DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                int Count = 0;
                int RoomCount = 0;
                string RoomTypeName = "";
                foreach (DataRow dr in dt.Rows)
                {
                    if (Count == 0)
                    {
                        HotelReservations.Add(Convert.ToInt64(dr["HotelRoomID"]));
                    }

                    if (!HotelReservations.Contains(Convert.ToInt64(dr["HotelRoomID"])))
                    {
                        RoomType += "* " + "<label> " + RoomTypeName + "</label>" + " ( <label>" + RoomCount + " </label>)  <br />";
                        HotelReservations.Add(Convert.ToInt64(dr["HotelRoomID"]));
                        RoomCount = Convert.ToInt32(dr["RoomCount"]);
                    }
                    else
                    {
                        RoomCount += Convert.ToInt32(dr["RoomCount"]);
                        RoomTypeName = dr["RoomTypeName"].ToString();
                    }

                    if (dr == lastRow)
                    {
                        RoomType += "* " + "<label> " + dr["RoomTypeName"].ToString() + "</label>" + " ( <label>" + RoomCount + " </label>)  <br />";
                    }



                    Count++;
                }
            }
            return RoomType;
        }

        public string GetCancellationPolicy(string Culture)
        {
            string Code = "RefundableInfo";
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[B_GetCancelPolicyText_BizTbl_Message_SP]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Code", Code);
            cmd.Parameters.AddWithValue("@CultureCode", Culture);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            Code = Convert.ToString(cmd.ExecuteScalar());
            _sqlConnection.Close();
            return Code;
        }
        public DateTime GetDate(string OrderBy, string Culture, Int64 RecordID, int OperationTypeID)
        {
            DataTable dt = new DataTable();
            DateTime value = DateTime.Now;

            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetUserOperations", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", Culture);
            cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            cmd.Parameters.AddWithValue("@RecordIDs", RecordID);
            cmd.Parameters.AddWithValue("@OperationTypeID", OperationTypeID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        value = Convert.ToDateTime(dt.Rows[0]["Date"]);
                    }
                }
            }
            return value;
        }
        public int GetFirmRequest(int RequestTypeID, Int64 ReservationID, string FirmRequstStatus = "")
        {
            DataTable dt = new DataTable();
            int value = 0;

            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_GetFirmRequests_TB_FirmRequest_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RequestTypeID", RequestTypeID);
            cmd.Parameters.AddWithValue("@ReservationID", ReservationID);
            cmd.Parameters.AddWithValue("@StatusID", FirmRequstStatus);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        value = Convert.ToInt32(dt.Rows[0]["RequestCount"]);
                    }
                }
            }
            return value;
        }
        public int AddDaysvalue(string culture, string ParameterCode)
        {
            DataTable dt = new DataTable();
            int value = 0;

            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_GetParameter_BizTbl_Parameter_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@UserRemindLink", ParameterCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        value = Convert.ToInt32(dt.Rows[0]["Value"]);
                    }
                }
            }
            return value;
        }

        public string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        public class Encryption64
        {

            private byte[] key = { };
            //private byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

            private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            //public string Decrypt(string stringToDecrypt, string sEncryptionKey)
            //{
            //    byte[] inputByteArray = new byte[stringToDecrypt.Length];
            //    try
            //    {
            //        key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //        inputByteArray = Convert.FromBase64String(stringToDecrypt);
            //        MemoryStream ms = new MemoryStream();
            //        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV),
            //                                                          CryptoStreamMode.Write);
            //        cs.Write(inputByteArray, 0, inputByteArray.Length);
            //        cs.FlushFinalBlock();
            //        Encoding encoding = Encoding.UTF8;
            //        return encoding.GetString(ms.ToArray());
            //    }
            //    catch (Exception ex)
            //    {
            //        return ex.Message;
            //    }
            //}



            public string Encrypt(string stringToEncrypt, string sEncryptionKey)
            {
                try
                {
                    key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    Byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV),
                                                                      CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}