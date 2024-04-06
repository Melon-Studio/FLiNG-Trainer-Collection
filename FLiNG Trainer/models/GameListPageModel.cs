using System.Windows.Media;

namespace FLiNG_Trainer.models;

public class GameListPageModel
{
    #pragma warning disable CS0169
    private string id;
    private string enName;
    private string trainerUrl;
    private string gameCoverId;
    private string gameCoverUrl;

    public string Id { get; set; }
    public string EnName { get; set; }
    public string TrainerUrl { get; set; }
    public string GameCoverId { get; set; }
    public string GameCoverUrl { get; set; }
}
