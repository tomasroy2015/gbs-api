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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
namespace GBSExtranet.Api.ServiceLayer
{
    public class RoomRateAvailabilityService : BaseService
    {
        CommonService ObjCommon = new CommonService();

        //public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public string ReplaceComma(string text)
        {
            string value = "";
            if (text.Contains(','))
            {
                value = text.Replace(",", ";");
                value = value + ";";
            }
            else
            {
                value = text;
            }
            return value;
        }
        public void CreateHotelRoomAvailability(string HotelRoomID, string StartDate, string EndDate, RoomRateAvailability RoomCount)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (StartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (EndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_CreateHotelRoomAvailability]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            cmd.Parameters.AddWithValue("@RoomCount", RoomCount.RoomCount);
            cmd.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        public void CreateHotelRoomRate(string HotelRoomID, string StartDate, string EndDate, string HotelAccommodationTypeID, string PricePolicyTypeID)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (StartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (EndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_CreateHotelRoomRate]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            cmd.Parameters.AddWithValue("@HotelAccommodationTypeID", HotelAccommodationTypeID);
            cmd.Parameters.AddWithValue("@PricePolicyTypeID", PricePolicyTypeID);
            cmd.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        public HttpResponseMessage ExportRoomData(List<RoomRateAvailability> list, string fileFormat)
        {
            string Path = "";
            string fileName = "RoomData.xls";
            HttpResponseMessage response = new HttpResponseMessage(); 
            try
            {
               // DataTable dt = UtilityPlus.ToDataTable<RoomRateAvailability>(list);
               // DataSet ds = new DataSet();
               // ds.Tables.Add(dt);
               // //if (fileFormat == "excel")
               // //{
               //     Path = HttpContext.Current.Server.MapPath("~/App_Data/") + fileName;

               //     FileInfo FI = new FileInfo(Path);
               //     StringWriter stringWriter = new StringWriter();
               //     HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
               //     DataGrid DataGrd = new DataGrid();
               //     DataGrd.DataSource = ds;
               //     DataGrd.DataBind();

               //     DataGrd.RenderControl(htmlWrite);

               //     System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
               //     stringWriter.ToString().Normalize();
               //     vw.Write(stringWriter.ToString());
               //     vw.Flush();
               //     vw.Close();

               //     using (MemoryStream ms = new MemoryStream())
               //     {
               //         using (FileStream file = new FileStream(Path, FileMode.Open, FileAccess.Read))
               //         {
               //             byte[] bytes = new byte[file.Length];
               //             file.Read(bytes, 0, (int)file.Length);
               //             ms.Write(bytes, 0, (int)file.Length);


               //             response.Content = new ByteArrayContent(bytes.ToArray());
               //             response.Content.Headers.Add("x-filename", fileName);
               //             response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
               //             response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
               //             response.Content.Headers.ContentDisposition.FileName = fileName;
               //             response.StatusCode = HttpStatusCode.OK;
                           
               //         }
               //     }
                    
               //   //  response.Content = new StreamContent(new FileStream(Path, FileMode.Open));
               //    // response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xls");
               //    // response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    
               //     //if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempReport/" + FI.Name)))
               //     //{
               //     //    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempReport/" + FI.Name));
               //     //}
               //// }

                    
                        StringBuilder str = new StringBuilder();
                        str.Append("<table border=`" + "1px" + "`b>");
                        str.Append("<tr>");
                        str.Append("<td><b><font face=Arial Narrow size=3>Date</font></b></td>");
                        str.Append("<td><b><font face=Arial Narrow size=3>Day</font></b></td>");
                        str.Append("<td><b><font face=Arial Narrow size=3>SinglePrice</font></b></td>");
                        str.Append("</tr>");
                        foreach (RoomRateAvailability val in list)
                        {
                            str.Append("<tr>");
                            str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Date.ToString() +"</font></td>");
                            str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Day.ToString() + "</font></td>");
                            str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.SinglePrice.ToString() + "</font></td>");
                            str.Append("</tr>");
                        }
                        str.Append("</table>");
                        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str.ToString()); 

                        response.Content = new ByteArrayContent(bytes.ToArray());
                        response.Content.Headers.Add("x-filename", fileName);
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.xls");
                        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                        response.Content.Headers.ContentDisposition.FileName = fileName;
                        response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        public List<RoomRateAvailability> GetHotelRooms(int HotelID,string culture)
        {
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRooms]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", "RoomTypeName");
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            List<RoomRateAvailability> ListOfModel = new List<RoomRateAvailability>();
            string Roomname = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //if (Convert.ToInt32(dr["ID"]) == RoomID)
                    //{
                    RoomRateAvailability HotelObj = new RoomRateAvailability();
                    HotelObj.RoomID = Convert.ToInt32(dr["ID"]);
                    HotelObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelObj.MaxPeopleCount = Convert.ToInt32(dr["MaxPeopleCount"]);
                    HotelObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                    HotelObj.RoomTypeName = dr["RoomTypeName"].ToString();
                    ListOfModel.Add(HotelObj);
                    //}
                }
            }
            return ListOfModel;
        }
        public List<RoomRateAvailability> Getdates(string culture,string StartDate, string EndDate, string WeekDay, DataTable AvailabilityTable, DataTable RateTable, int RoomID, int MaximamPeopleCount)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (StartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (EndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetDates]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", "Date");
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            cmd.Parameters.AddWithValue("@WeekDayIDs", ReplaceComma(WeekDay));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            List<RoomRateAvailability> ListOfModel = new List<RoomRateAvailability>();
            string Roomname = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {                  
                    RoomRateAvailability HotelObj = new RoomRateAvailability();
                    HotelObj.DayID = Convert.ToInt32(dr["DayID"]);
                    HotelObj.Day = Convert.ToInt32(dr["Day"]);
                    HotelObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
                    HotelObj.MonthID = Convert.ToInt32(dr["MonthID"]);
                    HotelObj.Year = Convert.ToInt32(dr["Year"]);
                    HotelObj.DateID = dr["ID"].ToString();
                    string DateID = dr["ID"].ToString();
                    HotelObj.Date = dr["Date"].ToString();
                    HotelObj.DayName = dr["DayName"].ToString();
                    HotelObj.MonthName = dr["MonthName"].ToString();
                    HotelObj.MaxPeopleCount = MaximamPeopleCount;
                    if (RateTable.Rows.Count > 0)
                    {
                        foreach (DataRow drRate in RateTable.Rows)
                        {
                            if (DateID == drRate["DateID"].ToString() && RoomID == Convert.ToInt32(drRate["HotelRoomID"]))
                            {
                                HotelObj.SinglePrice = Convert.ToDecimal(drRate["SinglePrice"]);
                                HotelObj.DoublePrice = Convert.ToDecimal(drRate["DoublePrice"]);
                                HotelObj.RoomPrice = Convert.ToDecimal(drRate["RoomPrice"]);
                                int AvailableRoomCount = Convert.ToInt32(drRate["AvailableRoomCount"]);
                                bool Closed = Convert.ToBoolean(drRate["Closed"]);
                                bool RoomRateMissing = Convert.ToBoolean(drRate["RoomRateMissing"]);
                                if (Closed)
                                {
                                    HotelObj.CssClass = "Closed";
                                }
                                else if (AvailableRoomCount == 0)
                                {
                                    HotelObj.CssClass = "Full";
                                }
                                else if (RoomRateMissing)
                                {
                                    HotelObj.CssClass = "RateMissing";
                                }
                                else
                                {
                                    HotelObj.CssClass = "";
                                }
                            }
                        }
                    }
                    if (AvailabilityTable.Rows.Count > 0)
                    {
                        foreach (DataRow drAvail in AvailabilityTable.Rows)
                        {
                            if (DateID == drAvail["DateID"].ToString() && RoomID == Convert.ToInt32(drAvail["HotelRoomID"]))
                            {
                                HotelObj.AvailableRoomCount = Convert.ToInt32(drAvail["AvailableRoomCount"]);

                                HotelObj.MinimumStay = Convert.ToInt32(drAvail["MinimumStay"]);
                                HotelObj.RoomRateMissing = Convert.ToBoolean(drAvail["RoomRateMissing"]);
                                HotelObj.CloseToArrival = Convert.ToBoolean(drAvail["CloseToArrival"]);
                                HotelObj.CloseToDeparture = Convert.ToBoolean(drAvail["CloseToDeparture"]);
                                HotelObj.Closed = Convert.ToBoolean(drAvail["Closed"]);


                            }
                        }
                    }
                    
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }

        public bool SaveRoomAvailabilityAndRate(string StartDate, string Enddate,string culture,int hotelID, int AccommodationType, int PricePolicy, int RoomID, long UserSessionID, List<RoomRateAvailability> rates)
        {

            bool status = true;
            try
            {
                var HotelRooms = GetHotelRooms(hotelID, culture).FirstOrDefault(f => f.RoomID == RoomID);
                CreateHotelRoomAvailability(RoomID.ToString(), StartDate, Enddate, HotelRooms);
                CreateHotelRoomRate(RoomID.ToString(), StartDate, Enddate, AccommodationType.ToString(), PricePolicy.ToString());

                foreach (var rate in rates)
                {
                    int dateId = Convert.ToInt32(rate.DateID);
                    var AvailabilityTable = _db.TB_HotelAvailability.Where(x => x.DateID == dateId && x.HotelRoomID == RoomID).FirstOrDefault();

                    AvailabilityTable.RoomCount = rate.AvailableRoomCount;

                    AvailabilityTable.CloseToArrival = rate.CloseToArrival;
                    AvailabilityTable.CloseToDeparture = rate.CloseToDeparture;
                    AvailabilityTable.Closed = rate.Closed;

                    AvailabilityTable.MinimumStay = rate.MinimumStay <= 0 ? 1 : rate.MinimumStay;

                    AvailabilityTable.OpDateTime = DateTime.Now;
                    //  AvailabilityTable.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

                    var RateTable = _db.TB_HotelRate.Where(x => x.DateID == dateId && x.HotelRoomID == RoomID && x.PricePolicyTypeID == PricePolicy && x.HotelAccommodationTypeID == AccommodationType).FirstOrDefault();

                    RateTable.SinglePrice = rate.SinglePrice;

                    if (HotelRooms.MaxPeopleCount > 1)
                    {
                        RateTable.DoublePrice = rate.DoublePrice;
                    }
                    if (HotelRooms.MaxPeopleCount > 2)
                    {
                        RateTable.RoomPrice = rate.RoomPrice;
                    }
                    RateTable.OpDateTime = DateTime.Now;
                    //  RateTable.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                    _db.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //BizTbl_UserOperation UserObjAvail = new BizTbl_UserOperation();
            ////UserObjAvail.UserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            //UserObjAvail.Date = DateTime.Now;
            //UserObjAvail.OperationTypeID = 101;
            //UserObjAvail.IPAddress = ObjCommon.GetIPAddress();
            //UserObjAvail.PartID = 5;
            //UserObjAvail.RecordID = RoomID;
            //UserObjAvail.UserSessionID = UserSessionID;
            //_db.BizTbl_UserOperation.Add(UserObjAvail);
            //_db.SaveChanges();

            //BizTbl_UserOperation UserObjRate = new BizTbl_UserOperation();
            ////UserObjRate.UserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            //UserObjRate.Date = DateTime.Now;
            //UserObjRate.OperationTypeID = 100;
            //UserObjRate.IPAddress = ObjCommon.GetIPAddress();
            //UserObjRate.PartID = 5;
            //UserObjRate.RecordID = RoomID;
            //UserObjRate.UserSessionID = UserSessionID;
            //_db.BizTbl_UserOperation.Add(UserObjRate);
            //_db.SaveChanges();
            //}
            return status;
        }
        public DataTable GetHotelAvailability(int HotelID, string StartDate, string EndDate)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (StartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (EndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelAvailability]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            //cmd.Parameters.AddWithValue("@HotelRoomID", "RoomID");
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            //List<RoomRateAvailability> ListOfModel = new List<RoomRateAvailability>();
            //string Roomname = "";

            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        RoomRateAvailability HotelObj = new RoomRateAvailability();
            //        HotelObj.HotelAvailabityID = Convert.ToInt32(dr["ID"]);
            //        HotelObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
            //        HotelObj.RoomID = Convert.ToInt32(dr["HotelRoomID"]);
            //        HotelObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
            //        HotelObj.DateID = dr["DateID"].ToString();
            //        HotelObj.Date = dr["Date"].ToString();
            //        HotelObj.AvailableRoomCount = Convert.ToInt32(dr["AvailableRoomCount"]);
            //        HotelObj.MinimumStay = Convert.ToInt32(dr["MinimumStay"]);
            //        HotelObj.RoomRateMissing = Convert.ToBoolean(dr["RoomRateMissing"]);
            //        HotelObj.CloseToArrival = Convert.ToBoolean(dr["CloseToArrival"]);
            //        HotelObj.CloseToDeparture = Convert.ToBoolean(dr["CloseToDeparture"]);
            //        HotelObj.Closed = Convert.ToBoolean(dr["Closed"]);

            //        ListOfModel.Add(HotelObj);
            //    }
            // }
            return dt;
        }

        public DataTable GetHotelRate(int HotelID, string StartDate, string EndDate, string HotelAccommodationTypeID, string PricePolicyTypeID)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (StartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (EndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRate]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            cmd.Parameters.AddWithValue("@HotelAccommodationTypeID", HotelAccommodationTypeID);
            cmd.Parameters.AddWithValue("@PricePolicyTypeID", PricePolicyTypeID);
            //cmd.Parameters.AddWithValue("@HotelRoomID", "RoomID");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            //List<RoomRateAvailability> ListOfModel = new List<RoomRateAvailability>();
            //string Roomname = "";

            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        RoomRateAvailability HotelObj = new RoomRateAvailability();
            //        HotelObj.HotelRateID = Convert.ToInt32(dr["ID"]);
            //        HotelObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
            //        HotelObj.DateID = dr["DateID"].ToString();
            //        HotelObj.Date = dr["Date"].ToString();
            //        HotelObj.RoomID = Convert.ToInt32(dr["HotelRoomID"]);
            //        HotelObj.AvailableRoomCount = Convert.ToInt32(dr["AvailableRoomCount"]);
            //        HotelObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
            //        HotelObj.SinglePrice = Convert.ToDecimal(dr["SinglePrice"]);
            //        HotelObj.DoublePrice = Convert.ToDecimal(dr["DoublePrice"]);
            //        HotelObj.RoomPrice = Convert.ToDecimal(dr["RoomPrice"]);
            //        HotelObj.RoomRateMissing = Convert.ToBoolean(dr["RoomRateMissing"]);
            //        HotelObj.Closed = Convert.ToBoolean(dr["Closed"]);
            //        ListOfModel.Add(HotelObj);
            //    }
            //}
            return dt;
        }
    }
}