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
using GBSExtranet.Api.Models;
using GBSExtranet.Repository;
using System.Globalization;

namespace GBSExtranet.Api.ServiceLayer
{
    public class PropertyOperationService : BaseService
    {
        public Hotel GetHotels(string culture,Int64? hotelID )
        {
           
            DataTable dt = new DataTable();
             _sqlConnection.Open();
             SqlCommand cmd = new SqlCommand("B_Ex_GetHotels_TB_Hotel_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
           
            if(hotelID!=null)
                cmd.Parameters.AddWithValue("@HotelID", hotelID);
            
            cmd.Parameters.AddWithValue("@OrderBy", "Name ASC,ID ASC");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            List<Hotel> ListOfModel = new List<Hotel>();
            Hotel HotelObj = null;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HotelObj = new Hotel();
                    Encryption64 objEncryptreservation = new Encryption64();
                    HotelObj.EncryptHotelID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(dr["ID"].ToString(), "58421043")));

                    HotelObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelObj.Name = dr["Name"].ToString();
                    HotelObj.FirmName = dr["FirmName"].ToString();
                    HotelObj.HotelTypeName = dr["HotelTypeName"].ToString();
                    HotelObj.HotelClassName = dr["HotelClassName"].ToString();
                    HotelObj.HotelAccommodationTypeName = dr["HotelAccommodationTypeName"].ToString();
                    HotelObj.HotelChainName = dr["HotelChainName"].ToString();
                    HotelObj.CountryName = dr["CountryName"].ToString();
                    HotelObj.CityName = dr["CityName"].ToString();
                    HotelObj.RegionName = dr["RegionName"].ToString();
                    HotelObj.MainRegionName = dr["MainRegionName"].ToString();
                    HotelObj.MainRegionID = dr["MainRegionID"].ToString();
                    HotelObj.Address = dr["Address"].ToString();
                    HotelObj.PostCode = dr["PostCode"].ToString();
                    HotelObj.Phone = dr["Phone"].ToString();
                    HotelObj.Fax = dr["Fax"].ToString();
                    HotelObj.Email = dr["Email"].ToString();
                    HotelObj.WebAddress = dr["WebAddress"].ToString();
                    HotelObj.StatusName = dr["StatusName"].ToString();
                    int active = Convert.ToInt32(dr["Active"]);
                    HotelObj.CultureID = Convert.ToInt32(dr["CultureID"]);
                    HotelObj.Active = Convert.ToBoolean(active);
                    HotelObj.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    HotelObj.OpDateTime = Convert.ToDateTime(dr["OpDateTime"]);
                    HotelObj.FirmID = Convert.ToInt32(dr["FirmID"]);
                    HotelObj.CountryID = dr["CountryID"].ToString();
                    HotelObj.RegionID = dr["RegionID"].ToString();
                    HotelObj.CityID = dr["CityID"].ToString();
                    HotelObj.CurrencyID = dr["CurrencyID"].ToString();
                    HotelObj.RoutingName = dr["RoutingName"].ToString();
                    HotelObj.HotelAccommodationTypeID =Convert.ToInt32(dr["HotelAccommodationTypeID"]);
                    //string test = dr["AvailabilityRateUpdate"].ToString();
                    if (dr["AvailabilityRateUpdate"].ToString() != "")
                    {
                        HotelObj.AvailabilityRateUpdate = Convert.ToBoolean(Convert.ToInt32(dr["AvailabilityRateUpdate"]));
                    }
                    else
                    {
                        HotelObj.AvailabilityRateUpdate = false;
                    }
                    HotelObj.Latitude = dr["Latitude"].ToString();
                    HotelObj.Longitude = dr["Longitude"].ToString();
                    HotelObj.ParentRegionID = dr["ParentRegionID"].ToString();
                    HotelObj.HotelTypeID = dr["HotelTypeID"].ToString();
                    HotelObj.HotelClassID = dr["HotelClassID"].ToString();
                    HotelObj.CityTaxApplied = dr["CityTaxApplied"].ToString();
                    HotelObj.ClosestAirportID = dr["ClosestAirportID"].ToString();
                    HotelObj.CountryCode = dr["CountryCode"].ToString();
                    HotelObj.VAT = dr["VAT"].ToString();
                    HotelObj.ClosestAirportName = dr["ClosestAirportName"].ToString();
                    HotelObj.ClosestAirportDistance = dr["ClosestAirportDistance"].ToString().Replace(',', '.');
                    HotelObj.Description = dr["Description"].ToString();
                    HotelObj.RoomCount = dr["RoomCount"].ToString();
                    HotelObj.CheckinStart = dr["CheckinStart"].ToString();
                    HotelObj.CheckinEnd = dr["CheckinEnd"].ToString();
                    HotelObj.RenovationYear = dr["RenovationYear"].ToString();
                    HotelObj.CheckoutStart = dr["CheckoutStart"].ToString();
                    HotelObj.CheckoutEnd = dr["CheckoutEnd"].ToString();
                    HotelObj.FloorCount = dr["FloorCount"].ToString();
                    HotelObj.BuiltYear = dr["BuiltYear"].ToString();
                    HotelObj.HitCount = dr["HitCount"].ToString();
                    HotelObj.Sort = dr["Sort"].ToString();
                    HotelObj.MapZoomIndex = dr["MapZoomIndex"].ToString();
                    HotelObj.StatusID = dr["StatusID"].ToString();
                    if (dr["IsPreferred"].ToString() != "")
                    {
                        HotelObj.IsPreferred = Convert.ToBoolean(Convert.ToInt32(dr["IsPreferred"]));
                    }
                    else
                    {
                        HotelObj.IsPreferred = false;
                    }
                    if (dr["IsSecret"].ToString() != "")
                    {
                        HotelObj.IsSecret = Convert.ToBoolean(Convert.ToInt32(dr["IsSecret"]));
                    }
                    else
                    {
                        HotelObj.IsSecret = false;
                    }
                    // HotelObj.IsPreferred = dr["IsPreferred"].ToString();
                    HotelObj.ShowOffline = dr["ShowOffline"].ToString();
                    HotelObj.ChannelManagerID = dr["ChannelManagerID"].ToString();
                    HotelObj.CreditCardNotRequired = dr["CreditCardNotRequired"].ToString();
                    HotelObj.IPAddress = dr["IPAddress"].ToString();
                    HotelObj.HotelChainID = dr["HotelChainID"].ToString();
                    HotelObj.CurrencyName = dr["CurrencyName"].ToString();
                    HotelObj.HotelCreditCardList = new DropdownService().GetHotelCreditCardList(HotelObj.ID.ToString());
                    HotelObj.AllCreditCardList = new DropdownService().GetCreditCardType();
                    HotelObj.CheckInFromList = new DropdownService().FillTimeList(7, 18);
                    HotelObj.CheckInToList = new DropdownService().FillTimeList(12, 24);
                    HotelObj.CheckOutFromList = new DropdownService().FillTimeList(0, 14);
                    HotelObj.CheckOutToList = new DropdownService().FillTimeList(7, 18); 
                    //ListOfModel.Add(HotelObj);
                }
            }
            return HotelObj;
        }

        public Hotel UpdateProperty(Hotel hotel)
        {
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            try
            {
                IRepository<GBSExtranet.Repository.TB_Hotel> hotelRepository = uow.RepositoryFor<GBSExtranet.Repository.TB_Hotel>();
                var hotelObj = hotelRepository.Find(o => o.ID == hotel.ID).FirstOrDefault();
                if (hotelObj != null)
                {
                    hotelObj.CheckinStart = hotel.CheckinStart;
                    hotelObj.CheckinEnd = hotel.CheckinEnd;
                    hotelObj.CheckoutStart = hotel.CheckoutStart;
                    hotelObj.CheckoutEnd = hotel.CheckoutEnd;
                    hotelObj.OpDateTime = DateTime.Now;
                   // hotelObj.OpUserID = 0;
                    uow.SaveChanges();

                    string SelectedCards = string.Empty;
                    foreach (var card in hotel.HotelCreditCardList)
                    {
                        SelectedCards = card.ID.ToString() + ',' + SelectedCards;
                    }
                    SelectedCards = SelectedCards.Remove(SelectedCards.Length - 1, 1);
                    var HotelIDParameter = new SqlParameter("@HotelID", hotel.ID);
                    var SelectedCardsParameter = new SqlParameter("@SelectedCards", SelectedCards);
                    int i = _db.Database.ExecuteSqlCommand("B_Ex_UpdateHotelCreditCard_TB_HotelCreditCard_SP @HotelID,@SelectedCards", HotelIDParameter, SelectedCardsParameter);
      
                }
            }
            catch (SqlException ex)
            {
                throw new FaultException(ErrorMessage.FAULT_DATABASE_NOT_REACHABLE);
            }
            catch (InvalidOperationException ex)
            {
                throw new FaultException(ErrorMessage.FAULT_DATABASE_OPERATION_FAILD);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
            return hotel;
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
    }
}