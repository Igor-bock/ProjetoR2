using rei_logicaRazor.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace rei_logicaRazor.Pages;

public partial class FetchData : ComponentBase
{
    [Inject]
    public HttpClient Http { get; set; }
    protected List<Cliente>? clientes;

    protected override async Task OnInitializedAsync()
    {
        clientes = await Http.GetFromJsonAsync<List<Cliente>>("clientes");
    }
}
