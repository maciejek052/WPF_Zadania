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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Zadanie3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int naklad;
        double cena_format = 0.2;
        double mnoznik_grubosc_papieru; 
        double cena;
        double rabat, cena_po_rabacie;
        string kolor_papieru; 
        string gramatura; 
        bool kolor_papier, kolor_druk, dwustronny, uv, ekspres; 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool sprawdzenie = int.TryParse(TextBox_naklad.Text, out naklad); 
            if (!sprawdzenie)
            {
                MessageBox.Show("Możesz podać tylko liczbę", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ObliczWartosc(); 
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int wartosc = (int)suwak.Value; 
            switch (wartosc)
            {
                case 0:
                    cena_format = 0.2;
                    label_format.Content = "A5 - cena " + cena_format * 100 + "gr/szt.";
                    ObliczWartosc(); 
                    break;
                case 1:
                    cena_format = 0.2 * 2.5; 
                    label_format.Content = "A4 - cena " + cena_format * 100 + "gr/szt.";
                    ObliczWartosc();
                    break;
                default:
                    cena_format = Math.Round((0.2 * Math.Pow(2.5, wartosc)), 2);
                    label_format.Content = "A" + Math.Abs(wartosc-5) + " - cena " + cena_format + "zł/szt.";
                    ObliczWartosc(); 
                    break; 
            }
        }

        private void kolorowy_Checked(object sender, RoutedEventArgs e)
        {
            lista_kolorow.IsEnabled = (bool)kolorowy.IsChecked;
            kolor_papier = (bool)kolorowy.IsChecked; 
            ObliczWartosc(); 
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (g80.IsChecked == true)
            {
                gramatura = "80 g/m²";
                mnoznik_grubosc_papieru = 1;
            }
            else if (g120.IsChecked == true)
            {
                gramatura = "120 g/m²";
                mnoznik_grubosc_papieru = 2;
            }
            else if (g200.IsChecked == true)
            {
                gramatura = "200 g/m²";
                mnoznik_grubosc_papieru = 2.5;
            }
            else if (g240.IsChecked == true)
            {
                gramatura = "240 g/m²";
                mnoznik_grubosc_papieru = 3;
            }
            ObliczWartosc(); 
        }
        private void kolordruk_Checked(object sender, RoutedEventArgs e)
        {
            kolor_druk = (bool)CheckKolor.IsChecked; 
            ObliczWartosc();
        }
        private void dwustronny_Checked(object sender, RoutedEventArgs e)
        {
            dwustronny = (bool)CheckDwustronny.IsChecked;
            ObliczWartosc();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close(); 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Zamówienie zostało przyjęte!", "Drukarnia", MessageBoxButton.OK, MessageBoxImage.Information);
            // reset formularza
            TextBox_naklad.Text = 0.ToString();
            suwak.Value = 0;
            kolorowy.IsChecked = false;
            g80.IsChecked = true;
            CheckKolor.IsChecked = false;
            CheckDwustronny.IsChecked = false;
            CheckUV.IsChecked = false;
            Standard.IsChecked = true;
            ObliczWartosc();
        }

        private void lista_kolorow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            kolor_papieru = ((ComboBoxItem)lista_kolorow.SelectedItem).Content.ToString();
            ObliczWartosc(); 
        }

        private void UV_Checked(object sender, RoutedEventArgs e)
        {
            uv = (bool)CheckUV.IsChecked; 
            ObliczWartosc();
        }
        private void Termin_Checked(object sender, RoutedEventArgs e)
        {
            if (Ekspres != null && Ekspres.IsChecked == true)
                ekspres = true;
            else ekspres = false; 
            ObliczWartosc();
        }
        void ObliczWartosc()
        {
            cena = naklad * cena_format * mnoznik_grubosc_papieru;
            cena *= kolor_papier ? 1.5 : 1;
            cena *= kolor_druk ? 1.3 : 1;
            cena *= dwustronny ? 1.5 : 1;
            cena *= uv ? 1.2 : 1;
            cena += ekspres ? 15 : 0;
            cena = Math.Round(cena, 2);
            rabat = ((naklad / 100) > 10 ? 10 : (naklad / 100));
            cena_po_rabacie = cena * (1 - rabat / 100);
            cena_po_rabacie = Math.Round(cena_po_rabacie, 2); 
            PokazPodsumowanie(); 
        }
        void PokazPodsumowanie()
        {
            if (podsumowanie != null)
            {
                string tekst =
                    "Przedmiot zamówienia: " + naklad + " szt., format A" +
                    Math.Abs((int)suwak.Value - 5) + ", " + gramatura + ", ";
                if (kolor_papier)
                    tekst += "Kolorowy papier " + kolor_papieru + ", ";
                if (kolor_druk)
                    tekst += ", druk w kolorze, ";
                if (dwustronny)
                    tekst += "druk dwustronny, ";
                if (uv)
                    tekst += "lakier UV, ";
                if (ekspres)
                    tekst += "termin ekspres";
                else
                    tekst += "termin standard";
                tekst += "\nCena przed rabatem: " + cena + " zł\n" +
                    "Naliczony rabat: " + rabat + "%\n" + 
                    "Cena po rabacie: " + cena_po_rabacie + "zł";
                
                podsumowanie.Text = tekst;
            }
        }
    }
}
