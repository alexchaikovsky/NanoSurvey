# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY NanoSurveyAPI/*.csproj ./NanoSurveyAPI/
RUN dotnet restore

# copy everything else and build app
COPY NanoSurveyAPI/. ./NanoSurveyAPI/
WORKDIR /source/NanoSurveyAPI
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "NanoSurveyAPI.dll"]