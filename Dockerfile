# Use the official ASP.NET Core runtime image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5078

# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project file to perform the restore
COPY ["Groko.Api/Groko.Api.csproj", "Groko.Api/"]

# Restore dependencies for the main project
WORKDIR /src/Groko.Api
RUN dotnet restore

# Copy the remaining files and build the project
COPY . .
RUN dotnet build "Groko.Api.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "Groko.Api.csproj" -c Release -o /app/publish

# Stage 3: Final image with published app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Groko.Api.dll"]
