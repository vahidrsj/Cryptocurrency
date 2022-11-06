#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cryptocurrency.ConsoleUI/Cryptocurrency.ConsoleUI.csproj", "Cryptocurrency.ConsoleUI/"]
COPY ["Cryptocurrency.Application/Cryptocurrency.Application.csproj", "Cryptocurrency.Application/"]
COPY ["Cryptocurrency.Core/Cryptocurrency.Core.csproj", "Cryptocurrency.Core/"]
COPY ["Cryptocurrency.Infrastructure/Cryptocurrency.Infrastructure.csproj", "Cryptocurrency.Infrastructure/"]
RUN dotnet restore "Cryptocurrency.ConsoleUI/Cryptocurrency.ConsoleUI.csproj"
COPY . .
WORKDIR "/src/Cryptocurrency.ConsoleUI"
RUN dotnet build "Cryptocurrency.ConsoleUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cryptocurrency.ConsoleUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cryptocurrency.ConsoleUI.dll"]