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

namespace WPF_Zadanie1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MaszynaLosujaca maszyna = new MaszynaLosujaca();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // sprawdzenie czy użytkownik nie chce dodać pustej wartości
            if (textBox.Text != "") 
            {
                maszyna.DodajDoMaszyny(textBox.Text);
                label.Content = "Dodano napis " + textBox.Text;
                label1.Content = maszyna.WypiszZawartoscMaszyny();
                textBox.Text = ""; // wyzerowanie textboxa
            }
            else
            {
                label.Content = "Nie można dodać pustego napisu"; 
                label1.Content = maszyna.WypiszZawartoscMaszyny();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (maszyna.CzySaKupony())
            {
                string losowy = maszyna.WyjmijLosowyKupon();
                label.Content = "Wyjęto napis " + losowy;
                label1.Content = maszyna.WypiszZawartoscMaszyny();
            }
            else
            {
                label.Content = "Nie można wyjąć kuponu, bo maszyna jest pusta";
                label1.Content = maszyna.WypiszZawartoscMaszyny();
            }

        }
    }
}
