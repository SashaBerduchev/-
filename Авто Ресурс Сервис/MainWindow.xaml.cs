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
        private List<News> newsfromprod;
        private List<Brends> brendfromprod;
        private List<TiresImage> tiresImagesprod;
        public MainWindow()
        {
            InitializeComponent();
            Config.DEBUG_MODE = "false";
            Post post = new Post(this);
            GetNews();
            TypeMode.Items.Add("false");
            TypeMode.Items.Add("true");
            Trace.WriteLine(this);
        }

        public void UpdateMode(string type)
        {
            TypeModeSelect.Text = type;
        }

        public async Task GetNews()
        {
            try
            {
                Task data = await Task.WhenAny(Post.Send("News", "GetNews"));
                if (data is Task<string>)
                {
                    List<News> news = JsonConvert.DeserializeObject<List<News>>((data as Task<string>).Result);
                    string newdata = JsonConvert.SerializeObject(news);
                    newsfromprod = JsonConvert.DeserializeObject<List<News>>(newdata);
                    data.Dispose();
                    data = null;
                    for (int i = 0; i < news.Count; i++)
                    {
                        if (news[i].BaseInfo.Length > 20)
                        {
                            news[i].BaseInfo = news[i].BaseInfo.Substring(0, 20) + "...";
                        }
                        if (news[i].AllInfo.Length > 30)
                        {
                            news[i].AllInfo = news[i].AllInfo.Substring(0, 30) + "...";
                        }
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
                var data = await Task.WhenAny(Post.Send("News", "GetNews"), Task.Delay(5000));
                if (data is Task<string>)
                {
                    List<News> news = JsonConvert.DeserializeObject<List<News>>((data as Task<string>).Result);
                    for (int i = 0; i < news.Count; i++)
                    {
                        if (news[i].BaseInfo.Length > 20)
                        {
                            news[i].BaseInfo = news[i].BaseInfo.Substring(0, 20) + "...";
                        }
                        if (news[i].AllInfo.Length > 40)
                        {
                            news[i].AllInfo = news[i].AllInfo.Substring(0, 40) + "...";
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
                    BrendName.ItemsSource = tires.Select(x => x.Name).ToList();
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
                var tires = await Task.WhenAny(Post.Send("TiresImages", "GetTiresPict"), Task.Delay(5000));
                if (tires is Task<string>)
                {
                    List<TiresImage> tiresImages = JsonConvert.DeserializeObject<List<TiresImage>>((tires as Task<string>).Result);
                    TiresPictGrid.ItemsSource = tiresImages;
                    tiresImagesprod = tiresImages;
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
                    for (int i = 0; i < brends.Count; i++)
                    {
                        if (brends[i].Information.Length > 200)
                        {
                            brends[i].Information = brends[i].Information.Substring(0, 200) + "...";
                        }

                    }
                    BrendstGrid.ItemsSource = brends;
                    brendfromprod = brends;
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

        private void BrendName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetTiresFilterBrendClient();
        }

        private async Task GetTiresFilterBrendClient()
        {
            try
            {
                var tires = await Task.WhenAny(Post.Send("Tires", "GetBrendClientTires", BrendName.SelectedItem.ToString())); ;
                if (tires is Task<string>)
                {
                    List<Tires> tire = JsonConvert.DeserializeObject<List<Tires>>((tires as Task<string>).Result);
                    TiresGrid.ItemsSource = tire;
                    TiresCount.Text = tire.Count.ToString();
                    //BrendName.ItemsSource = tire.Select(x => x.Name).ToList();
                }

            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp);
                MessageBox.Show("Не вдається під'єднатися до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdaeTireInfo_Click(object sender, RoutedEventArgs e)
        {
            GetTiresInfo();
        }

        public async Task GetTiresInfo()
        {
            try
            {
                var tires = await Task.WhenAny(Post.Send("TiresInformations", "GetTiresInfo"));
                if (tires is Task<string>)
                {
                    List<TiresInformation> tire = JsonConvert.DeserializeObject<List<TiresInformation>>((tires as Task<string>).Result);
                    for (int i = 0; i < tire.Count; i++)
                    {
                        if (tire[i].Info.Length > 50)
                        {
                            tire[i].Info = tire[i].Info.Substring(0, 50)+"...";
                        }
                    }
                    TiresInfo.ItemsSource = tire;
                    //TiresCount.Text = tire.Count.ToString();
                    //BrendName.ItemsSource = tire.Select(x => x.Name).ToList();
                    tires.Dispose();
                }

            }
            catch (Exception exp)
            {
                Trace.WriteLine(exp);
                MessageBox.Show("Не вдається під'єднатися до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TiresInformation_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetTiresInfo();
        }

        private void AddTiresWindows_Click(object sender, RoutedEventArgs e)
        {
            TiresInfoWindowOpen();
        }

        private async Task TiresInfoWindowOpen()
        {
            try
            {
                var news = await Task.WhenAny(Post.Send("Tires", "GetTiresClient"));
                List<Tires> item = JsonConvert.DeserializeObject<List<Tires>>((news as Task<string>).Result);
                new AddTireInfoWindow(this, item).Show();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка подключения к серверу", "Error", MessageBoxButton.OK);
            }
        }

        private void TiresInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GetEditTireInfo();
        }

        private async Task GetEditTireInfo()
        {
            try
            {
                Task data = await Task.WhenAny(Post.Send("TiresInformations", "GetTireInfoSelect", JsonConvert.SerializeObject(TiresInfo.SelectedItem as TiresInformation)));
                if(data is Task<string>)
                {
                    TiresInformation tiresInformation = JsonConvert.DeserializeObject<TiresInformation>((data as Task<string>).Result);
                    new TiresInfoEditWindow(this, tiresInformation).Show();
                }
                
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка подключения к серверу", "Error", MessageBoxButton.OK);
                MessageBox.Show(exp.ToString(), "Error", MessageBoxButton.OK);
            }
        }

        private void TypeMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Config.DEBUG_MODE = TypeMode.SelectedItem.ToString();
            Post post = new Post(this);
        }

        private void CollectionSave_Click(object sender, RoutedEventArgs e)
        {
            SaveNewsCall();
            
        }

        private async Task SaveNewsCall()
        {
            new NewsReLoadWindow(this, newsfromprod).Show();
            
        }

        private void TestSave_Click(object sender, RoutedEventArgs e)
        {
            new BrendReLoadWindow(brendfromprod).Show();
        }

        private void ImagesTireSaveLocal_Click(object sender, RoutedEventArgs e)
        {
            new TiresImagesReLoadWindow(tiresImagesprod).Show();
        }

        private void SaveNesInfo_Click(object sender, RoutedEventArgs e)
        {
            new SaveNesInfoWindow().Show();
        }

        private void NewsGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                OpenNewsEditWindows();
            }
        }
        private async Task OpenNewsEditWindows()
        {
            try
            {
                var item = NewsGrid.SelectedItems;
                for (int i = 0; i < item.Count; i++)
                {
                    var news = await Task.WhenAny(Post.Send("News", "GetNewsSelect", (item[i] as News).NameNews), Task.Delay(500));
                    News newsselect = JsonConvert.DeserializeObject<News>((news as Task<string>).Result);
                    new NewsEditWindow(this, newsselect).Show();
                }
                
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка подключения к серверу", "Error", MessageBoxButton.OK);
            }

        }
    }
}
