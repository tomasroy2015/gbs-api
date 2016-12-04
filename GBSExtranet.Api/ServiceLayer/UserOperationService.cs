using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using System.ServiceModel;
using GBSExtranet.Api.ViewModel;
using Business;
using System.Data;
using GBSExtranet.Api.Models;
using GBSExtranet.Repository;
using System.Globalization;

namespace GBSExtranet.Api.ServiceLayer
{
    public class UserOperationService : BaseService
    {
        public ResponseObject ReadAll(RequestObject filter)
        {
            List<UserOperation> list = new List<UserOperation>();
            ResponseObject data = new ResponseObject();

            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetUserOperationDetails_BizTbl_UserOperation_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;           
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    UserOperation userObj = new UserOperation();
                    userObj.ID = Convert.ToInt32(dr["ID"]);
                    userObj.User = dr["FK_UserID_ID"].ToString();
                    userObj.Part = dr["FK_PartID_ID"].ToString();
                    userObj.OperationType = dr["FK_OperationTypeID_ID"].ToString();
                    userObj.Date = Convert.ToDateTime(dr["Date"]).ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    userObj.RecordID = dr["RecordID"].ToString();
                    userObj.UserSessionID = dr["FK_UserSessionID_ID"].ToString();
                    userObj.IPAddress = dr["IPAddress"].ToString();
                    userObj.UserID = dr["UserID"].ToString();
                    list.Add(userObj);
                }
            }

            if (filter.Filters != null)
                list = new Tools().FilterData<UserOperation>(list, filter.Filters.ToList());

            data.totalRows = list.Count;
            // list = list.OrderByDescending(i => i.ID).Skip(filter.Offset).Take(filter.Length).ToList();

            if (filter.Order == "DESC")
                list = new Tools().Sort_List<UserOperation>("DESC", filter.OrderBy, list);
            else if (filter.Order == "ASC")
                list = new Tools().Sort_List<UserOperation>("ASC", filter.OrderBy, list);
            else
                list = list.OrderBy(o => o.ID).Skip(filter.Offset).Take(filter.Length).ToList();

            list = list.Skip(filter.Offset).Take(filter.Length).ToList();
            //data.rows = list.Cast<object>().ToList();
            //data.totalRows = dt.Rows.Count;
            data.rows = list.Cast<object>().ToList();


            return data;

        }
    }
}