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
    public class RegionService : BaseService
    {
        public ResponseObject ReadAll(RequestObject filter)
        {
            List<GBSExtranet.Api.ViewModel.Region> list = new List<GBSExtranet.Api.ViewModel.Region>();
            ResponseObject data = new ResponseObject();
            try
            {
                DataTable dt = new DataTable();
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("B_Ex_GetAllRegions_TB_Region_SP", _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
               // cmd.Parameters.AddWithValue("@CultureCode", filter.Culture);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                _sqlConnection.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        GBSExtranet.Api.ViewModel.Region RegObj = new GBSExtranet.Api.ViewModel.Region();
                        RegObj.ID = Convert.ToInt64(dr["ID"].ToString());
                        RegObj.Country = dr["FK_CountryID_ID"].ToString();
                        RegObj.ParentID = dr["ParentID"].ToString();
                        RegObj.secondParentID = dr["SecondParentID"].ToString();
                        RegObj.RegionType = dr["RegionType"].ToString();
                        RegObj.SubRegionType = dr["SubRegionType"].ToString();
                        RegObj.Name = dr["Name"].ToString();
                        RegObj.NameASCII = dr["NameASCII"].ToString();
                        RegObj.Name_en = dr["Name_en"].ToString();
                        RegObj.Name_tr = dr["Name_tr"].ToString();
                        RegObj.Name_de = dr["Name_de"].ToString();
                        RegObj.Name_es = dr["Name_es"].ToString();
                        RegObj.Name_fr = dr["Name_fr"].ToString();
                        RegObj.Name_ru = dr["Name_ru"].ToString();
                        RegObj.Name_it = dr["Name_it"].ToString();
                        RegObj.Name_ar = dr["Name_ar"].ToString();
                        RegObj.Name_ja = dr["Name_ja"].ToString();
                        RegObj.Name_pt = dr["Name_pt"].ToString();
                        RegObj.Name_zh = dr["Name_zh"].ToString();
                        RegObj.Code = dr["Code"].ToString();
                        RegObj.Population = dr["Population"].ToString();
                        RegObj.IsIncludedInSearch = Convert.ToBoolean(dr["IsIncludedInDestinationSearch"].ToString());
                        RegObj.IsCity = Convert.ToBoolean(dr["IsCity"].ToString());
                        RegObj.IsPopular = Convert.ToBoolean(dr["IsPopular"].ToString());
                        RegObj.IsFilter = Convert.ToBoolean(dr["IsFilter"].ToString());
                        RegObj.IsMainPageDisplay = Convert.ToBoolean(dr["IsMainPageDisplay"].ToString());
                        RegObj.MainPageDisplaySort = dr["MainPageDisplaySort"].ToString();
                        RegObj.HitCount = dr["HitCount"].ToString();
                        RegObj.Sort = dr["Sort"].ToString();
                        RegObj.Latitude = dr["Latitude"].ToString();
                        RegObj.Longitude = dr["Longitude"].ToString();
                        RegObj.MapZoomIndex = dr["MapZoomIndex"].ToString();
                        string citytax = dr["HasCityTax"].ToString();
                        if (citytax != null && citytax != "")
                        {
                            RegObj.CityTax = Convert.ToBoolean(dr["HasCityTax"].ToString());
                        }
                        else
                        {
                            RegObj.CityTax = false;
                        }

                        RegObj.Active = Convert.ToBoolean(dr["Active"].ToString());
                        RegObj.CountryID = dr["CountryID"].ToString();
                       
                        list.Add(RegObj);                        
                    }
                }

                if (filter.Filters != null)
                    list = new Tools().FilterData<GBSExtranet.Api.ViewModel.Region>(list, filter.Filters.ToList());

                data.totalRows = list.Count;                

                if (filter.Order == "DESC")
                    list = new Tools().Sort_List<GBSExtranet.Api.ViewModel.Region>("DESC", filter.OrderBy, list);
                else if (filter.Order == "ASC")
                    list = new Tools().Sort_List<GBSExtranet.Api.ViewModel.Region>("ASC", filter.OrderBy, list);
                else
                    list = list.OrderBy(o=>o.ID).Skip(filter.Offset).Take(filter.Length).ToList();

                list = list.Skip(filter.Offset).Take(filter.Length).ToList();
                //data.rows = list.Cast<object>().ToList();
                //data.totalRows = dt.Rows.Count;
                data.rows = list.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return data;

        }

        public bool Create(Region model)
        {
            bool status = true;
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            try
            {
                IRepository<GBSExtranet.Repository.TB_Region> regionRepository = uow.RepositoryFor<GBSExtranet.Repository.TB_Region>();
                GBSExtranet.Repository.TB_Region regionObj = new GBSExtranet.Repository.TB_Region();

                if(!string.IsNullOrEmpty(model.CountryID))
                    regionObj.CountryID = Convert.ToInt32(model.CountryID);

                if(!string.IsNullOrEmpty(model.ParentID))
                    regionObj.ParentID = Convert.ToInt64(model.ParentID);

                if(!string.IsNullOrEmpty(model.secondParentID))
                    regionObj.SecondParentID = Convert.ToInt64(model.secondParentID);

                regionObj.RegionType = model.RegionType;
                regionObj.SubRegionType = model.SubRegionType;
                regionObj.Name = model.Name;
                regionObj.NameASCII = model.NameASCII;
                regionObj.Name_en = model.Name_en;
                regionObj.Name_tr = model.Name_tr;
                regionObj.Name_de = model.Name_de;
                regionObj.Name_es = model.Name_es;
                regionObj.Name_fr = model.Name_fr;
                regionObj.Name_ru = model.Name_ru;
                regionObj.Name_it = model.Name_it;
                regionObj.Name_ar = model.Name_ar;
                regionObj.Name_ja = model.Name_ja;
                regionObj.Name_pt = model.Name_pt;
                regionObj.Name_zh = model.Name_zh;
                regionObj.Code = model.Code;

                if(!string.IsNullOrEmpty(model.Population))
                    regionObj.Population = Convert.ToInt64(model.Population);
                
                regionObj.IsIncludedInDestinationSearch = model.IsIncludedInSearch;
                regionObj.IsCity = model.IsCity;
                regionObj.IsPopular = model.IsPopular;
                regionObj.IsFilter = model.IsFilter;
                regionObj.IsMainPageDisplay = model.IsMainPageDisplay;
                if(!string.IsNullOrEmpty(model.MainPageDisplaySort))
                    regionObj.MainPageDisplaySort = Convert.ToInt32(model.MainPageDisplaySort);

                if(!string.IsNullOrEmpty(model.HitCount))
                    regionObj.HitCount = Convert.ToInt64(model.HitCount);

                if(!string.IsNullOrEmpty(model.Sort))
                    regionObj.Sort = Convert.ToInt16(model.Sort);

                regionObj.Latitude = model.Latitude;
                regionObj.Longitude = model.Longitude;

                if(!string.IsNullOrEmpty(model.MapZoomIndex))
                    regionObj.MapZoomIndex = Convert.ToInt32(model.MapZoomIndex);

                regionObj.HasCityTax = model.CityTax;
                regionObj.Active = model.Active;
                regionObj.OpDateTime = DateTime.Now;
                regionObj.OpUserID  = model.OpUserID;

                regionRepository.Add(regionObj);
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

        public bool Edit(Region model) 
        {
            bool status = true;
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            try
            {

                IRepository<GBSExtranet.Repository.TB_Region> regionRepository = uow.RepositoryFor<GBSExtranet.Repository.TB_Region>();

                var RegionTable = regionRepository.Find(x => x.ID == model.ID).SingleOrDefault();
               
                if (!string.IsNullOrEmpty(model.CountryID)) { RegionTable.CountryID = Convert.ToInt32(model.CountryID); }
                if (!string.IsNullOrEmpty(model.ParentID)) { RegionTable.ParentID = Convert.ToInt64(model.ParentID); }
                if (!string.IsNullOrEmpty(model.secondParentID)) { RegionTable.SecondParentID = Convert.ToInt64(model.secondParentID); }
                RegionTable.RegionType = model.RegionType;
                RegionTable.SubRegionType = model.SubRegionType;
                RegionTable.Name = model.Name;
                RegionTable.NameASCII = model.NameASCII;
                RegionTable.Name_en = model.Name_en;
                RegionTable.Name_tr = model.Name_tr;
                RegionTable.Name_de = model.Name_de;
                RegionTable.Name_es = model.Name_es;
                RegionTable.Name_fr = model.Name_fr;
                RegionTable.Name_ru = model.Name_ru;
                RegionTable.Name_it = model.Name_it;
                RegionTable.Name_ar = model.Name_ar;
                RegionTable.Name_ja = model.Name_ja;
                RegionTable.Name_pt = model.Name_pt;
                RegionTable.Name_zh = model.Name_zh;
                RegionTable.Code = model.Code;
                if (!string.IsNullOrEmpty(model.Population)) { RegionTable.Population = Convert.ToInt64(model.Population); }
                RegionTable.IsIncludedInDestinationSearch = model.IsIncludedInSearch;
                RegionTable.IsCity = model.IsCity;
                RegionTable.IsPopular = model.IsPopular;
                RegionTable.IsFilter = model.IsFilter;
                RegionTable.IsMainPageDisplay = model.IsMainPageDisplay;
                if (!string.IsNullOrEmpty(model.MainPageDisplaySort)) { RegionTable.MainPageDisplaySort = Convert.ToInt32(model.MainPageDisplaySort); }
                if (!string.IsNullOrEmpty(model.HitCount)) { RegionTable.HitCount = Convert.ToInt64(model.HitCount); }
                if (!string.IsNullOrEmpty(model.Sort)) { RegionTable.Sort = Convert.ToInt16(model.Sort); }
                RegionTable.Latitude = model.Latitude;
                RegionTable.Longitude = model.Longitude;
                if (!string.IsNullOrEmpty(model.MapZoomIndex)) { RegionTable.MapZoomIndex = Convert.ToInt32(model.MapZoomIndex); }
                RegionTable.HasCityTax = model.CityTax;
                RegionTable.Active = model.Active;
                RegionTable.OpUserID = model.OpUserID;
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
            return status;
        }

        public bool Delete(Region model)
        {
            bool status = true;
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            try
            {
                IRepository<GBSExtranet.Repository.TB_Region> regionRepository = uow.RepositoryFor<GBSExtranet.Repository.TB_Region>();
                var RegionTable = regionRepository.Find(x => x.ID == model.ID).FirstOrDefault();
                regionRepository.Delete(RegionTable);
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
            return status;
        }
        public bool DeleteRegions(string[] ids) 
        {
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            try
            {
                IRepository<GBSExtranet.Repository.TB_Region> regionRepository = uow.RepositoryFor<GBSExtranet.Repository.TB_Region>();
                foreach (string id in ids)
                {
                    long cID = Convert.ToInt64(id.Trim());
                    var region = regionRepository.Find(x => x.ID == cID).SingleOrDefault();
                    regionRepository.Delete(region);
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