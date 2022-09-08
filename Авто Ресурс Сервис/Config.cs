using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Авто_Ресурс_Сервис
{
    class Config
    {
        public static string FALSE = false.ToString();
        public static string TRUE = true.ToString();
        public static string connstring;
        public static string DEBUG_MODE = false.ToString();
        public static string GetString()
        {
            if (DEBUG_MODE == true.ToString())
            {
                connstring = "https://localhost:44314/";
            }
            else
            {
                connstring = "http://arsshina.com/";
            }
            return connstring;
        }
    }
}
