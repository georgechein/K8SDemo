FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY K8SDemoWebV1/K8SDemoWebV1.csproj K8SDemoWebV1/
RUN dotnet restore K8SDemoWebV1/K8SDemoWebV1.csproj
COPY . .
WORKDIR /src/K8SDemoWebV1
RUN dotnet build K8SDemoWebV1.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish K8SDemoWebV1.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "K8SDemoWebV1.dll"]
