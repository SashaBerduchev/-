using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace Авто_Ресурс_Сервис.Http
{
    class Post
    {
        private static string constring;
        static readonly HttpClient client = new HttpClient();
        Window Window;
        public Post(Window window)
        {
            window = window;
            constring = Config.GetString();
            //MessageBox.Show(constring, "Info", MessageBoxButton.OK);
        }

        public static async Task<string> Send(string ctr, string act, string data = null)
        {
            try
            {
                string constr = constring + ctr + '/' + act;
                Trace.WriteLine("POST " + constr);
                string filedata = "PostLog.log";
                FileStream fileStream = new FileStream(filedata, FileMode.Append);
                byte[] array = Encoding.Default.GetBytes(constr + "             ");
                fileStream.Write(array, 0, constr.Length);
                fileStream.Close();
                if (data != null)
                {
                    var poststr = data.Trim('/');
                    StringContent stringContent = new StringContent(poststr);
                    HttpResponseMessage request = await client.PostAsync(constr, new StringContent(new JavaScriptSerializer().Serialize(data), Encoding.UTF8, "application/json"));
                    var ddd = await request.Content.ReadAsStringAsync();
                    Trace.WriteLine(request);
                    Trace.WriteLine(ddd);
                    return ddd;
                }
                else
                {
                    HttpResponseMessage response = await client.GetAsync(constr);
                    response.EnsureSuccessStatusCode();
                    Trace.WriteLine("POST  " + response);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                return "";

            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return "Не удается подключится к серверу";
            }
        }
    }
}
