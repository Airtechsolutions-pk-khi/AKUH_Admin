using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin.BLL._Services
{
	public class baseService
	{


		public baseService()
		{
		}
		public DateTime _UTCDateTime_SA()
		{
			return DateTime.UtcNow.AddMinutes(300);
		}


		public string UploadImage(string image, string folderName, IWebHostEnvironment _env)
		{
			try
			{
				var chkImage = IsBase64Encoded(image
					.Replace("data:image/png;base64,", "")
					.Replace("data:image/jpg;base64,", "")
					.Replace("data:image/jpeg;base64,", ""));

				if (chkImage)
				{
					if (image != null && image != "")
					{
						image = uploadFiles(image, folderName, _env);
					}
				}
			}
			catch { }

			return image;
		}
		public string uploadFiles(string _bytes, string foldername, IWebHostEnvironment _webHostEnvironment)
		{
			try
			{
				if (_bytes != null && _bytes.ToString() != "")
				{

					byte[] bytes = Convert.FromBase64String(_bytes.Replace("data:image/png;base64,", "")
						.Replace("data:image/jpg;base64,", "")
						.Replace("data:image/jpeg;base64,", "")
						.Replace("data:image/svg+xml;base64,", ""));

					string webRootPath = _webHostEnvironment.WebRootPath;
					string contentRootPath = _webHostEnvironment.ContentRootPath;

					string path = "/ClientApp/dist/assets/Upload/" + foldername + "/" + Path.GetFileName(Guid.NewGuid() + ".jpg");
					string filePath = contentRootPath + path;

					System.IO.File.WriteAllBytes(filePath, bytes);

					_bytes = path.Replace("/ClientApp/dist/", "");

				}
				else
				{
					_bytes = "";
				}
			}
			catch (Exception ex)
			{
				_bytes = "";
			}
			return _bytes;
		}

		public bool IsBase64Encoded(String str)
		{
			try
			{
				// If no exception is caught, then it is possibly a base64 encoded string
				byte[] data = Convert.FromBase64String(str);
				// The part that checks if the string was properly padded to the
				// correct length was borrowed from d@anish's solution
				return (str.Replace(" ", "").Length % 4 == 0);
			}
			catch
			{
				// If exception is caught, then it is not a base64 encoded string
				return false;
			}
		}
		public string uploadpdf(string sourceFilePath, string folderName, IWebHostEnvironment _webHostEnvironment)
		{
			try
			{
				string webRootPath = _webHostEnvironment.WebRootPath;
				string contentRootPath = _webHostEnvironment.ContentRootPath;

				string uniqueFileName = Guid.NewGuid().ToString() + ".pdf";
				string destinationPath = $"/ClientApp/dist/assets/Upload/pdfFiles/{uniqueFileName}";
				string targetFilePath = Path.Combine(contentRootPath, "wwwroot", destinationPath);

				// Ensure the directory path exists
				string directoryPath = Path.GetDirectoryName(targetFilePath);
				if (!Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);
				}

				// Copy the file from the source to the destination
				File.Copy(sourceFilePath, targetFilePath);

				// Return the path of the saved file
				string savedFilePath = destinationPath;

				// Modify _bytes with the new path
				string modifiedBytes = savedFilePath.Replace("/ClientApp/dist/", "");

				return modifiedBytes;
			}
			catch (Exception ex)
			{
				// Handle exceptions as needed, e.g., log the error
				return string.Empty; // Return an appropriate value if the file upload fails
			}
		}


		public string UploadFile(string filepath, string folderName, IWebHostEnvironment _env)
		{
			try
			{
				if (!string.IsNullOrEmpty(filepath) && File.Exists(filepath))
				{
					// Call the uploadpdf method to save the PDF file
					string savedFilePath = uploadpdf(filepath, folderName, _env);
					return savedFilePath;
				}
			}
			catch (Exception ex)
			{
				// Handle exceptions as needed
			}

			return string.Empty; // Return an appropriate value if the file upload fails
		}


	}

}
