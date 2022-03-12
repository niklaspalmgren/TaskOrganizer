#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaskOrganizer.Api/TaskOrganizer.Api.csproj", "TaskOrganizer.Api/"]
COPY ["TaskOrganizer.Shared/TaskOrganizer.Shared.csproj", "TaskOrganizer.Shared/"]
RUN dotnet restore "TaskOrganizer.Api/TaskOrganizer.Api.csproj"
COPY . .
WORKDIR "/src/TaskOrganizer.Api"
RUN dotnet build "TaskOrganizer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskOrganizer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskOrganizer.Api.dll"]