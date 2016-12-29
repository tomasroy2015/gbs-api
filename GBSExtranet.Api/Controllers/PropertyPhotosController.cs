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
using System.Threading.Tasks;
using System.IO;

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

        [Route("propertyPhotos/uploadPhotos")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadPhotos(int hotelID,int partID,long userID,int recordID)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    
                    string fileName = "";
                    var root = HttpContext.Current.Server.MapPath("~/App_Data/Uploadfiles");
                    Directory.CreateDirectory(root);
                    var provider = new MultipartFormDataStreamProvider(root);
                    var result = await Request.Content.ReadAsMultipartAsync(provider);
                   
                    if (this.ModelState.IsValid)
                    {
                        string subFolderParentName = "Hotel";
                        string subFolderName = Convert.ToString(hotelID);
                        //var uploadPath = Server.MapPath("~/Upload/");
                        //chunk = chunk ?? 0;
                        var UploadPath = "~/Upload/";
                        if (subFolderParentName != "")
                        {
                            UploadPath = UploadPath + "/" + subFolderParentName + "/";
                        }
                        if (subFolderName != "")
                        {
                            UploadPath = UploadPath + subFolderName + "/";
                        }
                        UploadPath = HttpContext.Current.Server.MapPath(UploadPath);
                        new PropertyPhotoService().CreateDirectory(UploadPath);
                        foreach (var fileData in result.FileData)
                        {
                            //TODO: Do something with uploaded file.  
                            fileName = fileData.Headers.ContentDisposition.FileName;
                            if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                            {
                                fileName = fileName.Trim('"');
                            }
                            if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                            {
                                fileName = Path.GetFileName(fileName);
                            }
                            //string f = Path.GetFileNameWithoutExtension(fileName);
                            //string e = Path.GetExtension(fileName);
                            if (File.Exists(UploadPath + fileName))
                            {
                                File.Delete(UploadPath + fileName);
                                File.Copy(fileData.LocalFileName, Path.Combine(UploadPath, fileName));
                            }
                            else
                            {
                                File.Copy(fileData.LocalFileName, Path.Combine(UploadPath, fileName));
                            }
                            new PropertyPhotoService().UploadOperation("Upload", result.FileData.ToList().Count, fileName, partID, hotelID, recordID,userID);

                        }
                        Tools.ClearFolder(root);
                       
                        return Request.CreateResponse(HttpStatusCode.OK, "");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
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
