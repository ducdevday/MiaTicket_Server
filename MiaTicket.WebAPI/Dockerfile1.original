#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MiaTicket.WebAPI/MiaTicket.WebAPI.csproj", "MiaTicket.WebAPI/"]
COPY ["MiaTicket.BussinessLogic/MiaTicket.BussinessLogic.csproj", "MiaTicket.BussinessLogic/"]
COPY ["MiaTicket.CloudinaryStorage/MiaTicket.CloudinaryStorage.csproj", "MiaTicket.CloudinaryStorage/"]
COPY ["MiaTicket.Setting/MiaTicket.Setting.csproj", "MiaTicket.Setting/"]
COPY ["MiaTicket.DataAccess/MiaTicket.DataAccess.csproj", "MiaTicket.DataAccess/"]
COPY ["MiaTicket.DataCache/MiaTicket.DataCache.csproj", "MiaTicket.DataCache/"]
COPY ["MiaTicket.Data/MiaTicket.Data.csproj", "MiaTicket.Data/"]
COPY ["MiaTicket.Email/MiaTicket.Email.csproj", "MiaTicket.Email/"]
COPY ["MiaTicket.VNPay/MiaTicket.VNPay.csproj", "MiaTicket.VNPay/"]
COPY ["MiaTicket.ZaloPay/MiaTicket.ZaloPay.csproj", "MiaTicket.ZaloPay/"]
RUN dotnet restore "./MiaTicket.WebAPI/MiaTicket.WebAPI.csproj"
COPY . .
WORKDIR "/src/MiaTicket.WebAPI"
RUN dotnet build "./MiaTicket.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiaTicket.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiaTicket.WebAPI.dll"]