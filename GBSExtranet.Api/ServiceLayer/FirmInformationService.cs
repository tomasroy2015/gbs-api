using GBSExtranet.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ServiceLayer
{
    public class FirmInformationService : BaseService
    {
        public List<FirmInformation> GetFirmInformation(string FirmID, string cultureCode)
        {
            List<FirmInformation> ListOfModel = new List<FirmInformation>();
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_GetFirmByFirmID_TB_Firm_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirmID", FirmID);
            cmd.Parameters.AddWithValue("@CultureCode", cultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    FirmInformation firmObj = new FirmInformation();
                    firmObj.Name = dr["Name"].ToString();
                    firmObj.Country = dr["Country"].ToString();
                    firmObj.City = dr["City"].ToString();
                    firmObj.Address = dr["Address"].ToString();
                    firmObj.PostCode = dr["PostCode"].ToString();
                    firmObj.Phone = dr["Phone"].ToString();

                    firmObj.Fax = dr["Fax"].ToString();
                    firmObj.Email = dr["Email"].ToString();
                    firmObj.TaxOffice = dr["TaxOffice"].ToString();
                    firmObj.TaxID = dr["TaxID"].ToString();
                    firmObj.ExecutiveName = dr["ExecutiveName"].ToString();
                    firmObj.ExecutiveSurname = dr["ExecutiveSurname"].ToString();

                    ListOfModel.Add(firmObj);
                }
            }
            return ListOfModel;
        }
    }
}