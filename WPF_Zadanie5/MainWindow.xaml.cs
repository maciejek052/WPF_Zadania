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

namespace WPF_Zadanie5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            if (moviesList.SelectedIndex != -1)
            {
                btnModify.IsEnabled = true;
                btnPreview.IsEnabled = true;
                btnRemove.IsEnabled = true;
            }
            else
            {
                btnModify.IsEnabled = false;
                btnPreview.IsEnabled = false;
                btnRemove.IsEnabled = false;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ModifyMovie newMovie = new ModifyMovie();
            newMovie.Owner = this;
            if (newMovie.ShowDialog() == true)
            {
                moviesList.Items.Add(newMovie.movie);
                moviesList.Items.Refresh();
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            ModifyMovie modifyMovie = new ModifyMovie(moviesList.SelectedItem as Movie);
            modifyMovie.Owner = this; 
            if (modifyMovie.ShowDialog() == true)
            {
                moviesList.Items.Refresh();
            }
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            PreviewMovie previewMovie = new PreviewMovie(moviesList.SelectedItem as Movie);
            previewMovie.Owner = this;
            previewMovie.Show(); 
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć ten film?", "Usuwanie filmu", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                moviesList.Items.Remove(moviesList.SelectedItem);
        }

        private void moviesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var window in OwnedWindows)
            {
                PreviewMovie prev = window as PreviewMovie;
                if (prev != null)
                    prev.UpdateView(moviesList.SelectedItem as Movie);
            }

            if (moviesList.SelectedIndex != -1)
            {
                btnModify.IsEnabled = true;
                btnPreview.IsEnabled = true;
                btnRemove.IsEnabled = true; 
            }
            else
            {
                btnModify.IsEnabled = false;
                btnPreview.IsEnabled = false;
                btnRemove.IsEnabled = false;
            }
        }
    }
}
