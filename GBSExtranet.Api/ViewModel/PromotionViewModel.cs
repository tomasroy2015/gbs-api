using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PromotionViewModel
    {
        public List<DropdownList> PricePolicies { get; set; }
        public List<PropertyPhotos> HotelRooms { get; set; }
        public List<NewPromotion> DaysDetails { get; set; }
        public int MinimumPromotionDiscountPercentage { get; set; }
        public int MaximumPromotionDiscountPercentage { get; set; }
        public int DefaultPromotionDiscountPercentage { get; set; }
        public int MaximumDayCountForMinimumStayPromotion { get; set; }
        public int MaximumHourCountForMinimumStayPromotion { get; set; }
    }
}