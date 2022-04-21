FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["must-ep1800.csproj", "./"]
RUN dotnet restore "must-ep1800.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "must-ep1800.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "must-ep1800.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "must-ep1800.dll"]
