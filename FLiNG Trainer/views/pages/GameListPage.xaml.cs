using FLiNG_Trainer.viewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FLiNG_Trainer.views.pages;

public partial class GameListPage : Page
{
    public GameListPage()
    {
        InitializeComponent();
        this.DataContext = new GameListPageViewModel();
    }
}
