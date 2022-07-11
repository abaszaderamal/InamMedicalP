#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/Services/DXOperationService/DXOperationService.Api/DXOperationService.Api.csproj", "Src/Services/DXOperationService/DXOperationService.Api/"]
COPY ["Src/Services/DXOperationService/DXOperationService.Api.Business/DXOperationService.Api.Business.csproj", "Src/Services/DXOperationService/DXOperationService.Api.Business/"]
COPY ["Src/Services/DXOperationService/DXOperationService.Api.Data/DXOperationService.Api.Data.csproj", "Src/Services/DXOperationService/DXOperationService.Api.Data/"]
COPY ["Src/Services/DXOperationService/DXOperationService.Api.Core/DXOperationService.Api.Core.csproj", "Src/Services/DXOperationService/DXOperationService.Api.Core/"]
COPY ["Src/SharedLib/Med.Shared/Med.Shared.csproj", "Src/SharedLib/Med.Shared/"]
RUN dotnet restore "Src/Services/DXOperationService/DXOperationService.Api/DXOperationService.Api.csproj"
COPY . .
WORKDIR "/src/Src/Services/DXOperationService/DXOperationService.Api"
RUN dotnet build "DXOperationService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DXOperationService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DXOperationService.Api.dll"]