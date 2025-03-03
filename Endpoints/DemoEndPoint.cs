using afi_demo.Models.Requests;
using afi_demo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace afi_demo.Endpoints;

public static class DemoEndPoint
{

    public static void RegisterDemoEndPoints(this WebApplication app)
    {
        string groupTagName = "Demo";


        app.MapPost("demo/{validateAll}", async Task<IResult> ([FromRoute] bool validateAll,
                                                               [FromBody] RegisterUserRequest model,
                                                               [FromServices] IDemoService service) =>
        {
            var res = await service.RegisterNewUser(model, validateAll);
            if (res != null)
                return Results.Created(new Uri(""), res.Value);
            else
                return Results.BadRequest();

        })
        .WithName("RegisterUser")
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Register a user demo for AFI",
            Description = "Register a user demo for AFI.",
            Tags = new List<OpenApiTag> { new() { Name = groupTagName } }
        });

    }
}



