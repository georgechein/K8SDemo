FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY K8SDemoAPI/K8SDemoAPI.csproj K8SDemoAPI/
RUN dotnet restore K8SDemoAPI/K8SDemoAPI.csproj
COPY . .
WORKDIR /src/K8SDemoAPI
RUN dotnet build K8SDemoAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish K8SDemoAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
RUN mkdir /mnt/kubeshare
ENTRYPOINT ["dotnet", "K8SDemoAPI.dll"]
