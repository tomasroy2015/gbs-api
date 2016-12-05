using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GBSExtranet.Repository;

using System.ComponentModel.DataAnnotations.Schema;

namespace GBSExtranet.Repository 
{
    public class GBSDbContext : DbContext
    {
        public DbSet<TB_Country> Countries { get; set; }
        public DbSet<TB_Region> Regions { get; set; }
        public DbSet<BizTbl_Parameter> Parameters { get; set; }
        public DbSet<BizTbl_SecurityGroupRight> SecurityGroupRights { get; set; }
        public DbSet<TB_Hotel> Hotels { get; set; }
        public DbSet<TB_HotelAttribute> HotelAttributes { get; set; }
        public DbSet<TB_Attribute> Attributes { get; set; }
        public DbSet<ChildrenPolicies> ChildrenPolicies { get; set; }
        public DbSet<ChildrenPolicyUnit> ChildrenPolicyUnits { get; set; }
        public DbSet<ChildrenPoliciesHeader> ChildrenPolicyHeaders { get; set; }
        public DbSet<ChildrenPolicyItems> ChildrenPolicyItems { get; set; }
        public DbSet<HotelChildrenPolicy> HotelChildrenPolicy { get; set; }
        public DbSet<ChildrenPolicySummary> ChildrenPolicySummary { get; set; }
        public DbSet<PropertyPolicy> PropertyPolicies { get; set; }
        public DbSet<PropertyPolicyItem> PropertyPolicyItems { get; set; }
        public DbSet<PropertyPolicyUnit> PropertyPolicyUnits { get; set; }
        public DbSet<HotelPropertyPolicy> HotelPropertyPolicies { get; set; }
        public DbSet<PriceUnit> PriceUnits { get; set; }
        public DbSet<HotelPolicySummary> HotelPoicySummary { get; set; }
        public DbSet<TB_TypePrepayment> TypePrepayments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
