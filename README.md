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

