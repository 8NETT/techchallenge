FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ["TechChallenge.sln", "./"]
COPY ["src/TechChallengeApi/WebApi.csproj", "src/TechChallengeApi/"]
COPY ["src/Core/Core.csproj", "src/Core/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["tests/WebApiTests/WebApiTests.csproj", "tests/WebApiTests/"]
COPY ["tests/CoreTests/CoreTests.csproj", "tests/CoreTests/"]
COPY ["tests/ApplicationTests/ApplicationTests.csproj", "tests/ApplicationTests/"]

RUN dotnet restore "TechChallenge.sln"

# Copy everything else and build
COPY . ./

# Run tests and coverage
# Run all tests defined in the solution file
RUN dotnet test "TechChallenge.sln" --no-restore --verbosity normal --collect:"XPlat Code Coverage"

# Build the application
RUN dotnet publish "src/TechChallengeApi/WebApi.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/publish .

ENTRYPOINT ["dotnet", "WebApi.dll"]