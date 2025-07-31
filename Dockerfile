# 1. .NET SDK base image for build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copy project file and restore dependencies
COPY ["Train Management App/Train Management App.csproj", "Train Management App/"]
RUN dotnet restore "Train Management App/Train Management App.csproj"

# 3. Copy everything and publish the project
COPY . .
RUN dotnet publish "Train Management App/Train Management App.csproj" -c Release -o /app/publish

# 4. Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# 5. Expose default port
EXPOSE 8080

# 6. Run the app
ENTRYPOINT ["dotnet", "Train Management App.dll"]
