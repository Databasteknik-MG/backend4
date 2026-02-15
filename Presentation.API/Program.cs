using Application.Instructors;
using Application.Modules.Instructors.Inputs;
using Domain.Instructors;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories.Instructors;
using Microsoft.EntityFrameworkCore;
using Presentation.API.Models.Instructors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddValidation();

builder.Services.AddDbContext<CourseOnlineDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("CourseOnlineDB")));

builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IInstructorService, InstructorService>();

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapPost("/api/instructors", async (CreateInstructorRequest request, IInstructorService service, CancellationToken ct) => {

    var input = new CreateInstructorInput(request.FirstName, request.LastName, request.Email,request.PhoneNumber);
    var result = await service.CreateAsync(input, ct);

    return result.Success ? Results.Created() : Results.BadRequest(result.Message);

});
app.MapGet("/api/instructors", async (IInstructorService service, CancellationToken ct) => {
    var result = await service.GetAllAsync(ct);
    return Results.Ok(result.Value);
});

app.Run();
