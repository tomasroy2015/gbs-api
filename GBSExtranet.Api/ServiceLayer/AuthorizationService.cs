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
    public class AuthorizationService : BaseService
    {
        public  List<Authorization> GetSecurityGroup(string CultureCode)
        {
            List<Authorization> list = new List<Authorization>();
            try
            {
                DataTable dt = new DataTable();
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("B_GetSecurityGroup_BizTbl_SecurityGroup_SP", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                _sqlConnection.Close();

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Authorization obj = new Authorization();
                        obj.ID = Convert.ToInt32(dr["ID"]);
                        obj.Info = dr["Info"].ToString();
                        list.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.FAULT_DATABASE_OPERATION_FAILD);
            }

            return list;
        }

        public ResponseObject GetSecurityGroupRights(string CultureCode, int SecurityGroupID, int offset)
        {
            List<Authorization> list = new List<Authorization>();
            ResponseObject data = new ResponseObject();
            try
            {
                DataTable dt = new DataTable();
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("B_GetSecurities_BizTbl_Security_SP", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
                cmd.Parameters.AddWithValue("@SecurityGroupID", SecurityGroupID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                _sqlConnection.Close();

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Authorization obj = new Authorization();
                        obj.ID = Convert.ToInt32(dr["ID"]);
                        obj.Code = dr["Code"].ToString();
                        obj.Description = dr["Description"].ToString();
                        obj.HasRight = dr["HasRight"].ToString();
                        string HasValue = dr["HasRight"].ToString();
                        if (HasValue == "1")
                        {
                            obj.HasValue = true;
                        }
                        else
                        {
                            obj.HasValue = false;
                        }

                        list.Add(obj);
                    }
                    data.totalRows = list.Count;
                    list = list.Skip(offset).Take(20).ToList();
                    data.rows = list.Cast<object>().ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.FAULT_DATABASE_OPERATION_FAILD);
            }
            return data;
        }



        public bool Delete(int SecurityGroupID)
        {
            bool status = true;
            try
            {
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("B_DeleteGroup_BizTbl_SecurityGroupRight_SP", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SecurityGroupID", SecurityGroupID);

                status = Convert.ToBoolean(cmd.ExecuteNonQuery());

            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.FAULT_DATABASE_OPERATION_FAILD);
            }
            return status;
        }

        public bool Update(int SecurityGroupID, List<GBSExtranet.Api.ViewModel.Authorization> selectedItems)
        {
            bool status = true;
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            try
            {
                IRepository<GBSExtranet.Repository.BizTbl_SecurityGroupRight> itemRepository = uow.RepositoryFor<GBSExtranet.Repository.BizTbl_SecurityGroupRight>();

                foreach (GBSExtranet.Api.ViewModel.Authorization obj in selectedItems)
                {
                    var objGroupItem = itemRepository.Find(x => x.SecurityID == obj.ID && x.SecurityGroupID == SecurityGroupID).SingleOrDefault();
                    if (objGroupItem != null)
                    {
                        objGroupItem.HasRight = obj.HasValue;
                        objGroupItem.OpDateTime = DateTime.Now;
                        objGroupItem.OpUserID = obj.OpUserID;
                        uow.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.FAULT_DATABASE_OPERATION_FAILD);
            }
            // int id = MsgObj.ID;
            return status;
        }
    }
}