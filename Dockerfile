FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Booking.Api/Booking.Api.csproj", "Booking.Api/"]
RUN dotnet restore "Booking.Api/Booking.Api.csproj"
COPY . .
WORKDIR "/src/Booking.Api"
RUN dotnet build "Booking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Booking.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN useradd -m bookinguser
USER bookinguser

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Booking.Api.dll
