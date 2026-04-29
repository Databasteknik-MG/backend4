using Application.Common.Results;
using Application.Extensions;
using Application.Instructors.Contracts;
using Application.Instructors.Inputs;
using Infrastructure.Extensions;
using Presentation.API.Models.Instructors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication(builder.Configuration, builder.Environment);
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CourseOnline API");
        options.RoutePrefix = string.Empty;
    });
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// --- Instructor Group ---
var instructors = app.MapGroup("api/instructors")
    .WithTags("Instructors");

instructors.MapPost("/", async (CreateInstructorRequest request, IInstructorService service, CancellationToken ct) =>
{
    var input = new CreateInstructorInput(
        request.FirstName,
        request.LastName,
        request.Email,
        request.PhoneNumber,
        request.RoleId
    );

    var result = await service.CreateInstructorAsync(input, ct);

    if (!result.Success)
    {
        return result.ErrorType switch
        {
            ErrorTypes.NotFound => Results.NotFound(result.ErrorMessage),
            ErrorTypes.BadRequest => Results.BadRequest(result.ErrorMessage),
            ErrorTypes.Conflict => Results.Conflict(result.ErrorMessage),
            ErrorTypes.Unexpected => Results.Problem(result.ErrorMessage),
            _ => Results.Problem("Unknown error")
        };
    }

    return Results.Created($"/api/instructors/{result.Value?.Id}", new { id = result.Value?.Id });
});

instructors.MapGet("/", async (IInstructorService service, CancellationToken ct) =>
{
    var instructorsList = await service.GetInstructorsAsync(ct);
    return Results.Ok(instructorsList);
});

instructors.MapGet("/{id}", async (string id, IInstructorService service, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(id))
        return Results.BadRequest("Id is required.");

    var result = await service.GetInstructorByIdAsync(id, ct);
    if (!result.Success)
    {
        return result.ErrorType switch
        {
            ErrorTypes.NotFound => Results.NotFound(result.ErrorMessage),
            ErrorTypes.BadRequest => Results.BadRequest(result.ErrorMessage),
            ErrorTypes.Conflict => Results.Conflict(result.ErrorMessage),
            ErrorTypes.Unexpected => Results.Problem(result.ErrorMessage),
            _ => Results.Problem("Unknown error")
        };
    }

    return Results.Ok(result.Value);
});

instructors.MapGet("/email/{email}", async (string email, IInstructorService service, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(email))
        return Results.BadRequest("Email is required.");

    var result = await service.GetInstructorByEmailAsync(email, ct);
    if (!result.Success)
    {
        return result.ErrorType switch
        {
            ErrorTypes.NotFound => Results.NotFound(result.ErrorMessage),
            ErrorTypes.BadRequest => Results.BadRequest(result.ErrorMessage),
            ErrorTypes.Conflict => Results.Conflict(result.ErrorMessage),
            ErrorTypes.Unexpected => Results.Problem(result.ErrorMessage),
            _ => Results.Problem("Unknown error")
        };
    }

    return Results.Ok(result.Value);
});

instructors.MapDelete("/{id}", async (string id, IInstructorService service, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(id))
        return Results.BadRequest("Id is required.");

    var result = await service.DeleteInstructorAsync(id, ct);
    if (!result.Success)
    {
        return result.ErrorType switch
        {
            ErrorTypes.NotFound => Results.NotFound(result.ErrorMessage),
            ErrorTypes.BadRequest => Results.BadRequest(result.ErrorMessage),
            ErrorTypes.Conflict => Results.Conflict(result.ErrorMessage),
            ErrorTypes.Unexpected => Results.Problem(result.ErrorMessage),
            ErrorTypes.Error => Results.Problem(result.ErrorMessage),
            _ => Results.Problem("Unknown error")
        };
    }
    return Results.NoContent();
});


// --- Roles Group ---
var roles = app.MapGroup("api/instructorroles")
    .WithTags("Instructor Roles");

roles.MapPost("/", async (CreateInstructorRoleRequest request, IInstructorRoleService service, CancellationToken ct) =>
{
    var result = await service.CreateInstructorRoleAsync(request.RoleName, ct);

    if (!result.Success)
    {
        return result.ErrorType switch
        {
            ErrorTypes.NotFound => Results.NotFound(result.ErrorMessage),
            ErrorTypes.BadRequest => Results.BadRequest(result.ErrorMessage),
            ErrorTypes.Conflict => Results.Conflict(result.ErrorMessage),
            ErrorTypes.Unexpected => Results.Problem(result.ErrorMessage),
            ErrorTypes.Error => Results.Problem(result.ErrorMessage),
            _ => Results.Problem("Unknown error")
        };
    }

    return Results.Created($"/api/instructorroles/{result.Value}", new { id = result.Value });
});

roles.MapGet("/", async (IInstructorRoleService service, CancellationToken ct) =>
{
    var rolesList = await service.GetInstructorRolesAsync(ct);
    return Results.Ok(rolesList);
});

roles.MapDelete("/{id:int}", async (int id, IInstructorRoleService service, CancellationToken ct) =>
{
    if (id < 1)
        return Results.BadRequest("Valid Id is required.");

    var result = await service.DeleteInstructorRoleAsync(id, ct);
    if (!result.Success)
    {
        return result.ErrorType switch
        {
            ErrorTypes.NotFound => Results.NotFound(result.ErrorMessage),
            ErrorTypes.BadRequest => Results.BadRequest(result.ErrorMessage),
            ErrorTypes.Conflict => Results.Conflict(result.ErrorMessage),
            ErrorTypes.Unexpected => Results.Problem(result.ErrorMessage),
            ErrorTypes.Error => Results.Problem(result.ErrorMessage),
            _ => Results.Problem("Unknown error")
        };
    }
    return Results.NoContent();
});

app.Run();
