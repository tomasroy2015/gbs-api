using Business;
using GBSExtranet.Api.Models;
using GBSExtranet.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ServiceLayer
{
    public class DropdownlistServices:BaseService
    {
        User _user = null;
        BizContext BizContext = new BizContext();
        UserContext userContext = new UserContext();
        public string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
         public List<DropDownListsExt> GetRoomType()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            GBSHotelsEntities entity = new GBSHotelsEntities();
            var Culture = new SqlParameter("@CultureCode", CultureValue);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetRoomType_Result>("B_Ex_GetRoomType_TB_TypeRoom_SP @CultureCode", Culture).ToList();
            foreach (GetRoomType_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
    }
}