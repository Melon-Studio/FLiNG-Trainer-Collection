using CommunityToolkit.Mvvm.ComponentModel;
using FLiNG_Trainer.core;
using FLiNG_Trainer.core.sqlite;
using FLiNG_Trainer.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FLiNG_Trainer.viewModels;

public class DetailDialogViewModel : ObservableObject
{
    private string _gameId;
    public DetailDialogViewModel(string gameId) 
    {
        _gameId = gameId;
        _ = LoadDataAsync(_gameId);
    }

    private ObservableCollection<DetailDialogModel> _models;
    public ObservableCollection<DetailDialogModel> Models
    {
        get => _models;
        set
        {
            SetProperty(ref _models, value);
            OnPropertyChanged(nameof(Models));
        }
    }

    private bool isVisibility = true;
    public bool IsVisibility
    {
        get => isVisibility;
        set => SetProperty(ref isVisibility, value);
    }

    private async Task LoadDataAsync(string gameId)
    {
        try
        {
            DataRow model = new GameListExecute().ExecuteDataByGameId(int.Parse(gameId));
            ObservableCollection<DetailDialogModel> detailDialogModels = new ObservableCollection<DetailDialogModel>();
            AngleSharpDomUtility domUtility = new AngleSharpDomUtility();
            var content = await domUtility.GetAnchorTagsContent(model["trainer_url"].ToString(), "da-attachments-table");
            for (int i = 0; i < content.Length; i++)
            {
                DetailDialogModel model1 = new DetailDialogModel();
                model1.Id = 1;
                model1.Name = content[i].text;
                model1.Url = content[i].href;
                model1.Image = model["game_cover_url"].ToString();
                detailDialogModels.Add(model1);
            }
            IsVisibility = false;
            Models = detailDialogModels;
        }
        catch (TaskCanceledException ex) 
        {
            throw ex;
        }
    }
}
