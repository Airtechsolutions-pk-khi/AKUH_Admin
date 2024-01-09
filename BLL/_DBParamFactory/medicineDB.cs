using AKU_Admin._Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;

namespace BAL.Repositories
{
    public class medicineDB : baseDB
    {
        public static MedicineBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public medicineDB() : base()
        {
            repo = new MedicineBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public List<MedicineBLL> GetAll()
        {
            try
            {
                var lst = new List<MedicineBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_getAllMedicine");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<MedicineBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public MedicineBLL Get(int id)
        {
            try
            {
                var _obj = new MedicineBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetMedicinebyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<MedicineBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(MedicineBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[12];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@ImagePath", data.Image);
                p[2] = new SqlParameter("@Description", data.Description);
                p[3] = new SqlParameter("@BrandDetails", data.BrandDetails);
                p[4] = new SqlParameter("@Price", data.Price);
                p[5] = new SqlParameter("@QuantityDescription", data.QuantityDescription);
                p[6] = new SqlParameter("@StatusID", data.StatusID);
                p[7] = new SqlParameter("@CreatedBy", data.CreatedBy);
                p[8] = new SqlParameter("@CreatedOn", data.CreatedOn);
                p[9] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[10] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[11] = new SqlParameter("@MedicineID", data.MedicineID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_insertMedicine_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Update(MedicineBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[10];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@ImagePath", data.Image);
                p[2] = new SqlParameter("@Description", data.Description);
                p[3] = new SqlParameter("@BrandDetails", data.BrandDetails);
                p[4] = new SqlParameter("@Price", data.Price);
                p[5] = new SqlParameter("@QuantityDescription", data.QuantityDescription);
                p[6] = new SqlParameter("@StatusID", data.StatusID);
                p[7] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[8] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[9] = new SqlParameter("@medicineID", data.MedicineID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateMedicine_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Delete(int MedicineID)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", MedicineID);
                

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteMedicine", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
