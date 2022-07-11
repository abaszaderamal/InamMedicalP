#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/Services/ToDoService/ToDoService.Api/ToDoService.Api.csproj", "Src/Services/ToDoService/ToDoService.Api/"]
COPY ["Src/SharedLib/Med.Shared/Med.Shared.csproj", "Src/SharedLib/Med.Shared/"]
RUN dotnet restore "Src/Services/ToDoService/ToDoService.Api/ToDoService.Api.csproj"
COPY . .
WORKDIR "/src/Src/Services/ToDoService/ToDoService.Api"
RUN dotnet build "ToDoService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoService.Api.dll"]