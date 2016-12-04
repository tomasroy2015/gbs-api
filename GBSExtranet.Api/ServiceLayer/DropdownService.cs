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

namespace GBSExtranet.Api.ServiceLayer
{
    public class DropdownService : BaseService
    {
        public ResponseObject ReadCurrencies(string CultureCode)
        {
            List<DropdownList> list = new List<DropdownList>();
            ResponseObject data = new ResponseObject();
            try
            {
                DataTable dt = new DataTable();
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("B_Ex_GetCurrencyDropdown_TB_Currency_SP", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                _sqlConnection.Close();

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        DropdownList drpObj = new DropdownList();
                        drpObj.ID = Convert.ToInt32(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
                data.rows = list.Cast<object>().ToList();
                data.totalRows = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return data;
        }

        public ResponseObject ReadCountries(string CultureCode)
        {
            List<DropdownList> list = new List<DropdownList>();
            ResponseObject data = new ResponseObject();
            try
            {
                DataTable dt = new DataTable();
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("B_GetCountrytble_TB_Country_SP", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                _sqlConnection.Close();

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        DropdownList drpObj = new DropdownList();
                        drpObj.ID = Convert.ToInt32(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
                data.rows = list.Cast<object>().ToList();
                data.totalRows = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return data;
        }

        public ResponseObject ReadRegions(string Culture)
        {
            List<DropdownList> list = new List<DropdownList>();
            ResponseObject data = new ResponseObject();
            try
            {
                DataTable dt = new DataTable();
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("B_Ex_GetRegionsDropdown_TB_Region_SP", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CultureCode", Culture);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                _sqlConnection.Close();

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        DropdownList drpObj = new DropdownList();
                        drpObj.ID = Convert.ToInt32(dr["ID"]);
                        drpObj.Name = dr["Region"].ToString();
                        list.Add(drpObj);
                    }
                }
                data.rows = list.Cast<object>().ToList();
                data.totalRows = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return data;
        }
        
        public List<DropdownList> GetHotelCreditCardList(string HotelID)
        {
            List<DropdownList> list = new List<DropdownList>();

            try
            {
                DataTable dt = new DataTable();
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("B_Ex_GetCreditCard_TB_TypeCreditCard_SP", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HotelID", HotelID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                _sqlConnection.Close();

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        DropdownList drpObj = new DropdownList();
                        drpObj.ID = Convert.ToInt32(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
             catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public List<DropdownList> ReadChildrenPolicyUnits(string culture)
        {
            var context = new GBSDbContext();
            List<DropdownList> list = new List<DropdownList>();
            var policies = context.ChildrenPolicyUnits.ToList();
            var dropObj = new DropdownList();
            policies.ForEach(f =>
            {
                dropObj = new DropdownList();
                dropObj.ID = f.ID;
                dropObj.Name = f.Name;
                list.Add(dropObj);
            });

            return list;
        }
        public List<DropdownList> GetCurrencies(string culture)
        {
            List<DropdownList> list = new List<DropdownList>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetCurrencyDropdown_TB_Currency_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", culture);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropdownList drpObj = new DropdownList();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Code = dr["Code"].ToString();
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropdownList> GetCreditCardType()
        {
            List<DropdownList> list = new List<DropdownList>();
           
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetDropdownCreditCardType_TB_TypeCreditCard_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;          
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropdownList drpObj = new DropdownList();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropdownList> FillTimeList(int starttime, int Endtime)
        {
            List<DropdownList> list = new List<DropdownList>();

            int startIndex = starttime;
            int endIndex = Endtime;
            for (int i = startIndex; i <= endIndex; i++)
            {
                string time = (i == 24 ? "00:00" : string.Format("{0:00}", i) + ":00");
                string halfTime = (i == 24 ? "00:30" : string.Format("{0:00}", i) + ":30");

                DropdownList drpObj = new DropdownList();
                drpObj.TimeID = time.ToString();
                drpObj.Name = time.ToString();
                list.Add(drpObj);
                if (i != endIndex)
                {
                    DropdownList drpObj1 = new DropdownList();
                    drpObj1.TimeID = halfTime.ToString();
                    drpObj1.Name = halfTime.ToString();
                    list.Add(drpObj1);
                }

            }
            return list;
        }

        public List<DropdownList> ReadUnit(string cultureCode)
        {
            List<DropdownList> list = new List<DropdownList>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetUnit_TB_Unit_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", cultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropdownList drpObj = new DropdownList();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
                //list = list.OrderBy(o => o.Name).Select(c => new { UnitID = c.ID, Unit = c.Name }).OrderBy(o => o.Unit).ToList();
            }
            return list;
        }

        public List<DropdownList> GetPenaltyRate(string culture,int rateTypeID)
        {
            List<DropdownList> list = new List<DropdownList>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetDropdownPenaltyRate_TB_TypePenaltyRate_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", culture);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropdownList drpObj = new DropdownList();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.PartID = Convert.ToInt64(dr["PartID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
                list = list.Where(f => f.PartID == rateTypeID).ToList();
            }
            return list;
        }

        public List<DropdownList> GetPriceUnits(string culture)
        {
            List<DropdownList> list = new List<DropdownList>();
            var _db = new GBSDbContext();
            var units = (from u in _db.PriceUnits
                         where u.Active == true
                         select u).ToList();
            if (units != null)
            {
                units.ForEach(u =>
                {
                    var drpObj = new DropdownList();
                    drpObj.Name = new Tools().GetDynamicSortProperty(u, "Name_" + culture) != null ? 
                                  Convert.ToString(new Tools().GetDynamicSortProperty(u, "Name_" + culture)) : string.Empty;
                    drpObj.ID = u.ID;
                    list.Add(drpObj);
                });
            }
            
            return list;
        }
        public List<DropdownList> GetYears()
        {
            DateTime lastFifth = DateTime.Now.AddYears(-4);
            DropdownList item = new DropdownList();
            List<DropdownList> years = new List<DropdownList>();
            int year = lastFifth.Year;
           
            for (int i = 0; i < 5; i++)
            {
                item = new DropdownList();
                item.ID = i;
                item.Name = year.ToString();
                year++;
                years.Add(item);
            }

            return years;
        }

        public List<DropdownList> GetHotelRooms(int hotelID,string culture)
        {
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRooms]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", culture);
            cmd.Parameters.AddWithValue("@OrderBy", "Sort,RoomTypeName");
            cmd.Parameters.AddWithValue("@HotelID", hotelID);
            cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            List<DropdownList> listOfModel = new List<DropdownList>(); 
            string Roomname = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    DropdownList hotelObj = new DropdownList();
                    hotelObj.ID = Convert.ToInt32(dr["ID"]);
                  //  hotelObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    hotelObj.Name = dr["RoomTypeName"].ToString();
                    listOfModel.Add(hotelObj);
                }
            }
            return listOfModel;
        }

        public List<DropdownList> GetPricePlicy(string culture)
        {
            DataTable dt = new DataTable();
            List<DropdownList> list = new List<DropdownList>();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[B_Ex_GetPricePlicy_TB_TypePricePolicy_SP]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", culture);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropdownList drpObj = new DropdownList();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["PricePolicy"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropdownList> GetTypeHotelAccommodationByID(int Id,string culture)
        {
            List<DropdownList> list = new List<DropdownList>();

            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[B_Ex_GetHotelAccommodation_TB_TypeHotelAccommodation_SP]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Cultureid", culture);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
          
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    {
                        if (Convert.ToInt32(dr["ID"]) == Id)
                        {
                            DropdownList drpObj = new DropdownList();
                            drpObj.ID = Convert.ToInt64(dr["ID"]);
                            drpObj.Name = dr["Name"].ToString();
                            list.Add(drpObj);
                        }
                    }
                }
            }
            return list;
        }

        public List<DropdownList> GetDays(string culture) 
        {
            List<DropdownList> list = new List<DropdownList>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[B_Ex_GetDay_TB_TypeDays_SP]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", culture);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropdownList drpObj = new DropdownList();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropdownList> FillUnitList(int startIndex, int endIndex)
        {
            List<DropdownList> list = new List<DropdownList>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                DropdownList drpObj = new DropdownList();
                drpObj.ID = Convert.ToInt64(i);
                drpObj.Name = i.ToString();
                list.Add(drpObj);
            }
            return list;
        }
    }
}