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
    /// Логика взаимодействия для TiresImagesReLoadWindow.xaml
    /// </summary>
    public partial class TiresImagesReLoadWindow : Window
    {
        List<TiresImage> tiresImages;
        public TiresImagesReLoadWindow(List<TiresImage> img)
        {
            InitializeComponent();
            tiresImages = img;
            TypeMode.Items.Add("false");
            TypeMode.Items.Add("true");
        }

        private void TypeMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Config.DEBUG_MODE = TypeMode.SelectedItem.ToString();
            Post post = new Post(this);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Config.DEBUG_MODE == "true")
                for (int i = 0; i < tiresImages.Count; i++)
                {
                    var datas = JsonConvert.SerializeObject(tiresImages[i]);
                    //tiresImages[i].id = null;
                    Post.Send("TiresImages", "SaveImg", datas);
                }

            this.Close();
        }
    }
}
