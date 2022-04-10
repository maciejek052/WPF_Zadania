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
    /// Logika interakcji dla klasy ModifyMovie.xaml
    /// </summary>
    public partial class ModifyMovie : Window
    {
        public ModifyMovie()
        {
            InitializeComponent();
            Title = "Dodawanie filmu"; 
        }

        public ModifyMovie(Movie movie)
        {
            InitializeComponent();
            Title = "Edytowanie filmu";
            this.movie = movie;
            titleInput.Text = movie.title;
            dateInput.SelectedDate = movie.releaseDate;
            descriptionInput.Text = movie.description;
        }

        public Movie movie; 

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (movie == null)
                movie = new Movie();
            movie.title = titleInput.Text;
            movie.releaseDate = dateInput.DisplayDate;
            movie.description = descriptionInput.Text;
            DialogResult = true;
            Close(); 
        }
    }
}
