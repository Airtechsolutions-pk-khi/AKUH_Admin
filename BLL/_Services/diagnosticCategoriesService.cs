using BAL.Repositories;
using AKU_Admin._Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin.BLL._Services
{
    public class diagnosticCategoriesService : baseService
    {
        diagnosticCategoriesDB _service;
        public diagnosticCategoriesService()
        {
            _service = new diagnosticCategoriesDB();
        }
        public List<DiagnosticCategoriesBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<DiagnosticCategoriesBLL>();
            }
        }
        public DiagnosticCategoriesBLL Get(int id)
        {
            try
            {
                return _service.Get(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(DiagnosticCategoriesBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Diagnostic", _env);
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(DiagnosticCategoriesBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Diagnostic", _env);
                var result = _service.Update(data);
                return result;
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
                var result = _service.Delete(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
