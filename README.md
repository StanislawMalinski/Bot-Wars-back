# Bot-Wars #
Jest projektem studenckim przeprowadzanym w ramach przedmiotu projekt zespołowy na Politechnice Warszawskiej. Celeme projektu jest stworzenie aplikacji webowek, umożliwiającej przeprowadzania turnieji między programami stworzonych w celu grania w gry komputerowe. <br>
Projekt został podzielony na dwie części w kontekście repozytoriów Backend ([Bot-Wars-back](https://github.com/StanislawMalinski/Bot-Wars-back)) oraz Frontend ([Bot-Wars-front](https://github.com/jordus100/Bot-Wars-front))). Jednocześnie można wyróżnić 5 różnych mikroserwisów z których składa się system: <br>
1. Frontend - Graficzny interfejs webowy.
2. Api - Interfejs aplikacji.
3. Engine - Serwis przeprowadzania turniejów.
4. Baza Danych
5. Serwis przechowywania plików 

## Uruchomienie projektu ##

1. Frontend <br>
'
git clone https://github.com/jordus100/Bot-Wars-front.git
npm i && npm start
'

2. API <br>
'
docker run -itd -p 8080:8080 -p 8081:8081 stanislawmalinski/bot-wars-api:latest
'

3. Engine <br>
'
docker run -itd -p 7001:7001 stanislawmalinski/bot-wars-api:latest
'

4. Baza Danych <br>
'
docker pull mcr.microsoft.com/mssql/server:2022-latest
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Sqlhaslo123!" -e "MSSQL_PID=Evaluation" -p 1433:1433  --name dbwysocki --hostname wysocki -d mcr.microsoft.com/mssql/server:latest
'

5. in progress...

6. Whole project can be run by:<br>
'
git clone https://github.com/jordus100/Bot-Wars-front.git
docker-compose up
'


