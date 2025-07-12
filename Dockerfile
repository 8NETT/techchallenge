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

# Install the agent
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y 'newrelic-dotnet-agent' \
&& rm -rf /var/lib/apt/lists/*

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so

WORKDIR /app
COPY --from=build-env /app/publish .

ENTRYPOINT ["dotnet", "WebApi.dll"]