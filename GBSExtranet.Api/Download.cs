using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Hosting;
using System.Globalization;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;

using Newtonsoft.Json;
using System.Data;
using System.Runtime.Caching;
using GBSExtranet.Api.ServiceLayer;
using GBSExtranet.Api.ViewModel;
using GBSExtranet.Api.Models;
namespace GBSExtranet.Api
{
    public class Download
    {
        string _hotelID=string.Empty;
        string _culture=string.Empty; 
        string _startDate=string.Empty;
        string _endDate = string.Empty;
        string _roomType=string.Empty;
        string _pricePolicy=string.Empty;
        string _accommodationType=string.Empty;
        string _weekDay =string.Empty;
        public Download(string hotelID, string culture, string StartDate, string Enddate, string RoomType, string PricePolicy, string AccommodationType, string WeekDay)
        {
            _hotelID = hotelID;
            _culture =culture;
            _startDate = StartDate;
            _endDate = Enddate;
            _roomType=RoomType;
            _pricePolicy=PricePolicy;
            _accommodationType=AccommodationType;
            _weekDay=WeekDay;
        }

 public HSSFWorkbook ExportTemplate(){
            var workbook = new HSSFWorkbook();          
            var sheet = workbook.CreateSheet("RoomRate");
            int RoomID = 0;
            int hotelID =0;
            try
            {
                RoomID = Convert.ToInt32(_roomType);
                hotelID = Convert.ToInt32(_hotelID);
            }
            catch
            {
                RoomID = 0;
            }
            try
            {
                RoomRateAvailabilityService objRep = new RoomRateAvailabilityService();
                var HotelRooms = objRep.GetHotelRooms(hotelID, _culture).FirstOrDefault(f => f.RoomID == RoomID);
                int MaximamPeopleCount = HotelRooms.MaxPeopleCount;
                objRep.CreateHotelRoomAvailability(_roomType, _startDate, _endDate, HotelRooms);
                objRep.CreateHotelRoomRate(_roomType, _startDate, _endDate, _accommodationType, _pricePolicy);
                DataTable AvailabilityTable = objRep.GetHotelAvailability(hotelID, _startDate, _endDate);
                DataTable RateTable = objRep.GetHotelRate(hotelID, _startDate, _endDate, _accommodationType, _pricePolicy);

                List<RoomRateAvailability> rates = objRep.Getdates(_culture, _startDate, _endDate, _weekDay, AvailabilityTable, RateTable, Convert.ToInt32(_roomType), MaximamPeopleCount);

               
                int width = 0;
                sheet.SetColumnWidth(width++, 20 * 300);
                sheet.SetColumnWidth(width++, 20 * 300);
                sheet.SetColumnWidth(width++, 20 * 300);
                sheet.SetColumnWidth(width++, 20 * 300);
                sheet.SetColumnWidth(width++, 20 * 300);
                sheet.SetColumnWidth(width++, 20 * 300);
                sheet.SetColumnWidth(width++, 20 * 300);
                sheet.SetColumnWidth(width++, 20 * 300);
                sheet.SetColumnWidth(width++, 20 * 256);
                sheet.SetColumnWidth(width++, 20 * 256);
                sheet.SetColumnWidth(width++, 20 * 256);

                int addHeaderCell = 0;
                int  rowNo = 0;
                var headerRow = sheet.CreateRow(rowNo);

                headerRow.CreateCell(addHeaderCell++).SetCellValue("Date");
                headerRow.CreateCell(addHeaderCell++).SetCellValue("Day");
                headerRow.CreateCell(addHeaderCell++).SetCellValue("Single Price");
                headerRow.CreateCell(addHeaderCell++).SetCellValue("Double Price");
                headerRow.CreateCell(addHeaderCell++).SetCellValue("Room Price");
                headerRow.CreateCell(addHeaderCell++).SetCellValue("Availability");
                headerRow.CreateCell(addHeaderCell++).SetCellValue("Minimum Stay");
                headerRow.CreateCell(addHeaderCell++).SetCellValue("CTA");
                headerRow.CreateCell(addHeaderCell++).SetCellValue("CTD");
                headerRow.CreateCell(addHeaderCell++).SetCellValue("Closed");

                if(rates.Count > 0){
                    rowNo = rowNo + 1;
                    int rowNumber = rowNo;
                    foreach(var item in rates){
                        var row = sheet.CreateRow(rowNumber++);
                        //Set the Values for Cells
                        int addDataCell = 0;

                        row.CreateCell(addDataCell++).SetCellValue(item.Day +" "+item.MonthName+" "+item.Year);
                         row.CreateCell(addDataCell++).SetCellValue(item.Date);
                         row.CreateCell(addDataCell++).SetCellValue(item.SinglePrice.ToString());
                         row.CreateCell(addDataCell++).SetCellValue(item.DoublePrice.ToString());
                         row.CreateCell(addDataCell++).SetCellValue(item.RoomPrice.ToString());
                         row.CreateCell(addDataCell++).SetCellValue(item.AvailableRoomCount.ToString());
                         row.CreateCell(addDataCell++).SetCellValue(item.MinimumStay.ToString());
                         row.CreateCell(addDataCell++).SetCellValue(item.CloseToArrival.ToString());
                         row.CreateCell(addDataCell++).SetCellValue(item.CloseToDeparture.ToString());
                         row.CreateCell(addDataCell++).SetCellValue(item.Closed.ToString());
                         
                    }
                }
            }
            catch(Exception ex)
            {

            }
          return workbook;
        }
        
    }
}