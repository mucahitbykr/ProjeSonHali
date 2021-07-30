using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;


namespace Teknofest_Görüntüleme_Arayüzü_v1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {


        List<Team_Info> takim_bilgileri = new List<Team_Info>();
       
        public string JsonPath = @"C:\Users\PC_1951\Desktop\LastVersion-master\LastVersion-master\Teknofest_Görüntüleme_Arayüzü_v1\JsonFiles\Takim_Verileri.json";
        //Takım_Verileri.json dosyasının konumu
        public MainWindow()
        {
            InitializeComponent();
            comboboxa_takim_adi_ekle();
        }

        private void verileri_yaz()
        {
            Team_Info new_team = new Team_Info()
            {
                Takim_Ad = txt_takim_adi.Text,
                Takim_Ip = txt_Ip.Text,
                Takim_Port = txt_Port.Text
            };


            var json = JsonConvert.SerializeObject(new_team);
            takim_bilgileri.Add(new_team);

        }

        private void verileri_Oku()
        {
            
            var check = File.ReadAllText(JsonPath);
            if (check == "")
            {
                var serializedObj = JsonConvert.SerializeObject(takim_bilgileri);
                File.WriteAllText(JsonPath, serializedObj);
            }
            else
            {

                List<Team_Info> YeniTakim = JsonConvert.DeserializeObject<List<Team_Info>>(check);
                takim_bilgileri.AddRange(YeniTakim);
                var sOjb = JsonConvert.SerializeObject(takim_bilgileri);
                File.WriteAllText(JsonPath, sOjb);

            }
            takim_bilgileri.Clear();
            comboboxa_takim_adi_ekle();

        }

   
        private void comboboxa_takim_adi_ekle()
        {
            var LastJsonFile = File.ReadAllText(JsonPath);
            List<Team_Info> takimlar = JsonConvert.DeserializeObject<List<Team_Info>>(LastJsonFile);

            cmbBox.Items.Clear();
            for (int i = 0; i < takimlar.Count; i++)
            {
             
                    cmbBox.Items.Add(takimlar[i].Takim_Ad);
                
            }   

        }

        private void btn_takim_ekle_Click(object sender, RoutedEventArgs e)
        {
            verileri_yaz();
            verileri_Oku();

        }

        private void btn_takim_izle_Click(object sender,RoutedEventArgs e)
        {
            var ip = takim_bilgi_dondur(cmbBox.Text);
            Iha_Udp_Goruntu goruntu = new Iha_Udp_Goruntu(ip.Takim_Ad, ip.Takim_Ip, ip.Takim_Port);
            goruntu.yayin_izle();
        }
        private Team_Info takim_bilgi_dondur(string takim_adi)
        {
          

            var takımlar_json = File.ReadAllText(JsonPath);

            List<Team_Info> TakimListe = JsonConvert.DeserializeObject<List<Team_Info>>(takımlar_json);

            for (int i = 0; i < TakimListe.Count; i++)
            {

                if (takim_adi == TakimListe[i].Takim_Ad)
                {
                    return TakimListe[i];
                }

            }

            return new Team_Info();


        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Pencere Kapatılamıyor");
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }

}


