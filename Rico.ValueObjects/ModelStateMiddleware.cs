using Microsoft.AspNetCore.Http;

namespace Rico.ValueObjects;

public sealed class ModelStateMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        context.Items[nameof(MinimalApiModelState)] = new MinimalApiModelState();

        await _next(context);
    }
}
