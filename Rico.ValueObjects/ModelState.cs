using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace Rico.ValueObjects;

internal sealed class ModelState(IHttpContextAccessor _httpContextAccessor) : IModelState
{
    private bool _validatedNullables = false;

    public IResult ToApiResult<T>(T model)
    {
        if (HasErrors(model))
        {
            return Results.UnprocessableEntity(_httpContextAccessor.HttpContext?.GetModelState().ErrorsByProperty);
        }

        throw new Exception();
    }

    public bool HasErrors<T>(T model)
    {
        var modelState = _httpContextAccessor.HttpContext?.GetModelState();

        if (!_validatedNullables)
        {
            var props = typeof(T)
                .GetProperties()
                .Where(prop => prop.GetValue(model) is null && Nullable.GetUnderlyingType(prop.PropertyType) is null);

            var errors = modelState!.ErrorsByProperty;

            foreach (var prop in props)
            {
                if (!errors.ContainsKey(prop.PropertyType.Name))
                {
                    modelState!.AddError(prop.PropertyType.Name, Error.Validation("FieldIsRequired"));
                }
            }

            _validatedNullables = true;
        }

        return modelState!.HasErrors;
    }
}
