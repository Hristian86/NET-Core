#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MBshop/MBshop.csproj", "MBshop/"]
COPY ["MBshop.Service/MBshop.Service.csproj", "MBshop.Service/"]
COPY ["MBshop.Models/MBshop.Models.csproj", "MBshop.Models/"]
COPY ["MBshop.Data/MBshop.Data.csproj", "MBshop.Data/"]
RUN dotnet restore "MBshop/MBshop.csproj"
COPY . .
WORKDIR "/src/MBshop"
RUN dotnet build "MBshop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MBshop.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MBshop.dll"]