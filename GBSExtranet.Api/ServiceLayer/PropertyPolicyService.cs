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
using GBSExtranet.Repository;
using System.Globalization;
using System.Reflection;
using System.Web.Configuration;
namespace GBSExtranet.Api.ServiceLayer
{
    public class PropertyPolicyService : BaseService
    {
        public List<PropertyPolicies> GetPropertyPolicies(string culture, int hotelID)
        {
            var _context = new GBSDbContext();
            List<PropertyPolicies> _policies = null;
            
            try
            {
                var policies = _context.PropertyPolicies.Where(f => f.Active == true).ToList().OrderBy(o=>o.Sort);
                _policies = new List<PropertyPolicies>();
                foreach (var header in policies)
                {
                    var policy = new PropertyPolicies();
                    policy.PropertyPolicyItems = new List<PropertyPolicyItems>();
                    policy.ID = header.ID;
                    policy.Name = new Tools().GetDynamicSortProperty(header, "Name_"+culture) == null ? string.Empty : 
                                         Convert.ToString(new Tools().GetDynamicSortProperty(header, "Name_"+culture));
                    policy.Description = new Tools().GetDynamicSortProperty(header, "Description_" + culture) == null ? string.Empty :
                                         Convert.ToString(new Tools().GetDynamicSortProperty(header, "Description_" + culture));
                    policy.Code = header.Code;
                    policy.Sort = header.Sort;
                    policy.Icons = header.Icons;
                    var items = (from i in _context.PropertyPolicyItems
                                 join h in _context.HotelPropertyPolicies on
                                 new { ItemID = i.ID, HeaderID = i.PropertyPolicyID } equals
                                 new { ItemID = h.PropertyPolicyItemID, HeaderID = h.PropertyPolicyID }
                                 into p
                                 from h in p.DefaultIfEmpty()
                                 where i.Active == true && i.PropertyPolicyID == header.ID && h.HotelID == hotelID
                                 select new
                                 {
                                     ID = i.ID,
                                     PropertyPolicyID = i.PropertyPolicyID,
                                     Description = i.Description,
                                     Name = i.Name,
                                     Name_ar = i.Name_ar,
                                     Name_de = i.Name_de,
                                     Name_en = i.Name_en,
                                     Name_es = i.Name_es,
                                     Name_fr = i.Name_fr,
                                     Name_it = i.Name_it,
                                     Name_ja = i.Name_ja,
                                     Name_pt = i.Name_pt,
                                     Name_ru = i.Name_ru,
                                     Name_tr = i.Name_tr,
                                     Name_zh = i.Name_zh, 
                                     IsParentItem = i.IsParentItem,
                                     IsChargedItem = i.IsChargedItem,
                                     PriceLabel = i.PriceLabel,
                                     ParentPolicyItemID = i.ParentPolicyItemID,
                                     PriceUnitID = h.PriceUnitID,
                                     HotelID = hotelID,
                                     UnitID = h.UnitID,
                                     UnitValue=h.UnitValue,
                                     CurrencyID = h.CurrencyID,
                                     Price = h.Price
                                 }).ToList();

                    if (items != null && items.Count > 0)
                    {
                        items.ForEach(f=>{
                            var pItem = new PropertyPolicyItems();
                            pItem.ID = f.ID;
                            pItem.HotelID = f.HotelID;
                            pItem.IsChargedItem = f.IsChargedItem;
                            pItem.IsParentItem = f.IsParentItem;
                            pItem.Name = new Tools().GetDynamicSortProperty(f, "Name_" + culture) == null ? string.Empty : Convert.ToString(new Tools().GetDynamicSortProperty(f, "Name_" + culture));
                            pItem.Description = f.Description;
                            pItem.ParentPolicyItemID = f.ParentPolicyItemID;
                            pItem.PropertyPolicyID = f.PropertyPolicyID;
                            pItem.Price = f.Price;
                            pItem.PriceLabel = f.PriceLabel;
                            pItem.UnitID = f.UnitID;
                            pItem.UnitValue = f.UnitValue;
                            pItem.PriceUnitID = f.PriceUnitID;
                            pItem.CurrencyID = f.CurrencyID;
                            pItem.PolicyItemUnits = PolicyUnitService.GetPolicyUnits(culture, f.ID);
                            policy.PropertyPolicyItems.Add(pItem);
                        });
                    }
                    else
                    {
                        var pItems = (from i in _context.PropertyPolicyItems
                                     join h in _context.HotelPropertyPolicies on
                                     new { ItemID = i.ID, HeaderID = i.PropertyPolicyID } equals
                                     new { ItemID = h.PropertyPolicyItemID, HeaderID = h.PropertyPolicyID }
                                     into p
                                     from h in p.DefaultIfEmpty()
                                     where i.Active == true && i.PropertyPolicyID == header.ID
                                     select new
                                     {
                                         ID = i.ID,
                                         PropertyPolicyID = i.PropertyPolicyID,
                                         Description = i.Description,
                                         Name = i.Name,
                                         Name_ar = i.Name_ar,
                                         Name_de = i.Name_de,
                                         Name_en = i.Name_en,
                                         Name_es = i.Name_es,
                                         Name_fr = i.Name_fr,
                                         Name_it = i.Name_it,
                                         Name_ja = i.Name_ja,
                                         Name_pt = i.Name_pt,
                                         Name_ru = i.Name_ru,
                                         Name_tr = i.Name_tr,
                                         Name_zh = i.Name_zh,
                                         IsParentItem = i.IsParentItem,
                                         IsChargedItem = i.IsChargedItem,
                                         PriceLabel = i.PriceLabel,
                                         PriceUnitID = h.PriceUnitID,
                                         ParentPolicyItemID = i.ParentPolicyItemID,
                                         HotelID = h.HotelID == null ? hotelID : h.HotelID,
                                         UnitID = h.UnitID,
                                         UnitValue = h.UnitValue,
                                         CurrencyID = h.CurrencyID == null ? -1 :h.CurrencyID,
                                         Price = h.Price
                                     }).ToList();

                        pItems.ForEach(f =>
                        {
                            var pItem = new PropertyPolicyItems();
                            pItem.ID = f.ID;
                            pItem.HotelID = f.HotelID;
                            pItem.IsChargedItem = f.IsChargedItem;
                            pItem.IsParentItem = f.IsParentItem;
                            pItem.Name = new Tools().GetDynamicSortProperty(f, "Name_" + culture) == null ? string.Empty : Convert.ToString(new Tools().GetDynamicSortProperty(f, "Name_" + culture));
                            pItem.Description = f.Description;
                            pItem.ParentPolicyItemID = f.ParentPolicyItemID;
                            pItem.PropertyPolicyID = f.PropertyPolicyID;
                            pItem.Price = f.Price;
                            pItem.PriceLabel = f.PriceLabel;
                            pItem.UnitID = f.UnitID;
                            pItem.UnitValue = f.UnitValue;
                            pItem.PriceUnitID = f.PriceUnitID;
                            pItem.CurrencyID = f.CurrencyID;
                            pItem.PolicyItemUnits = PolicyUnitService.GetPolicyUnits(culture, f.ID);
                            policy.PropertyPolicyItems.Add(pItem);
                        });
                    }
                    _policies.Add(policy);
                }
            }
            catch (EntitySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.FAULT_DATABASE_OPERATION_FAILD);
            }
            return _policies;
        }

        public List<HotelPolicySummary> UpdatePropertyPolicies(List<PropertyPolicies> policies, string culture, int hotelID, long userID, int currencyID)
        {
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            List<HotelPolicySummary> summaries = null;
            try
            {
                IRepository<Repository.HotelPropertyPolicy> propertyRepository = uow.RepositoryFor<Repository.HotelPropertyPolicy>();
                policies.ForEach(header =>
                {
                    header.PropertyPolicyItems.ForEach(item =>
                    {
                        var policy = propertyRepository.Find(f => f.HotelID == hotelID && f.PropertyPolicyID == header.ID && f.PropertyPolicyItemID == item.ID).FirstOrDefault();
                        if (policy != null)
                        {
                            propertyRepository.Delete(policy);
                        }
                        var newPolicy = new Repository.HotelPropertyPolicy();
                        newPolicy.PropertyPolicyID = item.PropertyPolicyID;
                        newPolicy.PropertyPolicyItemID = item.ID;
                        newPolicy.UnitID = item.UnitID;
                        newPolicy.UnitValue = item.UnitID != null ? PolicyUnitService.GetPolicyUnitsByID(culture, item.UnitID).FirstOrDefault().Name : string.Empty;
                        newPolicy.Active = true;
                        newPolicy.CurrencyID = currencyID;
                        newPolicy.HotelID = hotelID;
                        newPolicy.PriceUnitID = item.PriceUnitID;
                        newPolicy.Price = item.Price;
                        newPolicy.OpDateTime = DateTime.Now;
                        newPolicy.OpUserID = userID;
                        propertyRepository.Add(newPolicy);
                    });
                });
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
            UpdatePolicySummary(hotelID, culture, userID);
            summaries = GetPolicySummaries(hotelID);
            return summaries;
        }
        public List<HotelPolicySummary> GetPolicySummaries(int hotelID)
        {
            var _db = new GBSDbContext();
            var summaries = (from s in _db.HotelPoicySummary
                             where s.HotelID == hotelID
                             select s).ToList().Select(n => new HotelPolicySummary
                             {
                                 ID = n.ID,
                                 HotelID = n.HotelID,
                                 Icons = WebConfigurationManager.AppSettings["ImageURL"] + (n.Icons == null ? "Nothing.png": n.Icons),
                                 Description = n.Description,
                                 OpUserID = n.OpUserID,
                                 OpDateTime = n.OpDateTime
                             }).ToList();
            return summaries;
        }
        private bool UpdatePolicySummary( int hotelID,string culture,long userID)
        {
            List<PropertyPolicies> policies = GetPropertyPolicies(culture,hotelID);
           
            bool isSaved = false;
            try
            {
                GBSDbContext _db = new GBSDbContext();
                var allUnits = PolicyUnitService.GetPolicyUnitsByID(culture,null);
                var summary = _db.HotelPoicySummary.Where(w => w.HotelID == hotelID).ToList();
                if (summary != null)
                {
                    _db.HotelPoicySummary.RemoveRange(summary);
                }

                var policyItems = new List<ChildrenPolicyItem>();
                policies.ForEach(h =>
                {
                    var noItem = (from i in h.PropertyPolicyItems
                                  where i.UnitValue.Trim().ToLower().Equals("no") && i.PropertyPolicyID == h.ID
                                  select i).ToList().FirstOrDefault();
                    if (noItem != null)
                    {
                        string description1 = string.Empty;
                        var policy = new HotelPolicySummary();

                        if (noItem.Name.Trim().ToLower().Contains("allow"))
                            description1 += h.Name + " are not allowed.";

                        if (noItem.Name.Trim().ToLower().Contains("available"))
                            description1 += h.Name + " is not available.";

                        policy.Description = description1;
                        policy.HotelID = hotelID;
                        policy.OpDateTime = DateTime.Now;
                        policy.OpUserID = userID;
                        policy.Icons =  h.Icons; 
                        _db.HotelPoicySummary.Add(policy);
                    }
                    else
                    {
                        var pItems = (from i in h.PropertyPolicyItems                                     
                                      select i).ToList();
                        string description = string.Empty;
                        //description = new Tools().GetDynamicSortProperty(h, "Name_" + culture) != null ? Convert.ToString(new Tools().GetDynamicSortProperty(h, "Name_" + culture)) :string.Empty;
                        int count = 0;
                        description = h.Name;
                        bool isFreeOfCharge = false;
                        //pItems.ForEach(f =>
                        //{
                        foreach (var f in pItems)
                        {
                            count++;
                            if (f.IsParentItem == true)
                            {
                                if (!f.UnitValue.Trim().ToLower().Equals("yes"))
                                {
                                    if (f.Name.Trim().ToLower().Contains("allow"))
                                        description += " are allowed." + f.UnitValue;

                                    if (f.Name.Trim().ToLower().Contains("available"))
                                        description += " available and " + f.UnitValue;
                                    isFreeOfCharge = false;
                                }
                                if (f.UnitValue.Trim().ToLower().Equals("yes"))
                                {
                                    if (f.Name.Trim().ToLower().Contains("allow"))
                                        description += " are allowed.";

                                    if (f.Name.Trim().ToLower().Contains("available"))
                                        description += " available.";
                                    isFreeOfCharge = false;
                                }
                                if (f.UnitValue.Trim().ToLower().Contains("free"))
                                {
                                  //  description += f.UnitValue;
                                    isFreeOfCharge = true;
                                    break;
                                }
                                
                            }
                            else
                            {
                                description += " " + f.UnitValue;
                                if (count == pItems.Count - 1)
                                    description += " for guests,";
                            }


                            if (f.IsChargedItem == true && f.Price > 0 && !isFreeOfCharge)
                            {
                                description += "Charge " + new DropdownService().GetCurrencies(culture).Find(a => a.ID == f.CurrencyID).Name + " " + f.Price.ToString() + " "
                                             + new DropdownService().GetPriceUnits(culture).Find(a => a.ID == f.PriceUnitID).Name;
                            }
                            //});
                        }
                        var pol = new HotelPolicySummary();
                        pol.Description = description+".";
                            //+" as "+h.Name+" policies.";
                        pol.HotelID = hotelID;
                        pol.OpDateTime = DateTime.Now;
                        pol.OpUserID = userID;
                        pol.Icons = h.Icons;
                        _db.HotelPoicySummary.Add(pol);
                    }                  
                });
               _db.SaveChanges();
                isSaved = true;
            }
            catch (Exception ex)
            {
                isSaved = false;
                throw new Exception(ErrorMessage.ERROR_IN_DATA_ADDING);
            }
            finally
            {
                _db.Dispose();
            }
            return isSaved;
        }
    }
}