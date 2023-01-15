FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["src/Movie.Api/Movie.Api.csproj", "src/Movie.Api/"]
COPY ["src/Movie.Infrastructure/Movie.Infrastructure.csproj", "src/Movie.Infrastructure/"]
RUN dotnet restore "src/Movie.Api/Movie.Api.csproj"
COPY . .
WORKDIR "src/Movie.Api"
RUN dotnet build "Movie.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Movie.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Movie.Api.dll"]
