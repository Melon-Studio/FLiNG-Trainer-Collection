using FLiNG_Trainer.core.sqlitep;
using FLiNG_Trainer.viewModels;
using System.Data;
using System.Windows;
using System.Windows.Media;
using Wpf.Ui.Controls;

namespace FLiNG_Trainer.views;

public partial class MainWindow : FluentWindow
{
    private bool isGameListMouseDown = false;
    private bool isMyListMouseDown = false;
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new MainWindowViewModel();
    }

    // GameListButton 交互事件

    private void GameListButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (!isGameListMouseDown)
        {
            GameListButton.Background = new SolidColorBrush(Color.FromRgb(55, 55, 55));
        }
    }

    private void GameListButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (!isGameListMouseDown)
        {
            GameListButton.Background = new SolidColorBrush(Color.FromRgb(37, 37, 37));
        }
    }

    private void GameListButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (!isGameListMouseDown)
        {
            GameListButton.Background = new SolidColorBrush(Color.FromRgb(105, 105, 105));
        }
    }

    private void GameListButton_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        GameListButton.Background = new SolidColorBrush(Color.FromRgb(211, 56, 28));
        MyListButton.Background = new SolidColorBrush(Color.FromRgb(37, 37, 37));
        isGameListMouseDown = true;
        isMyListMouseDown = false;
        MainWindowFrame.Source = new System.Uri("pack://application:,,,/FLiNG Trainer;component/views/pages/GameListPage.xaml", System.UriKind.Absolute);
    }

    // MyListButton 交互事件

    private void MyListButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (!isMyListMouseDown)
        {
            MyListButton.Background = new SolidColorBrush(Color.FromRgb(55, 55, 55));
        }
    }

    private void MyListButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (!isMyListMouseDown)
        {
            MyListButton.Background = new SolidColorBrush(Color.FromRgb(37, 37, 37));
        }
    }

    private void MyListButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (!isMyListMouseDown)
        {
            MyListButton.Background = new SolidColorBrush(Color.FromRgb(105, 105, 105));
        }
    }
    private void MyListButton_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        MyListButton.Background = new SolidColorBrush(Color.FromRgb(211, 56, 28));
        GameListButton.Background = new SolidColorBrush(Color.FromRgb(37, 37, 37));
        isMyListMouseDown = true;
        isGameListMouseDown = false;
        MainWindowFrame.Source = new System.Uri("pack://application:,,,/FLiNG Trainer;component/views/pages/MyListPage.xaml", System.UriKind.Absolute);
    }

    // SettingButton 交互事件

    private void SettingButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        SettingButton.Background = new SolidColorBrush(Color.FromRgb(55, 55, 55));
    }

    private void SettingButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        SettingButton.Background = new SolidColorBrush(Color.FromRgb(37, 37, 37));
    }

    private void SettingButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SettingButton.Background = new SolidColorBrush(Color.FromRgb(28, 28, 28));
    }

    private void SettingButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SettingButton.Background = new SolidColorBrush(Color.FromRgb(28, 28, 28));
    }

    private void SettingButton_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SettingButton.Background = new SolidColorBrush(Color.FromRgb(55, 55, 55));
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
    }
}
