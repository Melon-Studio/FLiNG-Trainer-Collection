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


    private bool flag = false;
    private CancellationTokenSource _loadMoreDataCTS = new CancellationTokenSource();
    private async void DynamicScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        await Task.Delay(2000);

        var scrollViewer = sender as ScrollViewer;
        if (scrollViewer == null) return;

        _loadMoreDataCTS.Cancel();
        _loadMoreDataCTS = new CancellationTokenSource();

        try
        {
            await Task.Delay(200, _loadMoreDataCTS.Token);

            if (!_loadMoreDataCTS.IsCancellationRequested &&
            scrollViewer.VerticalOffset + scrollViewer.ViewportHeight >= scrollViewer.ExtentHeight)
            {
                if (!flag)
                {
                    await LoadMoreDataAsync();
                }
            }else
            {
                flag = false;
            }
        }catch (TaskCanceledException) { }
    }

    private async Task LoadMoreDataAsync()
    {
        flag = true;
        MessageBox.Show("滚动到底部了，加载更多数据");
    }
}
