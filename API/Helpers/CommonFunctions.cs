using System;
using System.Collections.Generic;
using System.IO;
using API.Dtos;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class CommonFunctions
    {
         public static bool CreateFolder(string argPath, out string errorMsg)
        {
            errorMsg = "";
            if (Directory.Exists(argPath))
                return true;
            try
            {
                Directory.CreateDirectory(argPath);
                return true;
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return false;
            }

        }

        public static bool checkFileExistAndMove(string filePathFromApi, out string destinationFilPath)
        {

            var valSourcePath = "";
            var valFilePath = "";
            var errorMsg = "";
            destinationFilPath = "";
            var tempFilePath = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();

            string filePath = config.GetSection("FTPAddress").Value;

            #region check file exist in location
            valFilePath = filePath + filePathFromApi;
            if (!File.Exists(valFilePath))
            {
                return false;
            }

            #endregion

            #region file move
      
            valSourcePath = filePathFromApi.Replace("temp", "images/products");

            if (!fileMove(filePath + filePathFromApi, filePath + valSourcePath, out errorMsg, out tempFilePath))
            {
                return false;
            }

            #endregion

            destinationFilPath = valSourcePath;
            delete_File(tempFilePath);
            return true;

        }

        public static bool fileMove(string valSourcePath, string valDestPath, out string errorMsg, out string tempFilePath)
        {
            errorMsg = "";
            tempFilePath = "";
            try
            {
                if (!CreateFolder(Path.GetDirectoryName(valDestPath), out errorMsg))
                {
                    
                    return false;
                }
              
                File.Copy(valSourcePath, valDestPath, true);
               tempFilePath = valSourcePath;
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        public static void delete_File(string sourceFilePath)
        {
            if (File.Exists(sourceFilePath))
            {
                File.Delete(sourceFilePath);
            }
            
        }
    }

    
}