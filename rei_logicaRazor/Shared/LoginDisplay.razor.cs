using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace rei_logicaRazor.Shared;

public partial class LoginDisplay : ComponentBase
{
    [Inject]
    protected NavigationManager? Navigation { get; set; }

    protected void BeginLogOut()
    {
        Navigation.NavigateToLogout("authentication/logout");
    }
}
