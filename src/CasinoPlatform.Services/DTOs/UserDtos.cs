namespace CasinoPlatform.Services.DTOs;

public record RegisterRequest(string Username, string Password);

public record LoginRequest(string Username, string Password);

public record AuthResponse(Guid UserId, string Username, decimal Balance, string? Token = null);

public record BalanceResponse(Guid UserId, decimal Balance);
