using BlazorIdentity.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorIdentity.Pages;

public partial class FetchData
{
    [Inject]
    public HttpClient Http { get; set; }
    private List<Cliente>? clientes;

    protected override async Task OnInitializedAsync()
    {
        clientes = await Http.GetFromJsonAsync<List<Cliente>>("clientes");
    }
}
