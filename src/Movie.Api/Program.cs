using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Api.Controllers;
using Movie.Api.Infrastructure;
using Movie.Api.Service;
using Movie.Infrastructure.Context;
using Movie.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

const string cors = "movies_api_cors_policy";

var allowedHosts = builder.Configuration.GetSection("FrontendUrls").Get<string[]>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(MoviesController).Assembly);
builder.Services.AddCors(setup =>
{
    setup.AddPolicy(cors, configure =>
    {
        configure.WithOrigins(allowedHosts!)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddDbContextPool<MovieDbContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("MovieDatabase")!,
        optionsBuilder =>
        {
            optionsBuilder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
});
builder.Services.AddScoped<IGuidTransformService, GuidTransformService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseCors(cors);
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();