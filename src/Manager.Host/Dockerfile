FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Manager.Host/Manager.Host.csproj", "Manager.Host/"]
COPY ["AzureKeyVault/AzureKeyVault.csproj", "AzureKeyVault/"]
COPY ["BlobStorage.Accessor.Host/BlobStorage.Accessor.Host.csproj", "BlobStorage.Accessor.Host/"]
COPY ["BlobStorage.Accessor.Contracts/BlobStorage.Accessor.Contracts.csproj", "BlobStorage.Accessor.Contracts/"]
COPY ["ExecutionPipeline/ExecutionPipeline.csproj", "ExecutionPipeline/"]
COPY ["BlobStorage.Accessor.Service/BlobStorage.Accessor.Service.csproj", "BlobStorage.Accessor.Service/"]
COPY ["History.Accessor.Contracts/History.Accessor.Contracts.csproj", "History.Accessor.Contracts/"]
COPY ["History.Accessor.Host/History.Accessor.Host.csproj", "History.Accessor.Host/"]
COPY ["History.Accessor.Service/History.Accessor.Service.csproj", "History.Accessor.Service/"]
COPY ["Localization.Accessor.Host/Localization.Accessor.Host.csproj", "Localization.Accessor.Host/"]
COPY ["Localization.Accessor.Contracts/Localization.Accessor.Contracts.csproj", "Localization.Accessor.Contracts/"]
COPY ["Localization.Accessor.Service/Localization.Accessor.Service.csproj", "Localization.Accessor.Service/"]
COPY ["LoggerInfrastructure/LoggerInfrastructure.csproj", "LoggerInfrastructure/"]
COPY ["MessageDispatcher/MessageDispatcher.csproj", "MessageDispatcher/"]
COPY ["MessageDispater.Contracts/MessageDispater.Contracts.csproj", "MessageDispater.Contracts/"]
COPY ["Manager.Service/Manager.Service.csproj", "Manager.Service/"]
COPY ["SessionInfoExtractor/SessionInfoExtractor.csproj", "SessionInfoExtractor/"]
RUN dotnet restore "Manager.Host/Manager.Host.csproj"
COPY . .
WORKDIR "/src/Manager.Host"
RUN dotnet build "Manager.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Manager.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Manager.Host.dll"]
