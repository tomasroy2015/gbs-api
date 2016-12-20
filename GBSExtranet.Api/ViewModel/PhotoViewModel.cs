using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PhotoViewModel
    {
        public List<PropertyPhotos> HotelRooms { get; set; }
        public string Path { get; set; }
        public string MaxPhotoSize { get; set; }
        public string AllowedPhotoFileExtensions { get; set; }
        public string MaxHotelPhotoCount { get; set; }
    }
}