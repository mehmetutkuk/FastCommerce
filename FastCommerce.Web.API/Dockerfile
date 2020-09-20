#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FastCommerce.Web.API/FastCommerce.Web.API.csproj", "FastCommerce.Web.API/"]
COPY ["FastCommerce.Entities/FastCommerce.Entities.csproj", "FastCommerce.Entities/"]
COPY ["FastCommerce.DAL/FastCommerce.DAL.csproj", "FastCommerce.DAL/"]
COPY ["FastCommerce.UnitTests/FastCommerce.UnitTests.csproj", "FastCommerce.UnitTests/"]
RUN dotnet restore "FastCommerce.Web.API/FastCommerce.Web.API.csproj"
RUN dotnet restore "FastCommerce.UnitTests/FastCommerce.UnitTests.csproj"
COPY . . 
RUN dotnet build "FastCommerce.Web.API/FastCommerce.Web.API.csproj" -c Release -o /app/build
RUN dotnet build "FastCommerce.UnitTests/FastCommerce.UnitTests.csproj" -c Release -o /app/build

RUN dotnet test "FastCommerce.UnitTests/FastCommerce.UnitTests.csproj" --logger "trx;LogFileName=fastcommercelog.trx" 


FROM build AS publish
RUN dotnet publish "FastCommerce.Web.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastCommerce.Web.API.dll", "--urls", "http://*:5000;http://*:5001"]

