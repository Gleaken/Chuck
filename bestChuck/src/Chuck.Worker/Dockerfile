FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Chuck.Worker/Chuck.Worker.csproj", "Chuck.Worker/"]
COPY ["Chuck.Application/Chuck.Application.csproj", "Chuck.Application/"]
COPY ["Chuck.Infrastructure/Chuck.Infrastructure.csproj", "Chuck.Infrastructure/"]
RUN dotnet restore "Chuck.Worker/Chuck.Worker.csproj"
COPY . .
WORKDIR "/src/Chuck.Worker"
RUN dotnet build "Chuck.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chuck.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chuck.Worker.dll"]
