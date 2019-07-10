FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["HashValidatorApp/HashValidatorApp.csproj", "HashValidatorApp/"]
COPY ["HashValidator.Business/HashValidator.Business.csproj", "HashValidator.Business/"]

RUN dotnet restore "HashValidatorApp/HashValidatorApp.csproj"
COPY . .
WORKDIR "/src/HashValidatorApp"
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm
RUN npm install
RUN dotnet build "HashValidatorApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HashValidatorApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet HashValidatorApp.dll