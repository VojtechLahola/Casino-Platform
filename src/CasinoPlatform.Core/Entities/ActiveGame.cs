namespace CasinoPlatform.Core.Entities;

public class ActiveGame
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string GameType { get; set; } = string.Empty;

    public string GameState { get; set; } = string.Empty;

    public decimal CurrentMultiplier { get; set; } = 1m;

    public User User { get; set; } = null!;
}
