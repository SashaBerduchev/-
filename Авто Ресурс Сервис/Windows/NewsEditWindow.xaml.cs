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
        Window windows;
        News newsadd;
        public NewsEditWindow(Window window, News news)
        {
            InitializeComponent();
            windows = window;
            newsadd = news;
            try
            {
                this.NameNews.Text = news.NameNews;
                this.AllNews.Text = news.AllInfo;
                this.BaseNews.Text = news.BaseInfo;
                this.LinkOnNews.Text = news.NewsLinkSrc;
                //this.Id.Text = news.id.ToString();
                string filedata = "C:\\Users\\sasha\\OneDrive\\Изображения\\Saved Pictures\\img.jpg";
                //using (var stream = File.Create(filedata)) {
                //    File.WriteAllBytes(filedata, news.Image);
                //    stream.Close();
                //}
                FileStream fileStream = new FileStream(filedata, FileMode.OpenOrCreate);
                fileStream.Write(news.Image, 0, news.Image.Length);
                fileStream.Close();
                this.Img.Source = new BitmapImage(new Uri(filedata));
                filedata = null;
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Error", MessageBoxButton.OK);
            }
            Trace.WriteLine(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                News news = new News
                {
                    AllInfo = AllNews.Text,
                    BaseInfo = BaseNews.Text,
                    DateTime = DateTime.Now,
                    id = newsadd.id,
                    NameNews = NameNews.Text,
                    NewsLinkSrc = LinkOnNews.Text
                };
                string data = JsonConvert.SerializeObject(news);
                var datareq = Post.Send("News", "UpdateClient", data);
                this.Close();
                (windows as MainWindow).GetNews();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //this.Img.Source = null;
            //string filedata = "C:\\Users\\sasha\\OneDrive\\Изображения\\Saved Pictures\\img.jpg";
            //if (File.Exists(filedata))
            //{
            //    try
            //    {
            //        //using (var stream = File.Open(filedata, FileMode.Open))
            //        //{
            //        //    File.Delete(filedata);
            //        //    stream.Close();
            //        //}
            //        File.Delete(filedata);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK); 
                    
            //    }
            //}

        }
    }
}
