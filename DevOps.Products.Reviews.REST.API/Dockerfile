FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["DevOps.Products.Reviews.REST.API/DevOps.Products.Reviews.REST.API.csproj", "DevOps.Products.Reviews.REST.API/"]
COPY ["DevOps.Products.Reviews.DAL/DevOps.Products.Reviews.DAL.csproj", "DevOps.Products.Reviews.DAL/"]
COPY ["DevOps.Products.Common.Repository/DevOps.Products.Common.Repository.csproj", "DevOps.Products.Common.Repository/"]
RUN dotnet restore "DevOps.Products.Reviews.REST.API/DevOps.Products.Reviews.REST.API.csproj"
COPY . .
WORKDIR "/src/DevOps.Products.Reviews.REST.API"
RUN dotnet build "DevOps.Products.Reviews.REST.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevOps.Products.Reviews.REST.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevOps.Products.Reviews.REST.API.dll"]