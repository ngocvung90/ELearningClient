using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using ElearningClient.Interface;
using System.IO;
using Xamarin.Forms;
using ElearningClient.iOS;

[assembly: Dependency(typeof(iosTextService))]
namespace ElearningClient.iOS
{
    public class iosTextService : ITextService
    {
        public string[] GetListFiles(string folderPath)
        {
            return Directory.GetFiles(folderPath);
        }

        public bool IsFileExist(string filename)
        {
            return File.Exists(filename);
        }

        public READ_TEXT_ERRORCODE LoadText(string fileName, out string result)
        {
            result = "";
            try
            {
                if (IsFileExist(fileName))
                {
                    result = File.ReadAllText(fileName);
                    return READ_TEXT_ERRORCODE.SUCCESS;
                }
                else
                    return READ_TEXT_ERRORCODE.FILE_NOT_FOUND;
            }
            catch (Exception ex)
            {
                return READ_TEXT_ERRORCODE.UNKNOWN;
            }
        }
    }
}