using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Cors;
using GBSExtranet.Api.Models;
using GBSExtranet.Api.ServiceLayer;
using GBSExtranet.Api.ViewModel;
using System.ServiceModel.Channels;
using Business;
using System.Data;
using System.Net;


namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class SystemSettingsController: ApiController
    {
        [Route("systemsettings/insertsystemsettings")]
        [HttpGet]
        public HttpResponseMessage Insertsystemsettings(int hotelID, bool SamedayLateBooking, int SamedayLateBookingCount, bool SamedayEarlyBooking, 
            int SamedayEarlyBookingCount, bool NextDayBooking, int NextDayBookingCount, bool PriorityLateCheckOut,
            bool PriorityEarlyCheckIn, bool AirportShuttle, bool WelcomeDrink, bool FreeBikeRental, bool FreeBreakfast, bool FreeParking, 
            bool FreeWiFi, bool RateMissing, bool AddressFromGuests, bool CCRChecking, bool WithoutPhoneNumber, bool GracePeriod,
            int GracePeriodHour, bool Arrivals, bool AuroReplenishment, bool SMS, int SMSHour, bool Gbshotels, bool GuestMessage, string cultureCode)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new SystemSettingService().Insertsystemsettings(hotelID, SamedayLateBooking, SamedayLateBookingCount, 
                        SamedayEarlyBooking, SamedayEarlyBookingCount, NextDayBooking, NextDayBookingCount, PriorityLateCheckOut,
                        PriorityEarlyCheckIn, AirportShuttle, WelcomeDrink, FreeBikeRental, FreeBreakfast, FreeParking, FreeWiFi, 
                        RateMissing, AddressFromGuests, CCRChecking, WithoutPhoneNumber, GracePeriod, GracePeriodHour, Arrivals, 
                        AuroReplenishment, SMS, SMSHour, Gbshotels, GuestMessage);
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("systemsettings/getsystemsettings")]
        [HttpGet]
        public HttpResponseMessage GetSystemSettings(string HotelID, string cultureCode)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new SystemSettingService().GetSystemSettings(HotelID, cultureCode);
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}