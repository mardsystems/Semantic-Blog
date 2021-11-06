#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /art
COPY ["src/Blog.Mvc/Blog.Mvc.csproj", "src/Blog.Mvc/"]
RUN dotnet restore "src/Blog.Mvc/Blog.Mvc.csproj"
COPY . .
WORKDIR "/art/src/Blog.Mvc"
RUN dotnet build "Blog.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.Mvc.dll"]