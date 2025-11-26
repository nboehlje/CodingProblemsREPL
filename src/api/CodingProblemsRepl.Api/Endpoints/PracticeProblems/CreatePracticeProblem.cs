
using CodingProblemsRepl.Api.Contracts.Requests;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CodingProblemsRepl.Api.Endpoints.PracticeProblems;

public static partial class PracticeProblems
{
    public static async Task<Results<Created, BadRequest>> CreatePracticeProblemAsync(
        CreatePracticeProblemRequest request)
    {
        return TypedResults.Created();
    }

}