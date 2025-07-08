FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

ARG NUGET_TOKEN

RUN dotnet nuget add source \
    --username ErickGoldberg \
    --password $NUGET_TOKEN \
    --store-password-in-clear-text \
    --name github \
    https://nuget.pkg.github.com/caiofabiogomes/index.json

RUN dotnet restore FastTechFoods.ProductsService.sln

COPY . .

RUN dotnet publish FastTechFoods.ProductsService.API/FastTechFoods.ProductsService.API.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish ./

CMD ["dotnet", "./api/FastTechFoods.ProductsService.API.dll"]
