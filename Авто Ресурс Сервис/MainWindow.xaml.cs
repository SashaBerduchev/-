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
        private List<Tires> tireses;
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
                var data = await Task.WhenAny(Post.Send("News", "GetNews"), Task.Delay(5000));
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
                var data = await Task.WhenAny(Post.Send("News", "GetNews"), Task.Delay(5000));
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
            new NewsAddWindow(this).Show();
        }

        

        private async void Tires_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var data = await Task.WhenAny(Post.Send("News", "GetTiresClient"), Task.Delay(5000));
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
                var data = await Task.WhenAny(Post.Send("Tires", "GetTiresClient"), Task.Delay(5000));
                if (data is Task<string>)
                {
                    List<Tires> tires = JsonConvert.DeserializeObject<List<Tires>>((data as Task<string>).Result);
                    TiresGrid.ItemsSource = tires;
                    tireses = tires;
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
                var data = await Task.WhenAny(Post.Send("News", "GetNews"), Task.Delay(5000));
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
                var data = await Task.WhenAny(Post.Send("News", "GetNews"), Task.Delay(5000));
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

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Config.DEBUG_MODE = false;
            Post postprod = new Post();
            var data = await Task.WhenAny(Post.Send("News", "GetNews"), Task.Delay(5000));
            if(data != null )
            {
                Config.DEBUG_MODE = true;
                Post posttest = new Post();
                var res = Post.Send("Tires", "SaveAllClientTires", (data as Task<string>).Result);
                var resp = await Task.WhenAny(Post.Send("Tires", "GetTiresClient"), Task.Delay(5000));
                if (resp is Task<string>)
                {
                    List<Tires> tires = JsonConvert.DeserializeObject<List<Tires>>((resp as Task<string>).Result);
                    TiresGrid.ItemsSource = tires;
                    tireses = tires;
                }
            }
            
        }

        private async void NewsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var news = await Task.WhenAny(Post.Send("News", "GetNewsSelect", (NewsGrid.SelectedItem as News).NameNews), Task.Delay(5000)); 
            News item = JsonConvert.DeserializeObject<News>((news as Task<string>).Result);
            new NewsEditWindow(this, item).Show();
        }

        private async void TelegramGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var teledata = await Task.WhenAny(Post.Send("Home", "TelegramGetUser"), Task.Delay(1000));
            Trace.WriteLine(teledata);
        }

        private async void TelegramUserUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var teledata = await Task.WhenAny(Post.Send("Home", "TelegramGetUser"), Task.Delay(1000));
                TelegramGrid.ItemsSource = JsonConvert.DeserializeObject<List<TelegramBotUser>>((teledata as Task<string>).Result);
            }catch(Exception exp)
            {
                Trace.WriteLine(exp.ToString());
                MessageBox.Show("Ошибка подключения к серверу", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
