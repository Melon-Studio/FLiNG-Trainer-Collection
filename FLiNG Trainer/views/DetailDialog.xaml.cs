using FLiNG_Trainer.core;
using FLiNG_Trainer.models;
using FLiNG_Trainer.viewModels;
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
        private string _gameId { get; set; }

        public DetailDialog(string gameId)
        {
            _gameId = gameId;
            InitializeComponent();
            this.DataContext = new DetailDialogViewModel(gameId);
            
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
        }
    }

}
