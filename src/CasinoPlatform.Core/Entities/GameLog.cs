namespace CasinoPlatform.Core.Entities;

public class GameLog
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string GameName { get; set; } = string.Empty;

    public decimal BetAmount { get; set; }

    public decimal Multiplier { get; set; }

    public decimal Payout { get; set; }

    public DateTime PlayedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}
