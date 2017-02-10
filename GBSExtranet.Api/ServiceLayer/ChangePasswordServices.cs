using GBSExtranet.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace GBSExtranet.Api.ServiceLayer
{
    public class ChangePasswordServices : BaseService
    {
        public int UpdatePassword(string UserID, string CurrentPassword, string NewPassword, string cultureCode)
        {
            int status = 0;
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_ChangePassword_Biz_Tbl_User_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CurrentPassword", CurrentPassword);
            cmd.Parameters.AddWithValue("@NewPassword", NewPassword);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            status = Convert.ToInt32(cmd.ExecuteScalar());
            _sqlConnection.Close();
            return status;
        }
    }
}