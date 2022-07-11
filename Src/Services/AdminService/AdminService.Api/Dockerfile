#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/Services/AdminService/AdminService.Api/AdminService.Api.csproj", "Src/Services/AdminService/AdminService.Api/"]
COPY ["Src/Services/AdminService/AdminService.Api.Business/AdminService.Api.Business.csproj", "Src/Services/AdminService/AdminService.Api.Business/"]
COPY ["Src/Services/AdminService/AdminService.Api.Data/AdminService.Api.Data.csproj", "Src/Services/AdminService/AdminService.Api.Data/"]
COPY ["Src/Services/AdminService/AdminService.Api.Core/AdminService.Api.Core.csproj", "Src/Services/AdminService/AdminService.Api.Core/"]
COPY ["Src/SharedLib/Med.Shared/Med.Shared.csproj", "Src/SharedLib/Med.Shared/"]
RUN dotnet restore "Src/Services/AdminService/AdminService.Api/AdminService.Api.csproj"
COPY . .
WORKDIR "/src/Src/Services/AdminService/AdminService.Api"
RUN dotnet build "AdminService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminService.Api.dll"]