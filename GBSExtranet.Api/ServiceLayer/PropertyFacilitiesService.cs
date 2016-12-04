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
using System.Globalization;
using GBSExtranet.Repository;
namespace GBSExtranet.Api.ServiceLayer
{
    public class PropertyFacilitiesService : BaseService
    {
        public List<PropertyFacilities> GetHotelAttributes(int AttributeHeaderID, int HotelID, string CultureValue)
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetAttributes", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Active", 1);
            cmd.Parameters.AddWithValue("@PartID", 1);
            cmd.Parameters.AddWithValue("@AttributeHeaderID", AttributeHeaderID);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "Name Asc");
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            _sqlConnection.Close();

            List<PropertyFacilities> attributeList = new List<PropertyFacilities>();
            List<PropertyFacilities> propertyFacilities = new List<PropertyFacilities>();
            bool charged1 = false;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyFacilities obj = new PropertyFacilities();
                    obj.AttributeID = Convert.ToInt32(dr["ID"].ToString());
                    obj.AttributeName = dr["Name"].ToString();
                    obj.Chargable = Convert.ToBoolean(dr["Chargeable"]);
                    attributeList.Add(obj);
                }
            }

            DataTable SelectedRoomAttribute = HotelRoomAttributes(AttributeHeaderID, HotelID,CultureValue);
            foreach (var items in attributeList)
            {
                PropertyFacilities HotelRoom = new PropertyFacilities();
                if (SelectedRoomAttribute.Rows.Count > 0)
                { 
                    DataRow[] hotelRoomAttribute = SelectedRoomAttribute.Select("AttributeID=" + items.AttributeID);
                    HotelRoom.hasAttribute = Convert.ToBoolean(hotelRoomAttribute.Length > 0);

                    HotelRoom.Charged = ((HotelRoom.hasAttribute) && (Convert.ToBoolean(hotelRoomAttribute[0]["charged"])));

                    if ((items.Chargable == true) && (HotelRoom.Charged == true))
                    {
                        HotelRoom.Charged = true;
                    }
                    else if ((items.Chargable == true) && (HotelRoom.Charged == false))
                    {
                        HotelRoom.Charged = false;
                    }
                    else
                    {
                        HotelRoom.Charged = null;
                    }
                    //HotelRoom.UnitID = items.UnitID;
                    HotelRoom.AttributeID = items.AttributeID;
                    HotelRoom.AttributeName = items.AttributeName;

                    if (HotelRoom.Charged == true)
                    {
                        HotelRoom.PaidAmount = string.IsNullOrEmpty(hotelRoomAttribute[0]["Charge"].ToString()) ? 0.00 : Convert.ToDouble(hotelRoomAttribute[0]["Charge"]);
                        HotelRoom.UnitID = string.IsNullOrEmpty(hotelRoomAttribute[0]["HotelUnitID"].ToString()) ? -1 : Convert.ToInt32(hotelRoomAttribute[0]["HotelUnitID"]);
                    }
                   
                    propertyFacilities.Add(HotelRoom);
                }
                else
                {
                    DataRow[] hotelRoomAttribute = SelectedRoomAttribute.Select("AttributeID=" + items.AttributeID);
                    HotelRoom.hasAttribute = Convert.ToBoolean(hotelRoomAttribute.Length > 0);

                    HotelRoom.Charged = ((HotelRoom.hasAttribute) && (Convert.ToBoolean(hotelRoomAttribute[0]["charged"])));

                    if ((items.Chargable == true) && (HotelRoom.Charged == true))
                    {
                        HotelRoom.Charged = true;
                    }
                    else if ((items.Chargable == true) && (HotelRoom.Charged == false))
                    {
                        HotelRoom.Charged = false;
                    }
                    else
                    {
                        HotelRoom.Charged = null;
                    }
                    HotelRoom.AttributeID = items.AttributeID;
                    HotelRoom.AttributeName = items.AttributeName;
                    if (HotelRoom.Charged == true)
                    {
                        HotelRoom.PaidAmount = string.IsNullOrEmpty(hotelRoomAttribute[0]["Charge"].ToString()) ? 0.00 : Convert.ToDouble(hotelRoomAttribute[0]["Charge"]);
                        HotelRoom.UnitID = string.IsNullOrEmpty(hotelRoomAttribute[0]["HotelUnitID"].ToString()) ? -1 : Convert.ToInt32(hotelRoomAttribute[0]["HotelUnitID"]);
                    }
                   
                    propertyFacilities.Add(HotelRoom);
                }
               
            }

            return propertyFacilities;
        }
        public DataTable HotelRoomAttributes(int AttributeHeaderID, int HotelID, string CultureValue)
        {
            PropertyRoom obj = new PropertyRoom();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelAttributes", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@AttributeHeaderID", AttributeHeaderID);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@Active", 1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }

        #region new code
        public List<PropertyFacilitiesHeader> GetPropertyFacilitiesHeader(int hotelID,string CultureValue)
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetAttributeHeaders", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@AttributeTypeID", 1);
            cmd.Parameters.AddWithValue("@Active", 1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            _sqlConnection.Close();

            List<PropertyFacilitiesHeader> propertyFacilitiesHeaders = new List<PropertyFacilitiesHeader>();

            if (dt.Rows.Count > 0)
            {
                // var Count = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyFacilitiesHeader propertyHeader = new PropertyFacilitiesHeader();
                    propertyHeader.ID = Convert.ToInt32(dr["ID"]);
                    propertyHeader.AttributeHeaderName = dr["Name"].ToString();
                    propertyFacilitiesHeaders.Add(propertyHeader);
                }

            }
            return propertyFacilitiesHeaders;
        }
        #endregion
        public bool DeleteHotelAttributes(ref GBSDbContext db,int HotelID)
        {

            DateTime Date1 = DateTime.Now.Date;

            var hotelAttributesAvailability = from hotelAttribute in db.HotelAttributes
                                              join Attributes in db.Attributes
                                              on hotelAttribute.AttributeID equals Attributes.ID
                                              where Attributes.AttributeTypeID == 1 && hotelAttribute.HotelID == HotelID
                                              && Date1 >= hotelAttribute.StartDate && Date1 <= hotelAttribute.EndDate
                                              select hotelAttribute;

            db = new GBSDbContext();
            foreach (var items in hotelAttributesAvailability)
            {

                int hotelAvailabilityIDValue = Convert.ToInt32(items.ID);             
                var obj = db.HotelAttributes.Where(x => x.ID == hotelAvailabilityIDValue).FirstOrDefault();
                obj.EndDate = DateTime.Now.Date;
                obj.Active = false;
                //db.HotelAttributes.Remove(obj);
            }
            db.SaveChanges();
            return true;

        }
        public bool SavePropertyFacility(List<PropertyFacilities> items,int hotelID,string culture)
        {           
            var context = new GBSDbContext();
            var uow = new UnitOfWork(context);
            bool status = true;
            try               
            {
                IRepository<GBSExtranet.Repository.TB_HotelAttribute> attbRepository = uow.RepositoryFor<GBSExtranet.Repository.TB_HotelAttribute>();
                if (items != null)
                {
                    bool deleted = DeleteHotelAttributes(ref context,hotelID);
                    foreach (var facility in items)
                    {
                        var newFacility = new GBSExtranet.Repository.TB_HotelAttribute();
                        newFacility.HotelID = hotelID;
                        newFacility.AttributeID = facility.AttributeID;
                        newFacility.Charged = facility.Charged == null ? false : facility.Charged;

                        if (facility.PaidAmount != null)
                            newFacility.Charge = Convert.ToDecimal(facility.PaidAmount);
                        else
                            newFacility.Charge = null;

                        if (facility.UnitID != null && facility.UnitID > 0)
                            newFacility.UnitID = facility.UnitID;
                        else
                            newFacility.UnitID = null;

                        newFacility.StartDate = DateTime.Now.Date;
                        newFacility.EndDate = Convert.ToDateTime("3000-12-31", new CultureInfo("en-US", false));
                        newFacility.Active = true; 
                        newFacility.OpDateTime = DateTime.Now.Date;
                        attbRepository.Add(newFacility);
                        uow.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.ERROR_IN_DATA_EDITING);
            }
            finally
            {
                uow.Dispose();
            }

            return status;
        }

    }
}