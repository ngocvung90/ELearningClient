using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElearningClient.Interface
{
    public enum READ_TEXT_ERRORCODE
    {
        SUCCESS = 0,
        FILE_NOT_FOUND ,
        CAN_NOT_ACCESS,
        UNKNOWN
    }
    public interface ITextService
    {
        READ_TEXT_ERRORCODE LoadText(string fileName, out string result);
        bool IsFileExist(string filename);
    }
}
