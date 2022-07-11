#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/Services/IdentityService/IdentityService.Api/IdentityService.Api.csproj", "Src/Services/IdentityService/IdentityService.Api/"]
COPY ["Src/SharedLib/Med.Shared/Med.Shared.csproj", "Src/SharedLib/Med.Shared/"]
RUN dotnet restore "Src/Services/IdentityService/IdentityService.Api/IdentityService.Api.csproj"
COPY . .
WORKDIR "/src/Src/Services/IdentityService/IdentityService.Api"
RUN dotnet build "IdentityService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.Api.dll"]