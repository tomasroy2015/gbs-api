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
using System.Data;
using System.Globalization;

namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ReservationController : ApiController
    {
        [Route("reservation/getReservationByProperty")]
        [HttpGet]
        public HttpResponseMessage GetReservationByProperty(int hotelID, string cultureCode, int offset)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new PropertyReservationService().GetReservationByProperty(hotelID, cultureCode,offset);
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

        [Route("reservation/getReservationDetails")]
        [HttpGet]
        public HttpResponseMessage GetReservationByDetails(long id, string culture) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new PropertyReservationService().GetReservationsForView(id, culture);
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

        [Route("reservation/getReservationPromotions")]
        [HttpGet]
        public HttpResponseMessage GetReservationPromotions(int reservationID,string culture) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new PropertyReservationService().ReservationPromotions(reservationID, culture);
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

        [Route("reservation/getReservationOperation")]
        [HttpGet]
        public HttpResponseMessage GetReservationOperation(Int64 reservationID, bool systemAdmin, long userID, string culture) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {                  
                    var data = new ReservationService().GetReservations(reservationID, userID, systemAdmin, culture).FirstOrDefault(f=>f.ReservationID == reservationID);                  
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

        [Route("reservation/getReservationHistory")]
        [HttpGet]
        public HttpResponseMessage GetReservationHistory(long reservationID, string culture)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    DataTable ReservationHistory = new DataTable();
                    BaseService svc = new BaseService();
                    ReservationHistory = BizApplication.GetUserOperations(svc._context, "Date DESC, ID DESC", culture, null, Convert.ToString(reservationID));

                    List<Reservation> ListOfModel = new List<Reservation>();

                    if (ReservationHistory.Rows.Count > 0)
                    {
                        foreach (DataRow dr in ReservationHistory.Rows)
                        {
                            Reservation Obj = new Reservation();

                            // Obj.ReservationID = Convert.ToInt32(dr["RecordID"]);
                            Obj.ReservationOwner = dr["UserFullName"].ToString();

                            DateTime dt1 = Convert.ToDateTime(dr["Date"]);

                            Obj.ReservationDate = (dt1.ToString("d"));
                            Obj.ReservationOperation = dr["OperationTypeName"].ToString();

                            ListOfModel.Add(Obj);

                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, ListOfModel);
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

        [Route("reservation/getReservationStatement")]
        [HttpGet]
        public HttpResponseMessage GetReservationStatement(string hotelID, string culture, int offset)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new ReservationService().GetReservationStatement(hotelID,culture, offset);
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
        [Route("reservation/getReservationStatementByDate")]
        [HttpGet]
        public HttpResponseMessage GetReservationStatementByDate(string hotelID, string StartDate, string Enddate, string culture, int offset)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    //DateTime StartDate1 = Convert.ToDateTime(StartDate);
                    //DateTime Enddate1 = Convert.ToDateTime(Enddate);
                    DateTime StartDate1 = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime Enddate1 = DateTime.ParseExact(Enddate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var data = new ReservationService().GetReservationStatementByDate(hotelID, StartDate1, Enddate1, culture, offset);
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
