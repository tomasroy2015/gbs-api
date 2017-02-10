using GBSExtranet.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ServiceLayer
{
    public class SystemSettingService : BaseService
    {
        public int Insertsystemsettings(int hotelID, bool SamedayLateBooking, int SamedayLateBookingCount, bool SamedayEarlyBooking,
            int SamedayEarlyBookingCount, bool NextDayBooking, int NextDayBookingCount, bool PriorityLateCheckOut,
            bool PriorityEarlyCheckIn, bool AirportShuttle, bool WelcomeDrink, bool FreeBikeRental, bool FreeBreakfast, bool FreeParking,
            bool FreeWiFi, bool RateMissing, bool AddressFromGuests, bool CCRChecking, bool WithoutPhoneNumber, bool GracePeriod,
            int GracePeriodHour, bool Arrivals, bool AuroReplenishment, bool SMS, int SMSHour, bool Gbshotels, bool GuestMessage)
        {
            int status = 0;
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_InsertSystemSettings_TB_HotelSettings_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@hotelID", hotelID);
            cmd.Parameters.AddWithValue("@SamedayLateBooking", SamedayLateBooking);
            cmd.Parameters.AddWithValue("@SamedayLateBookingCount", SamedayLateBookingCount);
            cmd.Parameters.AddWithValue("@SamedayEarlyBooking", SamedayEarlyBooking);
            cmd.Parameters.AddWithValue("@SamedayEarlyBookingCount", SamedayEarlyBookingCount);
            cmd.Parameters.AddWithValue("@NextDayBooking", NextDayBooking);
            cmd.Parameters.AddWithValue("@NextDayBookingCount", NextDayBookingCount);
            cmd.Parameters.AddWithValue("@PriorityLateCheckOut", PriorityLateCheckOut);
            cmd.Parameters.AddWithValue("@PriorityEarlyCheckIn", PriorityEarlyCheckIn);
            cmd.Parameters.AddWithValue("@AirportShuttle", AirportShuttle);
            cmd.Parameters.AddWithValue("@WelcomeDrink", WelcomeDrink);
            cmd.Parameters.AddWithValue("@FreeBikeRental", FreeBikeRental);
            cmd.Parameters.AddWithValue("@FreeBreakfast", FreeBreakfast);
            cmd.Parameters.AddWithValue("@FreeParking", FreeParking);
            cmd.Parameters.AddWithValue("@FreeWiFi", FreeWiFi);
            cmd.Parameters.AddWithValue("@RateMissing", RateMissing);
            cmd.Parameters.AddWithValue("@AddressFromGuests", AddressFromGuests);
            cmd.Parameters.AddWithValue("@CCRChecking", CCRChecking);
            cmd.Parameters.AddWithValue("@WithoutPhoneNumber", WithoutPhoneNumber);
            cmd.Parameters.AddWithValue("@GracePeriod", GracePeriod);
            cmd.Parameters.AddWithValue("@GracePeriodHour", GracePeriodHour);
            cmd.Parameters.AddWithValue("@Arrivals", Arrivals);
            cmd.Parameters.AddWithValue("@AuroReplenishment", AuroReplenishment);
            cmd.Parameters.AddWithValue("@SMS", SMS);
            cmd.Parameters.AddWithValue("@SMSHour", SMSHour);
            cmd.Parameters.AddWithValue("@Gbshotels", Gbshotels);
            cmd.Parameters.AddWithValue("@GuestMessage", GuestMessage);
           
            status = cmd.ExecuteNonQuery();
            _sqlConnection.Close();
            return status;
        }

        public List<SystemSettings> GetSystemSettings(string HotelID, string cultureCode)
        {
            List<SystemSettings> ListOfModel = new List<SystemSettings>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_GetSystemSettings_TB_HotelSettings_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SystemSettings Obj = new SystemSettings();
                    Obj.SamedayLateBooking = Convert.ToBoolean(dr["SamedayLateBooking"]);
                    Obj.SamedayLateBookingCount = Convert.ToInt32(dr["SamedayLateBookingCount"]);
                    Obj.SamedayEarlyBooking = Convert.ToBoolean(dr["SamedayEarlyBooking"]);
                    Obj.SamedayEarlyBookingCount = Convert.ToInt32(dr["SamedayEarlyBookingCount"]);
                    Obj.NextDayBooking = Convert.ToBoolean(dr["NextDayBooking"]);
                    Obj.NextDayBookingCount = Convert.ToInt32(dr["NextDayBookingCount"]);

                    Obj.PriorityLateCheckOut = Convert.ToBoolean(dr["PriorityLateCheckOut"]);
                    Obj.PriorityEarlyCheckIn = Convert.ToBoolean(dr["PriorityEarlyCheckIn"]);
                    Obj.AirportShuttle = Convert.ToBoolean(dr["AirportShuttle"]);
                    Obj.WelcomeDrink = Convert.ToBoolean(dr["WelcomeDrink"]);
                    Obj.FreeBikeRental = Convert.ToBoolean(dr["FreeBikeRental"]);
                    Obj.FreeBreakfast = Convert.ToBoolean(dr["FreeBreakfast"]);
                    Obj.FreeParking = Convert.ToBoolean(dr["FreeParking"]);
                    Obj.FreeWiFi = Convert.ToBoolean(dr["FreeWiFi"]);

                    Obj.RateMissing = Convert.ToBoolean(dr["RateMissing"]);
                    Obj.AddressFromGuests = Convert.ToBoolean(dr["AddressFromGuests"]);

                    Obj.CCRChecking = Convert.ToBoolean(dr["CCRChecking"]);
                    Obj.WithoutPhoneNumber = Convert.ToBoolean(dr["WithoutPhoneNumber"]);
                    Obj.GracePeriod = Convert.ToBoolean(dr["GracePeriod"]);
                    Obj.GracePeriodHour = Convert.ToInt32(dr["GracePeriodHour"]);

                    Obj.Arrivals = Convert.ToBoolean(dr["Arrivals"]);
                    Obj.AuroReplenishment = Convert.ToBoolean(dr["AuroReplenishment"]);
                    Obj.SMS = Convert.ToBoolean(dr["SMS"]);
                    Obj.SMSHour = Convert.ToInt32(dr["SMSHour"]);
                    Obj.Gbshotels = Convert.ToBoolean(dr["Gbshotels"]);
                    Obj.GuestMessage = Convert.ToBoolean(dr["GuestMessage"]);

                    ListOfModel.Add(Obj);
                }
            }
            return ListOfModel;
        }
    }
}