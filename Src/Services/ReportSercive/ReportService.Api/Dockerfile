#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/Services/ReportSercive/ReportService.Api/ReportService.Api.csproj", "Src/Services/ReportSercive/ReportService.Api/"]
COPY ["Src/Services/ReportSercive/ReportService.Api.Businness/ReportService.Api.Businness.csproj", "Src/Services/ReportSercive/ReportService.Api.Businness/"]
COPY ["Src/Services/ReportSercive/ReportService.Api.Data/ReportService.Api.Data.csproj", "Src/Services/ReportSercive/ReportService.Api.Data/"]
COPY ["Src/Services/ReportSercive/ReportService.Api.Core/ReportService.Api.Core.csproj", "Src/Services/ReportSercive/ReportService.Api.Core/"]
COPY ["Src/SharedLib/Med.Shared/Med.Shared.csproj", "Src/SharedLib/Med.Shared/"]
RUN dotnet restore "Src/Services/ReportSercive/ReportService.Api/ReportService.Api.csproj"
COPY . .
WORKDIR "/src/Src/Services/ReportSercive/ReportService.Api"
RUN dotnet build "ReportService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.Api.dll"]