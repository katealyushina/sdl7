# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY *.csproj .
RUN dotnet restore "sdl7.csproj"

# Copy the rest of the application
COPY . .

# Build the application
RUN dotnet build "sdl7.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "sdl7.csproj" -c Release -o /app/publish

# Use the official .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port and run the application
EXPOSE 80
ENTRYPOINT ["dotnet", "sdl7.dll"]