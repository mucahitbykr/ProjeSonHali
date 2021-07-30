using System;
using System.Windows;
using System.Threading;
using System.ComponentModel;

namespace Teknofest_Görüntüleme_Arayüzü_v1
{
    /// <summary>
    /// Interaction logic for Iha_Udp_Goruntu.xaml
    /// </summary>
    /// 



     //10.0.4.118
     //1234

    public partial class Iha_Udp_Goruntu : Window
    {
        public string takim_adi;
        public string ip_Num;
        public string port_Num;
        private string ffmpeg_kutuphane_yolu = @"C:\Users\PC_1951\Desktop\LastVersion-master\LastVersion-master\packages\FFmpeg.Shared.4.0.2\ffmpeg";
        //Packages Klasörü içinde\packages\FFmpeg.Shared.4.0.2\ffmpeg tüm dosya yolu
        public Iha_Udp_Goruntu(string Takim_Adi,string Ip_Numarasi,string Port_Numarasi)
            
        {
            takim_adi = Takim_Adi;
            ip_Num = Ip_Numarasi;
            port_Num = Port_Numarasi;
            InitializeComponent();
           
            Unosquare.FFME.Library.FFmpegDirectory = ffmpeg_kutuphane_yolu;
          

        }



        public void yayin_izle()
        {
            
            try
            {
              iha_ekran.Open(new Uri("udp://@" + ip_Num + ":" + port_Num)); 
                this.Show();
                this.Title = takim_adi;
                lbl_baslik.Content = takim_adi;
                
            }
            catch (Exception)
            {

                MessageBox.Show("Tüm alanları doğru doldurduğunuzdan emin olun.");
            }
          
        }
        public void yayin_kapat()
        {
            try
            {
               
              iha_ekran.Close();
               
            }
            catch (Exception)
            {
                MessageBox.Show("Yayın kapatılamıyor");
            }
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
               
                this.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Pencere Kapatılamıyor");
            }
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMaximize_Click(object sender,RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        } 
    }
}
