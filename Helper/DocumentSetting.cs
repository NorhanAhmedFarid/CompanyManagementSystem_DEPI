using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace CompanyG02.PL.Helper
{
    public class DocumentSetting
    {
        public static string UploadImage(IFormFile file,string FolderName)
        {
            //1.Get Located Folder Path
         //   string FolderPath = "C:\\Users\\Mohamed Hassan\\OneDrive\\......";
           // string FolderPath =Directory.GetCurrentDirectory()+"\\wwwroot\\files\\"+ FolderName;
            string FolderPath =Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);
            //Step2
            //Get File Name And Make it Uniqe
            string FileName =$"{Guid.NewGuid()}{file.FileName}";
            //3.Get FilePath
            string FilePath = Path.Combine(FolderPath, FileName);

            var fs=new FileStream(FilePath, FileMode.Create);

            file.CopyTo(fs);

            return FileName;
        }

        public static void DeleteFile(string FileName, string FolderName ) 
        { 
            if (FileName is not null && FolderName is not null)
            {

        string filepath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName, FileName);
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            }
        }
    }
}
