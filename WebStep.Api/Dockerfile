
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebStep.Api/WebStep.Api.csproj", "WebStep.Api/"]
COPY ["WebStep.Dto/WebStep.Dto.csproj", "WebStep.Dto/"]
RUN dotnet restore "WebStep.Api/WebStep.Api.csproj"
COPY . .
WORKDIR "/src/WebStep.Api"
RUN dotnet build "WebStep.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebStep.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebStep.Api.dll"]