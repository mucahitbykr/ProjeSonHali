using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;

namespace Teknofest_Görüntüleme_Arayüzü_v1
{
    /// <summary>
    /// Interaction logic for Yayin_Izleme_Ekrani.xaml
    /// </summary>
    public partial class Yayin_Izleme_Ekrani : Window
    {
        public Yayin_Izleme_Ekrani()
        {
            InitializeComponent();
        }
        
        private void takimi_izle_Click(object sender, RoutedEventArgs e)
        {
            var ip= takim_bilgi_dondur(cmbBox.Text);
            Iha_Udp_Goruntu goruntu = new Iha_Udp_Goruntu(ip.Takim_Ad, ip.Takim_Ip, ip.Takim_Port);
            goruntu.yayin_izle();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            var ip = takim_bilgi_dondur(cmbBox.Text);
            Iha_Udp_Goruntu goruntu = new Iha_Udp_Goruntu(ip.Takim_Ad, ip.Takim_Ip, ip.Takim_Port);
            goruntu.yayin_kapat();


        }

        private Team_Info takim_bilgi_dondur(string takim_adi)
        {
            MainWindow MainWindow = new MainWindow();

            var takımlar_json = File.ReadAllText(MainWindow.JsonPath);

            List<Team_Info> TakimListe = JsonConvert.DeserializeObject<List<Team_Info>>(takımlar_json);

            for (int i = 0; i < TakimListe.Count; i++)
            {
             
                if(takim_adi==TakimListe[i].Takim_Ad)
                {
                    return TakimListe[i];
                }

            }

            return new Team_Info();
                

        }

    }
}
