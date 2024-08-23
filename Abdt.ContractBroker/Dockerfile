FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Abdt.ContractBroker/Abdt.ContractBroker.csproj", "Abdt.ContractBroker/"]
RUN dotnet restore "./Abdt.ContractBroker/Abdt.ContractBroker.csproj"
COPY . .
WORKDIR "/src/Abdt.ContractBroker"
RUN dotnet build "./Abdt.ContractBroker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Abdt.ContractBroker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Abdt.ContractBroker.dll"]