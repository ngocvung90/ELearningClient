using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ElearningClient.Interface;
using System.IO;
using ElearningClient.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(aTextService))]

namespace ElearningClient.Droid
{
    public class aTextService : ITextService
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