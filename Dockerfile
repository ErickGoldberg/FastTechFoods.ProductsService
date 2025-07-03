# Dockerfile 

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FastTechFoods.ProductsService.API/*.csproj FastTechFoods.ProductsService.API/
COPY FastTechFoods.ProductsService.Worker/*.csproj FastTechFoods.ProductsService.Worker/
COPY FastTechFoods.ProductsService.Application/*.csproj FastTechFoods.ProductsService.Application/
COPY FastTechFoods.ProductsService.Domain/*.csproj FastTechFoods.ProductsService.Domain/
COPY FastTechFoods.ProductsService.Infrastructure/*.csproj FastTechFoods.ProductsService.Infrastructure/
COPY FastTechFoods.SDK/*.csproj FastTechFoods.SDK/

COPY nuget.config ./

RUN dotnet restore FastTechFoods.ProductsService.API/FastTechFoods.ProductsService.API.csproj
RUN dotnet restore FastTechFoods.ProductsService.Worker/FastTechFoods.ProductsService.Worker.csproj

COPY . .

RUN dotnet publish FastTechFoods.ProductsService.API/FastTechFoods.ProductsService.API.csproj -c Release -o /app/publish-api
RUN dotnet publish FastTechFoods.ProductsService.Worker/FastTechFoods.ProductsService.Worker.csproj -c Release -o /app/publish-worker

FROM base AS final
WORKDIR /app

ARG PROJECT=api
ENV PROJECT=$PROJECT

COPY --from=build /app/publish-api ./api
COPY --from=build /app/publish-worker ./worker

CMD if [ "$PROJECT" = "worker" ]; then \
    dotnet ./worker/FastTechFoods.ProductsService.Worker.dll; \
  else \
    dotnet ./api/FastTechFoods.ProductsService.API.dll; \
  fi
