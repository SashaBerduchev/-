using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для NewsReLoadWindow.xaml
    /// </summary>
    public partial class NewsReLoadWindow : Window
    {
        public List<News> newsese;
        Window windows;
        public NewsReLoadWindow(Window window, List<News> news)
        {
            InitializeComponent();
            TypeMode.Items.Add("false");
            TypeMode.Items.Add("true");
            newsese = news;
        }

        public void UpdateMode(string type)
        {
            TypeModeSelect.Text = type;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            if(Config.DEBUG_MODE == "true")
            for (int i = 0; i < newsese.Count; i++)
            {
                var datas = JsonConvert.SerializeObject(newsese[i]);
                Post.Send("News", "AddNews", datas);
            }
            
            this.Close();
            //(windows as MainWindow).GetNews();
        }

        private void TypeMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Config.DEBUG_MODE = TypeMode.SelectedItem.ToString();
            Post post = new Post(this);
        }
    }
}
