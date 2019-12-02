FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["DevOps.Products.Products.REST.API/DevOps.Products.Products.REST.API.csproj", "DevOps.Products.Products.REST.API/"]
COPY ["DevOps.Products.Products.DAL/DevOps.Products.Products.DAL.csproj", "DevOps.Products.Products.DAL/"]
COPY ["DevOps.Products.Common.Repository/DevOps.Products.Common.Repository.csproj", "DevOps.Products.Common.Repository/"]
RUN dotnet restore "DevOps.Products.Products.REST.API/DevOps.Products.Products.REST.API.csproj"
COPY . .
WORKDIR "/src/DevOps.Products.Products.REST.API"
RUN dotnet build "DevOps.Products.Products.REST.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevOps.Products.Products.REST.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevOps.Products.Products.REST.API.dll"]