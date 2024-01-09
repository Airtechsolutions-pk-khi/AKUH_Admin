

using AKU_Admin._Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPICode.Helpers;

namespace BAL.Repositories
{

    public class doctorDB : baseDB
    {
        public static DoctorBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public doctorDB()
           : base()
        {
            repo = new DoctorBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public List<DoctorBLL> GetAll()
        {
            try
            {
                var lst = new List<DoctorBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_Doctor_admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<DoctorBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<SpecialistBLL> GetSpeciality()
        {
            try
            {
                var lst = new List<SpecialistBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetALLSpecialities_admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SpecialistBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<DaysBLL> GetDocDays()
        {
            try
            {
                var lst = new List<DaysBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetDocDays_admin");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<DaysBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<DoctorBLL> Get(int id)
        {
            try
            {
                List<DoctorBLL> Doctor = new List<DoctorBLL>();
                List<DoctorScheduleBLL> DocSchedule = new List<DoctorScheduleBLL>();
                List<DoctorTimingBLL> DocTiming = new List<DoctorTimingBLL>();
                List<DoctorProfilesBLL> DocProf = new List<DoctorProfilesBLL>();

                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@DoctorID", id);
                _ds = (new DBHelper().GetDatasetFromSP)("sp_GetDoctorbyID_Admin", p);
                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {

                        if (_ds.Tables[0] != null)
                        {
                            var _dsDoctor = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<DoctorBLL>>();
                            var _dsSchedule = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[1])).ToObject<List<DoctorScheduleBLL>>();
                            var _dsDoctorTimings = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[2])).ToObject<List<DoctorTimingBLL>>();
                            var _dsDoctorProfile = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[3])).ToObject<List<DoctorProfilesBLL>>();


                            foreach (var _i in _dsDoctor)
                            {
                                DocSchedule = new List<DoctorScheduleBLL>();
                                foreach (var _j in _dsSchedule.ToList())
                                {
                                    DocSchedule.Add(new DoctorScheduleBLL
                                    {
                                        DoctorID = _j.DoctorID,
                                        DayName = _j.DayName,
                                        Name = _j.Name,
                                        SpecialistID = _j.SpecialistID,
                                        TimeSlot = _j.TimeSlot
                                    });
                                }

                                DocTiming = new List<DoctorTimingBLL>();
                                foreach (var _j in _dsDoctorTimings.ToList())
                                {
                                    DocTiming.Add(new DoctorTimingBLL
                                    {
                                        DaysID = _j.DaysID,
                                        DoctorID = _j.DoctorID,
                                        DayName = _j.DayName,
                                        Name = _j.Name,
                                        SpecialistID = _j.SpecialistID,
                                        TimeSlot = _j.TimeSlots.Split(",")
                                    });

                                }

                                DocProf = new List<DoctorProfilesBLL>();
                                foreach (var _j in _dsDoctorProfile.ToList())
                                {
                                    DocProf.Add(new DoctorProfilesBLL
                                    {
                                        SpecialistID = _j.SpecialistID,
                                        Fees = _j.Fees,
                                        Profile = _j.Profile,
                                        Name = _j.Name,
                                        DoctorID = _j.DoctorID

                                    });
                                }

                                Doctor.Add(new DoctorBLL
                                {
                                    DoctorID = _i.DoctorID,
                                    FullName = _i.FullName,
                                    Email = _i.Email,
                                    Education = _i.Education,
                                    Skills = _i.Skills,
                                    Profile = _i.Profile,
                                    Gender = _i.Gender,
                                    StatusID = _i.StatusID,
                                    doctorSchedule = DocSchedule,
                                    DoctorTimings = DocTiming,
                                    DoctorProfiles = DocProf
                                });
                            }

                        }
                        //Subcategory.CategoryList = Category;
                    }
                }
                return Doctor;


                //SqlParameter[] p = new SqlParameter[2];
                //var _objsch = new DoctorScheduleBLL();
                //var _objtiming = new DoctorTimingBLL();
                //var _objprof = new DoctorProfilesBLL();
                //var ds = GetDoctorByID(id);

                //var _dsDoctor = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<DoctorBLL>>();
                //var _dsSchedule = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[1])).ToObject<List<DoctorScheduleBLL>>();
                //var _dsDoctorTimings = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[2])).ToObject<List<DoctorTimingBLL>>();
                //var _dsDoctorProfile = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[3])).ToObject<List<DoctorProfilesBLL>>();



            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetDoctorByID(int id)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@DoctorID", id);
                ds = (new DBHelper().GetDatasetFromSP)("sp_GetDoctorbyID_Admin", p);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(DoctorBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[13];

                p[0] = new SqlParameter("@FullName", data.FullName);
                p[1] = new SqlParameter("@UrduName", data.UrduName);
                p[2] = new SqlParameter("@ImagePath", data.ImagePath);
                p[3] = new SqlParameter("@Email", data.Email);
                p[4] = new SqlParameter("@Skills", data.Skills);
                p[5] = new SqlParameter("@Education", data.Education);
                p[6] = new SqlParameter("@Fees", data.Fees);
                p[7] = new SqlParameter("@Gender", data.Gender);
                p[8] = new SqlParameter("@StatusID", data.StatusID);
                p[9] = new SqlParameter("@CreatedBy", data.CreatedBy);
                p[10] = new SqlParameter("@CreatedOn", data.CreatedOn);
                p[11] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[12] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_insertDoctor_Admin", p).Rows[0]["DoctorID"].ToString());
                //DBHelper dBHelper = new DBHelper();
                //var idResult = await dBHelper.GetTableFromSPAsync("dbo.sp_insertDoctor_Admin", p);

                //rtn = int.Parse(idResult.Rows[0]["DoctorID"].ToString());

                if (data.doctorSchedule.Count > 0)
                {
                    foreach (var i in data.doctorSchedule)
                    {
                        SqlParameter[] p2 = new SqlParameter[3];
                        p2[0] = new SqlParameter("@DoctorID", rtn);
                        p2[1] = new SqlParameter("@SpecialistID", i.SpecialistID);
                        p2[2] = new SqlParameter("@Name", i.DayName);

                        int rtn2 = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_insertDocDays_Admin", p2).Rows[0]["DaysID"].ToString());
                        //var idaResult = await dBHelper.GetTableFromSPAsync("dbo.sp_insertDocDays_Admin", p2);
                        //int rtn2 = int.Parse(idResult.Rows[0]["DaysID"].ToString());

                        foreach (var j in i.TimeSlot)
                        {
                            SqlParameter[] p3 = new SqlParameter[4];
                            p3[0] = new SqlParameter("@DaysID", rtn2);
                            p3[1] = new SqlParameter("@DoctorID", rtn);
                            p3[2] = new SqlParameter("@SpecialistID", i.SpecialistID);
                            p3[3] = new SqlParameter("@TimeSlot", j);
                            new DBHelper().GetTableFromSP("dbo.sp_insertDocTiming_Admin", p3);
                            //await dBHelper.GetTableFromSPAsync("dbo.sp_insertDocTiming_Admin", p3);
                        }
					}
                    foreach (var item in data.DoctorProfiles)
                    {
                        SqlParameter[] p4 = new SqlParameter[5];
                        p4[0] = new SqlParameter("@DoctorID", rtn);
                        p4[1] = new SqlParameter("@SpecialityID", item.SpecialistID);
                        p4[2] = new SqlParameter("@Profile", item.Profile);
                        p4[3] = new SqlParameter("@Fees", item.Fees);
                        p4[4] = new SqlParameter("@StatusID", 1);
                        new DBHelper().GetTableFromSP("dbo.sp_insertSpecialitiesJunc_Admin", p4);
                        //await dBHelper.GetTableFromSPAsync("dbo.sp_insertSpecialitiesJunc_Admin", p4);
                    }
				}

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(DoctorBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[12];
                p[0] = new SqlParameter("@FullName", data.FullName);
                p[1] = new SqlParameter("@UrduName", data.UrduName);
                p[2] = new SqlParameter("@ImagePath", data.ImagePath);
                p[3] = new SqlParameter("@Email", data.Email);
                p[4] = new SqlParameter("@Skills", data.Skills);
                p[5] = new SqlParameter("@Education", data.Education);
                p[6] = new SqlParameter("@StatusID", data.StatusID);
                p[7] = new SqlParameter("@CreatedBy", data.CreatedBy);
                p[8] = new SqlParameter("@CreatedOn", data.CreatedOn);
                p[9] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[10] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[11] = new SqlParameter("@DoctorID", data.DoctorID);

                rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_updateDoctor_Admin", p).Rows[0]["DoctorID"].ToString());

                if (data.DoctorID != 0)
                {
                    SqlParameter[] p6 = new SqlParameter[1];
                    p6[0] = new SqlParameter("@DoctorID", data.DoctorID);
                    new DBHelper().GetTableFromSP("sp_DeleteSpecialitiesJunc", p6);
                }

                try
                {
                    if (data.DoctorID != 0)
                    {
                        SqlParameter[] p5 = new SqlParameter[1];
                        p5[0] = new SqlParameter("@DoctorID", data.DoctorID);
                        new DBHelper().GetTableFromSP("sp_DeleteDocTimings", p5);
                    }
                    if (data.DoctorID != 0)
                    {
                        SqlParameter[] p4 = new SqlParameter[1];
                        p4[0] = new SqlParameter("@DoctorID", data.DoctorID);
                        new DBHelper().GetTableFromSP("sp_DeleteDocDays", p4);
                    }


                    if (data.doctorSchedule.Count > 0)
                    {

                        foreach (var i in data.doctorSchedule)
                        {
                            SqlParameter[] p2 = new SqlParameter[3];
                            p2[0] = new SqlParameter("@DoctorID", rtn);
                            p2[1] = new SqlParameter("@SpecialistID", i.SpecialistID);
                            p2[2] = new SqlParameter("@Name", i.DayName);

                            int rtn2 = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_UpdateDocDays_Admin", p2).Rows[0]["DaysID"].ToString());

                            foreach (var j in i.TimeSlot)
                            {
                                SqlParameter[] p3 = new SqlParameter[4];
                                p3[0] = new SqlParameter("@DaysID", rtn2);
                                p3[1] = new SqlParameter("@DoctorID", rtn);
                                p3[2] = new SqlParameter("@SpecialistID", i.SpecialistID);
                                p3[3] = new SqlParameter("@TimeSlot", j);
                                new DBHelper().GetTableFromSP("dbo.sp_updateDocTiming_Admin", p3);
                            }
                        }
                    }

                    foreach (var item in data.DoctorProfiles)
                    {
                        SqlParameter[] p4 = new SqlParameter[5];
                        p4[0] = new SqlParameter("@DoctorID", rtn);
                        p4[1] = new SqlParameter("@SpecialityID", item.SpecialistID);
                        p4[2] = new SqlParameter("@Profile", item.Profile);
                        p4[3] = new SqlParameter("@Fees", item.Fees);
                        p4[4] = new SqlParameter("@StatusID", 1);
                        new DBHelper().GetTableFromSP("dbo.sp_updateSpecialitiesJunc_Admin", p4);
                    }
                }
                catch { }
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        //public int Update(DoctorBLL data)
        //{
        //    try
        //    {
        //        int rtn = 0;
        //        SqlParameter[] p = new SqlParameter[11];
        //        p[0] = new SqlParameter("@FullName", data.FullName);
        //        p[1] = new SqlParameter("@ImagePath", data.ImagePath);
        //        p[2] = new SqlParameter("@Email", data.Email);
        //        p[3] = new SqlParameter("@Skills", data.Skills);
        //        p[4] = new SqlParameter("@Education", data.Education);
        //        p[5] = new SqlParameter("@StatusID", data.StatusID);
        //        p[6] = new SqlParameter("@CreatedBy", data.CreatedBy);
        //        p[7] = new SqlParameter("@CreatedOn", data.CreatedOn);
        //        p[8] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
        //        p[9] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
        //        p[10] = new SqlParameter("@DoctorID", data.DoctorID);

        //        rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_updateDoctor_Admin", p).Rows[0]["DoctorID"].ToString());

        //        if (data.doctorSchedule.Count > 0)
        //        {
        //            foreach (var i in data.doctorSchedule)
        //            {
        //                SqlParameter[] p2 = new SqlParameter[3];
        //                p2[0] = new SqlParameter("@DoctorID", rtn);
        //                p2[1] = new SqlParameter("@SpecialistID", i.SpecialistID);
        //                p2[2] = new SqlParameter("@Name", i.DayName);

        //                int rtn2 = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_UpdateDocDays_Admin", p2).Rows[0]["DaysID"].ToString());
        //                foreach (var j in i.TimeSlot)
        //                {
        //                    SqlParameter[] p3 = new SqlParameter[4];
        //                    p3[0] = new SqlParameter("@DaysID", rtn2);
        //                    p3[1] = new SqlParameter("@DoctorID", rtn);
        //                    p3[2] = new SqlParameter("@SpecialistID", i.SpecialistID);
        //                    p3[3] = new SqlParameter("@TimeSlot", j);
        //                    new DBHelper().GetTableFromSP("dbo.sp_updateDocTiming_Admin", p3);
        //                }
        //            }
        //            foreach (var item in data.DoctorProfiles)
        //            {
        //                SqlParameter[] p4 = new SqlParameter[5];
        //                p4[0] = new SqlParameter("@DoctorID", rtn);
        //                p4[1] = new SqlParameter("@SpecialityID", item.SpecialistID);
        //                p4[2] = new SqlParameter("@Profile", item.Profile);
        //                p4[3] = new SqlParameter("@Fees", item.Fees);
        //                p4[4] = new SqlParameter("@StatusID", 1);
        //                new DBHelper().GetTableFromSP("dbo.sp_updateSpecialitiesJunc_Admin", p4);
        //            }


        //        }
        //        return rtn;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}

        public int Delete(DoctorBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.DoctorID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteDoctor", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
