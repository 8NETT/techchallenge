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

# Define as variáveis de ambiente para o agente New Relic
ENV CORECLR_ENABLE_PROFILING=1 \
    CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
    CORECLR_PROFILER_PATH=/usr/local/newrelic-netcore20-agent/libNewRelicProfiler.so \
    NEW_RELIC_HOME=/usr/local/newrelic-netcore20-agent

# Baixa e extrai o agente do New Relic
RUN apt-get update && apt-get install -y wget && \
    wget -O /tmp/newrelic-agent.tar.gz "https://download.newrelic.com/dot_net_agent/latest_release/newrelic-dotnet-agent_amd64.tar.gz" && \
    mkdir -p /usr/local/newrelic-netcore20-agent && \
    tar -zxvf /tmp/newrelic-agent.tar.gz -C /usr/local/newrelic-netcore20-agent && \
    rm /tmp/newrelic-agent.tar.gz

WORKDIR /app
COPY --from=build-env /app/publish .

ENTRYPOINT ["dotnet", "WebApi.dll"]