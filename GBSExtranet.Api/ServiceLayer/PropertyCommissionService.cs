using GBSExtranet.Api.Models;
using GBSExtranet.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GBSExtranet.Api.ServiceLayer
{
    public class PropertyCommissionService : BaseService
    {
        public string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public int GetParameterValue(string Parameter)
        {
            int Status = 0;
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetParameter_BizTbl_Parameter_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Code", Parameter);
            Status = Convert.ToInt32(cmd.ExecuteScalar());
            _sqlConnection.Close();
            return Status;
        }
        public double Startdatecomp(string StartDate)
        {
            string nw = StartDate.ToString();
            DateTime dt = DateTime.Now;
            double DateDiff1;
           
                if (StartDate.Contains('.'))
                {
                    dt = DateTime.ParseExact(nw, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    dt = DateTime.ParseExact(nw, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }


                DateTime TodayDate = DateTime.Now;
                DateDiff1 = (TodayDate - dt).TotalDays;

            return DateDiff1;
        }
        public double Enddatecomp(string StartDate, string Enddate)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now;
            double DateDiff1;
           
                if (StartDate.Contains('.'))
                {
                    dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                if (Enddate.Contains('.'))
                {
                    dtEnd = DateTime.ParseExact(Enddate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    dtEnd = DateTime.ParseExact(Enddate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                //  DateTime TodayDate = DateTime.Now;
                DateDiff1 = (dtStart - dtEnd).TotalDays;


                return DateDiff1;
        }

        public List<PropertyCommissionModel> GetComission(int HotelMinumumComissionRate)
        {
            List<PropertyCommissionModel> list = new List<PropertyCommissionModel>();

            for (int i = HotelMinumumComissionRate; i <= 100; i++)
            {
                PropertyCommissionModel drpObj = new PropertyCommissionModel();
                drpObj.ComissionID = i;
                drpObj.Comission = i;
                list.Add(drpObj);
            }

            return list;
        }
        public DataTable GetComissionTable(int HotelID)
        {
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelComissions", _sqlConnection);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "StartDate");
            cmd.Parameters.AddWithValue("@Date", null);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            List<PropertyCommissionModel> commission = new List<PropertyCommissionModel>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyCommissionModel obj = new PropertyCommissionModel();
                    obj.commissionid = Convert.ToInt32(dr["ID"].ToString());
                    obj.commissionStartDate = dr["StartDate"].ToString();
                    obj.commissionEndDate = dr["EndDate"].ToString();
                    obj.commissionComission = dr["Comission"].ToString();
                    commission.Add(obj);
                    // ds.Tables.Add(dt.t);
                }
            }
            return dt;
        }

        public ResponseObject GetComissionTabledisplay(int HotelID, string culture, int offset)
        {
            DataTable dt = new DataTable();
            ResponseObject data = new ResponseObject();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelComissions", _sqlConnection);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "OpDateTime desc");
            cmd.Parameters.AddWithValue("@Date", null);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();
            List<PropertyCommissionModel> commission = new List<PropertyCommissionModel>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyCommissionModel obj = new PropertyCommissionModel();
                    obj.commissionid = Convert.ToInt32(dr["ID"].ToString());
                    obj.commissionStartDate = dr["StartDate"].ToString();
                    obj.commissionEndDate = dr["EndDate"].ToString();
                    obj.commissionComission = dr["Comission"].ToString();
                    commission.Add(obj);
                    // ds.Tables.Add(dt.t);
                }
                data.totalRows = commission.Count;
                commission = commission.Skip(offset).Take(5).ToList();
                data.rows = commission.Cast<object>().ToList();

            }
            return data;
        }
        public int UpdateComission(int ComissionID, string Comission, string HotelID)
        {

            int status = 2;
            var ComissionObj = _db.TB_HotelComission.Where(x => x.ID == ComissionID).FirstOrDefault();
            ComissionObj.Comission = Convert.ToInt16(Comission);
            ComissionObj.OpDateTime = DateTime.Now;
            ComissionObj.OpUserID = Convert.ToInt64(HotelID);

           // _db.TB_HotelComission.Attach(ComissionObj);
           _db.SaveChanges();
            return status;
        }
        public int SaveComission(int HotelID, DateTime StartDate, DateTime EndDate, string Comission)
        {

            int status = 1;
            TB_HotelComission ComissionObj = new TB_HotelComission();
            ComissionObj.HotelID = HotelID;
            ComissionObj.Comission = Convert.ToInt16(Comission);
            ComissionObj.StartDate = StartDate;
            ComissionObj.EndDate = EndDate;
            ComissionObj.OpDateTime = DateTime.Now;
            ComissionObj.OpUserID = Convert.ToInt64(HotelID);
            _db.TB_HotelComission.Add(ComissionObj);
            _db.SaveChanges();
            int id = ComissionObj.ID;
            return status;
        }
        public int DeleteComission(string IdtoDelete)
        {
            int status = 1;

            foreach (var ids in IdtoDelete.Split(','))
            {
                if (ids != "")
                {
                    try
                    {

                        int ComissionId = Convert.ToInt32(ids);
                        var MessageTable = _db.TB_HotelComission.Where(x => x.ID == ComissionId).FirstOrDefault();
                        _db.TB_HotelComission.Remove(MessageTable);
                        _db.SaveChanges();
                    }
                    catch
                    {

                    }
                }
            }
            return status;
        }
    }
}