using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace PL_PresentationLayerMVC.Helpers
{
	public static class DocumentSettings
	{
		public static string UploadFile(IFormFile file, string folderName)
		{ 
			if (file == null) 
				return null;
			string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			string fileName = Guid.NewGuid().ToString() + file.FileName;
			string filePath = Path.Combine(path, fileName);
			using var fileStream = new FileStream(filePath, FileMode.Create);
			file.CopyTo(fileStream);
			return fileName;
		}

		public static void DeleteFile(string fileName, string folderName)
		{
			if (String.IsNullOrEmpty(fileName)) 
				return;
			string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName, fileName);
			if (File.Exists(path))
				File.Delete(path);
		}
	}
}
