using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using System.ServiceModel;
using GBSExtranet.Api.ViewModel;
using GBSExtranet.Repository;
using Business;
using System.Data;
using GBSExtranet.Api.ServiceLayer;
namespace GBSExtranet.Api
{
    public class ChildrenPolicyService : BaseService
    {
        public ChidrenPoliciesModel GetChildrenPolicy(int hotelID)
        {
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            ChidrenPoliciesModel childrenPolicy = null;
            try
            {
                IRepository<ChildrenPolicies> childrenRepository = uow.RepositoryFor<ChildrenPolicies>();
                var policy = childrenRepository.Find(f => f.HotelID == hotelID).FirstOrDefault();
                if(policy != null)
                {
                    childrenPolicy = new ChidrenPoliciesModel();
                    childrenPolicy.ID = policy.ID;
                    childrenPolicy.HotelID = policy.HotelID;
                    childrenPolicy.IsAccommodate = policy.IsAccommodate;
                    childrenPolicy.IsAdult = policy.IsAdult;
                    childrenPolicy.IsExistingBedChild = policy.IsExistingBedChild;
                    childrenPolicy.IsExtraBedChild = policy.IsExtraBedChild;
                    childrenPolicy.IsExtraBedProvided = policy.IsExtraBedProvided;
                    childrenPolicy.IsTwoYearsOld = policy.IsTwoYearsOld;
                    childrenPolicy.NoOfExtraBed = policy.NoOfExtraBed;
                    childrenPolicy.TwoYearsChildCharge = policy.TwoYearsChildCharge;
                    childrenPolicy.AdultCharge = policy.AdultCharge;
                    childrenPolicy.CurrencyID = policy.CurrencyID;
                    childrenPolicy.ExistingBedChildCharge = policy.ExistingBedChildCharge;
                    childrenPolicy.ExistingBedChildCount = policy.ExistingBedChildCount;
                    childrenPolicy.ExistingBedChildUnit = policy.ExistingBedChildUnit;
                    childrenPolicy.ExtraBedChildUnit = policy.ExistingBedChildUnit;         
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.ERROR_IN_DATA_ADDING);
            }
            finally
            {
                uow.Dispose();
            }
            return childrenPolicy;
        }

        public List<ChildrenPolicy> GetChildrenPolicySettings(int hotelID, string culture)
        {
            GBSDbContext _db = new GBSDbContext();
            List<ChildrenPolicy> childrenPolicies = null;
            ChildrenPolicy childrenPolicy = null;
            ChildrenPolicyItem policyItem = null;
            try
            {
                var headers = _db.ChildrenPolicyHeaders.ToList();
                if (headers != null)
                {
                    childrenPolicies = new List<ChildrenPolicy>();
                    foreach (var header in headers)
                    {
                        childrenPolicy = new ChildrenPolicy();
                        childrenPolicy.ChildrenPolicyItems = new List<ChildrenPolicyItem>();

                        childrenPolicy.ID = header.ID;
                        childrenPolicy.Code = header.Code;
                        childrenPolicy.Active = header.Active;
                        childrenPolicy.Description = header.Description_en;
                        childrenPolicy.Name = header.Name_en;
                        childrenPolicy.Sort = header.Sort;
                        childrenPolicy.PolicyType = header.PolicyType;
                        childrenPolicy.OpDateTime = header.OpDateTime;
                        childrenPolicy.OpUserID = header.OpUserID;
                        
                        var items = (from i in _db.ChildrenPolicyItems
                                     join h in _db.HotelChildrenPolicy on 
                                     new { ItemID = i.ID,HeaderID = i.ChildrenPolicyHeaderID  } equals
                                     new {ItemID = h.ChildrenPolicyItemID,HeaderID = h.ChildrenPolicyHeaderID}
                                     into p
                                     from h in p.DefaultIfEmpty()
                                     where i.ChildrenPolicyHeaderID == header.ID && h.HotelID == hotelID
                                     select new  
                                     {
                                         ID = i.ID,
                                         ChildrenPolicyHeaderID = i.ChildrenPolicyHeaderID,                                         
                                         Description = i.Description_en,
                                         HasChildUnit = i.HasChildUnit,
                                         IsChargeable = i.IsChargeable,
                                         IsCheckedItem = i.IsCheckedItem,
                                         IsExtrabedItem = i.IsExtrabedItem,
                                         IsExistingBedItem = i.IsExistingBedItem,
                                         IsProviderNeeded = i.IsProviderNeeded,
                                         Name = i.Name_en,
                                         PriceLabel = i.PriceLabel,
                                         HotelID = h.HotelID,
                                         ChildUnitID = h.ChildUnitID,
                                         NoOfExtraBed = h.NoOfExtraBed,
                                         NoOfChildExistingBed = h.NoOfChildExistingBed,
                                         Price = h.Price,
                                         CurrencyID = h.CurrencyID,
                                         IsAttributeSelected = h.IsAttributeSelected == null ? false : h.IsAttributeSelected,
                                         IsChildrenAccommodated = h.IsChildrenAccommodated == null ? false : h.IsChildrenAccommodated,
                                         IsExtrabedNeeded = h.IsExtrabedNeeded == null ? false : h.IsExtrabedNeeded

                                     }).ToList();

                        if (items != null && items.Count > 0)
                        {
                            items.ForEach(item =>
                            {
                                policyItem = new ChildrenPolicyItem();
                                policyItem.ID = item.ID;
                                policyItem.ChildrenPolicyHeaderID = item.ChildrenPolicyHeaderID;
                                policyItem.ChildUnitID = item.ChildUnitID;
                                policyItem.Description = item.Description;
                                policyItem.HasChildUnit = item.HasChildUnit;
                                policyItem.IsChargeable = item.IsChargeable;
                                policyItem.IsCheckedItem = item.IsCheckedItem;
                                policyItem.IsExtrabedItem = item.IsExtrabedItem;
                                policyItem.IsExistingBedItem = item.IsExistingBedItem;
                                policyItem.IsProviderNeeded = item.IsProviderNeeded;
                                policyItem.Name = item.Name;
                                policyItem.PriceLabel = item.PriceLabel;
                                policyItem.HotelID = item.HotelID;
                                policyItem.NoOfExtraBed = item.NoOfExtraBed;
                                policyItem.NoOfChildExistingBed = item.NoOfChildExistingBed;
                                policyItem.Price = item.Price;
                                policyItem.CurrencyID = item.CurrencyID;
                                policyItem.IsAttributeSelected = item.IsAttributeSelected;
                                policyItem.IsChildrenAccommodated = item.IsChildrenAccommodated;
                                policyItem.IsExtrabedNeeded = item.IsExtrabedNeeded;
                                childrenPolicy.ChildrenPolicyItems.Add(policyItem);
                            });
                           
                        }
                        else
                        {
                            var nitems = (from i in _db.ChildrenPolicyItems
                                         join h in _db.HotelChildrenPolicy on
                                         new { ItemID = i.ID, HeaderID = i.ChildrenPolicyHeaderID } equals
                                         new { ItemID = h.ChildrenPolicyItemID, HeaderID = h.ChildrenPolicyHeaderID }
                                         into p
                                         from h in p.DefaultIfEmpty()
                                         where i.ChildrenPolicyHeaderID == header.ID 
                                         select new
                                         {
                                             ID = i.ID,
                                             ChildrenPolicyHeaderID = i.ChildrenPolicyHeaderID,
                                             Description = i.Description_en,
                                             HasChildUnit = i.HasChildUnit,
                                             IsChargeable = i.IsChargeable,
                                             IsCheckedItem = i.IsCheckedItem,
                                             IsExtrabedItem = i.IsExtrabedItem,
                                             IsExistingBedItem = i.IsExistingBedItem,
                                             IsProviderNeeded = i.IsProviderNeeded,
                                             Name = i.Name_en,
                                             PriceLabel = i.PriceLabel,
                                             HotelID = h.HotelID,
                                             ChildUnitID = h.ChildUnitID == null ? 1 : h.ChildUnitID,
                                             NoOfExtraBed = h.NoOfExtraBed == null ? 1 : h.NoOfExtraBed,
                                             NoOfChildExistingBed = h.NoOfChildExistingBed == null ? 1 :h.NoOfChildExistingBed,
                                             Price = h.Price,
                                             CurrencyID = h.CurrencyID == null ? 1 : h.CurrencyID,
                                             IsAttributeSelected = h.IsAttributeSelected == null ? false : h.IsAttributeSelected,
                                             IsChildrenAccommodated = h.IsChildrenAccommodated == null ? false : h.IsChildrenAccommodated,
                                             IsExtrabedNeeded = h.IsExtrabedNeeded == null ? false : h.IsExtrabedNeeded
                                         }).ToList();

                            nitems.ForEach(item =>
                            {
                                policyItem = new ChildrenPolicyItem();
                                policyItem.ID = item.ID;
                                policyItem.ChildrenPolicyHeaderID = item.ChildrenPolicyHeaderID;
                                policyItem.ChildUnitID = item.ChildUnitID;
                                policyItem.Description = item.Description;
                                policyItem.HasChildUnit = item.HasChildUnit;
                                policyItem.IsChargeable = item.IsChargeable;
                                policyItem.IsCheckedItem = item.IsCheckedItem;
                                policyItem.IsExtrabedItem = item.IsExtrabedItem;
                                policyItem.IsExistingBedItem = item.IsExistingBedItem;
                                policyItem.IsProviderNeeded = item.IsProviderNeeded;
                                policyItem.Name = item.Name;
                                policyItem.PriceLabel = item.PriceLabel;
                                policyItem.HotelID = item.HotelID;
                                policyItem.NoOfExtraBed = item.NoOfExtraBed;
                                policyItem.NoOfChildExistingBed = item.NoOfChildExistingBed;
                                policyItem.Price = item.Price;
                                policyItem.CurrencyID = item.CurrencyID;
                                policyItem.IsAttributeSelected = item.IsAttributeSelected;
                                policyItem.IsChildrenAccommodated = item.IsChildrenAccommodated;
                                policyItem.IsExtrabedNeeded = item.IsExtrabedNeeded;
                                childrenPolicy.ChildrenPolicyItems.Add(policyItem);
                            });
                           
                        }
                        childrenPolicies.Add(childrenPolicy);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.FAULT_DATABASE_OPERATION_FAILD);
            }

            return childrenPolicies;
        }

        public List<ChildrenPolicyViewModel> GetChildrenPolicySummary(string currency, int? hotelID, string culture)
        {
            var context = new GBSDbContext();
            var childrenPolicySummary = new ChildrenPolicySummary();
            ChildrenPolicyViewModel summary = new ChildrenPolicyViewModel();
            List<ChildrenPolicyViewModel> summaries = null;
            try
            {
                childrenPolicySummary = context.ChildrenPolicySummary.Where(f => f.HotelID == hotelID).ToList().FirstOrDefault();
                if (!childrenPolicySummary.HasChildrenPolicy && !childrenPolicySummary.HasExtraBedPolicy)
                {
                    summaries = new List<ChildrenPolicyViewModel>();
                    summary = new ChildrenPolicyViewModel();
                    summary.Description = "Children cannot be accommodated at the hotel.";
                    summaries.Add(summary);

                    summary = new ChildrenPolicyViewModel();
                    summary.Description = "There is no capacity for extra beds in the room.";
                    summaries.Add(summary);
                }
                if(childrenPolicySummary.HasChildrenPolicy && childrenPolicySummary.HasExtraBedPolicy){
                    summaries = new List<ChildrenPolicyViewModel>();
                    summary = new ChildrenPolicyViewModel();
                    summary.Description = "All children are welcome.";
                    summaries.Add(summary);

                    summary = new ChildrenPolicyViewModel();
                    summary.Description = "All children are charged " + new DropdownService().GetCurrencies(culture).Find(a => a.Code == currency).Name +" "+ childrenPolicySummary.ChildrenPrice + " per night for extra beds.";
                    summaries.Add(summary);
                    summary = new ChildrenPolicyViewModel();
                    summary.Description = "All adults are charged " + new DropdownService().GetCurrencies(culture).Find(a => a.Code == currency).Name + " " + childrenPolicySummary.AdultPrice + " per night for extra beds.";
                    summaries.Add(summary);
                    summary = new ChildrenPolicyViewModel();
                    summary.Description = "The maximum number of extra beds/children's cots permitted in a room is "+childrenPolicySummary.MaxNoExtraBed;
                    summaries.Add(summary);
                    summary = new ChildrenPolicyViewModel();
                    summary.Description = "All children are charged " + new DropdownService().GetCurrencies(culture).Find(a => a.Code == currency).Name + " " + childrenPolicySummary.ChildrenPrice + " per night for extra beds.";
                    summaries.Add(summary);
                }
                if(!childrenPolicySummary.HasChildrenPolicy && childrenPolicySummary.HasExtraBedPolicy){
                    summaries = new List<ChildrenPolicyViewModel>();
                    summary = new ChildrenPolicyViewModel();
                    summary.Description = "Children cannot be accommodated at the hotel.";
                    summaries.Add(summary);

                    summary = new ChildrenPolicyViewModel();
                    summary.Description = "Adults are charged EUR "+childrenPolicySummary.AdultPrice+" per night for extra beds.The maximum number of extra beds "+childrenPolicySummary.MaxNoExtraBed;
                    summaries.Add(summary);
                }
                if(childrenPolicySummary.HasChildrenPolicy && !childrenPolicySummary.HasExtraBedPolicy){
                    summaries = new List<ChildrenPolicyViewModel>();
                    summary = new ChildrenPolicyViewModel();
                    summary.Description = "There is no capacity for extra beds in the room.";
                    summaries.Add(summary);
                    summary = new ChildrenPolicyViewModel();
                    summary.Description = "The maximum number of children's that can stay in existing bed is "+childrenPolicySummary.MaxNoOfChild;
                    summaries.Add(summary);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorMessage.FAULT_DATABASE_OPERATION_FAILD);
            }
            finally
            {
                context.Dispose();
            }
            return summaries;
        }
        public List<ChildrenPolicy> UpdateChildrenPolicy(List<ChildrenPolicy> policies, int? hotelID, int? currencyID) 
        {
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            try
            {
                IRepository<Repository.HotelChildrenPolicy> childrenRepository = uow.RepositoryFor<Repository.HotelChildrenPolicy>();
                
                policies.ForEach(header =>
                {
                    header.ChildrenPolicyItems.ForEach(item =>
                    {
                        var hotelPolicy = childrenRepository.Find(f => f.HotelID == hotelID && f.ChildrenPolicyHeaderID == item.ChildrenPolicyHeaderID && f.ChildrenPolicyItemID == item.ID).FirstOrDefault();
                        if (hotelPolicy != null)
                        {
                            //hotelPolicy.ChildrenPolicyHeaderID = item.ChildrenPolicyHeaderID;
                            //hotelPolicy.ChildrenPolicyItemID = item.ID;
                            //hotelPolicy.ChildUnitID = item.ChildUnitID;
                            //hotelPolicy.CurrencyID = item.CurrencyID == null ? -1 : item.CurrencyID;
                            //hotelPolicy.HotelID = hotelID;
                            //hotelPolicy.NoOfChildExistingBed = item.NoOfChildExistingBed;
                            //hotelPolicy.NoOfExtraBed = item.NoOfExtraBed;
                            //hotelPolicy.Price = item.Price;
                            //hotelPolicy.IsAttributeSelected = item.IsAttributeSelected;
                            childrenRepository.Delete(hotelPolicy);
                        }
                        //else
                        //{
                            Repository.HotelChildrenPolicy newPolicy = new Repository.HotelChildrenPolicy();
                            newPolicy.ChildrenPolicyHeaderID = item.ChildrenPolicyHeaderID;
                            newPolicy.ChildrenPolicyItemID = item.ID;
                            newPolicy.ChildUnitID = item.ChildUnitID;
                            newPolicy.CurrencyID = currencyID;
                            newPolicy.HotelID = hotelID;
                            newPolicy.NoOfChildExistingBed = item.NoOfChildExistingBed;
                            newPolicy.NoOfExtraBed = item.NoOfExtraBed;
                            newPolicy.Price = item.Price;
                            newPolicy.IsAttributeSelected = item.IsAttributeSelected;
                            newPolicy.IsChildrenAccommodated = item.IsChildrenAccommodated;
                            newPolicy.IsExtrabedNeeded = item.IsExtrabedNeeded;
                            childrenRepository.Add(newPolicy);
                        //}
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
            UpdateChildrenSummary(policies, hotelID);
            return policies;
        }

        public bool UpdateChildrenSummary(List<ChildrenPolicy> policies, int? hotelID)
        {
            GBSDbContext _db = new GBSDbContext();
            UnitOfWork uow = new UnitOfWork(_db);
            bool isSaved = false;
            try
            {
                IRepository<Repository.ChildrenPolicySummary> childrenRepository = uow.RepositoryFor<Repository.ChildrenPolicySummary>();

                var summary = _db.ChildrenPolicySummary.Where(w => w.HotelID == hotelID).FirstOrDefault();
                if (summary != null)
                {
                    _db.ChildrenPolicySummary.Remove(summary);
                }
                
                var policyItems = new List<ChildrenPolicyItem>();
                policies.ForEach(h =>
                {
                    h.ChildrenPolicyItems.ForEach(i =>
                    {
                        policyItems.Add(i);
                    });
                });

                var childSummary = new Repository.ChildrenPolicySummary();
                childSummary.HotelID = hotelID;
                childSummary.HasChildrenPolicy = policyItems.Where(f => f.IsChildrenAccommodated == true).FirstOrDefault() != null ? true : false;
                childSummary.HasExtraBedPolicy = policyItems.Where(f => f.IsExtrabedNeeded == true).FirstOrDefault() != null ? true : false;
                var childItms = (from p in policyItems
                                where p.IsExtrabedItem == true && p.Name.Contains("Children")
                                select p).ToList();
                childSummary.ChildrenPrice =  childItms.Sum(s=>s.Price).Value;
                var adultItms = (from p in policyItems
                                where p.IsExtrabedItem == true && p.Name.Contains("Adult")
                                select p).ToList();
                childSummary.AdultPrice = adultItms.Sum(s => s.Price).Value;
                childSummary.MaxNoExtraBed = policyItems.Max(f => f.NoOfExtraBed).Value;
                childSummary.MaxNoOfChild = policyItems.Max(f => f.NoOfChildExistingBed).Value;
                _db.ChildrenPolicySummary.Add(childSummary);
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