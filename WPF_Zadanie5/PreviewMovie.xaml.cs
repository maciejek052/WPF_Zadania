using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Zadanie5
{
    /// <summary>
    /// Logika interakcji dla klasy PreviewMovie.xaml
    /// </summary>
    public partial class PreviewMovie : Window
    {
        public PreviewMovie(Movie movie)
        {
            InitializeComponent();
            if (movie != null)
            {
                titleInput.Text = movie.title;
                dateInput.SelectedDate = movie.releaseDate;
                descriptionInput.Text = movie.description;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void UpdateView(Movie movie)
        {
            if (movie == null)
            {
                Close();
                return; 
            }

            titleInput.Text = movie.title;
            dateInput.SelectedDate = movie.releaseDate;
            descriptionInput.Text = movie.description;
        }
    }
}
