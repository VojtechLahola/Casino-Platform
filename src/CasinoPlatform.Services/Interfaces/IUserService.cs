using CasinoPlatform.Services.DTOs;

namespace CasinoPlatform.Services.Interfaces;

public interface IUserService
{
    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

    Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

    Task<BalanceResponse> GetBalanceAsync(Guid userId, CancellationToken cancellationToken = default);
}
