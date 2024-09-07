using Microsoft.AspNetCore.Http;

namespace Rico.ValueObjects;

public interface IModelState
{
    bool HasErrors<T>(T model);

    IResult ToApiResult<T>(T model);
}
