using Api.Errors;
using Application.Common;
using Application.Errors;
using FluentValidation.Results;

namespace Api.Services;

public class ApiErrorService
{
    public static List<ApiError> FluentToApiErrors(List<ValidationFailure> validationFailures, List<string> path)
    {
        return validationFailures.Select((error) =>
        {
            var camelCaseFieldName = StringCaseConverter.ToCamelCase(error.PropertyName);
            var fullPath = path.Count == 0
                ? camelCaseFieldName
                : $"/{camelCaseFieldName}/{string.Join("/", path)}";

            return new ApiError(
                code: ApiErrorCodes.VALIDATION_ERROR,
                path: fullPath,
                message: error.ErrorMessage
            );
        }).ToList();
    }

    public static List<ApiError> MapApplicationErrors(List<ApplicationError> errors, List<string>? pathPrefix = null)
    {
        var result = new List<ApiError>();

        errors.ForEach((error) =>
        {
            List<string> finalPath = [..error.Path];

            if (finalPath.Count == 0)
            {
                // as prefix or form error
                finalPath = pathPrefix ?? ["_"];        
            }
            else if (pathPrefix is not null)
            {
                // insert path
                finalPath.InsertRange(0, pathPrefix);
            }

            var apiError = new ApiError(
                message: error.Message,
                path: string.Join("/", finalPath),
                code: ApiErrorCodes.APPLICATION_ERROR
            );
            
            result.Add(apiError);
        });

        return result;
    }
}