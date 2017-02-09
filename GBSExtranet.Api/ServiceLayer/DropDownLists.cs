using GBSExtranet.Api.Models;
using GBSExtranet.Api.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GBSExtranet.Api.ServiceLayer
{
    
    public class DropDownLists : BaseService
    {
       // public GBSHotelsEntities db;
        public string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
       
        //List<DropDownListsExt> AttributesListOfModel = new List<DropDownListsExt>();
        public static SelectList GetRoomType()
        {
            DropdownlistServices modelRepo = new DropdownlistServices();
            var RoomType = modelRepo.GetRoomType();

            List<SelectListItem> _ListRoomType = new List<SelectListItem>();

            foreach (var item in RoomType)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _ListRoomType.Add(itr);
            }

            return new SelectList(_ListRoomType, "Value", "Text");
        }
        public List<DropDownListsExt> Getsmoking()
        {
            List<DropDownListsExt> ListOfModel = new List<DropDownListsExt>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetTypeSmoking_TB_TypeSmoking_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureValue);         
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt HotelObj = new DropDownListsExt();
                    HotelObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelObj.Name = dr["Name"].ToString();
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }
        public List<DropDownListsExt> GetRoomView()
        {
            List<DropDownListsExt> ListOfModel = new List<DropDownListsExt>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetTypeView_TB_TypeView_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureValue);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt HotelObj = new DropDownListsExt();
                    HotelObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelObj.Name = dr["Name"].ToString();
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }
        public List<DropDownListsExt> GetLanguage(string CultureCode)
        {
            List<DropDownListsExt> ListOfModel = new List<DropDownListsExt>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_Getculture_BizTbl_Culture", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
           // cmd.Parameters.AddWithValue("@CultureCode", CultureValue);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt HotelObj = new DropDownListsExt();
                    HotelObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelObj.Name = dr["Description"].ToString();
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }
        public List<DropDownListsExt> GetAttributeHeaders()
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetAttributeHeaders", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PartID", 1);
            cmd.Parameters.AddWithValue("@AttributeTypeID", 1);
            cmd.Parameters.AddWithValue("@Active", 1);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "Sort");

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            _sqlConnection.Close();

            List<DropDownListsExt> ListOfModel = new List<DropDownListsExt>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt obj = new DropDownListsExt();

                    obj.AttributeHeaderId = dr["ID"].ToString();
                    obj.AttributeName = dr["Name"].ToString();
                    //  PropertyConditions += value;

                    //  obj.PropertyConditions = value;

                    ListOfModel.Add(obj);
                }
            }

            return ListOfModel;
        }
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        List<DropDownListsExt> Header1 = new List<DropDownListsExt>();
        public DataTable GetAttributes(string AttributeHeaderId)
        {
            // string PropertyConditions = "";
           
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetAttributes", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PartID", 1);
            cmd.Parameters.AddWithValue("@AttributeTypeID", 1);
            cmd.Parameters.AddWithValue("@AttributeHeaderID", AttributeHeaderId);
            cmd.Parameters.AddWithValue("@Active", 1);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "Name");

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);           
            _sqlConnection.Close();
            List<string> stringList = new List<string>();
            List<DropDownListsExt> AttributesListOfModel = new List<DropDownListsExt>();
           
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt obj = new DropDownListsExt();
                    obj.AttributeId = Convert.ToInt32(dr["ID"].ToString());
                    obj.AttributeName = dr["Name"].ToString();
                    obj.AttributeHeaderId = dr["AttributeHeaderID"].ToString();
                    AttributesListOfModel.Add(obj);                  
                    Header1.Add(obj);
                   // ds.Tables.Add(dt.t);
                }
            }
           // stringList.Add(obj.ToString());
            return dt;
        }

        public List<DropDownListsExt> GetBedTypes()
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_GetBedType_TB_TypeBed_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            _sqlConnection.Close();
            List<DropDownListsExt> ListOfBedTypes = new List<DropDownListsExt>();
            ArrayList OptionNo = new ArrayList();
            for (int i = 1; i <= 3; i++)
            {
                OptionNo.Add(i);
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DropDownListsExt obj = new DropDownListsExt();

                        string OptionNoValue = OptionNo[i].ToString();
                        obj.OptionNo = OptionNoValue;
                        obj.BedId = dr["ID"].ToString();
                        obj.BedName = dr["Name"].ToString();

                        ListOfBedTypes.Add(obj);
                    }
                }

            }

            
            return ListOfBedTypes;
        }
        public int InsertRoomDetails(int HotelID, string HotelRoomID, string RoomType, string RoomCount, string RoomSize, string RoomMaxPeopleCount, string RoomMaxChildrenCount, string BabyCotCount, string ExtraBedCount, string SmokingStatus, string ViewType, string Culture, string Description)
        {
            //_sqlConnection.Open();
            //SqlCommand cmd = new SqlCommand("B_InsertRoomDetails_TB_HotelRoom_SP", _sqlConnection);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@HotelId", HotelID);
            //cmd.Parameters.AddWithValue("@RoomType", RoomType);
            //cmd.Parameters.AddWithValue("@RoomCount", RoomCount);
            //cmd.Parameters.AddWithValue("@RoomSpace", RoomSize);
            //cmd.Parameters.AddWithValue("@RoomMaxPerson", RoomMaxPeopleCount);
            //cmd.Parameters.AddWithValue("@RoomMaxChildren", RoomMaxChildrenCount);
            //cmd.Parameters.AddWithValue("@RoomBabyCots", BabyCotCount);
            //cmd.Parameters.AddWithValue("@RoomExBabyCots", ExtraBedCount);
            //cmd.Parameters.AddWithValue("@RoomSmoking", SmokingStatus);
            //cmd.Parameters.AddWithValue("@RoomView", ViewType);
            //cmd.Parameters.AddWithValue("@Language", Culture);
            //cmd.Parameters.AddWithValue("@RoomDescription", Description);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //_sqlConnection.Close();
            //return dt;
            //string HotelRoomID = "";
            GBSHotelsEntities insertentity = new GBSHotelsEntities();
            TB_HotelRoom hotelRoom = new TB_HotelRoom();
            string culturecode = "";
            if (HotelRoomID != null)
            {
                int HotelRoomIdValue = Convert.ToInt32(HotelRoomID);
                hotelRoom = _db.TB_HotelRoom.Where(x => x.ID == HotelRoomIdValue).FirstOrDefault();
                //  hotelRoom = db.TB_HotelRoom.Where(x => x.ID == Convert.ToInt32(HotelRoomID)).FirstOrDefault();
                // hotelRoom = (from room in DataContext.TB_HotelRoomswhere room.ID == HotelRoomID).Single;
            }
            hotelRoom.HotelID = Convert.ToInt32(HotelID);
            hotelRoom.RoomTypeID = Convert.ToInt32(RoomType);
            hotelRoom.RoomCount = Convert.ToInt32(RoomCount);
            hotelRoom.RoomSize = Convert.ToInt32(RoomSize);
            hotelRoom.MaxPeopleCount = Convert.ToInt16(RoomMaxPeopleCount);
            hotelRoom.MaxChildrenCount = Convert.ToInt16(RoomMaxChildrenCount);
            hotelRoom.BabyCotCount = Convert.ToInt16(CheckEmptyStringDBParameter(BabyCotCount, true));
            hotelRoom.ExtraBedCount = Convert.ToInt16(CheckEmptyStringDBParameter(ExtraBedCount, true));
            hotelRoom.ViewTypeID = (Nullable<int>)CheckEmptyStringDBParameter(ViewType, true);
            hotelRoom.SmokingTypeID = (Nullable<int>)CheckEmptyStringDBParameter(SmokingStatus, true);

            if (Description != string.Empty)
            {

                if (Culture == "1")
                {
                    culturecode = "en";
                }
                else if (Culture == "3")
                {
                    culturecode = "de";
                }
                else if (Culture == "4")
                {
                    culturecode = "fr";

                }
                else if (Culture == "10")
                {
                    culturecode = "ar";
                }
                else if (Culture == "6")
                {
                    culturecode = "ru";

                }
                else if (Culture == "8")
                {
                    culturecode = "tr";
                }
                SetColumn(hotelRoom, "Description_" + culturecode, Description);
            }
            hotelRoom.Active = true;
            hotelRoom.OpDateTime = DateTime.Now;
           // hotelRoom.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            if (HotelRoomID == null)
            {
                hotelRoom.CreateDateTime = DateTime.Now;
               // hotelRoom.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                _db.TB_HotelRoom.Add(hotelRoom);
                // db.SaveChanges();
                // DataContext.TB_HotelRooms.InsertOnSubmit(hotelRoom);
            }
            _db.SaveChanges();
            // DataContext.SubmitChanges();

            return hotelRoom.ID;
        }

        public bool SaveHotelRoomAttributes(string HotelRoomID, int Charged, int AttributeID, string UnitValue, string Charge, string CurrencyID)
        {
            bool status = false;
            if (HotelRoomID != string.Empty)
            {
                TB_HotelRoomAttribute hotelRoomAttribute = new TB_HotelRoomAttribute();
                hotelRoomAttribute.HotelRoomID = Convert.ToInt32(HotelRoomID);
                hotelRoomAttribute.AttributeID = AttributeID;
                hotelRoomAttribute.Charged = Convert.ToBoolean(Charged);
                hotelRoomAttribute.UnitValue = (string)CheckEmptyStringDBParameter(UnitValue);
                hotelRoomAttribute.Charge = (Nullable<decimal>)CheckEmptyStringDBParameter(Charge, false, false, false, true);
                hotelRoomAttribute.CurrencyID = (Nullable<int>)CheckEmptyStringDBParameter(CurrencyID, true);
                hotelRoomAttribute.OpDateTime = DateTime.Now.Date;
                //hotelRoomAttribute.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                _db.TB_HotelRoomAttribute.Add(hotelRoomAttribute);
                _db.SaveChanges();
                // DataContext.TB_HotelRoomAttributes.InsertOnSubmit(hotelRoomAttribute);
                status = true;
            }
            return status;
        }

        public int SaveHotelRoomBed(string HotelRoomBedID, string OptionNo, string HotelRoomID, string BedTypeID, string BedCount)
        {
            TB_HotelRoomBed hotelRoomBed = new TB_HotelRoomBed();
            HotelRoomBedID = "";
            if (HotelRoomBedID != string.Empty)
            {
                int HotelRoomBedIDValue = Convert.ToInt32(HotelRoomBedID);
                hotelRoomBed = _db.TB_HotelRoomBed.Where(x => x.ID == HotelRoomBedIDValue).FirstOrDefault();
                //  hotelRoomBed = (from roomBed in DataContext.TB_HotelRoomBedswhere roomBed.ID == HotelRoomBedID).Single;
            }
            hotelRoomBed.OptionNo = Convert.ToInt32(OptionNo);
            hotelRoomBed.HotelRoomID = Convert.ToInt32(HotelRoomID);
            hotelRoomBed.BedTypeID = Convert.ToInt32(BedTypeID);
            hotelRoomBed.Count = Convert.ToInt32(BedCount);
            hotelRoomBed.OpDateTime = DateTime.Now.Date;
            //hotelRoomBed.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            if (HotelRoomBedID == string.Empty)
            {

                _db.TB_HotelRoomBed.Add(hotelRoomBed);
                // db.SaveChanges();
                // DataContext.TB_HotelRooms.InsertOnSubmit(hotelRoom);
            }
            _db.SaveChanges();
            return hotelRoomBed.ID;
        }
        public static object CheckEmptyStringDBParameter(object Value, bool ReturnInteger = false, bool ReturnDate = false, bool ReturnDouble = false, bool ReturnDecimal = false, bool ReturnBoolean = false, bool ReturnLong = false)
        {
           
            if (Value == string.Empty || Value == null)
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
        public void SetColumn(object DataObj, string ColumnName, object Value)
        {
            System.Reflection.PropertyInfo pi = DataObj.GetType().GetProperty(ColumnName);

            if ((pi != null))
            {
                pi.SetValue(DataObj, Value, null);
            }

        }


        public List<DropDownListsExt> GetEditDetails(string hotelroomid)
        {
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_GetHotelRoom_TB_HotelRoom_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@RoomID", hotelroomid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            List<DropDownListsExt> ListOfModel = new List<DropDownListsExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //if (!ArrayID.Contains(Convert.ToInt32(dr["ID"])))
                    //{
                    DropDownListsExt HotelRoom = new DropDownListsExt();

                    HotelRoom.ID = Convert.ToInt32(dr["ID"]);
                    HotelRoom.HotelID = Convert.ToInt64(dr["HotelID"]);
                    HotelRoom.Description = dr["Description"].ToString();
                    HotelRoom.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelRoom.RoomCount = dr["RoomCount"].ToString();


                    HotelRoom.RoomSize = dr["RoomSize"].ToString();
                    HotelRoom.MaxPeopleCount = dr["MaxPeopleCount"].ToString();


                    HotelRoom.MaxChildrenCount = dr["MaxChildrenCount"].ToString();
                    HotelRoom.BabyCotCount = dr["BabyCotCount"].ToString();

                    HotelRoom.ExtraBedCount = dr["ExtraBedCount"].ToString();
                    HotelRoom.SmokingTypeID = dr["SmokingTypeID"].ToString();


                    HotelRoom.ViewTypeID = dr["ViewTypeID"].ToString();
                    HotelRoom.Promotion = dr["Promotion"].ToString();

                    HotelRoom.RelatedHotelRoomID = dr["RelatedHotelRoomID"].ToString();
                    HotelRoom.CreateDateTime = dr["CreateDateTime"].ToString();
                    HotelRoom.CreateUserID = dr["CreateUserID"].ToString();

                    HotelRoom.OpDateTime = dr["OpDateTime"].ToString();
                    HotelRoom.OpUserID = dr["OpUserID"].ToString();

                    HotelRoom.Language = dr["Language"].ToString();
                    string Language = dr["Language"].ToString();

                    if (Language == "English")
                    {
                        HotelRoom.LanguageID = 1;
                    }
                    else if (Language == "Deutsch")
                    {
                        HotelRoom.LanguageID = 3;
                    }
                    else if (Language == "Français")
                    {
                        HotelRoom.LanguageID = 4;
                    }
                    else if (Language == "العربية")
                    {
                        HotelRoom.LanguageID = 10;
                    }
                    else if (Language == "Русский")
                    {
                        HotelRoom.LanguageID = 6;
                    }
                    else if (Language == "Türkçe")
                    {
                        HotelRoom.LanguageID = 8;
                    }

                    ListOfModel.Add(HotelRoom);
                    //}

                }
            }
            return ListOfModel;
        }

        public List<DropDownListsExt> EditAttributeHeaders(Int64 HotelRoomID, int AttributeTypeID, string OrderBy)
        {

            List<DropDownListsExt> Attributesheader = new List<DropDownListsExt>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelRoomAttributes", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            cmd.Parameters.AddWithValue("@AttributeTypeID", AttributeTypeID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt obj = new DropDownListsExt();
                    obj.AttributeId = Convert.ToInt32(dr["AttributeId"]);
                    obj.AttributeName = dr["AttributeName"].ToString();

                    obj.AttributeHeaderIds = Convert.ToInt32(dr["AttributeHeaderId"]);
                    Attributesheader.Add(obj);
                }
            }
            return Attributesheader;
        }      
        public List<DropDownListsExt> GetEditHotelRoomBeds(Int64 hotelroomid)
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelRoomBeds", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@HotelRoomID", hotelroomid);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            _sqlConnection.Close();
            List<DropDownListsExt> ListOfBedTypes = new List<DropDownListsExt>();
            List<DropDownListsExt> RoomBedInfo = new List<DropDownListsExt>();
            ArrayList OptionNo = new ArrayList();
           
            for (int i = 1; i <= 3; i++)
            {
                OptionNo.Add(i);
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DropDownListsExt obj = new DropDownListsExt();

                        string OptionNoValue = OptionNo[i].ToString();
                        obj.OptionNo = OptionNoValue;
                        obj.BedId = dr["ID"].ToString();
                        obj.OptionNo = dr["OptionNo"].ToString();
                        obj.HotelRoomID = dr["HotelRoomID"].ToString();
                        obj.BedTypeID = dr["BedTypeID"].ToString();
                        obj.BedTypeName = dr["BedTypeName"].ToString();
                        obj.Count = dr["Count"].ToString();
                        obj.BedTypeNameWithCount = dr["BedTypeNameWithCount"].ToString();
                        ListOfBedTypes.Add(obj);
                    }
                }

            }


            return ListOfBedTypes;
        }
        public bool DeleteHotelRoomAttributes(string HotelRoomID, string AttributeTypeID = "")
        {
            bool status = false;
            int HotelRoomValue = Convert.ToInt32(HotelRoomID);
            int AttributeTypeIDValue = Convert.ToInt32(AttributeTypeID);
            try
            {
                var hotelRoomAttributes = from hotelRoomAttribute in _db.TB_HotelRoomAttribute
                                          join attribute in _db.TB_Attribute
                                          on hotelRoomAttribute.AttributeID equals attribute.ID
                                          where hotelRoomAttribute.HotelRoomID == HotelRoomValue && attribute.AttributeTypeID == AttributeTypeIDValue
                                          select new { hotelRoomAttribute.ID };

                foreach (var items in hotelRoomAttributes)
                {
                    int hotelRoomAttributeIDValue = Convert.ToInt32(items.ID);
                    // var ID = items.ID;

                    var obj = _db.TB_HotelRoomAttribute.Where(x => x.ID == hotelRoomAttributeIDValue).FirstOrDefault();
                    _db.TB_HotelRoomAttribute.Remove(obj);
                    status = true;
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return status;
        }
        public bool DeleteHotelRoomBeds(string HotelRoomID)
        {
            bool status = true;
            int HotelRoomIDValue = Convert.ToInt32(HotelRoomID);
            try
            {
                var obj = _db.TB_HotelRoomBed.Where(x => x.HotelRoomID == HotelRoomIDValue).ToList();
                foreach (var obj1 in obj)
                {
                    _db.TB_HotelRoomBed.Remove(obj1);
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return status;
        }
    }


}