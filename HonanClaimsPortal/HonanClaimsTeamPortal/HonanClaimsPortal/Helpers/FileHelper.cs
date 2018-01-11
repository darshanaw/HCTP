using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace HonanClaimsPortal.Helpers
{
    public class FileHelper
    {
        public static void SaveFile(HttpPostedFileBase file, string AttachId)
        {

            if (file != null)
            {
                AttachId = AttachId.Replace("\"", "");
                string sPath = ConfigurationManager.AppSettings["FileUploadPath"];
                AttachId = Regex.Unescape(AttachId);
                string sFullfileName = sPath + "/" + "!" + AttachId + Path.GetFileName(file.FileName);

                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }

                // Create the path and file name to check for duplicates.
                string pathToCheck = sFullfileName;

                if (!System.IO.File.Exists(pathToCheck))
                {
                    file.SaveAs(sFullfileName);

                }
            }

        }
    }
}