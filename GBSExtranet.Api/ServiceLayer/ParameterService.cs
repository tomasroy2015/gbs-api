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
    public class ParameterService : BaseService
    {
        public ResponseObject ReadAll(RequestObject filter)
        {
            List<Parameter> list = new List<Parameter>();
            ResponseObject data = new ResponseObject();

            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_GetParameters_BizTbl_Parameters_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", filter.Culture);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Parameter paraObj = new Parameter();
                    paraObj.ID = Convert.ToInt32(dr["ID"]);
                    paraObj.Code = dr["Code"].ToString();
                    paraObj.Value = dr["Value"].ToString();
                    paraObj.Description = dr["Description"].ToString();
                    paraObj.IsCommon = Convert.ToBoolean(dr["IsCommon"]);
                    paraObj.Date = Convert.ToDateTime(dr["OpDateTime"]).ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    list.Add(paraObj);
                }
            }

            if (filter.Filters != null)
                list = new Tools().FilterData<Parameter>(list, filter.Filters.ToList());

            data.totalRows = list.Count;
            // list = list.OrderByDescending(i => i.ID).Skip(filter.Offset).Take(filter.Length).ToList();

            if (filter.Order == "DESC")
                list = new Tools().Sort_List<Parameter>("DESC", filter.OrderBy, list);
            else if (filter.Order == "ASC")
                list = new Tools().Sort_List<Parameter>("ASC", filter.OrderBy, list);
            else
                list = list.OrderBy(o => o.ID).Skip(filter.Offset).Take(filter.Length).ToList();

            list = list.Skip(filter.Offset).Take(filter.Length).ToList();
            //data.rows = list.Cast<object>().ToList();
            //data.totalRows = dt.Rows.Count;
            data.rows = list.Cast<object>().ToList();


            return data;

        }
        public bool Create(Parameter model)
        {
            bool status = true;
            var context = new GBSDbContext();
            var uow = new UnitOfWork(context);
            try
            {
                IRepository<GBSExtranet.Repository.BizTbl_Parameter> paramRepository = uow.RepositoryFor<GBSExtranet.Repository.BizTbl_Parameter>();

                GBSExtranet.Repository.BizTbl_Parameter objParam = new GBSExtranet.Repository.BizTbl_Parameter();
                objParam.Code = model.Code;
                objParam.Value = model.Value;
                objParam.Description_en = model.Description;
                objParam.IsCommon = model.IsCommon;
                objParam.OpDateTime = DateTime.Now;
                objParam.OpUserID = model.OpUserID;
                paramRepository.Add(objParam);
                uow.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.ERROR_IN_DATA_ADDING);
            }
            finally
            {
                uow.Dispose();
            }
           
            return status;
        }

        public bool Update(Parameter model)
        {
            var context = new GBSDbContext();
            var uow = new UnitOfWork(context);
            bool status = true;
            try
            {
                IRepository<GBSExtranet.Repository.BizTbl_Parameter> paramRepository = uow.RepositoryFor<GBSExtranet.Repository.BizTbl_Parameter>();

                var objParam = paramRepository.Find(x => x.ID == model.ID).SingleOrDefault();
                if (objParam != null)
                {
                    objParam.ID = model.ID;
                    objParam.Code = model.Code;
                    objParam.Value = model.Value;
                    objParam.Description_en = model.Description;
                    objParam.IsCommon = model.IsCommon;
                    objParam.OpDateTime = DateTime.Now;
                    objParam.OpUserID = model.OpUserID;
                    uow.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.ERROR_IN_DATA_EDITING);
            }
            finally
            {
                uow.Dispose();
            }
            
            return status;
        }



        public bool Delete(Parameter model)
        {
            var context = new GBSDbContext();
            var uow = new UnitOfWork(context);
            try
            {
                IRepository<GBSExtranet.Repository.BizTbl_Parameter> paramRepository = uow.RepositoryFor<GBSExtranet.Repository.BizTbl_Parameter>();

                var user = paramRepository.Find(x => x.ID == model.ID).SingleOrDefault();
                paramRepository.Delete(user);
                uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.ERROR_IN_DATA_DELETING);
            }
            finally
            {
                uow.Dispose();
            }
            return true;
        }


        public bool DeleteParameters(string[] ids)
        {
            var context = new GBSDbContext();
            var uow = new UnitOfWork(context);
            try
            {
                IRepository<GBSExtranet.Repository.BizTbl_Parameter> paramRepository = uow.RepositoryFor<GBSExtranet.Repository.BizTbl_Parameter>();
                foreach (string id in ids)
                {
                    int cID = Convert.ToInt32(id.Trim());
                    var country = paramRepository.Find(x => x.ID == cID).SingleOrDefault();
                    paramRepository.Delete(country);
                }
                uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.ERROR_IN_DATA_DELETING);
            }
            finally
            {
                uow.Dispose();
            }
            return true;
        }
    }
}