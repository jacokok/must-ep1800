FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
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
