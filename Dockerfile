FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app

COPY . .

RUN dotnet restore "GetApi.Ecommerce.Api/GetApi.Ecommerce.Api.csproj"

RUN dotnet test

RUN dotnet build "GetApi.Ecommerce.Api/GetApi.Ecommerce.Api.csproj" -c Release -o /app/build

FROM build-env AS publish
RUN dotnet publish "GetApi.Ecommerce.Api/GetApi.Ecommerce.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS http://*:$PORT

ENTRYPOINT ["dotnet", "GetApi.Ecommerce.Api.dll"]