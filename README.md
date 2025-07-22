# NewsCleaner

## Требования

- .NET SDK 8.0 или выше
- HtmlAgilityPack (добавлен в проект)

## Установка и запуск

1. Убедитесь, что установлен .NET SDK.
2. Склонируйте или откройте проект в любой .NET-совместимой IDE.
3. Убедитесь, что файл `corrupted-news.html` находится в корне проекта.
4. Выполните команду:

dotnet run

## После запуска

В папке сборки, например:

bin/Debug/net8.0/

будут созданы следующие файлы:

- clean-news.json — JSON-файл с валидными и очищенными новостями.
- log.txt — лог ошибок парсинга и пропущенных блоков.

## Альтернативный запуск (через публикацию)

в папке /publish находиться

newscleanerconsole.exe

## Структура проекта

newscleanerconsole/  
├── Program.cs  
├── Models/  
│   └── NewsItem.cs  
├── Services/  
│   ├── NewsParser.cs  
│   └── TextCleaner.cs  
├── publish/  
│   ├── clean-news.json  
│   ├── corrupted-news.html  
│   └── log.txt
├── clean-news.json        ← создаётся после запуска  
├── log.txt                ← создаётся после запуска  
├── corrupted-news.html  
├── newscleanerconsole.csproj  
├── README.md
