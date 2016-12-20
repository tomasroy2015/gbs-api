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
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Collections;
using GBSExtranet.Api.Models;

namespace GBSExtranet.Api.ServiceLayer
{
    public class PropertyPhotoService:BaseService
    {
        public List<PropertyPhotos> GetHotelRooms(int HotelID, string CultureValue)
        {
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRooms]", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "RoomTypeName");
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            List<PropertyPhotos> ListOfModel = new List<PropertyPhotos>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyPhotos HotelObj = new PropertyPhotos();
                    HotelObj.RoomID = Convert.ToInt32(dr["ID"]);
                    HotelObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelObj.RoomTypeName = dr["RoomTypeName"].ToString();
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }
        public List<PropertyPhotos> LoadPhoto(int PartID, int HotelID)
        {
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetPhotos_TB_Photo_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PartID", PartID);
            cmd.Parameters.AddWithValue("@RecordID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            _sqlConnection.Close();

            List<PropertyPhotos> ListOfModel = new List<PropertyPhotos>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyPhotos HotelObj = new PropertyPhotos();
                    HotelObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelObj.PartID = Convert.ToInt32(dr["PartID"]);
                    HotelObj.RecordID = Convert.ToInt32(dr["RecordID"]);
                    HotelObj.Name = dr["Name"].ToString();
                    if (dr["MainPhoto"].ToString() != "")
                    {
                        HotelObj.MainPhoto = Convert.ToBoolean(dr["MainPhoto"]);
                    }
                    ListOfModel.Add(HotelObj);
                }
            }
            PropertyPhotos HotelObjAll = new PropertyPhotos();
            HotelObjAll.AllPhotos = ListOfModel;
            return ListOfModel;
        }

        public string GetParameterValue(string Parameter)
        {
            string Status = "";
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetParameter_BizTbl_Parameter_SP", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Code", Parameter);
            Status = cmd.ExecuteScalar().ToString();
            _sqlConnection.Close();
            return Status;
        }

        public int MainPhoto(string ID, string RecordID, string PartID,long userID)
        {
            GBSHotelsEntities obj = new GBSHotelsEntities();
            var OpUserIDParameter = new SqlParameter("@OpUserID", userID);
            var IDParameter = new SqlParameter("@ID", Convert.ToInt32(ID));
            var RecordIDParameter = new SqlParameter("@RecordID", Convert.ToInt64(RecordID));
            var PartIDParameter = new SqlParameter("@PartID", Convert.ToInt64(PartID));
            int i = obj.Database.ExecuteSqlCommand("B_Ex_SetAsMainPhoto_TB_Photo_SP @PartID,@RecordID,@ID,@OpUserID", PartIDParameter, RecordIDParameter, IDParameter, OpUserIDParameter);
            return i;
        }

        public string DeletePhotos(string PhotoID, long userID)
        {
            GBSHotelsEntities obj = new GBSHotelsEntities();
            var PhotoIDParameter = new SqlParameter("@PhotoID", Convert.ToInt32(PhotoID));
            var OpUserIDParameter = new SqlParameter("@OpUserID", Convert.ToInt64(userID));
            int i = obj.Database.ExecuteSqlCommand("B_Ex_DeletePhotos_TB_Photo_SP @PhotoID,@OpUserID", PhotoIDParameter, OpUserIDParameter);
            return Convert.ToString(i);
        }

        //public string UploadImage(string HotelID, string Hotelname, Controller Ctrl)
        //{
        //    DBEntities obj = new DBEntities();
        //    var IDParameter = new SqlParameter("@PhotoID", Convert.ToInt32(HotelID));
        //    var NameParameter = new SqlParameter("@PhotoID", Hotelname);
        //    var OpUserIDParameter = new SqlParameter("@OpUserID", Convert.ToInt64(Ctrl.Session["UserID"]));
        //    int i = obj.Database.ExecuteSqlCommand("B_Ex_DeletePhotos_TB_Photo_SP @PhotoID,@OpUserID", IDParameter,NameParameter,OpUserIDParameter);
        //    return Convert.ToString(i);
        //}
        public string UpdateSort(string PhID, string PhValue, long userID)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand("B_Ex_UpdateSort_TB_Photo_SP", _sqlConnection);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PhotoID", Convert.ToInt64(PhID));

            cmd.Parameters.AddWithValue("@Sort", Convert.ToInt32(PhValue));

            cmd.Parameters.AddWithValue("@OpUserID", Convert.ToInt64(userID));

            string var = Convert.ToString(cmd.ExecuteScalar());

            _sqlConnection.Close();

            return var;

        }
        public string AddImage(int part, string FName, int id, long userID)
        {
            string status = "Success";
            GBSHotelsEntities insertentity = new GBSHotelsEntities();
            GBSExtranet.Api.Models.TB_Photo Obj = new GBSExtranet.Api.Models.TB_Photo();
            Obj.PartID = part;
            Obj.RecordID = Convert.ToInt64(id);
            Obj.Name = FName;
            // Obj.MainPhoto = Convert.ToBoolean(0);
            Obj.Active = true;
            Obj.CreateDateTime = DateTime.Now;
            Obj.CreateUserID = Convert.ToInt64(userID);
            Obj.OpDateTime = DateTime.Now;
            Obj.OpUserID = Convert.ToInt64(userID);
            insertentity.TB_Photo.Add(Obj);
            insertentity.SaveChanges();
            int ID = Convert.ToInt32(Obj.ID);

            return status;
        }
    }
}