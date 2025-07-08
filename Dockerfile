FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FastTechFoods.ProductsService.sln ./

COPY FastTechFoods.ProductsService.API/*.csproj FastTechFoods.ProductsService.API/
COPY FastTechFoods.ProductsService.Application/*.csproj FastTechFoods.ProductsService.Application/
COPY FastTechFoods.ProductsService.Domain/*.csproj FastTechFoods.ProductsService.Domain/
COPY FastTechFoods.ProductsService.Infrastructure/*.csproj FastTechFoods.ProductsService.Infrastructure/
COPY FastTechFoods.ProductsService.Tests/*.csproj FastTechFoods.ProductsService.Tests/
COPY FastTechFoods.SDK/*.csproj FastTechFoods.SDK/

COPY nuget.config ./

ARG NUGET_TOKEN

RUN dotnet nuget add source \
    --username ErickGoldberg \
    --password $NUGET_TOKEN \
    --store-password-in-clear-text \
    --name github \
    https://nuget.pkg.github.com/caiofabiogomes/index.json

RUN dotnet restore FastTechFoods.ProductsService.sln

COPY . .

RUN dotnet publish FastTechFoods.ProductsService.API/FastTechFoods.ProductsService.API.csproj -c Release -o /app/publish-api

FROM base AS final
WORKDIR /app

ARG PROJECT=api
ENV PROJECT=$PROJECT

COPY --from=build /app/publish-api ./api

CMD ["dotnet", "./api/FastTechFoods.ProductsService.API.dll"]
