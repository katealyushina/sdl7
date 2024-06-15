# Используем SDK-образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Копируем файл проекта и восстанавливаем зависимости
COPY sdl7.csproj ./
RUN dotnet restore

# Копируем остальные файлы и собираем проект
COPY . ./
RUN dotnet publish -c Release -o out

# Используем runtime-образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Копируем файл конфигурации
COPY sdl7.conf ./

ENTRYPOINT ["dotnet", "sdl7.dll"]
