FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["GeekShopping.Order.Api/GeekShopping.Order.Api.csproj", "GeekShopping.Order.Api/"]
COPY ["GeekShopping.Shared/GeekShopping.Shared.csproj", "GeekShopping.Shared/"]

RUN dotnet restore "GeekShopping.Order.Api/GeekShopping.Order.Api.csproj"

COPY . .

WORKDIR "/src/GeekShopping.Order.Api"

RUN dotnet build "GeekShopping.Order.Api.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "GeekShopping.Order.Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "GeekShopping.Order.Api.dll"]