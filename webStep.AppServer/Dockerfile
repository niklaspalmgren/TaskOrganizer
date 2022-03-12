#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["webStep.AppServer/webStep.AppServer.csproj", "webStep.AppServer/"]
COPY ["WebStep.Dto/WebStep.Dto.csproj", "WebStep.Dto/"]
RUN dotnet restore "webStep.AppServer/webStep.AppServer.csproj"
COPY . .
WORKDIR "/src/webStep.AppServer"
RUN dotnet build "webStep.AppServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "webStep.AppServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "webStep.AppServer.dll"]