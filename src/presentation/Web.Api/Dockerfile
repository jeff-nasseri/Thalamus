FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ARG GITLAB_USERNAME
ARG GITLAB_PASSWORD
COPY ["src/presentation/Web.Api/Web.Api.csproj", "src/presentation/Web.Api/"]
COPY ["src/presentation/Web.Common/Web.Common.csproj", "src/presentation/Web.Common/"]
COPY ["src/data-access/Infrastructure/Infrastructure.csproj", "src/data-access/Infrastructure/"]
COPY ["src/data-access/Migrations/Migrations.csproj", "src/data-access/Migrations/"]
COPY ["src/business/Application/Application.csproj", "src/business/Application/"]
COPY ["src/business/Domain/Domain.csproj", "src/business/Domain/"]
COPY ["src/business/ErrorHandling/ErrorHandling.csproj", "src/business/ErrorHandling/"]
COPY nuget.config .
RUN sed -i -e "s/%GITLAB_USERNAME%/${GITLAB_USERNAME}/g" -e "s/%GITLAB_PASSWORD%/${GITLAB_PASSWORD}/g" nuget.config
RUN cat nuget.config;
RUN dotnet restore "/src/src/presentation/Web.Api/Web.Api.csproj"
COPY . .
WORKDIR "/src/src/presentation/Web.Api"
RUN dotnet build "Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY *.pfx .
ENTRYPOINT ["dotnet", "Web.Api.dll"]
