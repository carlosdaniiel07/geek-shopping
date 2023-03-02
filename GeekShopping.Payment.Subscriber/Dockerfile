FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["GeekShopping.Payment.Subscriber/GeekShopping.Payment.Subscriber.csproj", "GeekShopping.Payment.Subscriber/"]
COPY ["GeekShopping.Shared/GeekShopping.Shared.csproj", "GeekShopping.Shared/"]

RUN dotnet restore "GeekShopping.Payment.Subscriber/GeekShopping.Payment.Subscriber.csproj"

COPY . .

WORKDIR "/src/GeekShopping.Payment.Subscriber"

RUN dotnet build "GeekShopping.Payment.Subscriber.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "GeekShopping.Payment.Subscriber.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "GeekShopping.Payment.Subscriber.dll"]