#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["src/", "src/"]
COPY ["nuget.config", "/"]
RUN dotnet restore "src/SQ.Senior.Quoting.External/SQ.Senior.Quoting.External.csproj"
COPY . .
WORKDIR "/src/src/SQ.Senior.Quoting.External"
RUN dotnet build "SQ.Senior.Quoting.External.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SQ.Senior.Quoting.External.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt-get update
RUN export DEBIAN_FRONTEND=noninteractive
RUN ln -fs /usr/share/zoneinfo/America/Los_Angeles /etc/localtime
RUN apt-get install -y tzdata
RUN dpkg-reconfigure --frontend noninteractive tzdata
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SQ.Senior.Quoting.External.dll"]
