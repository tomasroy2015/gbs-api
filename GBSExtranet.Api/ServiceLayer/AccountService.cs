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
namespace GBSExtranet.Api.ServiceLayer
{
    public class AccountService : BaseService
    {
        public User GetLoginUser(string username, string password, string culture, ref string userIpAddress)
        {
            User _user = null;
            BizContext BizContext = new BizContext();
            UserContext userContext = new UserContext();
            try
            {
                var encryptedPass = new BizCrypto.AES128().Encrypt(password);
                //var user = (from u in _db.BizTbl_User
                //             where u.UserName.ToLower().Trim() == username.ToLower().Trim() && u.Password.Trim() == encryptedPass.Trim()
                //             select u).FirstOrDefault();
                Business.BizTbl_User user = BizUser.GetUser(_context, string.Empty, username, password);
                if(user != null) 
                {
                    BizApplication.SetUserContext(_context, ref userContext, Convert.ToInt64(user.ID), culture);
                    BizContext.UserContext = userContext;

                    if(userContext.IsHotelAdmin())
                    {
                        int i = 0;
                        _sqlConnection.Open();
                        DataTable dt = new DataTable();
                        SqlCommand cmd = new SqlCommand("B_Ex_GetUserHotelByUserID_TB_Hotel_SP", _sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", user.ID.ToString());
                        cmd.Parameters.AddWithValue("@FirmID", userContext.FirmID);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        _sqlConnection.Close();
                        foreach (DataRow hotel in dt.Rows)
                        {
                            BizContext.Hotels.Add(Convert.ToInt32(hotel["ID"]), (hotel["Name"].ToString()));
                            if (i == 0)
                            {
                                BizContext.HotelID = Convert.ToInt32(hotel["ID"]);
                                BizContext.FirmID = hotel["FirmID"].ToString();
                                BizContext.CurrencyID = hotel["CurrencyID"].ToString();
                                BizContext.HotelAccommodationTypeID = Convert.ToInt32(hotel["HotelAccommodationTypeID"]);
                                BizContext.HotelRoutingName = hotel["Name"].ToString();                               
                            }
                            i++;
                        }
                    }
                    _user = new User();
                    _user.ID = user.ID;
                    _user.Active = user.Active;
                    _user.Address = user.Address;
                    _user.City = user.City;
                    _user.CityID = user.CityID;
                    if (userContext.IsHotelAdmin())
                    {
                        _user.CurrencyID = Convert.ToInt64(BizContext.CurrencyID.Trim());
                        _user.CurrencyCode = new DropdownService().GetCurrencies(culture).Find(f => f.ID == _user.CurrencyID).Code;
                        _user.HotelAccommodationTypeID = BizContext.HotelAccommodationTypeID;
                    }
                    _user.CountryID = user.CountryID;
                    _user.Email = user.Email;
                    _user.FirmID = user.FirmID;
                    _user.Locked = user.Locked;
                    _user.UserInfo = userContext;
                    _user.HotelID = Convert.ToInt64(BizContext.HotelID);
                    int userCountryID = 0;
                    try
                    {
                        Business.TB_Country userCountryInfo = BizApplication.GetCountryInfoFromIPAddress(_context, userIpAddress);
                        userCountryID = userCountryInfo.ID;
                    }
                    catch
                    {
                        userCountryID = 0;
                    }
                    string countryID = (userCountryID == 0 ? String.Empty : userCountryID.ToString());
                    string UserSessionID = BizUser.SaveUserSession(_context, String.Empty, Guid.NewGuid().ToString(), user.ID.ToString(), countryID, userIpAddress, DateTime.Now.ToString()).ToString();
                    BizUser.AddUserOperation(_context, user.ID.ToString(), DateTime.Now.ToString(), BizCommon.Operation.Login, "", "", userIpAddress, UserSessionID);
                    _user.SessionID = UserSessionID;
                    _user.IsAdmin = userContext.IsAdmin();
                    _user.IsHotelAdmin = userContext.IsHotelAdmin();
                }
                
            }
           
            catch (SqlException ex)
            {
                throw new FaultException(ErrorMessage.FAULT_DATABASE_NOT_REACHABLE);
            }
            catch (InvalidOperationException ex)
            {
                throw new FaultException(ErrorMessage.FAULT_DATABASE_OPERATION_FAILD);
            }
            finally
            {
                Dispose();
            }
            return _user;
        }
        public User ResetPassword(ResetPassword model)
        {
            List<User> users;
            User user;
            try
            {
                users = GetUserData(model.Email);
                user = users.Find(f => f.UserName == model.Email); 
                if (user == null)
                    throw new Exception(ErrorMessage.USER_NOT_FOUND);
               
                EmailService emailService = new EmailService();
                emailService.SendResetPasswordEmail(user);

            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            return user; 
        }
        public bool UpdatePassword(ResetPassword model)
        {
            try
            {
                var encryptedPass = new BizCrypto.AES128().Encrypt(model.Password);
                BizUser.UnlockUser(_context, model.UserID.ToString(), string.Empty);
                BizUser.UpdateUserPassword(_context, model.UserID.ToString(), encryptedPass);
            }
            catch (SqlException ex)
            {
                throw new FaultException(ErrorMessage.FAULT_DATABASE_NOT_REACHABLE);
            }
            catch (InvalidOperationException ex)
            {
                throw new FaultException(ErrorMessage.FAULT_DATABASE_OPERATION_FAILD);
            }
            return true;
        }
        public List<User> GetUserData(string username)
        {
            string FirmID = "";
            List<User> list = new List<User>();
            DataTable dt = new DataTable();
            try
            {
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("B_Ex_GetUserInfoByEmail_Sp", _sqlConnection);
                cmd.Parameters.AddWithValue("@Email", username);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                _sqlConnection.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        User user = new User();
                        user.ID = Convert.ToInt32(dr["ID"]);
                        // user.FirmID = dr["FirmID"] == null   ? 0 : Convert.ToInt32(dr["FirmID"]);
                        user.Name = dr["Name"] == null ? string.Empty : dr["Name"].ToString();
                        user.Surname = dr["Surname"] == null ? string.Empty : dr["Surname"].ToString();
                        user.UserName = dr["UserName"] == null ? string.Empty : dr["UserName"].ToString();
                        user.Email = username;
                        list.Add(user);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new FaultException(ErrorMessage.FAULT_DATABASE_NOT_REACHABLE);
            }
            catch (InvalidOperationException ex)
            {
                throw new FaultException(ErrorMessage.FAULT_DATABASE_OPERATION_FAILD);
            }
            return list;
        }
    }   
}