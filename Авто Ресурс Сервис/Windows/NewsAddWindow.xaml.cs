using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Авто_Ресурс_Сервис.Http;
using Авто_Ресурс_Сервис.Parser;

namespace Авто_Ресурс_Сервис.Windows
{
    /// <summary>
    /// Логика взаимодействия для NewsAddWindow.xaml
    /// </summary>
    public partial class NewsAddWindow : Window
    {
        Window window; 
        public NewsAddWindow(Window window)
        {
            InitializeComponent();
            this.window = window;
            Trace.WriteLine(this);
        }

        private string fileType = "image/jpeg";
        private byte[] fileName;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            News news = new News();
            news.NameNews = NameNews.Text;
            news.BaseInfo = BaseNews.Text;
            news.AllInfo = AllNews.Text;
            news.DateTime = DateTime.Now;
            news.Image = fileName;
            news.ImageMimeTypeOfData = fileType;
            news.NewsLinkSrc = LinkOnNews.Text;
            var data = JsonConvert.SerializeObject(news);
            Post.Send("News", "AddNews", data);
            this.Close();
            (window as MainWindow).GetNews();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            string fileContent;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            var fileStream = fileDialog.OpenFile();

            //using (StreamReader reader = new StreamReader(fileStream))
            //{
            //    fileContent = reader.ReadToEnd();
            //    byte[] arr = Encoding.Unicode.GetBytes(fileContent);
            //    MemoryStream ms = new MemoryStream(arr);
            //    fileName = ms.ToArray();
            //}

            //fileType = Path.GetExtension(fileDialog.FileName);
            string filedata = fileDialog.FileName;
            Img.Source = new BitmapImage(new Uri(filedata));
            byte[] bData = File.ReadAllBytes(fileDialog.FileName);
            fileName = bData;
            Trace.WriteLine(fileDialog);
            
        }
    }
}
