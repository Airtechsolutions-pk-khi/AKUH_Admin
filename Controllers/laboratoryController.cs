using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MohsinFoodAdmin._Models;


namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]
    public class laboratoryController : ControllerBase
    {
		private const string FolderName = "pdfFiles";
		private readonly IWebHostEnvironment _env;
        laboratoryService _service;
        public laboratoryController(IWebHostEnvironment env)
        {
            _service = new laboratoryService();
            _env = env;
        }
        [HttpGet("all/{fromDate}/{toDate}")]
        public List<LaboratoryBLL> GetAll(string fromDate, string toDate)
        {
            return _service.GetAll(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
		}
        [HttpGet("laboratory/{id}")]
        public LaboratoryBLL Get(int id)
        {
            return _service.Get(id);
        }
		[HttpGet("Detail/{registrationNo}")]
		public CustomerBLL GetDetail(string registrationNo)
		{
			return _service.GetDetail(registrationNo);
		}

		[HttpGet]
		[Route("loadpdf")]
       
        public ActionResult DownloadPdf(string path)
        {
            if (System.IO.File.Exists(path))
            {
                byte[] pdfBytes = System.IO.File.ReadAllBytes(path);
                return File(pdfBytes, "application/pdf");
            }
            else
            {
                // Handle the case where the file doesn't exist or there is an error
                return Content("File not found");
            }
        }

        [HttpPost]
        [Route("insert")]
        public async Task<int> Post(UploadViewModel Data)
        {
            if (Data.File != null)
            {
                var filePath = await CopyPdfToPath(Data.File, FolderName);                

               

                LaboratoryBLL data = new LaboratoryBLL();
                data.Name = Data.Name;
                data.RegistrationNo = Data.RegistrationNo;
                data.LabReferenceNo = Data.ReferenceNo == "undefined" ? "" : Data.ReferenceNo;
                data.FilePath = filePath;                
                data.DiagnoseCatID = Data.DiagnosticCatID;
                data.StatusID = 1;
				data.LastUpdatedBy = Data.UserName;
                data.CustomerID = Data.CustomerID;
                int res = _service.Insert(data);


                if (res != 0)
                {
                    _service.SendEmailtoCustLab(Data, _env);
                    //await PushAndriod.PushNotify("Success!", "Your report has been uploaded!");
                }
                return res;
            }
            return 0;
        }
        internal async Task<string> CopyPdfToPath(IFormFile file, string FolderName)
		{
            var fileName = Path.GetFileName(file.FileName);
            string ext = Path.GetExtension(file.FileName);

            if (!ext.ToLower().Equals(".pdf"))
            {
                return "";
            }

            string contentRootPath = _env.ContentRootPath;
            string path = "/ClientApp/dist/assets/Upload/" + FolderName + "/" + Path.GetFileName(Guid.NewGuid() + ".pdf");            
            string filePath = contentRootPath + path;
            
            try
            {                
                using FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);                
                await file.CopyToAsync(fileStream);
            }
            catch (Exception ex)
            {

                throw;
            }
            return path;
		}

		[HttpPost]
        [Route("update")]
        public async Task<int> PostUpdate(UploadViewModel Data)
        {
            var filePath = "";
            if (Data.File == null) {
                filePath = Data.Image;
            }
            else { 
             filePath = await CopyPdfToPath(Data.File, FolderName);
            }
            LaboratoryBLL data = new LaboratoryBLL();
			data.Name = Data.Name;
			data.RegistrationNo = Data.RegistrationNo;
			data.LabReferenceNo = Data.ReferenceNo;
			data.FilePath = filePath;
			data.DiagnoseCatID = Data.DiagnosticCatID;
			data.StatusID = 1;
			data.LastUpdatedBy = Data.UserName;
            data.LaboratoryID = Data.LaboratoryID;
            data.CustomerID = Data.CustomerID;
            
            return _service.Update(data);
        }
        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] LaboratoryBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
