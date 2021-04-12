FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /build

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ./API/*.csproj ./API/
COPY ./Application/*.csproj ./Application/
COPY ./Data/*.csproj ./Data/
COPY ./Domain/*.csproj ./Domain/
COPY ./Infrastructure/*.csproj ./Infrastructure/
RUN dotnet restore

# copy everything else and build app
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 as runtime
WORKDIR /app
COPY --from=build-env /build/API/out .
ENTRYPOINT ["dotnet", "API.dll"]
