FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FastTechFoods.ProductsService.API/FastTechFoods.ProductsService.API.csproj FastTechFoods.ProductsService.API/
COPY FastTechFoods.ProductsService.Application/FastTechFoods.ProductsService.Application.csproj FastTechFoods.ProductsService.Application/
COPY FastTechFoods.ProductsService.Domain/FastTechFoods.ProductsService.Domain.csproj FastTechFoods.ProductsService.Domain/
COPY FastTechFoods.ProductsService.Infrastructure/FastTechFoods.ProductsService.Infrastructure.csproj FastTechFoods.ProductsService.Infrastructure/
COPY FastTechFoods.SDK/FastTechFoods.SDK.csproj FastTechFoods.SDK/

RUN dotnet restore FastTechFoods.ProductsService.API/FastTechFoods.ProductsService.API.csproj

COPY . .
WORKDIR /src/FastTechFoods.ProductsService.API
RUN dotnet build "FastTechFoods.ProductsService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FastTechFoods.ProductsService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastTechFoods.ProductsService.API.dll"]