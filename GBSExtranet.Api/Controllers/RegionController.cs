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
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Configuration;

namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class RegionController : ApiController
    {
        [Route("region/getRegions")]
        [HttpPost]
        public HttpResponseMessage GetRegions(RequestObject filter) 
        {
            try
            {
                ApiResponseMessage message = new ApiResponseMessage();
                if (this.ModelState.IsValid)
                {
                    message.data = new RegionService().ReadAll(filter);
                    return Request.CreateResponse(HttpStatusCode.OK, message);
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
        [Route("region/addRegion")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddRegion() 
        {
            try
            {
                
                //if (!Request.Content.IsMimeMultipartContent())
                //{
                //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                //}
                string fileName = "";
                var root = HttpContext.Current.Server.MapPath("~/App_Data/Uploadfiles");
                Directory.CreateDirectory(root);
                var provider = new MultipartFormDataStreamProvider(root);
                var result = await Request.Content.ReadAsMultipartAsync(provider);
                var region = result.FormData["model"];
                if (region == null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }  
                if (this.ModelState.IsValid)
                {
                   //
                    var data = JsonConvert.DeserializeObject<Region>(region);
                                       
                    var resultData = new RegionService().Create(data);
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
                        string f = Path.GetFileNameWithoutExtension(fileName);
                        string e = Path.GetExtension(fileName);
                        string n = f.Replace(f, resultData.ID.ToString());
                        string file =  n + e;
                        string sPath =  HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["RegionImageURL"].ToString());
                        File.Copy(fileData.LocalFileName, Path.Combine(sPath, file)); 
                        resultData.Image = file;
                        Tools.ClearFolder(root);
                        new RegionService().Edit(resultData);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, resultData);
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

        [Route("region/editRegion")]
        [HttpPost]
        public async Task<HttpResponseMessage> EditRegion()
        {
            try
            {
                string fileName = "";
                var root = HttpContext.Current.Server.MapPath("~/App_Data/Uploadfiles");
                Directory.CreateDirectory(root);
                var provider = new MultipartFormDataStreamProvider(root);
                var result = await Request.Content.ReadAsMultipartAsync(provider);
                var region = result.FormData["model"];
                if (region == null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                if (this.ModelState.IsValid)
                {
                    var data = JsonConvert.DeserializeObject<Region>(region);
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
                        string f = Path.GetFileNameWithoutExtension(fileName);
                        string e = Path.GetExtension(fileName);
                        string n = f.Replace(f, data.ID.ToString());
                        string file = n + e;
                        string sPath = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["RegionImageURL"].ToString());
                        if (File.Exists(sPath + file))
                        {
                            File.Delete(sPath + file);
                            File.Copy(fileData.LocalFileName, Path.Combine(sPath, file));
                        }
                        else
                        {
                            File.Copy(fileData.LocalFileName, Path.Combine(sPath, file));
                        }
                        data.Image = file;                      
                    }
                    Tools.ClearFolder(root);
                    var resultData = new RegionService().Edit(data);
                    return Request.CreateResponse(HttpStatusCode.OK, resultData);
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

        [Route("region/deleteRegion")]
        [HttpPost]
        public HttpResponseMessage DeleteRegion(Region model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new RegionService().Delete(model);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
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
        [Route("region/deleteRegions")]
        [HttpPost]
        public HttpResponseMessage DeleteRegion(string[] ids)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new RegionService().DeleteRegions(ids);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
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
