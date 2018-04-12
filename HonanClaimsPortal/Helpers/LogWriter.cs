using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace HonanClaimsPortal.Helpers
{
    public class LogWriter
    {
        public static void WriteLog(string strLog,string userName = "",string claimNumber = "")
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;


            string logFilePath = ConfigurationManager.AppSettings["EmailLogpath"] + claimNumber + " " + userName + " - LogFile.txt";

            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            Console.WriteLine(strLog);
            log.WriteLine(strLog);
            log.Close();
        }

        public static void WriteLogMethodDetails(string methodName, Dictionary<string, string> parameters)
        {
            WriteLog(Environment.NewLine);
            WriteLog(" * " + DateTime.Now.ToString() + " " + methodName + " Initiated " + " with params ");
            foreach (var item in parameters)
            {
                WriteLog(item.Key + " : " + item.Value);
            }
        }
    }
}