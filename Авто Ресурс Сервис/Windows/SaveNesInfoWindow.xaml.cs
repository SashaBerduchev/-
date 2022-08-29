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
    /// Логика взаимодействия для SaveNesInfoWindow.xaml
    /// </summary>
    public partial class SaveNesInfoWindow : Window
    {
        List<Advantages> Advantages;
        List<Guarantee> Guarantees;
        public SaveNesInfoWindow()
        {
            InitializeComponent();
            TypeMode.Items.Add("false");
            TypeMode.Items.Add("true");
            GetGuaranties();
            GetAdvanteges();
        }

        private async Task GetAdvanteges()
        {
            var adv = await Task.WhenAny(Post.Send("Advantages", "GetAdvanteges"));
            Advantages = JsonConvert.DeserializeObject<List<Advantages>>((adv as Task<string>).Result);

        }

        private async Task GetGuaranties()
        {
            var gua = await Task.WhenAny(Post.Send("Guarantees", "GetGuaranties"));
            Guarantees = JsonConvert.DeserializeObject<List<Guarantee>>((gua as Task<string>).Result);
        }

        public void UpdateMode(string type)
        {
            TypeModeSelect.Text = type;
        }

        private void TypeMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Config.DEBUG_MODE = TypeMode.SelectedItem.ToString();
            Post post = new Post(this);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Config.DEBUG_MODE == "true")
                {
                    for (int i = 0; i < Advantages.Count; i++)
                    {
                        Post.Send("Advantages", "SaveAdv", JsonConvert.SerializeObject(Advantages[i]));
                    }
                    for (int j = 0; j < Guarantees.Count; j++)
                    {
                        Post.Send("Guarantees", "SaveGua", JsonConvert.SerializeObject(Guarantees[j]));
                    }
                }
                this.Close();
            }catch(Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Error", MessageBoxButton.OK);
            }
        }
    }
}
