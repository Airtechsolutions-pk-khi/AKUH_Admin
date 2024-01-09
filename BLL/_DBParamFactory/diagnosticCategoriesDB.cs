

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

    public class diagnosticCategoriesDB : baseDB
    {
        public static DiagnosticCategoriesBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public diagnosticCategoriesDB()
           : base()
        {
            repo = new DiagnosticCategoriesBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public List<DiagnosticCategoriesBLL> GetAll()
        {
            try
            {
                var lst = new List<DiagnosticCategoriesBLL>();
                //SqlParameter[] p = new SqlParameter[1];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllDiagnosticCategories");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<DiagnosticCategoriesBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DiagnosticCategoriesBLL Get(int id)
        {
            try
            {
                var _obj = new DiagnosticCategoriesBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetDiagnosticCategorybyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<DiagnosticCategoriesBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(DiagnosticCategoriesBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[3];

                p[0] = new SqlParameter("@Name", data.CategoryName);
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_insertDiagnosticCategory_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(DiagnosticCategoriesBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[4];

                p[0] = new SqlParameter("@Name", data.CategoryName);
                p[1] = new SqlParameter("@Image", data.Image);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                p[3] = new SqlParameter("@DiagnosticCatID", data.DiagnosticCatID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_updateDiagnosticCategory_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(DiagnosticCategoriesBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.DiagnosticCatID);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteDiagnosticCategory_Admin", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
