using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyPhotos
    {
        public int RoomID { get; set; }
        public int ID { get; set; }
        public int PartID { get; set; }
        public int RecordID { get; set; }
        public int? Sort { get; set; }
        public bool MarkAsDeleted { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public string Name { get; set; }
        public bool MainPhoto { get; set; }
        public string Path { get; set; }
        public List<PropertyPhotos> AllPhotos { get; set; }
    }
}