using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using System.ServiceModel;
using GBSExtranet.Api.ViewModel;
using GBSExtranet.Repository;
using Business;
using System.Data;
using GBSExtranet.Api.ServiceLayer;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Collections;
using System.Globalization;
namespace GBSExtranet.Api.ServiceLayer
{
    public class HotelRateAvailabilityService : BaseService
    {
        DataTable Dates = new DataTable();
        DataTable HotelRooms = new DataTable();
        DataTable HotelRateRefundable = new DataTable();
        DataTable HotelRateNonRefundable = new DataTable(); 
        DataTable HotelAvailabilityTable = new DataTable();
        DataTable HotelAvailability = new DataTable();
        Hashtable DateAvailability = new Hashtable();
        CommonService ObjCommon = new CommonService();
        public DataTable GetDates(string OrderBy, DateTime startDate, DateTime endDate,string culture)
        {
            _sqlConnection.Open();
            
            SqlCommand cmd = new SqlCommand("TB_SP_GetDates", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(Dates);
            _sqlConnection.Close();
            return Dates;

        }
        public List<HotelRateAvailability> GetRefundRateList(int hotelID, DateTime startDate, DateTime endDate)
        {
            HotelRateRefundable = new HotelRateService().GetHotelRate(hotelID, startDate, endDate, "1", "1");
            List<HotelRateAvailability> DateList = new List<HotelRateAvailability>();
            try
            {
                if (HotelRateRefundable.Rows.Count > 0)
                {
                    foreach (DataRow dr in HotelRateRefundable.Rows)
                    {
                        HotelRateAvailability DatesObj = new HotelRateAvailability();
                        DatesObj.ID = Convert.ToInt32(dr["ID"]);
                        DatesObj.Date = Convert.ToDateTime(dr["Date"]);
                        DatesObj.DateID = Convert.ToInt64(dr["DateID"]);
                        //DatesObj.Day = Convert.ToInt32(dr["Day"]);
                        DatesObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
                        DatesObj.HotelRoomID = Convert.ToInt32(dr["HotelRoomID"]);
                        DatesObj.RoomPrice = Convert.ToDecimal(dr["RoomPrice"]);
                        DateList.Add(DatesObj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return DateList;
            
        }
        public List<HotelRateAvailability> GetNonRefundRateList(int hotelID, DateTime startDate, DateTime endDate) 
        {
            HotelRateNonRefundable = new HotelRateService().GetHotelRate(hotelID, startDate, endDate, "1", "2");
            List<HotelRateAvailability> DateList = new List<HotelRateAvailability>();

            if (HotelRateNonRefundable.Rows.Count > 0)
            {
                foreach (DataRow dr in HotelRateNonRefundable.Rows)
                {
                    HotelRateAvailability DatesObj = new HotelRateAvailability();
                    DatesObj.ID = Convert.ToInt32(dr["ID"]);
                    DatesObj.Date = Convert.ToDateTime(dr["Date"]);
                    DatesObj.DateID = Convert.ToInt64(dr["DateID"]);
                   // DatesObj.Day = Convert.ToInt32(dr["Day"]);
                    DatesObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
                    DatesObj.HotelRoomID = Convert.ToInt32(dr["HotelRoomID"]);
                    DatesObj.RoomPrice = Convert.ToDecimal(dr["RoomPrice"]);
                    DateList.Add(DatesObj);
                }
            }
            return DateList;
            
        }
        public List<HotelRateAvailability> GetDatesList()
        {
            //_sqlConnection.Open();
            //SqlCommand cmd = new SqlCommand("TB_SP_GetDates", _sqlConnection);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Culture", CultureValue);
            //cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            //cmd.Parameters.AddWithValue("@StartDate", startDate);
            //cmd.Parameters.AddWithValue("@EndDate", endDate);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            //_sqlConnection.Close();

            List<HotelRateAvailability> DateList = new List<HotelRateAvailability>();

            if (Dates.Rows.Count > 0)
            {
                foreach (DataRow dr in Dates.Rows)
                {
                    HotelRateAvailability DatesObj = new HotelRateAvailability();
                    DatesObj.DateID = Convert.ToInt32(dr["ID"]);
                    DatesObj.Date = Convert.ToDateTime(dr["Date"]);
                    DatesObj.IsValidForOpenClose = DatesObj.Date >= DateTime.Today.Date ? true : false;
                    DatesObj.DayID = Convert.ToInt32(dr["DayID"]);
                    DatesObj.Day = Convert.ToInt32(dr["Day"]);
                    DatesObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
                    DatesObj.DayName = Convert.ToString(dr["DayName"]);
                    DatesObj.MonthID = Convert.ToInt32(dr["MonthID"]);
                    DatesObj.MonthName = Convert.ToString(dr["MonthName"]);
                    DatesObj.Year = Convert.ToInt32(dr["Year"]);
                    DateList.Add(DatesObj);
                }
            }
            return DateList;
        }

        public DataTable GetHotelAvailability(DateTime startDate, DateTime endDate, int HotelID)
        {
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelAvailability", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(HotelAvailabilityTable);
            _sqlConnection.Close();
            return HotelAvailabilityTable;
        }
        public List<HotelRateAvailability> GetHotelRateAvailability()
        {
            //_sqlConnection.Open();
            //SqlCommand cmd = new SqlCommand("TB_SP_GetHotelAvailability", _sqlConnection);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@StartDate", startDate);
            //cmd.Parameters.AddWithValue("@EndDate", endDate);
            //cmd.Parameters.AddWithValue("@HotelID", HotelID);
            //DataTable dt = new DataTable();
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            //_sqlConnection.Close();

            List<HotelRateAvailability> HotelAvailabilityList = new List<HotelRateAvailability>();

            if (HotelAvailabilityTable.Rows.Count > 0)
            {
                foreach (DataRow dr in HotelAvailabilityTable.Rows)
                {
                    HotelRateAvailability HotelAvailabilityObj = new HotelRateAvailability();
                    HotelAvailabilityObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelAvailabilityObj.DateID = Convert.ToInt64(dr["DateID"]);
                    HotelAvailabilityObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
                    HotelAvailabilityObj.HotelRoomID = Convert.ToInt32(dr["HotelRoomID"]);
                    HotelAvailabilityObj.AvailableRoomCount = Convert.ToInt32(dr["AvailableRoomCount"]);
                    HotelAvailabilityObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                    HotelAvailabilityObj.CloseToArrival = Convert.ToInt32(dr["CloseToArrival"]);
                    HotelAvailabilityObj.CloseToDeparture = Convert.ToInt32(dr["CloseToDeparture"]);
                    HotelAvailabilityObj.MinimumStay = Convert.ToInt32(dr["MinimumStay"]);
                    HotelAvailabilityObj.Closed = Convert.ToInt32(dr["Closed"]);
                    HotelAvailabilityObj.RoomRateMissing = Convert.ToInt32(dr["RoomRateMissing"]);

                    HotelAvailabilityList.Add(HotelAvailabilityObj);
                }
            }
            return HotelAvailabilityList;
        }

        public int CreateHotelRoomAvailability(string HotelRoomID, DateTime StartDate, DateTime EndDate, string RoomCount)
        {

            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_CreateHotelRoomAvailability", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@RoomCount", RoomCount);
            int i = Convert.ToInt32(cmd.ExecuteNonQuery());
            _sqlConnection.Close();
            return i;
        }

        public DataTable GetHotelRooms(string OrderBy, int HotelID,string culture)
        {
            _sqlConnection.Open();
            
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelRooms", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@Active", 1);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(HotelRooms);
            _sqlConnection.Close();
            return HotelRooms;
        }

        public List<HotelRateAvailability> GetHotelRoomsList()
        {

            List<HotelRateAvailability> HotelRoomsList = new List<HotelRateAvailability>();

            if (HotelRooms.Rows.Count > 0)
            {
                foreach (DataRow dr in HotelRooms.Rows)
                {
                    HotelRateAvailability HotelRoomsObj = new HotelRateAvailability();
                    HotelRoomsObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelRoomsObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                    HotelRoomsObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelRoomsObj.RoomTypeName = Convert.ToString(dr["RoomTypeName"]);
                    HotelRoomsObj.RoomSize = Convert.ToInt32(dr["RoomSize"]);
                    HotelRoomsObj.MaxPeopleCount = Convert.ToInt32(dr["MaxPeopleCount"]);
                    HotelRoomsObj.IDWithMaxPeopleCount = Convert.ToString(dr["IDWithMaxPeopleCount"]);
                    HotelRoomsObj.MaxChildrenCount = Convert.ToInt32(dr["MaxChildrenCount"]);
                    HotelRoomsObj.BabyCotCount = Convert.ToInt32(dr["BabyCotCount"]);
                    HotelRoomsObj.ExtraBedCount = Convert.ToInt32(dr["ExtraBedCount"]);
                    HotelRoomsObj.SmokingTypeID = Convert.ToString(dr["SmokingTypeID"]);

                    HotelRoomsObj.SmokingTypeName = Convert.ToString(dr["SmokingTypeName"]);
                    HotelRoomsObj.ViewTypeID = Convert.ToString(dr["ViewTypeID"]);
                    HotelRoomsObj.ViewTypeName = Convert.ToString(dr["ViewTypeName"]);
                    HotelRoomsObj.IncludedInRoomTypeCaption = Convert.ToString(dr["IncludedInRoomTypeCaption"]);

                    Encryption64 objEncryptHotelRoomID = new Encryption64();
                    string EncryptHotelRoomID = Convert.ToString(dr["ID"]);
                    EncryptHotelRoomID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptHotelRoomID.Encrypt(EncryptHotelRoomID, "58421043")));
                    HotelRoomsObj.EncryptHotelRoomID = EncryptHotelRoomID;

                    HotelRoomsList.Add(HotelRoomsObj);
                }
            }
            return HotelRoomsList;
        }



        public bool CloseOpenHotelAvailability(DateTime StartDate, DateTime EndDate, bool Closed, int HotelID, string HotelRoomID = "")
        {
            bool status = false;

            //  int HotelRoomIDs = Convert.ToInt32(HotelRoomID);
            try
            {
                if (HotelRoomID != "")
                {
                    int HotelRoomIDs = Convert.ToInt32(HotelRoomID);
                    var hotelAvailability = from availability in _db.TB_HotelAvailability
                                            join hotelRoom in _db.TB_HotelRoom
                                            on availability.HotelRoomID equals hotelRoom.ID
                                            join date in _db.TB_Date
                                            on availability.DateID equals date.ID
                                            where date.Date >= StartDate && date.Date <= EndDate && hotelRoom.HotelID == HotelID && availability.HotelRoomID == HotelRoomIDs
                                            select availability;

                    foreach (var items in hotelAvailability)
                    {

                        int hotelAvailabilityIDValue = Convert.ToInt32(items.ID);
                        // var ID = items.ID;
                        var obj = _db.TB_HotelAvailability.Where(x => x.ID == hotelAvailabilityIDValue).FirstOrDefault();
                        //db.TB_HotelRoomAttribute.Remove(obj);
                        obj.Closed = Closed;

                    }
                    _db.SaveChanges();
                    status = true;
                }
                else if (HotelRoomID == "")
                {

                    var hotelAvailability = from availability in _db.TB_HotelAvailability
                                            join hotelRoom in _db.TB_HotelRoom
                                            on availability.HotelRoomID equals hotelRoom.ID
                                            join date in _db.TB_Date
                                            on availability.DateID equals date.ID
                                            where date.Date >= StartDate && date.Date <= EndDate && hotelRoom.HotelID == HotelID
                                            select availability;

                    foreach (var items in hotelAvailability)
                    {
                        //availability = availability_loopVariable;
                        //availability.availability.Closed = Closed;

                        int hotelAvailabilityIDValue = Convert.ToInt32(items.ID);
                        // var ID = items.ID;
                        var obj = _db.TB_HotelAvailability.Where(x => x.ID == hotelAvailabilityIDValue).FirstOrDefault();
                        //db.TB_HotelRoomAttribute.Remove(obj);
                        obj.Closed = Closed;
                    }
                    _db.SaveChanges();
                    status = true;
                }
            }

            catch (Exception ex)
            {

            }


            return status;
        }

        public void AddUserOperation(int OperationTypeID, string PartID = "", string RecordID = "", string UserSessionID = "")
        {
            bool status = false;
            try
            {
                GBSExtranet.Api.Models.BizTbl_UserOperation UserObjRate = new GBSExtranet.Api.Models.BizTbl_UserOperation();
                //UserObjRate.UserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
                UserObjRate.Date = DateTime.Now;
                UserObjRate.OperationTypeID = 100;
                UserObjRate.IPAddress = ObjCommon.GetIPAddress();
                UserObjRate.PartID = 1;
                UserObjRate.RecordID = Convert.ToInt64(RecordID);
                UserObjRate.UserSessionID = Convert.ToInt64(UserSessionID);
                _db.BizTbl_UserOperation.Add(UserObjRate);
                _db.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {

            }
        }

        public static object CheckEmptyStringDBParameter(object Value, bool ReturnInteger = false, bool ReturnDate = false, bool ReturnDouble = false, bool ReturnDecimal = false, bool ReturnBoolean = false, bool ReturnLong = false)
        {

            if (Value == string.Empty)
            {
                return null;
            }

            if (ReturnInteger)
            {
                return Convert.ToInt32(Value);
            }

            //if (ReturnDate)
            //{

            //    return DateTime.ParseExact(Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            //}

            if (ReturnDouble)
            {
                return Convert.ToDouble(Value);
            }

            if (ReturnDecimal)
            {
                return Convert.ToDecimal(Value);
            }

            if (ReturnBoolean)
            {
                return Convert.ToBoolean(Value);
            }

            if (ReturnLong)
            {
                return Convert.ToInt64(Value);
            }

            return Value;

        }

        public class Encryption64
        {

            private byte[] key = { };
            //private byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

            private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

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

        public HotelRateAvailabilityViewModel HotelRateAndAvailability(int hotelID, string culture, DateTime startDate, DateTime endDate)
        {
            
            HotelRateAvailabilityViewModel rateAvailability = new HotelRateAvailabilityViewModel();
            rateAvailability.Day = new List<HotelRateAvailability>();
            rateAvailability.Room = new List<HotelRateAvailability>();
            rateAvailability.CloseOpenAvailabilityDay = new List<HotelRateAvailability>();
            rateAvailability.HotelAvailabilityList = new List<HotelRateAvailability>();
            rateAvailability.DisplayResult = string.Empty;

           // int HotelID = BizContext.HotelID;
            HotelRooms = GetHotelRooms("Sort,RoomTypeName", hotelID, culture);

            LoadCalendar(ref rateAvailability, hotelID, culture, startDate, endDate);
            RoomDay(ref rateAvailability, hotelID);

            return rateAvailability;
        }

        public void LoadCalendar(ref HotelRateAvailabilityViewModel rateAvailability, int hotelID, string culture, DateTime startDate, DateTime endDate)
        {
            try
            {
               // DateTime startDate = DateTime.Now.Date;
                //string s = startDate.ToString("yyyy/MM/dd");
                string formatted = startDate.ToString("yyyy/MM/dd");
              //  DateTime endDate = DateTime.Now.Date.AddDays(20);
                string formatted1 = endDate.ToString("yyyy/MM/dd");
                Dates = GetDates("Date",startDate,endDate, culture);

                string startingDate = Convert.ToString(startDate);
                string endingDate = Convert.ToString(endDate);

                HotelAvailability = GetHotelAvailability(startDate, endDate, hotelID);
                rateAvailability.HotelAvailabilityList = GetHotelRateAvailability();
                rateAvailability.RefundablePrices = GetRefundRateList(hotelID, startDate, endDate);
                rateAvailability.NonRefundablePrices = GetNonRefundRateList(hotelID, startDate, endDate);
                foreach (DataRow hotelRoom in HotelRooms.Rows)
                {
                    //Müsaitlik datası olmayanlar için yüklenir
                    int i = CreateHotelRoomAvailability(hotelRoom["ID"].ToString(), startDate, endDate, hotelRoom["RoomCount"].ToString());
                }

                if (startDate.Month != endDate.Month)
                {

                    rateAvailability.ColSpan = DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day + 1;
                    rateAvailability.Month1 = Dates.Select("Date='" + formatted + "'")[0]["MonthName"];
                    var value = Dates.Select("Date='" + formatted + "'")[0]["MonthName"];
                    rateAvailability.Month2ColSpan = endDate.Day;
                    // ViewBag.Month2 = 1;
                    rateAvailability.Month2 = Dates.Select("Date='" + formatted1 + "'")[0]["MonthName"];
                }
                else
                {
                    rateAvailability.ColSpan = endDate.Day - startDate.Day + 1;
                    rateAvailability.Month1 = Dates.Select("Date='" + formatted + "'")[0]["MonthName"];
                    rateAvailability.Month2ColSpan = 0;
                }
              //  HttpContext.Current.Session["DateAvailability"] = new Hashtable();

                //Day = GetDatesList();
                rateAvailability.Day = GetDatesList();

                //Room = GetHotelRoomsList();
                rateAvailability.Room = GetHotelRoomsList();
                // CloseOpenAvailabilityDay = GetDatesList();
                rateAvailability.CloseOpenAvailabilityDay = GetDatesList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void RoomDay(ref HotelRateAvailabilityViewModel rateAvailability,int hotelID)
        {
            List<HotelRateAvailability> RoomDayList = new List<HotelRateAvailability>();
            string DisplayResult = "";
            try
            {
                if (HotelAvailability != null)
                {
                    if (HotelAvailability.Rows.Count > 0)
                    {
                        DisplayResult = "Valid";
                        foreach (var RoomData in rateAvailability.Room)
                        {
                            foreach (var Date in rateAvailability.Day)
                            {
                                HotelRateAvailability Obj = new HotelRateAvailability();
                                Obj.DateID = Date.DateID;
                                Obj.HotelRoomID = RoomData.ID;
                                Obj.RoomTypeID = RoomData.RoomTypeID;
                                Obj.RoomTypeName = RoomData.RoomTypeName;
                                string hotelRoomID = Convert.ToString(RoomData.ID);
                                //string dateStr = Convert.ToString(Date.Date);
                                string formatted2 = Date.Date.ToString("yyyy/MM/dd");

                                DateTime date = Date.Date;
                                DataRow hotelRoomAvailability = HotelAvailability.Select("HotelRoomID=" + hotelRoomID + " AND Date='" + formatted2 + "'")[0];
                               // DateAvailability = (Hashtable)HttpContext.Current.Session["DateAvailability"];
                                if (!DateAvailability.Contains(date))
                                {
                                   DateAvailability.Add(date, true);
                                }
                                if (Convert.ToBoolean(hotelRoomAvailability["Closed"]))
                                {
                                    Obj.Date = Date.Date;
                                    Obj.hotelRoomAvailabilityText = "Closed";
                                    Obj.HotelAvailableStatus = "1";
                                    //DateAvailability[date] = false;
                                }
                                else if (Convert.ToBoolean(hotelRoomAvailability["RoomRateMissing"]))
                                {
                                    Obj.Date = Date.Date;
                                    Obj.hotelRoomAvailabilityText = "RoomRateMissing";
                                    Obj.lbtnAvailableRoomCount = "0";
                                    Obj.HotelAvailableStatus = "0";
                                    //DateAvailability[date] = true;
                                }
                                else if (Convert.ToInt16(hotelRoomAvailability["AvailableRoomCount"]) == 0)
                                {
                                    Obj.Date = Date.Date;
                                    Obj.hotelRoomAvailabilityText = "AvailableRoomCount";
                                    Obj.lbtnAvailableRoomCount = "0";
                                    Obj.HotelAvailableStatus = "0";
                                    //DateAvailability[date] = true;
                                }
                                else
                                {
                                    Obj.Date = Date.Date;
                                    Obj.lbtnAvailableRoomCount = Convert.ToString(hotelRoomAvailability["AvailableRoomCount"]);
                                    Obj.hotelRoomAvailabilityText = "";
                                    Obj.HotelAvailableStatus = "0";
                                    //DateAvailability[date] = true;
                                }
                                RoomDayList.Add(Obj);
                            }
                        }
                    }
                }

                rateAvailability.RoomDay = RoomDayList;
                rateAvailability.DisplayResult = DisplayResult;
                rateAvailability.DateAvailability = DateAvailability;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Hashtable GetDateAvailability()
        {
            return DateAvailability;
        }

        //public bool CloseOpenAvailabilityDayUpdate(string Datevalue, int HotelAvailabilityStatus, string HotelRoomID)
        //{
        //    //string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        //    Datevalue = Datevalue.Replace("12:00:00 ص", "12:00:00");

        //    DateTime StartDate = DateTime.Now.Date;
        //    string nw = Datevalue.ToString();
        //    DateTime dt = DateTime.Now.Date;
        //    //if (CultureValue == "ar")
        //    //{
        //    //    StartDate = DateTime.ParseExact(nw, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
        //    //}
        //    //else
        //    //{
        //    if (Datevalue.Contains('.'))
        //    {

        //        string[] nwFormat = nw.Split(' ');

        //        StartDate = DateTime.ParseExact(nwFormat[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
        //    }
        //    else
        //    {
        //        StartDate = DateTime.ParseExact(nw, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
        //    }
        //    //}



        //   // AssignBizContext();
        //    int HotelID = BizContext.HotelID;
        //    bool closed = (HotelAvailabilityStatus == 1 ? false : true);

        //    bool status = CloseOpenHotelAvailability(StartDate, StartDate, closed, HotelID, HotelRoomID);
        //    return status;
        //}
        public bool CloseOpenAvailabilityRefresh(string Datevalue, int hotelID, string userSessionID,string isClosed,string hotelRoomID)
        {
            try
            {
                DateTime StartDate = DateTime.Now;

                Datevalue = Datevalue.Replace("12:00:00 ص", "12:00:00");
                Datevalue = Datevalue.Replace('T', ' ');
                string nw = Datevalue.ToString();
                DateTime dt = DateTime.Now;
                if (Datevalue.Contains('.'))
                {
                    string[] nwFormat = nw.Split(' ');
                    StartDate = DateTime.ParseExact(nwFormat[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    StartDate = DateTime.ParseExact(nw, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
                    //DateTime result;
                    //DateTime.TryParseExact(Datevalue, "yyyy-dd-MM h:mm tt",CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
                    //StartDate = result;
                }

                //int HotelID = BizContext.HotelID;
                //string UserSessionID = BizContext.UserSessionID;

                //DateAvailability = (Hashtable)HttpContext.Current.Session["DateAvailability"];

                // bool closed = (HotelAvailabilityStatus == 1 ? false : true);

                bool i = CloseOpenHotelAvailability(StartDate, StartDate, (isClosed == "1" ? false : true), hotelID,hotelRoomID);

                AddUserOperation(101, "1", Convert.ToString(hotelID), userSessionID);
                return i;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}