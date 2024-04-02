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
using Wpf.Ui.Controls;

namespace FLiNG_Trainer.views
{
    public partial class DetailDialog : FluentWindow
    {
        public string Url { get; set; }

        public DetailDialog()
        {
            InitializeComponent();
        }

        // MinButton 交互事件

        private void MinButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinButton.Background = new SolidColorBrush(Color.FromRgb(28, 28, 28));
        }

        private void MinButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MinButton.Background = new SolidColorBrush(Color.FromRgb(35, 35, 35));
        }

        private void MinButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MinButton.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
        }

        private void MinButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinButton.Background = new SolidColorBrush(Color.FromRgb(28, 28, 28));
        }

        private void MinButton_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinButton.Background = new SolidColorBrush(Color.FromRgb(35, 35, 35));
            WindowState = WindowState.Minimized;
        }

        // CloseButton 交互事件

        private void CloseButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CloseButton.Background = new SolidColorBrush(Color.FromRgb(184, 40, 26));
        }

        private void CloseButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CloseButton.Background = new SolidColorBrush(Color.FromRgb(196, 43, 28));
        }

        private void CloseButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CloseButton.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
        }

        private void CloseButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CloseButton.Background = new SolidColorBrush(Color.FromRgb(184, 40, 26));
        }

        private void CloseButton_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CloseButton.Background = new SolidColorBrush(Color.FromRgb(196, 43, 28));
            Close();
        }

        private void DragArea_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void FluentWindow_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.MessageBox.Show(Url);
        }
    }

}
