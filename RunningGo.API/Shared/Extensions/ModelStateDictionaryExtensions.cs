using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RunningGo.API.Shared.Extensions;

public static class ModelStateDictionaryExtensions
{
    public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
    {
        return dictionary.SelectMany(m => m.Value.Errors)
            .Select(m => m.ErrorMessage)
            .ToList();
    }
}