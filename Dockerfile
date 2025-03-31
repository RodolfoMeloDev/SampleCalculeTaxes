FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 58982

ENV ASPNETCORE_URLS=http://+:58982

# config Timezone
RUN apt-get update && apt-get upgrade -y && \
    apt-get install -yq tzdata iputils-ping curl && \
    ln -fs /usr/share/zoneinfo/America/Sao_Paulo /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CalculateTaxes.Host/CalculateTaxes.Host.csproj", "CalculateTaxes.Host/"]
RUN dotnet restore "CalculateTaxes.Host/CalculateTaxes.Host.csproj"
COPY . .
WORKDIR "/src/CalculateTaxes.Host"
RUN dotnet build "CalculateTaxes.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalculateTaxes.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "CalculateTaxes.Host.dll"]
