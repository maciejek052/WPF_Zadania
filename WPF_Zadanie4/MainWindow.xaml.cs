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

namespace WPF_Zadanie4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle ostatniProstokat = null;
        public MainWindow()
        {
            InitializeComponent();
            canvas.Focus();
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(canvas);
            Rectangle nowyProstokat = new Rectangle();
            ostatniProstokat = nowyProstokat;
            nowyProstokat.Stroke = Brushes.Black;
            Point punkt = new Point();
            punkt = e.GetPosition(this);
            nowyProstokat.SetValue(Canvas.TopProperty, punkt.Y);
            nowyProstokat.SetValue(Canvas.LeftProperty, punkt.X);
            nowyProstokat.Height = 0;
            nowyProstokat.Width = 0;
            canvas.Children.Add(nowyProstokat);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point punkt = e.GetPosition(this);
                var top = (double)ostatniProstokat.GetValue(Canvas.TopProperty);
                var left = (double)ostatniProstokat.GetValue(Canvas.LeftProperty);
                var wysokosc = punkt.Y - top;
                var szerokosc = punkt.X - left;
                if (wysokosc > 0)
                {
                    if (szerokosc < top + ostatniProstokat.Height - punkt.Y)
                    {
                        ostatniProstokat.SetValue(Canvas.TopProperty, punkt.Y);
                        ostatniProstokat.Height += Math.Abs(wysokosc); 
                    }
                    else
                        ostatniProstokat.Height = wysokosc;
                }
                else
                {
                    ostatniProstokat.SetValue(Canvas.TopProperty, punkt.Y);
                    ostatniProstokat.Height += Math.Abs(wysokosc);
                }
                if (szerokosc > 0)
                {
                    if (szerokosc < left + ostatniProstokat.Width - punkt.X)
                    {
                        ostatniProstokat.SetValue(Canvas.LeftProperty, punkt.X);
                        ostatniProstokat.Width += Math.Abs(szerokosc);
                    }
                    else
                        ostatniProstokat.Width = szerokosc;
                }
                else
                {
                    ostatniProstokat.SetValue(Canvas.LeftProperty, punkt.X);
                    ostatniProstokat.Width += Math.Abs(szerokosc);
                }
            }
        }

        private void canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (ostatniProstokat == null)
                return;
            else
            {
                var top = (double)ostatniProstokat.GetValue(Canvas.TopProperty);
                var left = (double)ostatniProstokat.GetValue(Canvas.LeftProperty);
                switch (e.Key)
                {
                    case Key.Up:
                        if (Keyboard.IsKeyDown(Key.LeftShift))
                        {
                            ostatniProstokat.SetValue(Canvas.TopProperty, top - 1);
                            ostatniProstokat.Height++;
                        }
                        else
                            ostatniProstokat.SetValue(Canvas.TopProperty, top - 1);
                        break;
                    case Key.Down:
                        if (Keyboard.IsKeyDown(Key.LeftShift))
                            ostatniProstokat.Height++;
                        else
                            ostatniProstokat.SetValue(Canvas.TopProperty, top + 1);
                        break;
                    case Key.Left:
                        if (Keyboard.IsKeyDown(Key.LeftShift))
                        {
                            ostatniProstokat.SetValue(Canvas.LeftProperty, left - 1);
                            ostatniProstokat.Width++;
                        }
                        else
                            ostatniProstokat.SetValue(Canvas.LeftProperty, left - 1);
                        break;
                    case Key.Right:
                        if (Keyboard.IsKeyDown(Key.LeftShift))
                            ostatniProstokat.Width++;
                        else
                            ostatniProstokat.SetValue(Canvas.LeftProperty, left + 1);
                        break;
                }
            }
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
        }
    }
}