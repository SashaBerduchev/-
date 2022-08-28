using Microsoft.Win32;
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
    /// Логика взаимодействия для AddTiresPicWindow.xaml
    /// </summary>
    public partial class AddTiresPicWindow : Window
    {
        Window window;
        private string fileType = "image/jpeg";
        private byte[] fileName;
        public AddTiresPicWindow(Window window)
        {
            InitializeComponent();
            Trace.WriteLine(this);
            this.window = window;
            GetTiresImageParams();
        }

        private async Task GetTiresImageParams()
        {
            var tires = await Task.WhenAny(Post.Send("TiresImages", "GetTiresPictParams"));
            if (tires is Task<string>)
            {
                List<Tires> tireses = JsonConvert.DeserializeObject<List<Tires>>((tires as Task<string>).Result);
                NameTire.ItemsSource = tireses.OrderBy(x=>x.Name).Select(x => x.Name).Distinct();
                TypeTire.ItemsSource = tireses.Select(x => x.TypeOfTire).Distinct();
            }
        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileContent;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            var fileStream = fileDialog.OpenFile();

            string filedata = fileDialog.FileName;
            Img.Source = new BitmapImage(new Uri(filedata));
            byte[] bData = File.ReadAllBytes(fileDialog.FileName);
            fileName = bData;
            Trace.WriteLine(fileDialog);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (fileName != null)
            {
                TiresImage tiresImage = new TiresImage();
                tiresImage.NameTire = NameTire.SelectedItem.ToString();
                tiresImage.TypeTire = TypeTire.SelectedItem.ToString();
                tiresImage.ImageMimeTypeOfData = fileType;
                tiresImage.Image = fileName;
                string str = JsonConvert.SerializeObject(tiresImage);
                Post.Send("TiresImages", "SaveImg", str);
                this.Close();
                (window as MainWindow).GetTiresPic();
            }
            else
            {
                MessageBox.Show("Виберіть зображення!", "Error", MessageBoxButton.OK);
            }
        }
    }
}
