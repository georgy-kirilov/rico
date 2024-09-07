using ErrorOr;

namespace Rico.ValueObjects;

public sealed class MinimalApiModelState
{
    private readonly Dictionary<string, HashSet<Error>> _errorsByProperty = [];

    public IReadOnlyDictionary<string, HashSet<Error>> ErrorsByProperty => _errorsByProperty.ToDictionary().AsReadOnly();

    public void AddError(string property, Error error)
    {
        if (!_errorsByProperty.TryGetValue(property, out HashSet<Error>? value))
        {
            value = ([]);
            _errorsByProperty.Add(property, value);
        }

        value.Add(error);
    }

    public bool HasErrors => _errorsByProperty.Count != 0;
}
