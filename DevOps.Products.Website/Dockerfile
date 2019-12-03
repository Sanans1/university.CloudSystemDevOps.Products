FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["DevOps.Products.Website/DevOps.Products.Website.csproj", "DevOps.Products.Website/"]
RUN dotnet restore "DevOps.Products.Website/DevOps.Products.Website.csproj"
COPY . .
WORKDIR "/src/DevOps.Products.Website"
RUN dotnet build "DevOps.Products.Website.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevOps.Products.Website.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevOps.Products.Website.dll"]