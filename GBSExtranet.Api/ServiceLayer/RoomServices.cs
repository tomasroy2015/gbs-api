//Balstechnology-AJ

using GBSExtranet.Api.Models;
using GBSExtranet.Api.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ServiceLayer
{
    public class RoomServices : BaseService
    {
        public List<Room> GetRoomDetails(int hotelID, string cultureCode)
        {
            List<Room> ListOfMode1 = new List<Room>();
            List<Room> ListOfMode2 = new List<Room>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
           
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelRoomsDisplay", _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", cultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "HotelRoomID");
            cmd.Parameters.AddWithValue("@HotelID", hotelID);
            cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            _sqlConnection.Close();

            if (ds != null)
            {
                dt = ds.Tables[0];
                dt1 = ds.Tables[1];
            }
            int dtcount = 0;

                ArrayList OptionNo = new ArrayList();
               
                for (int i = 1; i <= 3; i++)
                {
                    OptionNo.Add(i);
                }

                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        foreach (DataRow dr in dt1.Rows)
                        {
                            Room HotelObj1 = new Room();
                            string OptionNoValue = OptionNo[i].ToString();
                            HotelObj1.OptionNo = OptionNoValue;
                            HotelObj1.TableName = "Tabe2";
                            HotelObj1.BedId = dr["ID"].ToString();
                            HotelObj1.OptionNo = dr["OptionNo"].ToString();
                            HotelObj1.rommid = Convert.ToInt32(dr["HotelRoomID"]);
                            HotelObj1.HotelRoomID = dr["HotelRoomID"].ToString();
                            HotelObj1.BedTypeID = dr["BedTypeID"].ToString();
                            HotelObj1.BedTypeName = dr["BedTypeName"].ToString();
                            HotelObj1.Count = dr["Count"].ToString();
                            HotelObj1.BedTypeNameWithCount = dr["BedTypeNameWithCount"].ToString();
                            ListOfMode2.Add(HotelObj1);
                        }
                    }

                }
                string optionNo = "";
                string BedText = "";
                int countValue = 0;

                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow Roomdr in dt1.Rows)
                    {
                        //if (HotelObj.RoomID == HotelObj.rommid)
                        //{
                            if (optionNo != Roomdr["OptionNo"].ToString())
                            {
                                optionNo = Roomdr["OptionNo"].ToString();
                                countValue++;
                                if (BedText == "")
                                {
                                    BedText = Roomdr["BedTypeNameWithCount"].ToString() +" " + Roomdr["HotelRoomID"].ToString();
                                }
                                else
                                {
                                    BedText = BedText + "" + Roomdr["BedTypeNameWithCount"].ToString() +" " + Roomdr["HotelRoomID"].ToString();
                                }
                            }
                            else
                            {
                                BedText = BedText + "" + Roomdr["BedTypeNameWithCount"].ToString() +" " + Roomdr["HotelRoomID"].ToString();
                            }

                        //}
                    }
                }
                //  for (int i = 0; i < dtcount; i++)
                //{
                //    string roomid = dt.Rows[i][0].ToString();
                //    //string[] marks = new string[100];
                //    string CurrentUrl = BedText;
                //    string[] marks = CurrentUrl.Split(new[] { roomid }, StringSplitOptions.None);
                //    //marks = CurrentUrl.Split(roomid[i]);

                //    string option1 = marks[marks.Length - 4];
                //    string option2 = marks[marks.Length - 2];
                //    string option3 = marks[marks.Length - 3];
                //    string option = option1 + option2 + option3;
                //    Room HotelObj2 = new Room();
                //    HotelObj2.roomname = option;
                //    ListOfMode1.Add(HotelObj2);
                   
                //}
                  if (dt.Rows.Count > 0)
                  {
                      foreach (DataRow dr in dt.Rows)
                      {
                          Room HotelObj = new Room();
                          HotelObj.TableName = "Tabe1";
                          HotelObj.RoomID = Convert.ToInt32(dr["HotelRoomID"]);
                          string RoomID = dr["HotelRoomID"].ToString();
                          HotelObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                          HotelObj.RoomTypeName = dr["RoomTypeName"].ToString();
                          HotelObj.SomkingTypeName = dr["SmokingTypeName"].ToString();
                          HotelObj.RoomSize = dr["RoomSize"].ToString();
                          HotelObj.RoomCount = dr["RoomCount"].ToString();
                          HotelObj.Image = dr["Name"].ToString();
                          HotelObj.People = dr["MaxPeopleCount"].ToString();
                          HotelObj.Children = dr["MaxChildrenCount"].ToString();
                          string overalldetails = BedText;
                          string[] Roomdetname = overalldetails.Split(new[] { RoomID }, StringSplitOptions.None);
                          //marks = CurrentUrl.Split(roomid[i]);
                          //string option1 = Roomdetname[Roomdetname.Length - 4];
                          //string option2 = Roomdetname[Roomdetname.Length - 2];
                          //string option3 = Roomdetname[Roomdetname.Length - 3];
                          //string option = option1 + " or " + option2 + " or " + option3;
                          var count = 1;
                          string option="";

                          for (int i = Roomdetname.Length; i > 1; i--)
                          {
                              if (count == 1)
                              {
                                  option = Roomdetname[Roomdetname.Length - i];
                              }
                              else
                              {
                                  option += "or " + Roomdetname[Roomdetname.Length - i];

                              }
                           count = count + 1;
                          }
                          BedText = Roomdetname[Roomdetname.Length -1];
                          HotelObj.roomname = option;
                          ListOfMode1.Add(HotelObj);
                          dtcount = dtcount + 1;
                      }
                  }
                //else
                //{
                //   // HotelObj.RoomBedInfo = "";
                //}
                //if (countValue > 1)
                //{
                //    //objRoom.RoomBedInfo = BedText;
                //}
                //else
                //{
                //   // objRoom.RoomBedInfo = "";
                //}
                

             
            return ListOfMode1;
        }



        
    }
}