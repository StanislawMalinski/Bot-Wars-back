### Uruchamianie projektu: budowanie lokalnie
Stawianie bazy danych: <br>
Pobierz ostatni obraz mssql servera oraz uruchom bazę danych na dockerze.
1.  `docker pull mcr.microsoft.com/mssql/server:2022-latest` <br>
2.  `docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Sqlhaslo123$" -e "MSSQL_PID=Evaluation" -p 1433:1433  --name dbwysocki --hostname wysocki -d mcr.microsoft.com/mssql/server:latest` <br>
W głównym katalogu repozytorium, przeprowadzamy migracje, za pierwszym razem może przydać się polecenie instalujące ef:
`dotnet tool install --global dotnet-ef` <br>
3.  `dotnet ef database update --project Shared --startup-project BotWars`
Na sam koniec uruchomiamy projekt przy pomocy: <br>
(W idealnym świecie...)
4.  `dotnet run --project BotWars/BotWars.csproj`
