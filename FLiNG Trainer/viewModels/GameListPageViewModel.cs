using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FLiNG_Trainer.core.sqlite;
using FLiNG_Trainer.models;
using FLiNG_Trainer.views;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wpf.Ui.Controls;

namespace FLiNG_Trainer.viewModels;

public partial class GameListPageViewModel : ObservableObject
{
    private CancellationTokenSource debounceTokenSource = new CancellationTokenSource();

    
    private ObservableCollection<GameListPageModel> _models;
    public ObservableCollection<GameListPageModel> Models
    {
        get => _models;
        set
        {
            SetProperty(ref _models, value);
            OnPropertyChanged(nameof(Models));
        }
    }

    
    private int _pageIndex = 0;
    public int PageIndex
    {
        get => _pageIndex;
        set => SetProperty(ref _pageIndex, value);
    }

    
    private string _trainerUrl;
    public string TrainerUrl
    {
        get => _trainerUrl;
        set => SetProperty(ref _trainerUrl, value);
    }

    private RelayCommand<DynamicScrollViewer> _scrollToBottomCommand;
    public ICommand ScrollToBottomCommand => _scrollToBottomCommand ??= new RelayCommand<DynamicScrollViewer>(OnScrollToBottom);

    private RelayCommand<string> _openDetailDialogCommand;
    public ICommand OpenDetailDialogCommand => _openDetailDialogCommand ??= new RelayCommand<string>(OpenDetailDialog);

    public GameListPageViewModel()
    {
        Models = new ObservableCollection<GameListPageModel>();
        _ = LoadDataAsync();
    }

    private void OpenDetailDialog(string gameId)
    {
        FluentWindow window = new DetailDialog(gameId);
        window.ShowDialog();
    }

    private async Task LoadDataAsync()
    {
        DataTable table;
        try
        {
            await Task.Run(() => {
                table = new GameListExecute().ExecuteGameListPage(PageIndex);
                foreach (DataRow row in table.Rows)
                {
                    GameListPageModel data = new GameListPageModel();
                    data.Id = row["id"].ToString();
                    data.EnName = row["en_name"].ToString();
                    data.TrainerUrl = row["trainer_url"].ToString();
                    data.GameCoverId = row["game_cover_id"].ToString();
                    data.GameCoverUrl = row["game_cover_url"].ToString();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Models.Add(data);
                    });
                }
            });
        }catch (TaskCanceledException) { }
    }

    private bool flag = false;
    private async void OnScrollToBottom(DynamicScrollViewer scrollViewer)
    {
        try
        {
            debounceTokenSource.Cancel(); 
            debounceTokenSource = new CancellationTokenSource(); 

            await Task.Delay(1000, debounceTokenSource.Token); 
            if (scrollViewer.VerticalOffset + scrollViewer.ViewportHeight >= scrollViewer.ExtentHeight)
            {
                flag = true;
                if (flag)
                {
                    PageIndex += 1;
                    await LoadDataAsync();
                }
            }
            else
            {
                flag = false;
            }
        }
        catch (TaskCanceledException) { }
    }
}
