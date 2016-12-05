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
using System.Globalization;
using System.Reflection;
using GBSExtranet.Api.Models;

namespace GBSExtranet.Api.ServiceLayer
{
    public class PromotionService:BaseService
    {
        CommonService ObjCommon = new CommonService();
        public List<NewPromotion> ReadAll(string culture)
        {
            List<NewPromotion> list = new List<NewPromotion>();

            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[B_Ex_GetPromotionDetails_TB_Promotion_SP]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", culture);
            cmd.Parameters.AddWithValue("@Part", 1);
            cmd.Parameters.AddWithValue("@GeneralPromotion", false);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    NewPromotion PageObj = new NewPromotion();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.PartID = Convert.ToInt32(dr["PartID"]);
                    if (dr["PromotionType"].ToString() == "BasicDeal")
                    {
                        PageObj.Color = "#df8733";
                    }
                    else if (dr["PromotionType"].ToString() == "MinimumStay")
                    {
                        PageObj.Color = "#686ddc";
                    }
                    else if (dr["PromotionType"].ToString() == "EarlyBooker")
                    {
                        PageObj.Color = "#db7166";
                    }
                    else if (dr["PromotionType"].ToString() == "LastMinute")
                    {
                        PageObj.Color = "#5fb1ff";
                    }
                    else if (dr["PromotionType"].ToString() == "TwentyFourHourPromotion")
                    {
                        PageObj.Color = "#ad6ee5";
                    }
                    else if (dr["PromotionType"].ToString() == "Genius")
                    {
                        PageObj.Color = "#7cc94a";
                    }
                    PageObj.PromotionType = dr["PromotionType"].ToString();
                    PageObj.PromotionDescription = dr["Description"].ToString();
                    PageObj.PromotionName = dr["Name"].ToString();
                    PageObj.PromotionSort = Convert.ToInt32(dr["Sort"]);
                    if (dr["StartDate"].ToString() != "")
                    {
                        PageObj.StartDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    }
                    if (dr["EndDate"].ToString() != "")
                    {
                        PageObj.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    }
                    if (dr["TargetStartDate"].ToString() != "")
                    {
                        PageObj.TargetStartDate = Convert.ToDateTime(dr["TargetStartDate"].ToString());
                    }
                    if (dr["TargetEndDate"].ToString() != "")
                    {
                        PageObj.TargetEndDate = Convert.ToDateTime(dr["TargetEndDate"].ToString());
                    }
                    PageObj.RegionID = dr["RegionID"].ToString();
                    PageObj.Count = dr["Count"].ToString();
                    PageObj.DiscountPercentage = dr["DiscountPercentage"].ToString();
                    PageObj.GeneralPromotion = Convert.ToBoolean(dr["GeneralPromotion"].ToString());

                    list.Add(PageObj);
                }
            }
            return list;
        }

        public PromotionViewModel LoadPromotionView(int hotelID,string culture)
        {
            DropdownService DrpRep = new DropdownService();
           
            PromotionViewModel promtion = new PromotionViewModel();
            promtion.PricePolicies = DrpRep.GetPricePlicy(culture);
            promtion.HotelRooms = GetHotelRooms(hotelID, culture);
            promtion.DaysDetails = GetDay(culture);
            int MinimumPromotionDiscountPercentage = GetParameterValue("MinimumPromotionDiscountPercentage");
            int MaximumPromotionDiscountPercentage = GetParameterValue("MaximumPromotionDiscountPercentage");
            promtion.MinimumPromotionDiscountPercentage = MinimumPromotionDiscountPercentage;
            promtion.MaximumPromotionDiscountPercentage = MaximumPromotionDiscountPercentage;
            promtion.DefaultPromotionDiscountPercentage = GetParameterValue("DefaultPromotionDiscountPercentage");
            promtion.MaximumDayCountForMinimumStayPromotion = GetParameterValue("MaximumDayCountForMinimumStayPromotion");
            promtion.MaximumHourCountForMinimumStayPromotion = GetParameterValue("MaximumHourCountForMinimumStayPromotion");
            return promtion;
        }
        public List<NewPromotion> GetDay(string culture)
        {
            List<NewPromotion> list = new List<NewPromotion>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[B_Ex_GetDay_TB_TypeDays_SP]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", culture);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            _sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    NewPromotion drpObj = new NewPromotion();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public string HotelPromotion(int HotelID, string AccommodationStartDate, string AccommodationEndDate, string PromotionRoomType, string culture)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (AccommodationStartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(AccommodationStartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(AccommodationStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (AccommodationEndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(AccommodationEndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(AccommodationEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelPromotions]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelIDs", HotelID);
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@Active", true);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            string Status = "NoConflict";
            DateTime AccommodationStartDateFromUser = dtStart;// Convert.ToDateTime(AccommodationStartDate);
            DateTime AccommodationEndDateFromUser = dtEnd;// Convert.ToDateTime(AccommodationEndDate);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    
                    DateTime AccommodationStartDateFromDB = DateTime.Parse(dr["AccommodationStartDate"].ToString(),CultureInfo.InvariantCulture);
                    DateTime AccommodationEndDateFromDB = DateTime.Parse(dr["AccommodationEndDate"].ToString(),CultureInfo.InvariantCulture);

                    if (dr["PromotionType"].ToString() == PromotionRoomType.ToString() && !(AccommodationStartDateFromUser < AccommodationStartDateFromDB || AccommodationEndDateFromUser > AccommodationEndDateFromDB))
                    {
                        Status = "Conflict";
                        return Status;
                    }

                }
            }
            return Status;
        }

        public int GetParameterValue(string Parameter)
        {
            int Status = 0;
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetParameter_BizTbl_Parameter_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Code", Parameter);
            Status = Convert.ToInt32(cmd.ExecuteScalar());
            _sqlConnection.Close();
            return Status;
        }
        public List<PropertyPhotos> GetHotelRooms(int HotelID, string culture)
        {
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRooms]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", "Sort,RoomTypeName");
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            List<PropertyPhotos> ListOfModel = new List<PropertyPhotos>();
            string Roomname = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    PropertyPhotos HotelObj = new PropertyPhotos();
                    HotelObj.RoomID = Convert.ToInt32(dr["ID"]);
                    HotelObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelObj.RoomTypeName = dr["RoomTypeName"].ToString();
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }

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
        public string PromotionInsert(PromotionInsert promotion, string WeekDay, string PricePolicy, string RoomCount, string RoomType, int HotelID, long userID)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (promotion.AccommodationStartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(promotion.AccommodationStartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(promotion.AccommodationStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (promotion.AccommodationEndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(promotion.AccommodationEndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(promotion.AccommodationEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            string status = "Success";
            Entities insertentity = new Entities();
            GBSExtranet.Api.Models.TB_HotelPromotion PageObj = new GBSExtranet.Api.Models.TB_HotelPromotion();
            //  PageObj.ID = model.ID;
            PageObj.HotelID = HotelID;
            PageObj.DiscountPercentage = promotion.DiscountPercentage;
            PageObj.PromotionID = promotion.PromotionID;
            PageObj.StartDate = DateTime.Now; ;
            PageObj.EndDate = DateTime.ParseExact("31.12.2200", "dd.MM.yyyy", CultureInfo.InvariantCulture); //Convert.ToDateTime("31.12.2100");
            PageObj.HasDiscount = promotion.HasDiscount;
            PageObj.AccommodationStartDate = dtStart;
            PageObj.AccommodationEndDate = dtEnd;
            PageObj.DayID = ReplaceComma(WeekDay);
            PageObj.DayCount = promotion.DayCount;           
            PageObj.EarlyBookerMargin = promotion.EarlyBookerMargin;
            PageObj.LastMinuteMargin = promotion.LastMinuteMargin;

            DateTime dtb = DateTime.Now;

            if (promotion.BookingDate != string.Empty)
            {
                if (promotion.BookingDate.Contains('.'))
                {
                    dtb = DateTime.ParseExact(promotion.BookingDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    dtb = DateTime.ParseExact(promotion.BookingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                PageObj.BookingDate = dtb;
            }
                 
          
            PageObj.PricePolicyID = ReplaceComma(PricePolicy);
            //PageObj.Region = model.Region;
            PageObj.ValidForAllRoomTypes = promotion.ValidForAllRoomTypes;
            PageObj.Active = true;
            PageObj.SecretDeal = promotion.SecretDeal;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = userID;
            PageObj.CreateDateTime = DateTime.Now;
            PageObj.CreateUserID = userID;
            insertentity.TB_HotelPromotion.Add(PageObj);
            insertentity.SaveChanges();
            int id = PageObj.ID;
            // DBEntities insertRoom = new DBEntities();
            GBSExtranet.Api.Models.TB_HotelPromotionRoom RoomObj = new GBSExtranet.Api.Models.TB_HotelPromotionRoom();
            // PageObj.ID = model.ID;
            foreach (string RoomID in RoomCount.Split(','))
            {
                if (RoomID != string.Empty)
                {
                    RoomObj.HotelPromotionID = id;
                    RoomObj.HotelRoomID = Convert.ToInt32(RoomID);
                    RoomObj.CreateDateTime = DateTime.Now;
                    RoomObj.CreateUserID = userID;
                    RoomObj.OpDateTime = DateTime.Now;
                    RoomObj.OpUserID = userID;
                    RoomObj.Active = true;
                    insertentity.TB_HotelPromotionRoom.Add(RoomObj);
                    insertentity.SaveChanges();
                }
            }


            return status;
        }

        public List<Promotion> ReadAll(int HotelID, string culture)
        {
            List<Promotion> list = new List<Promotion>();

            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelPromotions]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelIDs", HotelID);
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@Active", true);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
           
          //  string Alltypes = Resources.Resources.AllRoomTypes;
            string RoomNames = GetHotelRoomNames(HotelID,culture);
           // string DiscountText = Resources.Resources.Discount;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Promotion PageObj = new Promotion();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                    PageObj.PromotionID = Convert.ToInt32(dr["PromotionID"]);
                    PageObj.PromotionType = dr["PromotionType"].ToString();
                    PageObj.PromotionDescription = dr["PromotionDescription"].ToString();
                    PageObj.PromotionName = dr["PromotionName"].ToString();
                    PageObj.PromotionSort = Convert.ToInt32(dr["PromotionSort"]);
                  //  PageObj.Alltypes = Alltypes;
                    if (Convert.ToBoolean(dr["ValidForAllRoomTypes"].ToString()) == true)
                    {
                        PageObj.RoomNames = "All Rooms";
                    }
                    else
                    {
                        PageObj.RoomNames = GetHotelRoomNames(Convert.ToInt32(dr["ID"]),culture);
                    }
                   // PageObj.DiscountText = DiscountText;
                    PageObj.StartDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    PageObj.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    PageObj.AccommodationStartDate = Convert.ToDateTime(dr["AccommodationStartDate"].ToString());
                    PageObj.AccommodationEndDate = Convert.ToDateTime(dr["AccommodationEndDate"].ToString());
                    PageObj.HasDiscount = Convert.ToBoolean(dr["HasDiscount"].ToString());
                    PageObj.DiscountPercentage = dr["DiscountPercentage"].ToString() + "% ";
                    //PageObj.DayID = Convert.ToInt32(dr["DayID"].ToString());
                    PageObj.DayName = dr["DayName"].ToString();
                   
                    if (dr["DayCount"] != DBNull.Value) 
                        PageObj.DayCount =  Convert.ToInt32(dr["DayCount"]);
                    // PageObj.EarlyBookerMargin = Convert.ToInt32(dr["EarlyBookerMargin"].ToString());
                    //PageObj.LastMinuteMargin = Convert.ToInt32(dr["LastMinuteMargin"].ToString());
                    // PageObj.BookingDate = Convert.ToDateTime(dr["BookingDate"].ToString());
                    //  PageObj.PricePolicyID = Convert.ToInt32(dr["PricePolicyID"].ToString());
                    PageObj.PricePolicyName = dr["PricePolicyName"].ToString();
                    PageObj.SecretDeal = Convert.ToBoolean(dr["SecretDeal"].ToString());
                    // PageObj.Region = dr["Region"].ToString();
                    PageObj.ValidForAllRoomTypes = Convert.ToBoolean(dr["ValidForAllRoomTypes"].ToString());
                    PageObj.Active = Convert.ToBoolean(dr["Active"].ToString());
                    PageObj.CreateDate = Convert.ToDateTime(dr["CreateDateTime"].ToString());

                    list.Add(PageObj);
                }
               // list = list.Where(f => f.StartDate >= DateTime.Now).ToList();
            }
            return list;
        }
        public bool Delete(int id)
        {
            bool status = true;

            var obj = _db.TB_HotelPromotion.Where(x => x.ID == id).FirstOrDefault();
            _db.TB_HotelPromotion.Remove(obj);
            _db.SaveChanges();

            return status;
        }
        public string GetHotelRoomNames(int PromotionID, string culture)
        {
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelPromotionRooms", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", "RoomTypeName");
            cmd.Parameters.AddWithValue("@HotelPromotionID", PromotionID);
            //cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            string Roomname = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (Roomname == "")
                    {
                        Roomname = dr["RoomTypeName"].ToString();
                    }
                    else
                    {
                        Roomname = Roomname +  dr["RoomTypeName"].ToString();
                    }
                    //PropertyPhotosExt HotelObj = new PropertyPhotosExt();
                    //HotelObj.RoomID = Convert.ToInt32(dr["ID"]);
                    //HotelObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    //HotelObj.RoomTypeName = dr["RoomTypeName"].ToString();
                    //ListOfModel.Add(HotelObj);
                }
            }
            return Roomname;
        }
    }
}