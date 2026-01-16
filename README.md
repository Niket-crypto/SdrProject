# Лабораторный роботи 1-8

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Niket-crypto_SdrProject&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Niket-crypto_SdrProject)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Niket-crypto_SdrProject&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Niket-crypto_SdrProject)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=Niket-crypto_SdrProject&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=Niket-crypto_SdrProject)

Структура 8 лабораторних
Кожна робота — через Pull Request або окремий commit. Додати короткий опис: що змінено / як перевірити + звіт про хід виконання в Classroom.

Лаба 1 — Підключення SonarCloud і CI
Мета: створити проект у SonarCloud, підключити GitHub Actions, запустити перший аналіз.

Необхідно:

.NET 8 SDK
Публічний GitHub-репозиторій
Обліковка SonarCloud (організація прив’язана до GitHub)
1) Підключити SonarCloud

На SonarCloud створити проект з цього репозиторію (Analyze new project).
Згенерувати user token і додати в репозиторій як секрет SONAR_TOKEN (Settings → Secrets and variables → Actions).
Додати/перевірити .github/workflows/sonarcloud.yml з тригерами на PR і push у основну гілку. sonarcloud.yml:

name: SonarCloud analysis

on:
  push:
    branches: [ "master" ]
  pull_request:
    branches: [ "master" ]
  workflow_dispatch:

permissions:
  pull-requests: read

jobs:
  sonar-check:
    name: Sonar Check
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v4
        with:
          fetch-depth: 0

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'

      - name: SonarScanner Begin
        run: |
          dotnet tool install --global dotnet-sonarscanner
          echo "$env:USERPROFILE\.dotnet\tools" >> $env:GITHUB_PATH
          dotnet sonarscanner begin `
            /k:"Niket-crypto_SdrProject" `
            /o:"niket-crypto" `
            /d:sonar.token="${{ secrets.SONAR_TOKEN }}" `
            /d:sonar.host.url="https://sonarcloud.io" `
            /d:sonar.cs.opencover.reportsPaths="**/coverage.xml" `
            /d:sonar.coverage.exclusions="**/*.cs" `
            /d:sonar.exclusions="**/bin/**,**/obj/**,**/sonarcloud.yml" `
            /d:sonar.qualitygate.wait=true
        shell: pwsh

      - name: Restore
        run: dotnet restore SdrSolution.sln

      - name: Build
        run: dotnet build SdrSolution.sln -c Release --no-restore

      - name: Tests with coverage
        run: |
          dotnet test SdrSolution.sln -c Release --no-build `
            /p:CollectCoverage=true `
            /p:CoverletOutputFormat=opencover `
            /p:CoverletOutput="../../TestResults/coverage.xml"
        shell: pwsh

      - name: SonarScanner End
        run: |
          dotnet sonarscanner end /d:sonar.token="${{ secrets.SONAR_TOKEN }}"
        shell: pwsh
Вимкнути Automatic Analysis в проєкті.
Перевірити PR-декорацію (вкладка Checks у PR).
Здати: посилання на PR чи commit, скрін Quality Gate, скрін бейджів у README.





