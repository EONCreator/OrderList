FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrderList.WebApi/OrderList.WebApi.csproj", "OrderList.WebApi/"]
COPY ["OrderList.Infrastructure/OrderList.Infrastructure.csproj", "OrderList.Infrastructure/"]
COPY ["OrderList.Services/OrderList.Services.csproj", "OrderList.Services/"]
COPY ["OrderList.Application/OrderList.Application.csproj", "OrderList.Application/"]
RUN dotnet restore "OrderList.WebApi/OrderList.WebApi.csproj" --verbosity detailed
COPY . .
WORKDIR "/src/OrderList.WebApi"
RUN dotnet build "OrderList.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OrderList.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:8080
ENTRYPOINT ["dotnet", "OrderList.WebApi.dll"]
