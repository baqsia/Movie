FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Movie.Api/Movie.Api.csproj", "Movie.Api/"]
COPY ["Movie.Infrastructure/Movie.Infrastructure.csproj", "Movie.Infrastructure/"]
RUN dotnet restore "Movie.Api/Movie.Api.csproj"
COPY . .
WORKDIR "/src/Movie.Api"
RUN dotnet build "Movie.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Movie.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Movie.Api.dll"]
