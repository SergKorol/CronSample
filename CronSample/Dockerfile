FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CronSample/CronSample.csproj", "CronSample/"]
RUN dotnet restore "CronSample/CronSample.csproj"
COPY . .
WORKDIR "/src/CronSample"
RUN dotnet build "CronSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CronSample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CronSample.dll"]
