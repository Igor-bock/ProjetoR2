using Microsoft.AspNetCore.Components;

namespace rei_logicaRazor.Shared;

public class RedirectToLogin : ComponentBase
{
    [Inject]
    protected NavigationManager Navigation { get; set; }

    protected override void OnInitialized()
    {
        Navigation.NavigateTo("authentication/login");
    }
}
