using Microsoft.AspNetCore.Components;

namespace rei_logicaRazor.Pages;

public class Counter : ComponentBase
{
    protected int currentCount = 0;

    protected void IncrementCount()
    {
        currentCount++;
    }
}
