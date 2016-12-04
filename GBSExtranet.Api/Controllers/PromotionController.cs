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


namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PromotionController : ApiController
    {
        [Route("promotion/getPromotions")]
        [HttpGet]
        public HttpResponseMessage GetPromotions(string culture)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new PromotionService().ReadAll(culture);
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
        [Route("promotion/getAllPromotions")]
        [HttpGet]
        public HttpResponseMessage GetAllPromotions(int hotelID, string culture)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new PromotionService().ReadAll(hotelID,culture);
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

        [Route("promotion/loadPromotionView")]
        [HttpGet]
        public HttpResponseMessage LoadPromotionView(int hotelID,string culture)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new PromotionService().LoadPromotionView(hotelID,culture);
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

        [Route("promotion/promotionInsert")]
        [HttpPost]
        public HttpResponseMessage PromotionInsert(PromotionInsert promotion, string WeekDay, string PricePolicy, string RoomCount, string RoomType, int HotelID, long userID, string culture)
        {
            try
            {
                string Status = ""; 
                //if (this.ModelState.IsValid)
                //{
                   string CheckPromotion = new PromotionService().HotelPromotion(HotelID, promotion.AccommodationStartDate, promotion.AccommodationEndDate, RoomType,culture);
                   if (CheckPromotion == "NoConflict")
                   {
                       Status = new PromotionService().PromotionInsert(promotion, WeekDay, PricePolicy, RoomCount, RoomType, HotelID, userID);                      
                   }
                   else
                   {
                       Status = "Conflict";
                   }
                   return Request.CreateResponse(HttpStatusCode.OK, Status);
                //}
                //else
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                //}

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [Route("promotion/delete")]
        [HttpPost]
        public HttpResponseMessage Delete(int ID)
        {
            try
            {
               
                if (this.ModelState.IsValid)
                {
                    var response = new PromotionService().Delete(ID);
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
    }
}
