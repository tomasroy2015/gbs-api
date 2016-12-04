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

namespace GBSExtranet.Api.ServiceLayer
{
    public static class PolicyUnitService
    {
        public static List<PropertyPolicyUnits> GetPolicyUnits(string culture, int itemID)
        {
            List<PropertyPolicyUnits> _units = null;
            var _db = new GBSDbContext();
            var units = (from u in _db.PropertyPolicyUnits
                         where u.PolicyItemID == itemID && u.Active == true
                         select u).ToList();
            if (units != null)
            {
                _units = new List<PropertyPolicyUnits>();
                units.ForEach(u =>
                {
                    var pUnit = new PropertyPolicyUnits();
                    pUnit.ID = u.ID;
                    pUnit.Name = new Tools().GetDynamicSortProperty(u, "Name_" + culture) == null ? string.Empty : 
                                             Convert.ToString(new Tools().GetDynamicSortProperty(u, "Name_" + culture));
                    pUnit.PolicyItemID = u.PolicyItemID;
                    pUnit.IsHideAttribute = u.IsHideAttribute;
                    _units.Add(pUnit);

                });
            }
            return _units;
        }
        public static List<PropertyPolicyUnits> GetPolicyUnitsByID(string culture, int? unitID)
        {
            List<PropertyPolicyUnit> _units = null;
            List<PropertyPolicyUnits> _pUnits = new List<PropertyPolicyUnits>();
            var _db = new GBSDbContext();
            if (unitID == null)
            {
                _units = (from u in _db.PropertyPolicyUnits
                             where u.Active == true
                             select u).ToList();
            }
            else
            {
                _units = (from u in _db.PropertyPolicyUnits
                             where u.ID == unitID && u.Active == true
                             select u).ToList();
            }
            if (_units != null)
            {
                _units.ForEach(unit =>
                {
                    var _unit = new PropertyPolicyUnits();
                    _unit.ID = unit.ID;
                    _unit.Name = new Tools().GetDynamicSortProperty(unit, "Name_" + culture) == null ? string.Empty :
                            Convert.ToString(new Tools().GetDynamicSortProperty(unit, "Name_" + culture));
                    _unit.IsHideAttribute = unit.IsHideAttribute;
                    _unit.PolicyItemID = unit.PolicyItemID;
                    _pUnits.Add(_unit);
                });
               
            }
            return _pUnits;
        }
    }
}