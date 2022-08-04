using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Авто_Ресурс_Сервис.Http;
using Авто_Ресурс_Сервис.Parser;
using Авто_Ресурс_Сервис.Windows;

namespace Авто_Ресурс_Сервис
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Post post = new Post();
            GetNews();
        }

        public async void GetNews()
        {
            try
            {
                var data = await Task.WhenAny(Post.Send("News", "GetNews"), Task.Delay(10000));
                if (data is Task<string>)
                {
                    List<News> news = JsonConvert.DeserializeObject<List<News>>((data as Task<string>).Result);
                    for (int i = 0; i < news.Count; i++)
                    {
                        news[i].BaseInfo = news[i].BaseInfo.Substring(0, 20) + "...";
                        news[i].AllInfo = news[i].AllInfo.Substring(0, 50) + "...";
                        news[i].NewsLinkSrc = null;
                        news[i].ImageMimeTypeOfData = null;
                        news[i].Image = null;
                    }
                    NewsGrid.ItemsSource = news;
                }

            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp);
                MessageBox.Show("Не вдається під'єднатися до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = await Task.WhenAny(Post.Send("News", "GetNews"), Task.Delay(10000));
                if (data is Task<string>)
                {
                    List<News> news = JsonConvert.DeserializeObject<List<News>>((data as Task<string>).Result);
                    for (int i = 0; i < news.Count; i++)
                    {
                        news[i].BaseInfo = news[i].BaseInfo.Substring(0, 20) + "...";
                        news[i].AllInfo = news[i].AllInfo.Substring(0, 50) + "...";
                        news[i].NewsLinkSrc = null;
                        news[i].ImageMimeTypeOfData = null;
                        news[i].Image = null;
                    }
                    NewsGrid.ItemsSource = news;
                }

            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp);
                MessageBox.Show("Не вдається під'єднатися до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreatenewsBtn_Click(object sender, RoutedEventArgs e)
        {
            new NewsAddWindow().Show();
        }

        

        private async void Tires_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var data = await Task.WhenAny(Post.Send("News", "GetTiresClient"), Task.Delay(10000));
                if (data is Task<string>)
                {
                    List<Tires> news = JsonConvert.DeserializeObject<List<Tires>>((data as Task<string>).Result);
                    TiresGrid.ItemsSource = news;
                }

            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp);
                MessageBox.Show("Не вдається під'єднатися до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Tires_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var data = await Task.WhenAny(Post.Send("Tires", "GetTiresClient"), Task.Delay(10000));
                if (data is Task<string>)
                {
                    List<Tires> news = JsonConvert.DeserializeObject<List<Tires>>((data as Task<string>).Result);
                    TiresGrid.ItemsSource = news;
                }

            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp);
                MessageBox.Show("Не вдається під'єднатися до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void News_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var data = await Task.WhenAny(Post.Send("News", "GetNews"), Task.Delay(10000));
                if (data is Task<string>)
                {
                    List<News> news = JsonConvert.DeserializeObject<List<News>>((data as Task<string>).Result);
                    for (int i = 0; i < news.Count; i++)
                    {
                        news[i].BaseInfo = news[i].BaseInfo.Substring(0, 20) + "...";
                        news[i].AllInfo = news[i].AllInfo.Substring(0, 50) + "...";
                        news[i].NewsLinkSrc = null;
                        news[i].ImageMimeTypeOfData = null;
                        news[i].Image = null;
                    }
                    NewsGrid.ItemsSource = news;
                }

            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp);
                MessageBox.Show("Не вдається під'єднатися до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void TiresUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = await Task.WhenAny(Post.Send("News", "GetNews"), Task.Delay(10000));
                if (data is Task<string>)
                {
                    List<News> news = JsonConvert.DeserializeObject<List<News>>((data as Task<string>).Result);
                    for (int i = 0; i < news.Count; i++)
                    {
                        news[i].BaseInfo = news[i].BaseInfo.Substring(0, 20) + "...";
                        news[i].AllInfo = news[i].AllInfo.Substring(0, 50) + "...";
                        news[i].NewsLinkSrc = null;
                        news[i].ImageMimeTypeOfData = null;
                        news[i].Image = null;
                    }
                    NewsGrid.ItemsSource = news;
                }

            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp);
                MessageBox.Show("Не вдається під'єднатися до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
