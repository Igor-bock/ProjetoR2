using Maui_Lib.Models;

namespace Maui_Lib.Services;

public interface IAuthService
{
    Task<CurrentUser> CurrentUserInfo();
}
