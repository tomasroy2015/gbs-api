using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GBSExtranet.Api.Models;
using GBSExtranet.Api.ServiceLayer;
using GBSExtranet.Api.ViewModel;
using System.Web;
using System.ServiceModel.Channels;
using Business;
using System.Collections;
using System.Data;
using System.Net.Http.Headers;
using System.IO;
using System.Text;

namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class RoomRateAvailabilityController : ApiController
    {
        [Route("roomRateAvailability/getDays")]
        [HttpGet]
        public HttpResponseMessage GetRoomRateAvailability(string culture) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new DropdownService().GetDays(culture);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("roomRateAvailability/getRoomAvailabilityAndRate")]
        [HttpGet]
        public HttpResponseMessage GetRoomAvailabilityAndRate(int hotelID,string culture, string StartDate, string Enddate, string RoomType, string PricePolicy, string AccommodationType, string WeekDay)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    int RoomID = 0;
                    try
                    {
                        RoomID = Convert.ToInt32(RoomType);
                    }
                    catch
                    {
                        RoomID = 0;
                    }

                    RoomRateAvailabilityService objRep = new RoomRateAvailabilityService();
                    var HotelRooms = objRep.GetHotelRooms(hotelID, culture).FirstOrDefault(f => f.RoomID == RoomID);
                    int MaximamPeopleCount = HotelRooms.MaxPeopleCount;
                    objRep.CreateHotelRoomAvailability(RoomType, StartDate, Enddate, HotelRooms);
                    objRep.CreateHotelRoomRate(RoomType, StartDate, Enddate, AccommodationType, PricePolicy);
                    DataTable AvailabilityTable = objRep.GetHotelAvailability(hotelID, StartDate, Enddate);
                    DataTable RateTable = objRep.GetHotelRate(hotelID, StartDate, Enddate, AccommodationType, PricePolicy);

                    var response = objRep.Getdates(culture, StartDate, Enddate, WeekDay, AvailabilityTable, RateTable, Convert.ToInt32(RoomType), MaximamPeopleCount);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("roomRateAvailability/saveRoomAvailabilityAndRate")]
        [HttpPost]
        public HttpResponseMessage SaveRoomAvailabilityRate(string startDate, string endDate,string culture,int hotelID, int accommodationType, int pricePolicy, int roomID, long sessionID, List<RoomRateAvailability> rates)
        {
            try
            {
                if (this.ModelState.IsValid)
                {

                    var response = new RoomRateAvailabilityService().SaveRoomAvailabilityAndRate(startDate, endDate, culture, hotelID, accommodationType, pricePolicy, roomID, sessionID, rates);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("roomRateAvailability/exportData")]
        [HttpPost]
        public HttpResponseMessage DownloadData(List<RoomRateAvailability> rates, string fileType)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    //string Path = HttpContext.Current.Server.MapPath("~/TempReport/RoomData_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + ".xls");
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response = new RoomRateAvailabilityService().ExportRoomData(rates, fileType);

                    return Request.CreateResponse(HttpStatusCode.OK, response);


                    //var response = new HttpResponseMessage();
                    ////Create the file in Web App Physical Folder
                    //string fileName = "RoomData.xls";
                    //string filePath = HttpContext.Current.Server.MapPath(String.Format("~/TempReport/{0}", fileName));

                    //StringBuilder fileContent = new StringBuilder();
                    ////Get Data here
                    //DataTable dt = UtilityPlus.ToDataTable<RoomRateAvailability>(rates);
                    //if (dt != null)
                    //{
                    //    string str = string.Empty;
                    //    foreach (DataColumn dtcol in dt.Columns)
                    //    {
                    //        fileContent.Append(str + dtcol.ColumnName);
                    //        str = "\t";
                    //    }
                    //    fileContent.Append("\n");
                    //    foreach (DataRow dr in dt.Rows)
                    //    {
                    //        str = "";
                    //        for (int j = 0; j < dt.Columns.Count; j++)
                    //        {
                    //            fileContent.Append(str + Convert.ToString(dr[j]));
                    //            str = "\t";
                    //        }
                    //        fileContent.Append("\n");
                    //    }
                    //}
                    //// write the data into Excel file
                    //using (StreamWriter sw = new StreamWriter(filePath.ToString(), false))
                    //{
                    //    sw.Write(fileContent.ToString());
                    //}
                    ////IFileProvider FileProvider = new FileProvider();
                    ////Get the File Stream
                    //FileStream fileStream = File.Open(filePath, FileMode.Open);
                    ////Set response
                    //byte[] array = Encoding.ASCII.GetBytes(fileContent.ToString());
                    //MemoryStream mem = new MemoryStream(array);
                    //response.Content = new StreamContent(mem);
                    //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    //response.Content.Headers.ContentDisposition.FileName = fileName;
                    //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xls");
                    //response.Content.Headers.ContentLength = fileStream.Length;
                    ////Delete the file


                    ////if(File.Exists(filePath))
                    ////{
                    ////    File.Delete(filePath);
                    ////}
                    //return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
