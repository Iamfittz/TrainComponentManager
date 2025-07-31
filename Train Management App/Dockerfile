# 1. .NET SDK base image (for assembly)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 2. Copy and assemble
COPY . ./
RUN dotnet publish -c Release -o out

# 3. Runtime image - minimal
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# 4. Open the port
EXPOSE 8080

# 5. Run
ENTRYPOINT ["dotnet", "Train Management App.dll"]
