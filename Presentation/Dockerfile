# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY IdentityApp.sln ./
COPY Domain/Domain.csproj ./Domain/
COPY Application/Application.csproj ./Application/
COPY Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY Presentation/Presentation.csproj ./Presentation/

RUN dotnet restore

# Copy everything else and build
COPY . .

RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port your application will run on
EXPOSE 5186

# Start the application
ENTRYPOINT ["dotnet", "Presentation.dll"]
