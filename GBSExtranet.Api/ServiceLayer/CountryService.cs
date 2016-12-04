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

namespace GBSExtranet.Api.ServiceLayer
{
    public class CountryService : BaseService
    {
        public ResponseObject ReadAll(RequestObject filter)
        {
            List<Country> list = new List<Country>();
            ResponseObject data = new ResponseObject();

            DataTable dt = new DataTable();
             _sqlConnection.Open();
             SqlCommand cmd = new SqlCommand("B_GetCountrytble_TB_Country_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", filter.Culture);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Country CountryObj = new Country();
                    CountryObj.ID = Convert.ToInt32(dr["ID"]);
                    CountryObj.Name = dr["Name"].ToString();
                    CountryObj.Code = dr["Code"].ToString();
                    CountryObj.CultureCode = dr["CultureCode"].ToString();
                    CountryObj.CurrencyID = Convert.ToInt32(dr["CurrencyID"]);
                    CountryObj.Currency = dr["Currency"].ToString();
                    CountryObj.VAT = dr["VAT"].ToString();
                    CountryObj.CityTax = Convert.ToBoolean(dr["HasCityTax"]);
                    CountryObj.HitCount = Convert.ToInt64(dr["HitCount"]);
                    CountryObj.Sort = Convert.ToInt16(dr["Sort"]);
                    CountryObj.Active = Convert.ToBoolean(dr["Active"]);
                    CountryObj.TemporaryCode = dr["TempCode"].ToString();
                    list.Add(CountryObj);
                }
            }

            if(filter.Filters != null)
                list = new Tools().FilterData<Country>(list, filter.Filters.ToList());

            data.totalRows = list.Count;
           // list = list.OrderByDescending(i => i.ID).Skip(filter.Offset).Take(filter.Length).ToList();

            if (filter.Order == "DESC")
                list = new Tools().Sort_List<Country>("DESC", filter.OrderBy, list);
            else if (filter.Order == "ASC")
                list = new Tools().Sort_List<Country>("ASC", filter.OrderBy, list);
            else
                list = list.OrderBy(o => o.ID).Skip(filter.Offset).Take(filter.Length).ToList();

            list = list.Skip(filter.Offset).Take(filter.Length).ToList();
            //data.rows = list.Cast<object>().ToList();
            //data.totalRows = dt.Rows.Count;
            data.rows = list.Cast<object>().ToList();
            

            return data;

        }
        public Country Create(Country model)
        {
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db); 
            try
            {
                IRepository<GBSExtranet.Repository.TB_Country> countryRepository = uow.RepositoryFor<GBSExtranet.Repository.TB_Country>(); 

                GBSExtranet.Repository.TB_Country objCountry = new GBSExtranet.Repository.TB_Country();
                objCountry.CurrencyID = model.CurrencyID;
                objCountry.Name_en = model.Name;
                objCountry.Code = model.Code;
                objCountry.CultureCode = model.CultureCode;

                if (!string.IsNullOrEmpty(model.VAT))
                    objCountry.VAT =  Convert.ToInt32(model.VAT);

                objCountry.HasCityTax = model.CityTax;
                objCountry.HitCount = model.HitCount;
                objCountry.Sort = model.Sort;
                objCountry.Active = model.Active;
               
                if(!string.IsNullOrEmpty(model.TemporaryCode))
                    objCountry.TempCode = Convert.ToInt32(model.TemporaryCode);

                objCountry.OpDateTime = DateTime.Now;
                objCountry.OpUserID = model.OptUserID;
                countryRepository.Add(objCountry);
                uow.SaveChanges();
                model.ID  = objCountry.ID;
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.ERROR_IN_DATA_ADDING);
            }
            finally
            {
                uow.Dispose();
            }
            return model;
        }

        public Country Edit(Country model) 
        {
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            try
            {
                IRepository<GBSExtranet.Repository.TB_Country> countryRepository = uow.RepositoryFor<GBSExtranet.Repository.TB_Country>();

                var objCountry  = countryRepository.Find(x => x.ID == model.ID).SingleOrDefault();

                objCountry.ID = model.ID;
                objCountry.CurrencyID = model.CurrencyID;
                objCountry.Name_en = model.Name;
                objCountry.Code = model.Code;
                objCountry.CultureCode = model.CultureCode;

                if(!string.IsNullOrEmpty(model.VAT))
                    objCountry.VAT = Convert.ToInt32(model.VAT);

                objCountry.HasCityTax = model.CityTax;
                objCountry.HitCount = model.HitCount;
                objCountry.Sort = model.Sort;
                objCountry.Active = model.Active;

                if(!string.IsNullOrEmpty(model.TemporaryCode))
                    objCountry.TempCode = Convert.ToInt32(model.TemporaryCode);

                objCountry.OpDateTime = DateTime.Now;
                objCountry.OpUserID = model.OptUserID;
                uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.ERROR_IN_DATA_EDITING);
            }
            finally
            {
                uow.Dispose();
            }
            return model;
        }

        public bool Delete(Country model)
        {
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db); 
            try
            {
                IRepository<GBSExtranet.Repository.TB_Country> countryRepository = uow.RepositoryFor<GBSExtranet.Repository.TB_Country>(); 
                
                var user = countryRepository.Find(x => x.ID == model.ID).SingleOrDefault();
                countryRepository.Delete(user);
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
        public bool DeleteCountries(string[] ids)
        {
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            try
            {
                IRepository<GBSExtranet.Repository.TB_Country> countryRepository = uow.RepositoryFor<GBSExtranet.Repository.TB_Country>();
                foreach (string id in ids)
                {
                    int cID = Convert.ToInt32(id.Trim());
                    var country = countryRepository.Find(x => x.ID == cID).SingleOrDefault();
                    countryRepository.Delete(country);
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