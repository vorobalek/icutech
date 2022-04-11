FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IcuTech.Web/IcuTech.Web.csproj", "IcuTech.Web/"]
RUN dotnet restore "IcuTech.Web/IcuTech.Web.csproj"
COPY . .
WORKDIR "/src/IcuTech.Web"
RUN dotnet build "IcuTech.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IcuTech.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IcuTech.Web.dll"]
