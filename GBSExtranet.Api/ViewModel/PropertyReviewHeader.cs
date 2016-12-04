using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyReviewHeader
    {
        public string Name { get; set; }
        public DateTime ReviewDate { get; set; }
        public List<PropertyReview> IndividualReviews { get; set; }
        public string PositiveComment { get; set; }
        public string NegativeComment { get; set; }
    }
}