using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FLiNG_Trainer.models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FLiNG_Trainer.viewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _pageName;
    public string PageName
    {
        get { return _pageName; }
        set { SetProperty(ref _pageName, value); }
    }

    private RelayCommand<string> _updatePageNameCommand;
    public ICommand UpdatePageNameCommand => _updatePageNameCommand ??= new RelayCommand<string>(UpdatePageName);

    private RelayCommand _testExcption;
    public ICommand TestExcption => _testExcption ??= new RelayCommand(UpdatePageName);

    private void UpdatePageName()
    {
        throw new NotImplementedException();
    }

    public MainWindowViewModel()
    {
        _updatePageNameCommand = new RelayCommand<string>(UpdatePageName);
    }
    private void UpdatePageName(string name)
    {
        PageName = name;
    }

    
}
