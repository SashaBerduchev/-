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
    /// Interaction logic for BrendsAddWindow.xaml
    /// </summary>
    public partial class BrendsAddWindow : Window
    {
        Window window;
        private string fileType = "image/jpeg";
        private byte[] fileName;
        public BrendsAddWindow(Window window)
        {
            InitializeComponent();
            this.window = window;
            Trace.WriteLine(this);
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
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
            Brends brends = new Brends();
            brends.Name = NameText.Text;
            brends.Information = InfoText.Text;
            brends.ImageMimeTypeOfData = fileType;
            brends.Image = fileName;
            string str = JsonConvert.SerializeObject(brends);
            Post.Send("Brends", "SaveBrends", str);
            this.Close();
            (window as MainWindow).GetBrend();
        }
    }
}
