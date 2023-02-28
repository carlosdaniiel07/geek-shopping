FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["GeekShopping.Product.Api/GeekShopping.Product.Api.csproj", "GeekShopping.Product.Api/"]
COPY ["GeekShopping.Shared/GeekShopping.Shared.csproj", "GeekShopping.Shared/"]

RUN dotnet restore "GeekShopping.Product.Api/GeekShopping.Product.Api.csproj"

COPY . .

WORKDIR "/src/GeekShopping.Product.Api"

RUN dotnet build "GeekShopping.Product.Api.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "GeekShopping.Product.Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "GeekShopping.Product.Api.dll"]