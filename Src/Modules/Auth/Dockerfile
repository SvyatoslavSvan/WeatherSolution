FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .
RUN chmod +x Run.sh
RUN chmod +x efbundle
RUN apt-get update && apt-get install -y dos2unix
RUN dos2unix Run.sh
RUN dotnet publish -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish /app
COPY --from=build /app/efbundle /app
COPY --from=build /app/Run.sh /app

EXPOSE 1401 1402

ENTRYPOINT ["./Run.sh"]