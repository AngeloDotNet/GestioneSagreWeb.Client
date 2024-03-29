FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebApp/GestioneSagre.Web.App.csproj", "src/WebApp/"]
COPY ["src/WebApp.Shared/GestioneSagre.Web.Shared.csproj", "src/WebApp.Shared/"]
COPY ["src/WebApp.Services/GestioneSagre.Web.Services.csproj", "src/WebApp.Services/"]
COPY ["src/WebApp.Models/GestioneSagre.Web.Models.csproj", "src/WebApp.Models/"]

# Copy the "module" projects loaded from the web app
COPY ["src/ModuliWeb/GestioneSagre.Modules.Dashboard/GestioneSagre.Modules.Dashboard.csproj", "src/ModuliWeb/GestioneSagre.Modules.Dashboard/"]
COPY ["src/ModuliWeb/GestioneSagre.Modules.ConfigurazioneIniziale/GestioneSagre.Modules.ConfigurazioneIniziale.csproj", "src/ModuliWeb/GestioneSagre.Modules.ConfigurazioneIniziale/"]
COPY ["src/ModuliWeb/GestioneSagre.Modules.Categorie/GestioneSagre.Modules.Categorie.csproj", "src/ModuliWeb/GestioneSagre.Modules.Categorie/"]
COPY ["src/ModuliWeb/GestioneSagre.Modules.Prodotti/GestioneSagre.Modules.Prodotti.csproj", "src/ModuliWeb/GestioneSagre.Modules.Prodotti/"]

RUN dotnet restore "src/WebApp/GestioneSagre.Web.App.csproj"
COPY . .

WORKDIR "/src/src/WebApp/"
RUN dotnet build "GestioneSagre.Web.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneSagre.Web.App.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .