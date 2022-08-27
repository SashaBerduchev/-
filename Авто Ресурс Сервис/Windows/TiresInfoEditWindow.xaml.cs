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
    /// Interaction logic for TiresInfoEditWindow.xaml
    /// </summary>
    public partial class TiresInfoEditWindow : Window
    {
        private Window windows;
        private TiresInformation tiresinf;
        public TiresInfoEditWindow(Window window, TiresInformation tiresInformation)
        {
            InitializeComponent();
            windows = window;
            tiresinf = tiresInformation;
            NameTire.Text = tiresinf.NameTire;
            TypeTire.Text = tiresinf.TypeTire;
            InfoText.Text = tiresinf.Info;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            TiresInformation information = new TiresInformation();
            information.NameTire = NameTire.Text;
            information.TypeTire = TypeTire.Text;
            information.Info = InfoText.Text;
            string data = JsonConvert.SerializeObject(information);
            Post.Send("TiresInformations", "SaveInform", data);
            (windows as MainWindow).GetTiresInfo();
            this.Close();
        }
    }
}
