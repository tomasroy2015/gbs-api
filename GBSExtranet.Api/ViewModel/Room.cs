//Balstechnology-AJ
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class Room
    {
        public int RoomID { get; set; }

        public int RoomTypeID { get; set; }

        public string RoomTypeName { get; set; }

        public string SomkingTypeName { get; set; }

        public string RoomSize { get; set; }

        public string RoomCount { get; set; }

        public string Image { get; set; }
    }
}