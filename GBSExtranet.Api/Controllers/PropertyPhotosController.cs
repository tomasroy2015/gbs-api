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
    public class PropertyPhotosController : ApiController
    {
        [Route("propertyPhotos/propertyPhotosProperties")]
        [HttpGet]
        public HttpResponseMessage PropertyPhotos(int hotelID,string culture) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new PhotoViewModel();
                    var modelRepo = new PropertyPhotoService();
                    var HotelRooms = modelRepo.GetHotelRooms(hotelID,culture); 
                    data.HotelRooms = HotelRooms;
                    data.Path = modelRepo.GetParameterValue("HotelPhotoPath");
                    data.MaxPhotoSize = modelRepo.GetParameterValue("MaxPhotoSize");
                    data.AllowedPhotoFileExtensions = modelRepo.GetParameterValue("AllowedPhotoFileExtensions");
                    data.MaxHotelPhotoCount = modelRepo.GetParameterValue("MaxHotelPhotoCount");
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

        [Route("propertyPhotos/getListPhotos")]
        [HttpGet]
        public HttpResponseMessage GetListPhotos(int partID, int hotelID)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new PropertyPhotoService().LoadPhoto(partID, hotelID);
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

        [Route("propertyPhotos/delete")]
        [HttpPost]
        public HttpResponseMessage DeletePhoto(string photoID, long userID)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new PropertyPhotoService().DeletePhotos(photoID, userID);
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
        [Route("propertyPhotos/setMainPhoto")]
        [HttpPost]
        public HttpResponseMessage SetMainPhoto(string photoID, string recordID, string partID,long userID)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new PropertyPhotoService().MainPhoto(photoID, recordID, photoID, userID);
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
