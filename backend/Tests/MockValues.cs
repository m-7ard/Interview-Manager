using Application.Errors;

namespace Tests;

public static class MockValues 
{
    public static readonly ApplicationError EmptyApplicationError = new ApplicationError(message: "", code: "", path: []);
}