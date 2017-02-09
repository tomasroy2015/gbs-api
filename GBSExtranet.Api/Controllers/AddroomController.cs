using Business;
using GBSExtranet.Api.ServiceLayer;
using GBSExtranet.Api.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GBSExtranet.Api.Controllers
{
     [EnableCors("*", "*", "*")]
    public class AddroomController : ApiController
    {
         [Route("addrooms/addroomsinsert")]
         [HttpGet]
         public HttpResponseMessage AddRooms()       
         {
             var Addrooom = DropDownLists.GetRoomType();
             return Request.CreateResponse(HttpStatusCode.OK, Addrooom);
             //return null;
         }
         [Route("addrooms/smoking")]
         [HttpGet]
         public HttpResponseMessage smoking()
         {
             var Somking = new DropDownLists().Getsmoking();
             return Request.CreateResponse(HttpStatusCode.OK, Somking);
             //return null;
         }
         [Route("addrooms/RoomView")]
         [HttpGet]
         public HttpResponseMessage RoomView()
         {
             var Somking = new DropDownLists().GetRoomView();
             return Request.CreateResponse(HttpStatusCode.OK, Somking);
             //return null;
         }
         [Route("addrooms/Language")]
         [HttpGet]
         public HttpResponseMessage Language()
         {
             var Language = new DropDownLists().GetLanguage("en");
             return Request.CreateResponse(HttpStatusCode.OK, Language);
             //return null;
         }
         [Route("addrooms/Roombed")]
         [HttpGet]
         public HttpResponseMessage Roombed()
         {
             var AttributeHeaders = new DropDownLists().GetBedTypes();
             ArrayList OptionNo = new ArrayList();
             for (int i = 1; i <= 3; i++)
             {
                 OptionNo.Add(i);
             }
             return Request.CreateResponse(HttpStatusCode.OK, AttributeHeaders);
             //return null;
         }

         DataTable dt = new DataTable();
         DataSet ds = new DataSet();
         int count = 0;
         [Route("addrooms/AttributeHeaders")]
         [HttpGet]
         public HttpResponseMessage AttributeHeaders()
         {
             
            
             var AttributeHeaders = new DropDownLists().GetAttributeHeaders();

           List<string> stringList = new List<string>();
             string [] totalhead=new string[200];
             List<DropDownListsExt> Attributes = new List<DropDownListsExt>();
            
             if (AttributeHeaders != null)
             {
                 foreach (var items in AttributeHeaders)
                 {
                    //Attributes = new DropDownLists().GetAttributes(items.AttributeHeaderId);
                    // stringList.Add(Attributes.ToString());
                     dt = new DropDownLists().GetAttributes(items.AttributeHeaderId);                  
                     ds.Tables.Add(dt);
                     count++;
                   
                 }                
             }
             int headerid = 1;
             List<DropDownListsExt> empList = new List<DropDownListsExt>();
             for (int i = 0; i < count; i++)
             {
                 try
                 {
                     foreach (DataRow dr in ds.Tables[i].Rows)
                     {
                         DropDownListsExt obj = new DropDownListsExt();
                         obj.AttributeId = Convert.ToInt32(dr["ID"].ToString());
                         obj.AttributeName = dr["Name"].ToString();
                         obj.AttributeHeaderId = dr["AttributeHeaderID"].ToString();
                         obj.PartID = dr["PartID"].ToString();
                         if (headerid == 1)
                         {
                             obj.AttributeHeaderName = dr["AttributeHeaderName"].ToString();
                         }
                         else
                         {
                             obj.AttributeHeaderName ="";

                         }
                         empList.Add(obj);
                         headerid = headerid + 1;
                     }
                     headerid = 1;
                 }
                 catch(Exception ex)
                 {

                 }
             }
             return Request.CreateResponse(HttpStatusCode.OK, empList);
             //return null;
         }

         [Route("addrooms/InsertRoom")]
         [HttpPost]
         public HttpResponseMessage InsertRoomDetails(int HotelID, string HotelRoomID, string RoomType, string RoomCount, string RoomSize, string RoomMaxPeopleCount, string RoomMaxChildrenCount, string BabyCotCount, string ExtraBedCount, string SmokingStatus, string ViewType, string Culture, string Description, string HotelAttributes, string BedCountText)
         {
             
             //int HotelID = Convert.ToInt32(HotelRoomID);
             
            bool isNewRecord = (HotelRoomID == string.Empty);
            DropDownLists modelRepo = new DropDownLists();
            HotelRoomID = Convert.ToString(modelRepo.InsertRoomDetails(HotelID, HotelRoomID, RoomType, RoomCount, RoomSize, RoomMaxPeopleCount, RoomMaxChildrenCount, BabyCotCount, ExtraBedCount,
                 SmokingStatus, ViewType, Culture, Description));

             string[] AttributeID = HotelAttributes.Split(',');

             bool DeleteHotelAttribute = modelRepo.DeleteHotelRoomAttributes(HotelRoomID, "1");

             for (int i = 0; i < AttributeID.Length; i++)
             {
                 if (AttributeID[i] != "")
                 {
                     int AttributeIDValue = Convert.ToInt32(AttributeID[i]);
                     bool SaveHotelRoomAttributes = modelRepo.SaveHotelRoomAttributes(HotelRoomID, 0, AttributeIDValue, string.Empty, string.Empty, string.Empty);
                 }
             }

             bool BedDeleteStatus = modelRepo.DeleteHotelRoomBeds(HotelRoomID);

             string[] BedTextValues = BedCountText.Split(',');

             for (int k = 0; k < BedTextValues.Length; k++)
             {
                 string[] BedValues = BedTextValues[k].Split(';');
                 string OptionNo = BedValues[0];
                 string txtBedCount = BedValues[1];
                 string BedTypeID = BedValues[2];
                 if (txtBedCount != string.Empty)
                 {
                     modelRepo.SaveHotelRoomBed(string.Empty, OptionNo, HotelRoomID, BedTypeID, txtBedCount);
                 }

             }

             return Request.CreateResponse(HttpStatusCode.OK, HotelRoomID);


                
         }

         [Route("addrooms/Editroomdetails")]
         [HttpGet]
         public HttpResponseMessage Editroomdetails(string hotelroomid)
         {
             var EditDetails = new DropDownLists().GetEditDetails(hotelroomid);
             return Request.CreateResponse(HttpStatusCode.OK, EditDetails);
             //return null;
         }
         [Route("addrooms/EditAttributeHeaders")]
         [HttpGet]
         public HttpResponseMessage Editroomdetails(Int64 hotelroomid)
         {
             var EditDetails = new DropDownLists().EditAttributeHeaders(hotelroomid, 1, "AttributeName");
             return Request.CreateResponse(HttpStatusCode.OK, EditDetails);
             //return null;
         }
         [Route("addrooms/EditHotelRoomBeds")]
         [HttpGet]
         public HttpResponseMessage EditHotelRoomBeds(Int64 hotelroomid)
         {
             var EditDetails = new DropDownLists().GetEditHotelRoomBeds(hotelroomid);
             return Request.CreateResponse(HttpStatusCode.OK, EditDetails);
             //return null;
         }
    }
}