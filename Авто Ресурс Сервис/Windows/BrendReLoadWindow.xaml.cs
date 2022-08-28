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
    /// Логика взаимодействия для BrendReLoadWindow.xaml
    /// </summary>
    public partial class BrendReLoadWindow : Window
    {
        List<Brends> brendssave;
        public BrendReLoadWindow(List<Brends> brends)
        {
            InitializeComponent();
            TypeMode.Items.Add("false");
            TypeMode.Items.Add("true");
            brendssave = brends;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(Config.DEBUG_MODE == "true")
            for (int i = 0; i < brendssave.Count; i++)
            {
                var datas = JsonConvert.SerializeObject(brendssave[i]);
                Post.Send("Brends", "SaveBrends", datas);
            }

            this.Close();
        }

        private void TypeMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Config.DEBUG_MODE = TypeMode.SelectedItem.ToString();
            Post post = new Post(this);
        }
    }
}
