FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ["API/API.csproj", "API/"]

RUN dotnet restore "API/API.csproj"

COPY . .

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "API.dll"]

#ENV - Ask Damian for the value

EXPOSE 8080
