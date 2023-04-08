using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.BL.Helper
{
    public static class UploadingService
    {
        public static string UploadFile(string DirPath, IFormFile File)
        {
            try
            {
                var FilePath = Directory.GetCurrentDirectory() + DirPath;
                var FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);
                var FinalPath = Path.Combine(FilePath, FileName);

                using (var FileStream = new FileStream(FinalPath, FileMode.Create))
                {
                    File.CopyTo(FileStream);
                }

                return FileName;
            }
            catch (Exception ex)
            {
                return "Failed To Upload : " + ex.Message;
            }

        }

        public static string RemoveFile(string FolderName, string FileName)
        {
            try
            {
                var DeletedPath = Directory.GetCurrentDirectory() + FolderName + FileName;
                if (File.Exists(DeletedPath))
                {
                    File.Delete(DeletedPath);
                }

                var result = "Deleted Successfully";
                return result;
            }
            catch (Exception ex)
            {
                return "Failed To Upload : " + ex.Message;
            }


        }
    }
}
