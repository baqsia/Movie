using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Api.Controllers;
using Movie.Api.Infrastructure;
using Movie.Api.Service;
using Movie.Infrastructure.Context;
using Movie.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

const string cors = "movies_api_cors_policy";
 
var allowedHosts = builder.Configuration.GetValue<string>("FrontendUrl");
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
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("MovieDatabase")!);
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

await ApplyMigrationAsync(app);

app.Run();

async Task ApplyMigrationAsync(IHost host)
{
    await using var scope = host.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
    var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
    {
        await db.Database.MigrateAsync().ConfigureAwait(false);
    }
}