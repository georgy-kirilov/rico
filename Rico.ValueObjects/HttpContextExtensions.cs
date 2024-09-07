using Microsoft.AspNetCore.Http;

namespace Rico.ValueObjects;

public static class HttpContextExtensions
{
    public static MinimalApiModelState GetModelState(this HttpContext context)
    {
        return context.Items[nameof(MinimalApiModelState)] as MinimalApiModelState ?? throw new ArgumentNullException("MinimalApiModelState cannot be null.");
    }
}
