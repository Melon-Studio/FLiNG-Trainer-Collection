using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FLiNG_Trainer.core.sqlitep;
using FLiNG_Trainer.models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FLiNG_Trainer.viewModels;

public partial class GameListPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<GameListPageModel> _models;
    public ObservableCollection<GameListPageModel> Models
    {
        get { return _models; }
        set { SetProperty(ref _models, value); }
    }

    private RelayCommand<string> _openDetailDialogCommand;
    public ICommand OpenDetailDialogCommand => _openDetailDialogCommand ??= new RelayCommand<string>(OpenDetailDialog);

    public GameListPageViewModel()
    {
        Models = new ObservableCollection<GameListPageModel>();
        _openDetailDialogCommand = new RelayCommand<string>(OpenDetailDialog);
        _ = LoadDataAsync();
    }

    private void OpenDetailDialog(string url)
    {
        MessageBox.Show(url);
    }

    private async Task LoadDataAsync()
    {
        await Task.Run(() => {
            ObservableCollection<GameListPageModel> dataModel = new ObservableCollection<GameListPageModel>();
            SQLiteDataAccess dataAccess = new SQLiteDataAccess();
            dataAccess.OpenConnection();
            string query = "SELECT * FROM game_list";
            int pageIndex = 0;
            int pageSize = 20;
            DataTable table = dataAccess.GetPagedData(query, pageIndex, pageSize);
            foreach (DataRow row in table.Rows)
            {
                GameListPageModel data = new GameListPageModel();
                data.Id = row["id"].ToString();
                data.EnName = row["en_name"].ToString();
                data.TrainerUrl = row["trainer_url"].ToString();
                data.GameCoverId = row["game_cover_id"].ToString();
                dataModel.Add(data);
            }
            Models = dataModel;
            dataAccess.CloseConnection();
        });
    }
}
