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
using System.Windows.Shapes;
using Авто_Ресурс_Сервис.Http;
using Авто_Ресурс_Сервис.Parser;

namespace Авто_Ресурс_Сервис.Windows
{
    /// <summary>
    /// Логика взаимодействия для NewsEditWindow.xaml
    /// </summary>
    public partial class NewsEditWindow : Window
    {
        Window window;
        News newsadd;
        public NewsEditWindow(Window window, News news)
        {
            InitializeComponent();
            window = window;
            newsadd = news;
            NameNews.Text = news.NameNews;
            AllNews.Text = news.AllInfo;
            BaseNews.Text = news.BaseInfo;
            LinkOnNews.Text = news.NewsLinkSrc;
            Id.Text = news.id.ToString();
            string filedata = "img.jpg";
            File.WriteAllBytes(filedata, news.Image);
            Img.Source = new BitmapImage(new Uri(filedata));
            Trace.WriteLine(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            News news = new News
            {
                AllInfo = AllNews.Text,
                BaseInfo = BaseNews.Text,
                DateTime = DateTime.Now,
                id = Convert.ToInt32(Id.Text),
                NameNews = NameNews.Text,
                NewsLinkSrc = LinkOnNews.Text
            };
            string data = JsonConvert.SerializeObject(news);
            var datareq = Post.Send("News", "UpdateClient", data);
            this.Close();
            (window as MainWindow).GetNews();
        }
    }
}
