#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -yq nodejs build-essential

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -yq nodejs build-essential
WORKDIR /src
COPY ["src/ApiTest.Api/ApiTest.Api.csproj", "src/ApiTest.Api/"]
COPY ["src/ApiTest.Commands/ApiTest.Commands.csproj", "src/ApiTest.Commands/"]
COPY ["src/ApiTest.Models/ApiTest.Models.csproj", "src/ApiTest.Models/"]
COPY ["src/ApiTest.Utils/ApiTest.Utils.csproj", "src/ApiTest.Utils/"]
COPY ["src/ApiTest.Queries/ApiTest.Queries.csproj", "src/ApiTest.Queries/"]
RUN dotnet restore "src/ApiTest.Api/ApiTest.Api.csproj"
COPY . .
WORKDIR "/src/src/ApiTest.Api"
RUN dotnet build "ApiTest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiTest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiTest.Api.dll"]