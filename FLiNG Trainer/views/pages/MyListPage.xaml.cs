using FLiNG_Trainer.viewModels;
using System.Windows.Controls;

namespace FLiNG_Trainer.views.pages;

public partial class MyListPage : Page
{
    public MyListPage()
    {
        InitializeComponent();
        this.DataContext = new MyListPageViewModel();
    }
}
