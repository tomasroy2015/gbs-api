using GBSExtranet.Api.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GBSExtranet.Api.Controllers
{
      [EnableCors("*", "*", "*")]
    public class PropertyCommissionController : ApiController
    {
        [Route("propertycommission/CommissiondrpDisplay")]
        [HttpGet]
          public HttpResponseMessage CommissiondrpDisplay()
        {
            PropertyCommissionService obj = new PropertyCommissionService();
            int HotelMinumumComissionRate = obj.GetParameterValue("HotelMinumumComissionRate");
            var drpcommissiondisplay = obj.GetComission(HotelMinumumComissionRate);    
            return Request.CreateResponse(HttpStatusCode.OK, drpcommissiondisplay);
        }
        [Route("propertycommission/Startdatecomp")]
        [HttpGet]
        public HttpResponseMessage Startdatecomp(string StartDate)
        {
            PropertyCommissionService obj = new PropertyCommissionService();
            var Startdatecheck = obj.Startdatecomp(StartDate);
            return Request.CreateResponse(HttpStatusCode.OK, Startdatecheck);
        }
        [Route("propertycommission/Enddatecomp")]
        [HttpGet]
        public HttpResponseMessage Enddatecomp(string StartDate, string Enddate)
        {
            PropertyCommissionService obj = new PropertyCommissionService();
            var Startdatecheck = obj.Enddatecomp(StartDate, Enddate);
            return Request.CreateResponse(HttpStatusCode.OK, Startdatecheck);
        }
        [Route("propertycommission/DisplayComission")]
        [HttpGet]
        public HttpResponseMessage DisplayComission(int HotelID, string culture, int offset)
        {
            DataTable dt = new DataTable();
            PropertyCommissionService obj = new PropertyCommissionService();
            var display = obj.GetComissionTabledisplay(HotelID, culture, offset);

           return Request.CreateResponse(HttpStatusCode.OK, display);
        }
        [Route("propertycommission/SaveComission")]
        [HttpGet]
        public HttpResponseMessage SaveComission(string StartDate, string Enddate, string Comission, string HotelID)
        {
            PropertyCommissionService objupdate = new PropertyCommissionService();
            int i = 0;

            int id = Convert.ToInt32(HotelID);
                DateTime StartDat = DateTime.ParseExact(StartDate, @"d/M/yyyy", null);
                DateTime EndDat = DateTime.ParseExact(Enddate, @"d/M/yyyy", null);
                DataTable ComissionTable = objupdate.GetComissionTable(id);
                DataRow[] sameIntervalComission = ComissionTable.Select(string.Format("StartDate ='{0}' and EndDate='{1}'", StartDat, EndDat));

                if (sameIntervalComission.Length > 0)
                {
                    i = objupdate.UpdateComission(Convert.ToInt16(sameIntervalComission[0]["ID"]), Comission, HotelID);
                }
                else
                {
                     i = objupdate.SaveComission(id, StartDat, EndDat, Comission);
                }
                //else
                //{
                //    string comissionIDsToBeDeleted = "";
                //    DataRow[] previousComission = ComissionTable.Select(string.Format("StartDate <'{0}' and EndDate>='{1}'", StartDat, EndDat));
                //    if (previousComission.Length > 0)
                //    {
                //        i = objupdate.SaveComission(id, Convert.ToDateTime((previousComission[0]["StartDate"])), StartDat.AddDays(-1), Convert.ToString(previousComission[0]["Comission"]));
                //        if (comissionIDsToBeDeleted == "")
                //        {
                //            comissionIDsToBeDeleted = Convert.ToString(previousComission[0]["ID"]);
                //        }
                //        else
                //        {
                //            comissionIDsToBeDeleted = comissionIDsToBeDeleted + "," + Convert.ToString(previousComission[0]["ID"]);
                //        }

                //    }

                //    DataRow[] nextComission = ComissionTable.Select(string.Format("StartDate <='{0}' and EndDate>'{1}'", StartDat, EndDat));
                //    if (nextComission.Length > 0)
                //    {
                //        i = objupdate.SaveComission(id, StartDat.AddDays(1), Convert.ToDateTime(nextComission[0]["EndDate"]), Convert.ToString(nextComission[0]["Comission"]));
                //        if (comissionIDsToBeDeleted == "")
                //        {
                //            comissionIDsToBeDeleted = Convert.ToString(nextComission[0]["ID"]);
                //        }
                //        else
                //        {
                //            comissionIDsToBeDeleted = comissionIDsToBeDeleted + "," + Convert.ToString(nextComission[0]["ID"]);
                //        }
                //    }

                //    DataRow[] intervalComissions = ComissionTable.Select(string.Format("StartDate >='{0}' and EndDate<='{1}'", StartDat, EndDat));
                //    if (intervalComissions.Length > 0)
                //    {
                //        foreach (DataRow DRIC in intervalComissions)
                //        {
                //            if (comissionIDsToBeDeleted == "")
                //            {
                //                comissionIDsToBeDeleted = Convert.ToString(DRIC["ID"]);
                //            }
                //            else
                //            {
                //                comissionIDsToBeDeleted = comissionIDsToBeDeleted + "," + Convert.ToString(DRIC["ID"]);
                //            }
                //        }
                //    }
                //    i = objupdate.SaveComission(id, StartDat, EndDat, Comission);
                //    if (comissionIDsToBeDeleted != "")
                //    {
                //        objupdate.DeleteComission(comissionIDsToBeDeleted);
                //    }
            return Request.CreateResponse(HttpStatusCode.OK, i);
                
        }
                
        }
    }
