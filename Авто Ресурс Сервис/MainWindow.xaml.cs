﻿using Newtonsoft.Json;
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
            Trace.WriteLine(this);
        }

        public async Task GetNews()
        {
            try
            {
                Task data = await Task.WhenAny(Post.Send("News", "GetNews"));
                if (data is Task<string>)
                {
                    List<News> news = JsonConvert.DeserializeObject<List<News>>((data as Task<string>).Result);
                    //Task.WaitAny(data);
                    data.Dispose();
                    data = null;
                    for (int i = 0; i < news.Count; i++)
                    {
                        if(news[i].BaseInfo.Length > 20)
                        {
                            news[i].BaseInfo = news[i].BaseInfo.Substring(0, 20) + "...";
                        }
                        if(news[i].AllInfo.Length > 30)
                        {
                            news[i].AllInfo = news[i].AllInfo.Substring(0, 30) + "...";
                        }
                        news[i].NewsLinkSrc = null;
                        news[i].ImageMimeTypeOfData = null;
                        news[i].Image = null;
                    }
                    NewsCount.Text = news.Count.ToString();
                    NewsGrid.ItemsSource = news;

                }

            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp);
                MessageBox.Show("Не вдається під'єднатися до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show(exp.ToString(), "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            GetBtnNews();
            
        }

        private async Task GetBtnNews()
        {
            try
            {
                var data = await Task.WhenAny(Post.Send("News", "GetNews"));
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
                    NewsCount.Text = news.Count.ToString();
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


        //private async void NewsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    var news = await Task.WhenAny(Post.Send("News", "GetNewsSelect", (NewsGrid.SelectedItem as News).NameNews), Task.Delay(500));
        //    News item = JsonConvert.DeserializeObject<News>((news as Task<string>).Result);
        //    new NewsEditWindow(this, item).Show();
        //}

        private void Tires_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GetTires();
            
        }

        private async Task GetTires()
        {
            try
            {
                var data = await Task.WhenAny(Post.Send("Tires", "GetTiresClient"));
                if (data is Task<string>)
                {
                    List<Tires> tires = JsonConvert.DeserializeObject<List<Tires>>((data as Task<string>).Result);
                    TiresGrid.ItemsSource = tires;
                    TiresCount.Text = tires.Count.ToString();
                }

            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp);
                MessageBox.Show("Не вдається під'єднатися до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Tires_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetTires();
        }

        private void News_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetBtnNews();
        }

        private void TiresUpdate_Click(object sender, RoutedEventArgs e)
        {
            GetTires();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            //Config.DEBUG_MODE = false;
            //Post postprod = new Post();
            //var data = await Task.WhenAny(Post.Send("News", "GetNews"), Task.Delay(500));
            //if (data != null)
            //{
            //    Config.DEBUG_MODE = true;
            //    Post posttest = new Post();
            //    var res = Post.Send("Tires", "SaveAllClientTires", (data as Task<string>).Result);
            //    var resp = await Task.WhenAny(Post.Send("Tires", "GetTiresClient"), Task.Delay(500));
            //    if (resp is Task<string>)
            //    {
            //        List<Tires> tires = JsonConvert.DeserializeObject<List<Tires>>((resp as Task<string>).Result);
            //        TiresGrid.ItemsSource = tires;
            //        TiresCount.Text = tires.Count.ToString();
            //        tireses = tires;
            //    }
            //}

        }


        private void TelegramGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetTgUser();
        }

        private async Task GetTgUser()
        {
            var teledata = await Task.WhenAny(Post.Send("Home", "TelegramGetUser"), Task.Delay(500));
            Trace.WriteLine(teledata);
        }

        private void TelegramUserUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateTgUser();
        }

        public async Task UpdateTgUser()
        {
            try
            {
                var teledata = await Task.WhenAny(Post.Send("Home", "TelegramGetUser"), Task.Delay(500));
                TelegramGrid.ItemsSource = JsonConvert.DeserializeObject<List<TelegramBotUser>>((teledata as Task<string>).Result);
            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp.ToString());
                MessageBox.Show("Ошибка подключения к серверу", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NewsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenNewsEditWindow();
            
        }

        private async Task OpenNewsEditWindow()
        {
            try
            {
                var news = await Task.WhenAny(Post.Send("News", "GetNewsSelect", (NewsGrid.SelectedItem as News).NameNews), Task.Delay(500));
                News item = JsonConvert.DeserializeObject<News>((news as Task<string>).Result);
                new NewsEditWindow(this, item).Show();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка подключения к серверу", "Error", MessageBoxButton.OK);
            }

        }

        private void AddPictWindow_Click(object sender, RoutedEventArgs e)
        {
            new AddTiresPicWindow(this).Show();
        }

        private void UpdatePictures_Click(object sender, RoutedEventArgs e)
        {
            GetTiresPic();
        }

        private void TiresPicture_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetTiresPic();
            
        }

        public async Task GetTiresPic()
        {
            try
            {
                var tires = await Task.WhenAny(Post.Send("TiresImages", "GetTiresPict"));
                if (tires is Task<string>)
                {
                    List<TiresImage> tiresImages = JsonConvert.DeserializeObject<List<TiresImage>>((tires as Task<string>).Result);
                    TiresPictGrid.ItemsSource = tiresImages;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка подключения к серверу", "Error", MessageBoxButton.OK);
            }
        }

        private void UpdateBrend_Click(object sender, RoutedEventArgs e)
        {
            GetBrend();
        }

        public async Task GetBrend()
        {
            try
            {
                var tires = await Task.WhenAny(Post.Send("Brends", "GetBrends"));
                if (tires is Task<string>)
                {
                    List<Brends> brends = JsonConvert.DeserializeObject<List<Brends>>((tires as Task<string>).Result);
                    for (int i = 0;  i < brends.Count; i++)
                    {
                        if (brends[i].Information.Length > 200)
                        {
                            brends[i].Information = brends[i].Information.Substring(0, 200) + "...";
                        }
                            
                    }
                    BrendstGrid.ItemsSource = brends;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка подключения к серверу", "Error", MessageBoxButton.OK);
                MessageBox.Show(exp.ToString(), "Error", MessageBoxButton.OK);
            }
        }

        private void BrendsTab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetBrend();
        }

        private void AddBrend_Click(object sender, RoutedEventArgs e)
        {
            new BrendsAddWindow(this).Show();
        }
    }
}
