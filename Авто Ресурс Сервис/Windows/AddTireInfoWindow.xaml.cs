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
using System.Windows.Shapes;
using Авто_Ресурс_Сервис.Http;
using Авто_Ресурс_Сервис.Parser;

namespace Авто_Ресурс_Сервис.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddTireInfoWindow.xaml
    /// </summary>
    public partial class AddTireInfoWindow : Window
    {
        List<Tires> information;
        Window windows;
        public AddTireInfoWindow(Window window, List<Tires> tiresInformation)
        {
            InitializeComponent();
            Trace.WriteLine(this);
            windows = window;
            information = tiresInformation;
            BrendCombo.ItemsSource = information.Select(x=>x.Name).ToList().Distinct();
            TypeCombo.ItemsSource = information.Select(x=>x.TypeOfTire).ToList().Distinct();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            TiresInformation information = new TiresInformation();
            information.NameTire = BrendCombo.SelectedItem.ToString();
            information.TypeTire = TypeCombo.SelectedItem.ToString();
            information.Info = InfoText.Text;
            string data = JsonConvert.SerializeObject(information);
            Post.Send("TiresInformations", "SaveInfo", data);
            (windows as MainWindow).GetTiresInfo();
            this.Close();
        }
    }
}
