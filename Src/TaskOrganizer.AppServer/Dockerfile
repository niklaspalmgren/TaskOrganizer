#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaskOrganizer.AppServer/TaskOrganizer.AppServer.csproj", "TaskOrganizer.AppServer/"]
COPY ["TaskOrganizer.Shared/TaskOrganizer.Shared.csproj", "TaskOrganizer.Shared/"]
RUN dotnet restore "TaskOrganizer.AppServer/TaskOrganizer.AppServer.csproj"
COPY . .
WORKDIR "/src/TaskOrganizer.AppServer"
RUN dotnet build "TaskOrganizer.AppServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskOrganizer.AppServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskOrganizer.AppServer.dll"]