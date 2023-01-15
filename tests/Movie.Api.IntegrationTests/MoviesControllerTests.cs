using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Movie.Api.Controllers;
using Movie.Api.Dto;
using Movie.Api.Infrastructure.Pagination;
using Movie.Api.Service;
using Movie.Infrastructure.Context;
using Movie.Infrastructure.Repository;
using NUnit.Framework;

namespace Movie.Api.IntegrationTests;

public class MoviesControllerTests
{
    private MoviesController _sut;
    private IServiceProvider _serviceProvider;

    [SetUp]
    public void Setup()
    {
        var servicesCollection = new ServiceCollection();
        servicesCollection.AddMediatR(typeof(MoviesController).Assembly);
        servicesCollection.AddSingleton<MovieDbContext>(_ => ContextFactory.CreateInMemory());
        servicesCollection.AddSingleton<IMovieRepository, MovieRepository>();
        servicesCollection.AddSingleton<IGuidTransformService, GuidTransformService>();

        _serviceProvider = servicesCollection.BuildServiceProvider();
    }

    [Test]
    [TestCase(2, 1, 2, 2)]
    [TestCase(10, 1, 10, 10)]
    [TestCase(100, 1, 10, 100)]
    public async Task GetAll_ShouldReturn_OkObjectResult(int movieCount, int page, int itemCount, int totalCount)
    {
        //Arrange
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        var movieDbContext = _serviceProvider.GetRequiredService<MovieDbContext>();
        movieDbContext.Set<global::Movie.Infrastructure.Domain.Movie>()
            .AddRange(Enumerable.Range(0, movieCount)
                .Select(index => new global::Movie.Infrastructure.Domain.Movie
                {
                    Id = Guid.NewGuid(),
                    Title = $"title {index}",
                    ReleaseDate = DateTime.Now
                }));
        await movieDbContext.SaveChangesAsync();
        _sut = new MoviesController(mediator);

        //Act
        var result = await _sut.GetAllAsync(null, page, itemCount).ConfigureAwait(false);

        //Assert
        using (new AssertionScope())
        {
            result.Should().BeOfType<OkObjectResult>();
            var value = result.As<OkObjectResult>().Value.As<PagedResponse<MovieListItemModel>>();
            value.Data.Should().HaveCount(itemCount);
            value.TotalCount.Should().Be(totalCount);
        }
    }
    
    [Test]
    public async Task GetMovieByIdAsync_ShouldReturn_OkObjectResult()
    {
        //Arrange
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        var movieDbContext = _serviceProvider.GetRequiredService<MovieDbContext>();
        var converter = _serviceProvider.GetRequiredService<IGuidTransformService>();

        var movie = new global::Movie.Infrastructure.Domain.Movie
        {
            Id = Guid.NewGuid(),
            Title = $"title 1",
            ReleaseDate = DateTime.Now
        };
        var uiId = converter.ToUriString(movie.Id);
        movieDbContext.Set<global::Movie.Infrastructure.Domain.Movie>()
            .Add(movie);
        await movieDbContext.SaveChangesAsync();
        _sut = new MoviesController(mediator);

        //Act
        var result = await _sut.GetMovieByIdAsync(uiId).ConfigureAwait(false);

        //Assert
        using (new AssertionScope())
        {
            result.Should().BeOfType<OkObjectResult>();
            var value = result.As<OkObjectResult>().Value.As<MovieDetailsModel>();
            value.Id.Should().Be(uiId);
        }
    }
    
    [Test]
    public async Task GetMovieByIdAsync_ShouldReturn_NotFoundResult()
    {
        //Arrange
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        _sut = new MoviesController(mediator);

        //Act
        var result = await _sut.GetMovieByIdAsync("SRhqjbphG0+JllKSUPhEcA").ConfigureAwait(false);

        //Assert
        using (new AssertionScope())
        {
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}