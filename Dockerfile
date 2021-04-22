FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY *.sln .
COPY NanoSurveyAPI/*.csproj ./NanoSurveyAPI/
RUN dotnet restore

COPY NanoSurveyAPI/. ./NanoSurveyAPI/
WORKDIR /source/NanoSurveyAPI
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "NanoSurveyAPI.dll"]