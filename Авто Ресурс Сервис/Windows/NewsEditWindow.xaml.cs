using System;
using System.Collections.Generic;
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
using Авто_Ресурс_Сервис.Parser;

namespace Авто_Ресурс_Сервис.Windows
{
    /// <summary>
    /// Логика взаимодействия для NewsEditWindow.xaml
    /// </summary>
    public partial class NewsEditWindow : Window
    {
        Window windows;
        News newsadd;
        public NewsEditWindow(Window window, News news)
        {
            InitializeComponent();
            windows = window;
            newsadd = news;
            NameNews.Text = news.NameNews;
            AllNews.Text = news.AllInfo;
            BaseNews.Text = news.BaseInfo;
            LinkOnNews.Text = news.NewsLinkSrc;
            string filedata = "img.jpg";
            File.WriteAllBytes(filedata, news.Image);
            Img.Source = new BitmapImage(new Uri(filedata)); 


        }
    }
}
