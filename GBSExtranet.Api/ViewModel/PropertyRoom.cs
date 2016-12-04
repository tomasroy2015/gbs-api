using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyRoom
    {
        public Int64 ID { get; set; }

        public Int64 HotelID { get; set; }

        public int RoomTypeID { get; set; }

        public string RoomCount { get; set; }

        public string RoomTypeName { get; set; }

        public string RoomSize { get; set; }

        public string MaxPeopleCount { get; set; }

        public string IDWithMaxPeopleCount { get; set; }

        public string MaxChildrenCount { get; set; }

        public string BabyCotCount { get; set; }

        public string ExtraBedCount { get; set; }

        public string SmokingTypeID { get; set; }

        public string SmokingTypeName { get; set; }

        public string ViewTypeID { get; set; }

        public string ViewTypeName { get; set; }

        public string IncludedInRoomTypeCaption { get; set; }

        public bool Active { get; set; }

        public string CreateDateTime { get; set; }

        public string CreateUserID { get; set; }

        public string OpDateTime { get; set; }

        public string OpUserID { get; set; }


        public string BedId { get; set; }

        public string BedName { get; set; }
        public string OptionNo { get; set; }

        public string BedTypeId { get; set; }

        public string BedCount { get; set; }

        public string AttributeHeaderId { get; set; }
        public string AttributeHeaderName { get; set; }

        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public bool AttributeIsSelected { get; set; }

        public List<PropertyRoom> AttributeList { get; set; }


        public string EncryptRoomID { get; set; }
    }
}