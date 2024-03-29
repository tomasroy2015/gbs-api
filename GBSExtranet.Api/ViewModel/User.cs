﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;
namespace GBSExtranet.Api.ViewModel
{
    public class User
    {
        public long ID { get; set; }
        public Nullable<int> SalutationTypeID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<long> RegionID { get; set; }
        public Nullable<long> CityID { get; set; }
        public long CurrencyID { get; set; }
        public string CurrencyCode { get; set; }   
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PostCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> FirmID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<bool> PromotionalEmail { get; set; }
        public string VerificationCode { get; set; }
        public string DisplayName { get; set; }
        public Nullable<bool> Genius { get; set; }
        public Nullable<bool> Locked { get; set; }
        public Nullable<bool> Active { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public long CreateUserID { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string IPAddress { get; set; }
        public string Userphoto { get; set; }
        public string Country { get; set; }
        public string GoogleProfileID { get; set; }
        public string FacebookProfileID { get; set; }
        public UserContext UserInfo { get; set; }
        public string SessionID { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsHotelAdmin { get; set; }
        public Int64 HotelID { get; set; }

        public int HotelAccommodationTypeID { get; set; }
    }
}