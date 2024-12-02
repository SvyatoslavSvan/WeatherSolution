FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY WeatherApi ./WeatherApi
COPY Shared ./Shared

WORKDIR /app/WeatherApi

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 7068 5142

ENTRYPOINT ["dotnet", "WeatherForecast.dll"]
