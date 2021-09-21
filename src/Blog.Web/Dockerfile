#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /art
COPY ["src/Blog.Web/Blog.Web.csproj", "src/Blog.Web/"]
COPY ["src/Microsoft.AspNetCore.Extensions/Microsoft.AspNetCore.Extensions.csproj", "src/Microsoft.AspNetCore.Extensions/"]
COPY ["src/System.Extensions/System.Extensions.csproj", "src/System.Extensions/"]
COPY ["src/Blog.Domain/Blog.Domain.csproj", "src/Blog.Domain/"]
COPY ["src/Blog.Application/Blog.Application.csproj", "src/Blog.Application/"]
COPY ["src/Blog.Infrastructure/Blog.Infrastructure.csproj", "src/Blog.Infrastructure/"]
COPY ["src/Blog.InMemory/Blog.InMemory.csproj", "src/Blog.InMemory/"]
COPY ["src/Blog.Application.Core/Blog.Application.Core.csproj", "src/Blog.Application.Core/"]
RUN dotnet restore "src/Blog.Web/Blog.Web.csproj"
COPY . .
WORKDIR "/art/src/Blog.Web"
RUN dotnet build "Blog.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.dll"]