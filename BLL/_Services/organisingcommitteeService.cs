

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
    public class organisingcommitteeService : baseService
    {
        organisingcommitteeDB _service;
        public organisingcommitteeService()
        {
            _service = new organisingcommitteeDB();
        }


        public List<OrganisingCommitteeBLL> Get()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SpeakerBLL> GetDropdown()
        {
            try
            {
                return _service.GetDropdown();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public OrganisingCommitteeBLL Get(int id)
        {
            try
            {
                return _service.Get(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int Insert(OrganisingCommitteeBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "OrganisingCommittee", _env);
                data.Createdon = DateTime.UtcNow.AddMinutes(300);
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(OrganisingCommitteeBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "OrganisingCommittee", _env);
                data.Updatedon = DateTime.UtcNow.AddMinutes(300);
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(OrganisingCommitteeBLL data)
        {
            try
            {
                data.Updatedon = DateTime.UtcNow.AddMinutes(300);
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
