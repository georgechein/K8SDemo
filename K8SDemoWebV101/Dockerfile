FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY K8SDemoWebV101/K8SDemoWebV101.csproj K8SDemoWebV101/
RUN dotnet restore K8SDemoWebV101/K8SDemoWebV101.csproj
COPY . .
WORKDIR /src/K8SDemoWebV101
RUN dotnet build K8SDemoWebV101.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish K8SDemoWebV101.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "K8SDemoWebV101.dll"]
