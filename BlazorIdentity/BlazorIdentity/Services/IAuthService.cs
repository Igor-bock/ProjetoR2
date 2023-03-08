using BlazorIdentity.Models;

namespace BlazorIdentity.Services;

public interface IAuthService
{
    Task<CurrentUser> CurrentUserInfo();
}
