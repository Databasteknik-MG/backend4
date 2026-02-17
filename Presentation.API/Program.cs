using Application.Instructors;
using Application.Instructors.Inputs;
using Application.Modules.Instructors.Inputs;
using Domain.Instructors.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories.Instructors;
using Microsoft.EntityFrameworkCore;

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



app.Run();
