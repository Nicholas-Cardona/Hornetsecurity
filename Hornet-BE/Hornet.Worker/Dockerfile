FROM mcr.microsoft.com/dotnet/sdk:10.0-azurelinux3.0 AS build
WORKDIR /src

COPY ./Hornet-BE .
RUN dotnet restore Hornet.Worker/Hornet.Worker.csproj

RUN dotnet publish Hornet.Worker/Hornet.Worker.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0.8-azurelinux3.0

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Hornet.Worker.dll"]