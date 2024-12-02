FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY DataCore ./DataCore
COPY Shared ./Shared

WORKDIR /app/DataCore/DataCoreModule.WEB

RUN chmod +x Run.sh
RUN chmod +x efbundle
RUN apt-get update && apt-get install -y dos2unix
RUN dos2unix Run.sh
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish /app
COPY --from=build /app/DataCore/DataCoreModule.WEB/efbundle /app
COPY --from=build /app/DataCore/DataCoreModule.WEB/Run.sh /app
ENTRYPOINT ["./Run.sh"]

EXPOSE 1339 1400