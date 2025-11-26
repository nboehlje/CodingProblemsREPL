namespace CodingProblemsRepl.Api.Endpoints.PracticeProblems; 

public static class PracticeProblemEndpoints
{
    public static void RegisterPracticeProblemEndpoints(this WebApplication app)
    {
        var routeBuilder = app.MapGroup("/PracticeProblems");

        routeBuilder.MapPost("/", PracticeProblems.CreatePracticeProblemAsync);
    }
}