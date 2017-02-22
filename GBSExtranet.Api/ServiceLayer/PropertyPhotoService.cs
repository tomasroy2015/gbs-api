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
using System.Drawing.Imaging;

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
                    HotelObj.Sort = null;
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
        public bool SavePhotos(List<PropertyPhotos> photos, long userID)
        {
            foreach (var photo in photos)
            {
                if (photo.Sort!=null && photo.Sort > 0)
                    UpdateSort(photo.ID.ToString(), photo.Sort.ToString(), userID);
                if(photo.MarkAsDeleted)
                    DeletePhotos(photo.ID.ToString(), userID);
            }
            return true;
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
        public int UploadOperation(string Operation, int fileCount, string FileName, int PartID, int hotelID,int recordID, long userID) 
        {

            PropertyPhotoService modelRepo = new PropertyPhotoService();
            var  HotelPhotoPath = modelRepo.GetParameterValue("HotelPhotoPath");
            var UploadPath = modelRepo.GetParameterValue("HotelPhotoPath");
            var PhotoResizeWidt = modelRepo.GetParameterValue("PhotoResizeWidth");
            var PhotoResizeHeig = modelRepo.GetParameterValue("PhotoResizeHeight");
            string MaxPhotoCount = "";
            if (Operation == "Upload")
            {
                string NewFileName = "";
              //  string[] FileNames = FileName.Split(';');
               // int fileCount = Convert.ToInt16(FileNames.Length) - 1;
                int part = Convert.ToInt32(PartID);
                if (part == 1)
                {
                    MaxPhotoCount = modelRepo.GetParameterValue("MaxHotelPhotoCount");
                }
                else
                {
                    MaxPhotoCount = modelRepo.GetParameterValue("MaxRoomPhotoCount");
                }

                if (fileCount <= Convert.ToInt32(MaxPhotoCount))
                {
                    try
                    {
                        // TransactionScope ts = new TransactionScope();
                       // foreach (string Filename in FileNames)
                        //{
                        if (FileName.Trim() != "" && FileName != "undefined")
                            {
                                string[] FileNam = FileName.Split('.');

                                foreach (string Name in FileNam)
                                {
                                    if (Name != "jpg" && Name != "jpeg" && Name != "png")
                                    {
                                        NewFileName = Name + ".JPG";
                                    }
                                }
                                string Value = AddImage(part, NewFileName, recordID, userID);


                                string imagePath = HttpContext.Current.Server.MapPath("~/" + HotelPhotoPath + hotelID + "/" + NewFileName);
                                string adminImagePath = HttpContext.Current.Server.MapPath(HotelPhotoPath + hotelID + "/" + NewFileName);

                                //  UploadPath = UploadPath  + id + "/";

                                int PhotoResizeWidth = Convert.ToInt32(PhotoResizeWidt);

                                int PhotoResizeHeight = Convert.ToInt32(PhotoResizeHeig);
                                //   GetImageFile(Server.MapPath(UploadPath + Filename));
                                string subFolderParentName = "Hotel";
                                string subFolderName = Convert.ToString(hotelID);
                                UploadPath = "~/Upload/";
                                if (subFolderParentName != "")
                                {
                                    UploadPath = UploadPath + "/" + subFolderParentName + "/";
                                }
                                if (subFolderName != "")
                                {
                                    UploadPath = UploadPath + subFolderName + "/";
                                }
                                string Filepaths = HttpContext.Current.Server.MapPath(UploadPath + FileName);

                                System.Drawing.Image Resize = BizUtil.GetImageFile(HttpContext.Current.Server.MapPath(UploadPath + FileName));
                                System.Drawing.Size Size = new System.Drawing.Size(PhotoResizeWidth, PhotoResizeHeight);
                                System.Drawing.Image resizedImage = BizUtil.ResizeImage(BizUtil.GetImageFile(HttpContext.Current.Server.MapPath(UploadPath + FileName)), Size);
                                CreateDirectory(imagePath);
                                CreateDirectory(adminImagePath);
                                System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
                                System.Drawing.Imaging.EncoderParameters encoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
                                System.Drawing.Imaging.EncoderParameter myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(encoder, 90L);
                                encoderParameters.Param[0] = myEncoderParameter;
                                //System.Drawing.Imaging.ImageCodecInfo ImageCodecInfo = new System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders(1);                       
                                System.Drawing.Imaging.ImageCodecInfo ImageCodecInfo;
                                ImageCodecInfo = GetEncoderInfo("image/jpeg");
                                resizedImage.Save(imagePath, ImageCodecInfo, encoderParameters);
                                resizedImage.Save(adminImagePath, ImageCodecInfo, encoderParameters);
                            }


                        //}
                        // ts.Complete();
                    }
                    catch
                    {

                    }
                }

            }
            return hotelID;
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            string Encode;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    //  return encoders[j];
                    Encode = Convert.ToString(encoders[j]);

                return encoders[j];
            }
            return null;
        }
        public string CreateDirectory(string UploadPath)
        {
            string status = "Success";


            var fileInfo = new System.IO.FileInfo(UploadPath);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            return status;
        }
        public string GetImageFile(string Path)
        {

            var val = System.Drawing.Image.FromFile(Path);

            return Convert.ToString(val);
        }
    }
}